using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class network_test : NetworkBehaviour
    {
        // Start is called before the first frame update
        int player_number;
        [SerializeField] private int [] my_current_action;
        int my_id;
        void Start()
        {
            player_number=8;
            my_current_action = new int [player_number];
            for(int i=0; i< player_number; i++){
                my_current_action[i]=-9999;
            }
        }

        public void setNumberPlayers(int n){
            player_number=n;
            my_current_action = new int [n];
        }

        // Update is called once per frame
        void Update()
        {
            if(IsHost){
                for(int i=0; i<player_number; i++){
                    UpdateCurrentActionIdServerRPC(i);
                }
            }
        }

        [ServerRpc]
        void UpdateCurrentActionIdServerRPC(int i){
            UpdateCurrentActionIdClientRPC(i);
            
        }

        [ClientRpc]
        void UpdateCurrentActionIdClientRPC(int i){
            setData(i);
        }

        void setData(int i){
            GameObject obj=GameObject.Find("PlayerAvatar"+i);
            if(obj != null){
                ServerCharacter sc= obj.GetComponent<ServerCharacter>();
                int temp = (int)sc.GetMovement().GetMovementState();
                if(temp == 1)
                    my_current_action[i]= 1;
                else
                    my_current_action[i]= 0;
            }
        }

        public int getMyCurrentAction(int id){
            File.AppendAllText("C:/Users/miche/Desktop/log/Log.txt" , id+"-"+my_current_action[id]+" ");
            return my_current_action[id];
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
        }
    }
}
