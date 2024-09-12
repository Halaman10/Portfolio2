using System;
using UnityEngine;

namespace CTC.Game
{
    public class Member : MonoBehaviour
    {
        public enum TeamID { Blue = 0, Pink = 1 };

        [Tooltip("Represents the team of the player or NPC. Blue = 0 and Pink = 1")]
        public TeamID teamID;

        //[Tooltip("Represents the point where the member will spawn")]
        [NonSerialized] public Vector3 SpawnPos;

        void Start()
        {
            if (SpawnPos == null) SpawnPos = transform.position;
            Debug.Log("Member SpawnPos: " +  SpawnPos);

            if (!MembersManager.Instance.Members.Contains(this))
            {
                MembersManager.Instance.Members.Add(this);
            }
        }

        // Update is called once per frame
        void OnDestroy()
        {
            // Remove a member from the team
            if (MembersManager.Instance.Members.Contains(this))
            {
                MembersManager.Instance.Members.Remove(this);
            }
        }
    }
}
