using System.Collections;
using System.Collections.Generic;
using ThinkEngine.Planning;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class Move : Action
    {
        public int x { get; set; }
        public int z { get; set; }

        public int x_now { get; set;}

        public int z_now {get; set;}

        public ulong id { get; set; }
        
        GameObject player;
        Info info;
        public override void Do()
        {
            
            player = GameObject.Find("PlayerAvatar" + id);
            ClientInputSender script2 = player.GetComponent<ClientInputSender>();
            FloattoInt fti = player.GetComponent<FloattoInt>();
             
           
            fti.dest_x=this.x;
            fti.dest_z=this.z;
            
            float x_m = x / 100f;
            float z_m = z / 100f;

            script2.MoveTo(x_m, z_m);
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
