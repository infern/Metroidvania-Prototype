using UnityEngine;
using UnityEngine.UI;
using PlatformerPrototype.InfernKP.Events;
using System.Collections;

namespace PlatformerPrototype.InfernKP.UI
{
    public class UI_Player : UI_Health
    {
        #region Variables

        //[Header("Settings")]//***********

        //[Header("Data")]//***********
 

        //[Header("Components")]//***********
  

        #endregion

        #region Unity Methods

        void OnEnable()
        {
            UI_events.PlayerLostHpTrigger += AdjustHealthBar;

        }

         void OnDisable()
        {
            UI_events.PlayerLostHpTrigger -= AdjustHealthBar;
        }

        #endregion

        #region Unique Methods

        #endregion
    }
}
