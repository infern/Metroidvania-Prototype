using UnityEngine;
using UnityEngine.UI;
using PlatformerPrototype.InfernKP.Events;
using System.Collections;

namespace PlatformerPrototype.InfernKP.UI
{
    abstract public class UI_Health : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]//***********
        [SerializeField]
        float waitBeforeFillDuration = 0.7f;
        [SerializeField]
        float fillSpeed = 0.003f;

        [Header("Data")]//***********
        Coroutine fillHealthCoroutine;
 

        [Header("Components")]//***********
        [SerializeField]
        Image decayHealthBar;
        [SerializeField]
        Image fillHealthBar;


        #endregion

        #region Unique Methods

        public void AdjustHealthBar(float value)
        {
            fillHealthBar.fillAmount = value;
            if (fillHealthCoroutine != null)
            {
                StopCoroutine(fillHealthCoroutine);
            }

            fillHealthCoroutine = StartCoroutine(DelayFill());
        }
    
        IEnumerator DelayFill()
        {
            yield return new WaitForSeconds(waitBeforeFillDuration);
            for (float fill = decayHealthBar.fillAmount; fill >= fillHealthBar.fillAmount; fill -= fillSpeed)
            {
                decayHealthBar.fillAmount = fill;
                yield return null;
            }
        }
        #endregion
    }
}
