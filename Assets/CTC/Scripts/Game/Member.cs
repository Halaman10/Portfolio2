using UnityEngine;

namespace CTC.Game
{
    public class Member : MonoBehaviour
    {
        [Tooltip("Represents the team of the player or NPC. Blue = 0 and Pink = 1")]
        [Range(0, 1)] public int teamID = 0;

        [Tooltip("Represents the point where the opposing team members will aim when attacking")]
        [SerializeField] Transform AimPoint;

        void Start()
        {
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
