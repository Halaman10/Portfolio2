using UnityEngine;

namespace CTC.Gameplay
{
    public class CameraController : MonoBehaviour
    {
        [Tooltip("Sensitivity multiplier for moving the camera around")]
        public float lookSensitivity = 1f;

        [Tooltip("Limit the camera's vertical angle")]
        [SerializeField] int lockVertMin, lockVertMax;

        [Tooltip("Used to flip the vertical input axis")]
        [SerializeField] bool invertY;

        float rotX;

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            // get input
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime;

            // handle inverting vertical input
            if (invertY)
                rotX += mouseY;
            else
                rotX -= mouseY;

            // clamp the rotX on the x-axis
            rotX = Mathf.Clamp(rotX, lockVertMin, lockVertMax);

            // rotate the camera on the x-axis
            transform.localRotation = Quaternion.Euler(rotX, 0, 0);

            // rotate the PLAYER on the y-axis
            transform.parent.Rotate(Vector3.up * mouseX);
        }
    }
}
