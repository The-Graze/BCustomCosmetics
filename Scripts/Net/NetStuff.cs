using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace BCustomCosmetics.Scripts.Net
{
    public  class NetStuff : MonoBehaviourPunCallbacks
    {
        public static volatile NetStuff instance;

        public void AddHatsToProp()
        {

        }

        public override void OnJoinedRoom()
        {
            foreach(Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (player.CustomProperties.ContainsKey("BCS"))
                {
                    GorillaGameManager.instance.FindPlayerVRRig(player).concatStringOfCosmeticsAllowed += player.CustomProperties["BCS"];
                }
            }
        }
    }
}