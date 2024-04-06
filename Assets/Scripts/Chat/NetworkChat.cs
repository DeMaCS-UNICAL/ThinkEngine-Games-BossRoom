using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.BossRoom.Utils;
using Unity.Collections;
using Unity.BossRoom.Gameplay.GameplayObjects;
using UnityEngine.Assertions;
using System;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class NetworkChat : NetworkBehaviour
    {

        [SerializeField] ClientPlayerAvatarRuntimeCollection m_PlayerAvatars;
        [SerializeField] public TMP_Text textArea;
        [SerializeField] private TMP_InputField textInput;
        ServerCharacter m_OwnedServerCharacter;

        ClientPlayerAvatar m_OwnedPlayerAvatar;

        string m_name;

        ulong m_id;
        // Start is called before the first frame update
        void Start()
        {
            textArea.SetText("");
        }

        void Awake()
        {
            m_PlayerAvatars.ItemAdded += PlayerAvatarAdded;
            
        }

        void PlayerAvatarAdded(ClientPlayerAvatar clientPlayerAvatar)
        {
            if (clientPlayerAvatar.IsOwner)
            {
                SetHeroData(clientPlayerAvatar);
            }
        }

        void SetHeroData(ClientPlayerAvatar clientPlayerAvatar)
        {
            m_OwnedServerCharacter = clientPlayerAvatar.GetComponent<ServerCharacter>();

            Assert.IsTrue(m_OwnedServerCharacter, "ServerCharacter component not found on ClientPlayerAvatar");

            m_OwnedPlayerAvatar = clientPlayerAvatar;

            m_name = GetPlayerName(m_OwnedServerCharacter);

            m_id = m_OwnedPlayerAvatar.OwnerClientId;
            
        }

        string GetPlayerName(Component component)
        {
            var networkName = component.GetComponent<NetworkNameState>();
            return networkName.Name.Value;
        }

        public void SendMessage()
        {

            AddTextServerRPC(m_OwnedPlayerAvatar.name);
            textInput.SetTextWithoutNotify("");
            
        }

       

        [ServerRpc] //only the server can invoke this function
        void AddTextServerRPC(string text)
        {
            AddTextClientRPC(text);
        }

        [ClientRpc]
        void AddTextClientRPC(string text)
        {
            AddText(text);
        }

        //add string in the text area
        void AddText(string chat)
        {
            string lastText = textArea.text;
            textArea.SetText(chat);
        }

        public override void OnNetworkDespawn()
        {
            textArea.SetText("");
            base.OnNetworkDespawn();
        }
        
        private void FixedUpdate(){
            //try to find player from 0 to 8 
            int[] temp = new int[8];
            String text="";
            for(int i=0; i<8; i++)
            {
                GameObject player = GameObject.Find("PlayerAvatar"+i);
                if(player != null)
                {
                    //get his ServerCharacter script
                    ServerCharacter sc = player.GetComponent<ServerCharacter>();
                    if(sc != null)
                    {
                        //get his movement (idle or path following)
                        temp[i] = (int)sc.GetMovement().GetMovementState();
                        text = text + temp[i];

                        //get if the attack animation is started
                        if(sc.basic_attack_hit){
                            text = text + "_1, ";
                            sc.basic_attack_hit=false;
                        }
                        else
                            text = text + "_0, "; 
                    }
                }
            }
            //send the string in broadcast to all clients
            AddTextServerRPC(text);
            textInput.SetTextWithoutNotify("");
        }

        //get the string in the text area
        public String getTextArea(){
            return textArea.text;
        }


        public void chatTest(){
            string lastText = textArea.text;
            textArea.SetText(lastText + "\n" + m_OwnedPlayerAvatar.name);
        }
    }
}
