using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    
    public class FloattoInt : MonoBehaviour
    {
        public Transform player;
        public int x;
        public int y;
        public int z;

        public int dest_x;

        public int dest_z;
        // Start is called before the first frame update
        void Start()
        {
            float temp = player.position.x * 100;
            dest_x = ((int)temp);
            
            temp = player.position.z * 100;
            dest_z = ((int)temp);
        }

        // Update is called once per frame
        void Update()
        {
            float temp = player.position.x * 100;
            x = ((int)temp);
            temp = player.position.y * 100;
            y = ((int)temp);
            temp = player.position.z * 100;
            z = ((int)temp);

        }

        public int get_Unsigned_X(){
            if(this.dest_x < 0)
                return dest_x * -1;
            return dest_x;
        }

        public int get_Unsigned_Z(){
            if(this.dest_z < 0)
                return dest_z * -1;
            return dest_z;
        }
    }
}
