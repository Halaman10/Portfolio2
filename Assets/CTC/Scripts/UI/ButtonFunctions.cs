using UnityEngine;
using UnityEngine.SceneManagement;

namespace CTC.UI
{
    public class ButtonFunctions : MonoBehaviour
    {
        public void Resume()
        {
            PauseMenuManager.Instance.stateUnpause();
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            PauseMenuManager.Instance.stateUnpause();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
