using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class Ally
    {
        public int id{set;get;}
        public int isAlive{set;get;}
        public int health{set;get;}

        public Ally(int id, int alive, int health)
        {
            this.id=id;
            this.isAlive=alive;
            this.health=health;
        }



    }
}
