using UnityEngine.Events;

namespace PlatformerPrototype.InfernKP.Events
{
    public static class GameProgressEvents
    {
        public static event UnityAction PlayerDiedTrigger;
        public static void PlayerDied() => PlayerDiedTrigger?.Invoke();

        public static event UnityAction BossDiedTrigger;
        public static void BossDied() => BossDiedTrigger?.Invoke();

        public static event UnityAction PauseToggleTrigger;
        public static void PauseGameToggle() => PauseToggleTrigger?.Invoke();
        public static event UnityAction StartBossFightTrigger;
        public static void StartBossFight() => StartBossFightTrigger?.Invoke();



    }
}