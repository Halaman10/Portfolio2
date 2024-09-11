using UnityEngine;

namespace CTC.Game
{
    public class Damage : MonoBehaviour
    {
        [SerializeField] enum damageType { bullet, stationary, melee }
        [SerializeField] damageType type;
        [SerializeField] Rigidbody rb;

        [SerializeField] int damageAmount;
        [SerializeField] int speed;
        [SerializeField] int destroyTime;

        // Start is called before the first frame update
        void Start()
        {
            if (type == damageType.bullet)
            {
                //rb.velocity = (GameManager.Instance.player.transform.position - (transform.position - new Vector3(0, 0.5f, 0))).normalized * speed;
                Destroy(gameObject, destroyTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.isTrigger)
            {
                return;
            }

            IDamage dmg = other.GetComponent<IDamage>();

            if (dmg != null)
            {
                dmg.takeDamage(damageAmount);
            }
            if (type == damageType.bullet)
                Destroy(gameObject);
        }
    }
}
