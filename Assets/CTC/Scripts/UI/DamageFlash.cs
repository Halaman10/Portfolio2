using CTC.Game;
using System.Collections;
using UnityEngine;

namespace CTC.UI
{
    public class DamageFlash : MonoBehaviour
    {
        [Tooltip("Canvas used to toggle damage flash")]
        [SerializeField] GameObject damageFlashCanvas;

        [Tooltip("Duration of the damage flash (0.1 - 0.5)")]
        [Range(0.1f, 0.5f)][SerializeField] float damageFlashDuration;

        Health playerHealth;

        private void Start()
        {
            playerHealth = GameManager.Instance.playerScript.GetComponent<Health>();

            playerHealth.OnDamaged += OnTakeDamage;
        }

        void OnTakeDamage(float damage, GameObject damageSource)
        {
            StartCoroutine(FlashDamage());
        }

        IEnumerator FlashDamage()
        {
            damageFlashCanvas.SetActive(true);
            yield return new WaitForSeconds(damageFlashDuration);
            damageFlashCanvas.SetActive(false);
        }
    }
}
