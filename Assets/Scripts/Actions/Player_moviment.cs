using System.Collections;
using System.Collections.Generic;
using ThinkEngine.Planning;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class Player_moviment : Action
    {
        public int x { get; set; }
        public int z { get; set; }

        public ulong id { get; set; }

        public bool enemy_near { get; set; }
        
        GameObject player;
        Info info;
        public override void Do()
        {
           
            /*player = GameObject.Find("PlayerAvatar" + id);
            info = GameObject.Find("Info_object").GetComponent<Info>();
            if (player != null)
            {
                ClientInputSender script2 = player.GetComponent<ClientInputSender>();

               
                if (script2 != null && info.isMoving!=true && enemy_near==false )
                {
                    float x_m = x / 100f;
                    float z_m = z / 100f;
                    // Chiamare la funzione desiderata in Script2
                    script2.TestWorld(x_m, z_m);
                    info.isMoving = true;
                }
                else
                {
                    Debug.Log("NON MI MUOVO PERCHè HO NEMICI VICINI OPPURE MI STO GIà MUOVENDO");
                }  
            }
            else
            {
                Debug.LogError("PlayerAvatar non trovato.");
            }*/
           
        }
        
        public override State Done()
        {
           
            /*float x_m = x / 100f;
            float z_m = z / 100f;
            player = GameObject.Find("PlayerAvatar" + id);
            info = GameObject.Find("Info_object").GetComponent<Info>();
            if (player != null)
             {
                 Transform pos = player.GetComponent<Transform>();
                ClientInputSender script2 = player.GetComponent<ClientInputSender>();
               

                if (pos != null && info.isMoving && enemy_near==false && ((pos.position.x < (x_m-1f) || pos.position.x > x_m)  || (pos.position.z < (z_m-1f) || pos.position.z > z_m)))
                 {
                   
                    return State.READY;
                 }
                else if (script2 != null && info.isMoving == true && enemy_near == true )
                {
                    
                    info.isMoving = false;
                    script2.TestWorld(pos.position.x, pos.position.z);
                    return State.READY;
                }
                else
                 {

                     return State.READY;
                 }
             }
             else
             {

                 return State.READY;
             }*/
            return State.READY;
        }

        public override State Prerequisite()
        {
            float x_m = x / 100f;
            float z_m = z / 100f;

            player = GameObject.Find("PlayerAvatar" + id);
            if (player != null)
            {
                Transform pos = player.GetComponent<Transform>();
                
                // Assicurarsi che lo script2 sia presente
                if (pos != null && (pos.position.x != x_m || pos.position.z != z_m))
                {
                    return State.READY;
                }
                else
                {
                    
                    return State.ABORT;
                }
            }
            else
            {
                
                return State.ABORT;
            }

        
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
