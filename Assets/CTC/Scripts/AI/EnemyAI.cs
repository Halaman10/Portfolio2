using CTC.Game;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace CTC.AI
{
    [RequireComponent(typeof(Health), typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] NavMeshAgent agent;

        public UnityAction onAttack;
        public UnityAction onDetectedTarget;
        public UnityAction onLostTarget;
        public UnityAction onDamaged;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
