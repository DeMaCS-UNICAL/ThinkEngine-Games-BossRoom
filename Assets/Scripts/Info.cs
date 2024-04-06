using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using it.unical.mat.embasp.languages;
using ThinkEngine;
using Unity.BossRoom.Gameplay.GameplayObjects;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.BossRoom.Utils;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.PlayerLoop;
using VContainer.Unity;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class Info : MonoBehaviour
    {
        [SerializeField] ClientPlayerAvatarRuntimeCollection m_PlayerAvatars;

        ServerCharacter m_OwnedServerCharacter; 

        ClientPlayerAvatar m_OwnedPlayerAvatar;

        public string m_name; //player name

        public ulong m_id; //player ID

        public int my_health; //player health

        public int current_action; // Idle = 0, PathFollowing = 1, Charging = 2, Knockback = 3,
        public bool free_attack=true; 

        public int ho_un_target = 0;
        double time;

        public int tipo; //player class (tank, archer, mage, rogue)

        public int basic_attack_hit=0;

       

        GameObject player;

        Component[] com;

        //list of allies
        public List<GameObject> friends = new List<GameObject>();

        //allies ID
        public List<int> friends_id = new List<int>();
        public List<int> friends_is_alive = new List<int>();

        //allies health
        public List<int> friends_health = new List<int>();

        //allies position
        public List<Vector3Int> pos = new List<Vector3Int>();

        public GameObject target=null; //player target
        void Awake()
        {
            m_PlayerAvatars.ItemAdded += PlayerAvatarAdded;      
        }

        
        //method that iterates from 0 to 8 to find allies in the game
        void findAlly(){
            for(int i=0; i<8; i++){
                GameObject player = GameObject.Find("PlayerAvatar" + i);
                
                //if it finds someone with an ID different from its own, 
                //it adds all their data in the different lists
                if(player != null && (int)m_id != i){
                    ServerCharacter sc = player.GetComponent<ServerCharacter>();
                    friends.Add(player);
                    friends_id.Add((int)sc.OwnerClientId);
                    friends_is_alive.Add(sc.IsAlive());
                    friends_health.Add(sc.getHealthPercent());

                    Vector3Int tmp = new Vector3Int();
                    float aux = player.transform.position.x * 100f;
                    tmp.x = (int)aux;
                    aux = player.transform.position.y * 100f;
                    tmp.y = (int)aux;
                    aux = player.transform.position.z * 100f;
                    tmp.z = (int)aux;

                    pos.Add(tmp);
                }
            }
        }

        //updates the data of allies in game
        void updateFriendsHealth(){
            for(int i=0; i<friends.Count; i++){
                ServerCharacter sc = friends[i].GetComponent<ServerCharacter>();
                friends_health[i]=sc.getHealthPercent();
                friends_is_alive[i]=sc.IsAlive();

                Vector3Int tmp = new Vector3Int();
                float aux = friends[i].transform.position.x * 100f;
                tmp.x = (int)aux;
                aux = friends[i].transform.position.y * 100f;
                tmp.y = (int)aux;
                aux = friends[i].transform.position.z * 100f;
                tmp.z = (int)aux;
                pos[i] = tmp;
            }
        }

        

        private void FixedUpdate()
        {
            //look for the chat manager object in the hierarchy
            GameObject chat_manager = GameObject.Find("Chat Manager");
            NetworkChat nc = chat_manager.GetComponent<NetworkChat>();

            //takes the string and divides it according to the separators
            String text = nc.getTextArea();
            
            String [] text_splitted = text.Split(",");
            String [] text_second_split = text_splitted[(int)m_id].Split("_");
            current_action = int.Parse(text_second_split[0]);//idle or path following
            basic_attack_hit = int.Parse(text_second_split[1]);//attack animation started
            
            my_health = m_OwnedServerCharacter.getHealthPercent();

            if(free_attack==false)
            {
                double temp = DateTime.Now.TimeOfDay.TotalMilliseconds;
                int temp2 = (int)(temp - time);
                if(temp2>=500){
                    free_attack=true;
                    Debug.Log("attacco completato");
                }
            }

            
            if(basic_attack_hit!=0){//if the player hits an enemy, it sets everything to 0
                
                basic_attack_hit=0;
                ho_un_target=0;
                target=null;
                
            }
            else if(ho_un_target == 1 && target==null){//if the player has a target but that 
                ho_un_target=0;                        //target dies for some reason, set everything to 0 
                basic_attack_hit=0;
                target=null;
                
            }
            else if(current_action == 3){//if the player is pushed, it sets everything to 0
                ho_un_target=0;
                basic_attack_hit=0;
                target=null;
            }
            
            //if at the beginning of the game the number of 
            //allies is 0 then he searches for them in the hierarchy
            if(friends.Count==0)
                findAlly();
            else
                updateFriendsHealth();
            
            
        }

        void PlayerAvatarAdded(ClientPlayerAvatar clientPlayerAvatar)
        {
            //if is owner save data
            if (clientPlayerAvatar.IsOwner)
            {
                SetHeroData(clientPlayerAvatar);
            }
            else{//if is not owner disable components
                int temp_id = (int)clientPlayerAvatar.OwnerClientId;
                GameObject temp_player = GameObject.Find("PlayerAvatar" + temp_id);
                temp_player.GetComponent<SensorConfiguration>().enabled=false;
                
                Component[] temp = temp_player.GetComponentsInChildren<Component>();
                
                foreach (Component item in temp)
                {
                    if (item is Behaviour)
                        if(item.GetType().Name == "SensorConfiguration" || 
                            item.GetType().Name == "BoxCollider" || 
                                item.GetType().Name == "FieldOfView"){
                            (item as Behaviour).enabled=false;
                            
                        }
                        
                }   
                        
            }
        }

        void SetHeroData(ClientPlayerAvatar clientPlayerAvatar)
        {
            m_OwnedServerCharacter = clientPlayerAvatar.GetComponent<ServerCharacter>();

            Assert.IsTrue(m_OwnedServerCharacter, 
                "ServerCharacter component not found on ClientPlayerAvatar");

            m_OwnedPlayerAvatar = clientPlayerAvatar;

            m_name = GetPlayerName(m_OwnedServerCharacter);

            m_id = m_OwnedPlayerAvatar.OwnerClientId;

            my_health = m_OwnedServerCharacter.HitPoints;
            
            current_action=m_OwnedServerCharacter.my_current_action_ID;

            tipo = (int)m_OwnedServerCharacter.CharacterClass.CharacterType;
           
            player = GameObject.Find("PlayerAvatar" + (int)m_id);

            com = player.GetComponents<Component>();

            foreach (Component item in com)
            {
                if (item is Behaviour)
                    if((item as Behaviour).enabled == false)
                        (item as Behaviour).enabled=true;
            }
            
        }

        public void attack_started(){
            time = DateTime.Now.TimeOfDay.TotalMilliseconds;
            free_attack=false;
            Debug.Log("attacco iniziato");
        }
        

        string GetPlayerName(Component component)
        {
            var networkName = component.GetComponent<NetworkNameState>();
            return networkName.Name.Value;
        }

        public string getName()
        {
            return m_name;
        }

        public ulong getId()
        {
            return m_id;
        }


    }
}
