using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformerPrototype.InfernKP.Interfaces
{
    public interface IButtonPress
    {
        public void ButtonDown(){}
        public void ButtonDown(Vector2 direction){}
        public void ButtonUp(){}
    }
}
