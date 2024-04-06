using System.Collections;
using System.Collections.Generic;
using ThinkEngine.Planning;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class turn_towards_enemy :Action
    {
        GameObject player;
        public int x { get; set; }
        public int z { get; set; }
        public ulong id { get; set; }


        public override void Do()
        {
            player = GameObject.Find("PlayerAvatar" + id);
            
            if (player != null)
            {
                ClientInputSender script2 = player.GetComponent<ClientInputSender>();
                Transform pos = player.GetComponent<Transform>();

                if (script2 != null)
                {
                    float x_m = x / 100f;
                    float z_m = z / 100f;
                    // Chiamare la funzione desiderata in Script2
                    script2.MoveTo(x_m, z_m);
                    
                }
                else
                {
                    Debug.Log("NON MI MUOVO PERCHè HO NEMICI VICINI OPPURE MI STO GIà MUOVENDO");
                }
            }
            else
            {
                Debug.LogError("PlayerAvatar non trovato.");
            }
        }

        public override State Done()
        {
            float x_m = x / 100f;
            float z_m = z / 100f;
            player = GameObject.Find("PlayerAvatar" + id);

            if (player != null)
            { 
                Transform pos = player.GetComponent<Transform>();
                
            }
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
