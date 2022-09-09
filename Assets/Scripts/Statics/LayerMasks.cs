using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Statics
{
    public static class LayerMasks
    {
        public static LayerMask GroundLayerMask = LayerMask.GetMask("Terrain");
        public static LayerMask PlayerLayerMask = LayerMask.GetMask("Player");
        public static LayerMask EnemyLayerMask = LayerMask.GetMask("Enemies");


        public static int GroundLayer = 6;
        public static int PlayerLayer = 7;
        public static int EnemyLayer = 8;
        public static int CollideWithGroundOnlyLayer = 9;
        public static int RecoveringPlayerLayer = 10;
        public static int BoulderLayer = 11;






    }
}
