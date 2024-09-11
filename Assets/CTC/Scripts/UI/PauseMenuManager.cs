using UnityEngine;

namespace CTC.UI
{
    public class PauseMenuManager : MonoBehaviour
    {
        public static PauseMenuManager Instance;

        [Tooltip("Root canvas used to toggle Pause Menu activation")]
        [SerializeField] GameObject menuRoot;

        //[Tooltip("Slider component for look sensitivity")]
        //[SerializeField] Slider lookSensitivitySlider;

        //CameraController cameraController;

        public bool IsPaused { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            menuRoot.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!menuRoot.activeSelf)
                {
                    statePause();
                    menuRoot.SetActive(IsPaused);
                }
                else
                    stateUnpause();
            }
        }

        public void statePause()
        {
            IsPaused = !IsPaused;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void stateUnpause()
        {
            IsPaused = !IsPaused;
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            menuRoot.SetActive(IsPaused);
        }
    }
}
