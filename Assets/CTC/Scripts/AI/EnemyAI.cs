using CTC.Game;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace CTC.AI
{
    [RequireComponent(typeof(Health), typeof(Member), typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        public enum EnemyType { CrystalGuardian, FireSentry, IceSentry, WindSentry }

        [Header("Components")]
        [Tooltip("The type of enemy determines their behavior and weakness")]
        public EnemyType enemyType;

        [Tooltip("Component used to manage enemy movement")]
        [SerializeField] NavMeshAgent agent;

        [Tooltip("Script for managing enemy health")]
        [SerializeField] Health enemyHealth;

        [Tooltip("Script for managing enemy as a team member")]
        [SerializeField] Member member;

        [Header("Shooting")]

        [SerializeField] Transform shootPos;
        [SerializeField] Transform headPos;

        [SerializeField] GameObject bullet;

        [SerializeField] float shootRate;
        [SerializeField] int shootAngle;

        [SerializeField] int facePlayerSpeed;

        [Header("Damage Flash")]
        [Tooltip("The model used for rendering the enemy")]
        [SerializeField] Renderer model;

        [Tooltip("The color of the damage flash")]
        [SerializeField] Color flashColor;

        [Tooltip("The duration of the damage flash")]
        [Range(0.1f, 0.5f)]
        [SerializeField] float flashDuration;

        public UnityAction OnAttack;

        bool isShooting;

        float angleToPlayer;
        float stoppingDistanceOG;

        Vector3 playerDirection;

        Color colorOrig;

        void Start()
        {
            colorOrig = model.material.color;

            // Subscribe to Death & Damage events
            enemyHealth.OnDie += OnDie;
            enemyHealth.OnDamaged += OnDamaged;

            stoppingDistanceOG = agent.stoppingDistance;
        }

        // Update is called once per frame
        void Update()
        {
            playerInFront();
        }

        bool playerInFront()
        {
            playerDirection = GameManager.Instance.player.transform.position - headPos.position;
            angleToPlayer = Vector3.Angle(playerDirection, transform.forward);

            //Debug.Log(angleToPlayer);
            Debug.DrawRay(headPos.position, playerDirection);

            RaycastHit hit;

            if (Physics.Raycast(headPos.position, playerDirection, out hit))
            {
                agent.SetDestination(GameManager.Instance.player.transform.position);

                if (!isShooting && angleToPlayer <= shootAngle)
                {
                    StartCoroutine(Shoot());
                }
                if (agent.remainingDistance <= agent.stoppingDistance)
                    facePlayer();
                agent.stoppingDistance = stoppingDistanceOG;
                return true;
            }
            agent.stoppingDistance = 0;
            return false;
        }

        void facePlayer()
        {
            Quaternion rotation = Quaternion.LookRotation(playerDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
        }    

        void OnDamaged(float damage, GameObject damageSource)
        {
            StartCoroutine(DamageFlash());
        }

        void OnDie()
        {
            EventManager.Broadcast(Events.DeathEvent);
        }

        IEnumerator DamageFlash()
        {
            model.material.color = flashColor;

            yield return new WaitForSeconds(flashDuration);

            model.material.color = colorOrig;
        }

        IEnumerator Shoot()
        {
            isShooting = true;

            CreateBullet();

            yield return new WaitForSeconds(shootRate);

            isShooting = false;
        } 
        
        public void CreateBullet()
        {
            Instantiate(bullet, shootPos.position, transform.rotation);
        }
    }
}
