using CTC.Gameplay;
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

        private void Awake()
        {
            Instance = this;

            player = GameObject.FindWithTag("Player");
            playerScript = player.GetComponent<PlayerController>();
        }
    }
}
