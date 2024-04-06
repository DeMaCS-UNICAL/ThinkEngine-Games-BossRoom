using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ThinkEngine.Planning;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class player_attack : Action
    {
         GameObject player;

        public int enemy_index {set; get;}

        public int id {set; get;}

        public int type {set;get;}

        public override void Do()
        {
           
            
            player = GameObject.Find("PlayerAvatar" + id);
            ClientInputSender script2 = player.GetComponent<ClientInputSender>();
            FieldOfView fieldofview = player.GetComponentInChildren<FieldOfView>();
            
            
            if(type == 0 || type == 3){
                GameObject obj = GameObject.Find("Observer");
                Info info = obj.GetComponent<Info>();
                info.ho_un_target=1;
                info.target=fieldofview.getAt(enemy_index);
                
            }
            
            script2.setTarget(fieldofview.getAt(enemy_index));
            
        }

        public override State Done()
        {
            return State.READY;
        }

        public override State Prerequisite()
        {
            return State.READY;
        }

        // Start is called before the first frame update

        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
