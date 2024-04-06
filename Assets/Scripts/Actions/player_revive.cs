using System.Collections;
using System.Collections.Generic;
using System.IO;
using ThinkEngine.Planning;
using Unity.BossRoom.Gameplay.UserInput;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class player_revive : Action
    {
        public int friend_id {set;get;}

        public int my_id {set;get;}

        public int friend_index {set;get;}
        

        GameObject player;

        GameObject friend;



        GameObject observer;

        public override void Do()
        {
            friend = GameObject.Find("PlayerAvatar" + friend_id);
            player = GameObject.Find("PlayerAvatar" + my_id);
            ClientInputSender cis = player.GetComponent<ClientInputSender>();
            cis.reviveTarget(friend);

        }

        public override State Done()
        {
            observer = GameObject.Find("observer");
            Info i = observer.GetComponent<Info>();
            if(i.friends_health[friend_index]<=0)
                return State.WAIT;
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
