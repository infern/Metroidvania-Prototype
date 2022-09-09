using UnityEngine.Events;

namespace PlatformerPrototype.InfernKP.Events
{
    public static class UI_events
    {
        public static event UnityAction<float> PlayerLostHpTrigger;
        public static void PlayerLostHp(float value) => PlayerLostHpTrigger?.Invoke(value);

        public static event UnityAction<float> BossLostHpTrigger;
        public static void BossLostHp(float value) => BossLostHpTrigger?.Invoke(value);

        public static event UnityAction<bool> ToggleMenuTrigger;
        public static void ToggleMenu(bool value) => ToggleMenuTrigger?.Invoke(value);
        public static event UnityAction<bool> ShowEndBannerTrigger;
        public static void ShowEndBanner(bool playerWon) => ShowEndBannerTrigger?.Invoke(playerWon);

        public static event UnityAction EnragedTextToggleTrigger;
        public static void EnragedTextToggle() => EnragedTextToggleTrigger?.Invoke();

    }
}
