using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using PlatformerPrototype.InfernKP.Events;

namespace PlatformerPrototype.InfernKP.Player
{
    public class PlayerInputCallbacks : MonoBehaviour
    {
        [Header("Components")]//***********
        [SerializeField] PlayerController Controller;


        #region Unique Methods

        public void Move(CallbackContext context)
        {
            Controller.MoveScript.ButtonDown(context.ReadValue<Vector2>());
        }

        public void Jump(CallbackContext context)
        {
            if (context.started) Controller.JumpScript.ButtonDown();
            else if (context.canceled) Controller.JumpScript.ButtonUp();
        }

        public void Attack(CallbackContext context)
        {
            if (context.started) Controller.AttackScript.ButtonDown();
            else if (context.canceled) Controller.AttackScript.ButtonUp();
        }

        public void Pause(CallbackContext context)
        {
            if (context.started) GameProgressEvents.PauseGameToggle();
        }

        #endregion
    }
}
