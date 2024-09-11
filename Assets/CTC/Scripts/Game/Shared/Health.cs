using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CTC.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Max amount of health")]
        public float MaxHealth;

        [Tooltip("Rate of health regeneration")]
        [SerializeField] float HPRegenRate;

        [Tooltip("Wait time for health to regenerate")]
        [SerializeField] float HPRegenWaitTime;

        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction OnDie;

        public float CurrentHealth { get; set; }

        Coroutine regenCoroutine;

        bool isDead;

        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage, GameObject damageSource)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            // Call OnDamaged event handler
            float trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }
            HandleDeath();

            if (regenCoroutine != null)
            {
                StopCoroutine(regenCoroutine);
            }
            regenCoroutine = StartCoroutine(EnableHealthRegen());
        }

        public void Kill()
        {
            CurrentHealth = 0;

            // Call OnDamaged event handler
            OnDamaged?.Invoke(MaxHealth, null);

            HandleDeath();
        }

        void HandleDeath()
        {
            if (isDead)
                return;

            // Call OnDie event handler
            if (CurrentHealth <= 0)
            {
                isDead = true;
                OnDie?.Invoke();
            }
        }

        void RegenerateHealth()
        {
            //Debug.Log("Regenerating health: " + HPRegenRate * Time.deltaTime);
            CurrentHealth += HPRegenRate * Time.deltaTime;

            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        }

        IEnumerator EnableHealthRegen()
        {
            yield return new WaitForSeconds(HPRegenWaitTime);
            regenCoroutine = null;
        }
    }
}
