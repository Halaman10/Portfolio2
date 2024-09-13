using UnityEngine;

namespace CTC.Game
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;

        [Tooltip("This represents the origin of the Blue Team spawn zone")]
        GameObject blueOrigin;

        [Tooltip("This represents the origin of the Pink Team spawn zone")]
        GameObject pinkOrigin;

        [Tooltip("This is the radius of the spawn zone")]
        [SerializeField] float zoneRadius;

        Vector3 randomDist;

        void Awake()
        {
            Instance = this;

            blueOrigin = GameObject.FindWithTag("Blue Spawn Pos");
            pinkOrigin = GameObject.FindWithTag("Pink Spawn Pos");
        }

        public void AssignSpawnPos()
        {
            foreach (Member member in MembersManager.Instance.Members)
            {
                Debug.Log("TeamID: " + member.teamID);
                if (member.teamID == Member.TeamID.Blue)
                {
                    member.SpawnPos = BlueTeamSpawnPos();
                }
                else if (member.teamID == Member.TeamID.Pink)
                {
                    member.SpawnPos = PinkTeamSpawnPos();
                }
                Debug.Log("SpawnManager SpawnPos: " + member.SpawnPos);
            }
        }

        Vector3 BlueTeamSpawnPos()
        {
            randomDist = Random.insideUnitSphere * zoneRadius;

            return blueOrigin.transform.position + randomDist;
        }

        Vector3 PinkTeamSpawnPos()
        {
            randomDist = Random.insideUnitSphere * zoneRadius;

            return pinkOrigin.transform.position + randomDist;
        }
    }
}
