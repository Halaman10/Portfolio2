using CTC.Game;
using CTC.UI;
using System.Collections;
using UnityEngine;

namespace CTC.Gameplay
{
    [RequireComponent(typeof(CharacterController), typeof(Member), typeof(Health))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] CharacterController controller;

        [Tooltip("Choose player mask to ignore")]
        [SerializeField] LayerMask ignoreMask;

        [Tooltip("Script for managing player health")]
        [SerializeField] Health playerHealth;

        [Tooltip("Script for managing player as a team member")]
        [SerializeField] Member member;

        [Header("Movement")]
        [Tooltip("Max walking speed (1 - 5)")]
        [Range(1, 5)][SerializeField] int speed;

        [Tooltip("Multiplicator for the sprint speed (2 - 4)")]
        [Range(2, 4)][SerializeField] int sprintModifier;

        [Header("Shooting")]
        [Tooltip("Damage amount of shot")]
        [SerializeField] int shootDamage;

        [Tooltip("Time between each shot")]
        [SerializeField] float shootRate;

        [Tooltip("Max distance of shot before it is destroyed")]
        [SerializeField] int shootDistance;

        [Header("Jump")]
        [Tooltip("Max number of jumps (1 - 2)")]
        [Range(1, 2)][SerializeField] int jumpMax;

        [Tooltip("Max jump speed (8 - 20)")]
        [Range(8, 20)][SerializeField] int jumpSpeed;

        [Tooltip("Gravity applied to airborne player (15 - 30)")]
        [Range(15, 30)][SerializeField] int gravity;

        int jumpCount;

        Vector3 move;
        Vector3 playerVelocity;

        public bool IsDead { get; private set; }

        bool isSprinting;
        bool isShooting;

        void Start()
        {
            // Spawn player at assigned spawn pos
            SpawnPlayer();

            // Subscribe to Death event
            playerHealth.OnDie += OnDie;
            playerHealth.OnRespawn += OnRespawn;
        }

        public void SpawnPlayer()
        {
            transform.position = member.SpawnPos;
        }

        private void Update()
        {
            if (!PauseMenuManager.Instance.IsPaused)
            {
                Movement();
            }
            Sprint();
        }

        void Movement()
        {
            if (controller.isGrounded)
            {
                jumpCount = 0;
                playerVelocity = Vector3.zero;
            }

            move = Input.GetAxis("Vertical") * transform.forward +
                Input.GetAxis("Horizontal") * transform.right;

            // Normalize the move vector
            move = Vector3.ClampMagnitude(move, 1);

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
            {
                jumpCount++;
                playerVelocity.y = jumpSpeed;
            }

            controller.Move(playerVelocity * Time.deltaTime);

            playerVelocity.y -= gravity * Time.deltaTime;

            if (Input.GetButton("Shoot") && !isShooting)
            {
                StartCoroutine(Shoot());
            }
        }

        void Sprint()
        {
            if (Input.GetButtonDown("Sprint"))
            {
                speed *= sprintModifier;
                isSprinting = true;
            }
            else if (Input.GetButtonUp("Sprint"))
            {
                speed /= sprintModifier;
                isSprinting = false;
            }
        }

        IEnumerator Shoot()
        {
            isShooting = true;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDistance, ~ignoreMask))
            {
                //Debug.Log(hit.collider.name);
                Health targetHealth = hit.collider.GetComponent<Health>();

                if (targetHealth != null)
                {
                    targetHealth.TakeDamage(shootDamage, null);
                }
            }
            yield return new WaitForSeconds(shootRate);
            isShooting = false;
        }

        void OnDie()
        {
            IsDead = true;

            EventManager.Broadcast(Events.DeathEvent);
        }

        void OnRespawn()
        {
            IsDead = false;

            playerHealth.CurrentHealth = playerHealth.MaxHealth;
        }
    }
}