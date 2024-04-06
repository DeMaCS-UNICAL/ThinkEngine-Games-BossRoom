using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ThinkEngine.Planning;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class player_heal : Action
    {
        public int friend_id {set;get;}

        public int my_id {set;get;}

        

        GameObject player;

        GameObject friend;



        GameObject observer;

        public override void Do()
        {
            friend = GameObject.Find("PlayerAvatar" + friend_id);
            player = GameObject.Find("PlayerAvatar" + my_id);
            ClientInputSender cis = player.GetComponent<ClientInputSender>();
            cis.healTarget(friend);
            

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
