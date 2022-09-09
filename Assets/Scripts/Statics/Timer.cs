using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Statics
{
    public static class Timer
    {
        public static float Start(float duration)
        {
            return Time.time + duration;
        }

        public static bool IsFinished(float duration)
        {
            return Time.time > duration;
        }
    }
}
