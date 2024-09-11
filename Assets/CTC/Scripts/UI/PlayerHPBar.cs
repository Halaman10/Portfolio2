using CTC.Game;
using UnityEngine;
using UnityEngine.UI;

namespace CTC.UI
{
    public class PlayerHPBar : MonoBehaviour
    {
        [Tooltip("Image component displaying player HP")]
        [SerializeField] Image HPFillImage;

        Health playerHP;

        void Start ()
        {
            playerHP = GameManager.Instance.playerScript.GetComponent<Health>();
        }

        private void Update()
        {
            HPFillImage.fillAmount = playerHP.CurrentHealth / playerHP.MaxHealth;
        }
    }
}
