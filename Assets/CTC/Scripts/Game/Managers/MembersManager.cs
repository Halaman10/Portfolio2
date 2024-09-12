using System.Collections.Generic;
using UnityEngine;

namespace CTC.Game
{
    public class MembersManager : MonoBehaviour
    {
        public static MembersManager Instance;
        public List<Member> Members {  get; private set; }

        private void Awake()
        {
            Instance = this;

            Members = new List<Member>();
        }
    }
}
