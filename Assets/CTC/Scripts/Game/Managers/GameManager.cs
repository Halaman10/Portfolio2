using CTC.Gameplay;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace CTC.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Tooltip("GameObject tagged Player")]
        public GameObject player;

        [Tooltip("PlayerController script")]
        public PlayerController playerScript;

        [Tooltip("Duration of delay before respawning")]
        [Range(0, 5)] public float respawnDelayTime;

        void Awake()
        {
            Instance = this;

            player = GameObject.FindWithTag("Player");
            playerScript = player.GetComponent<PlayerController>();

            EventManager.AddEventListener<DeathEvent>(OnDeath);
        }

        void Start()
        {
            SpawnManager.Instance.AssignSpawnPos();
        }

        void OnDeath(DeathEvent evt) => Respawn();

        void Respawn()
        {
            if (playerScript.IsDead)
            {
                StartCoroutine(DelayRespawn());

                playerScript.SpawnPlayer();
            }
        }

        IEnumerator DelayRespawn()
        {
            yield return new WaitForSeconds(respawnDelayTime);
        }
    }
}
