using System.Collections;
using UnityEngine;
// using ThunderWire.Helpers;
// using ThunderWire.Utility;
// using ThunderWire.Input;
// using HFPS.Systems;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {
        //protected Timekeeper timekeeper = new Timekeeper();

        [SerializeField] private GameObject mainCameraGameObject;
        [SerializeField] private GameObject player;

        private Camera mainCamera; 
        
        public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
        public RotationAxes axes = RotationAxes.MouseXAndY;

        public bool isLocalCamera;
        public bool smoothLook;
        public float smoothTime = 5f;

        [Header("Sensitivity")]
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        [Header("Look Limits")]
        public float minimumX = -60F;
        public float maximumX = 60F;
        public float minimumY = -60F;
        public float maximumY = 60F;

        [Header("Look Offsets")]
        public float offsetY = 0F;
        public float offsetX = 0F;

        [Header("Debug")]
        public float rotationX = 0F;
        public float rotationY = 0F;

        [Header("Options Prefixes")]
        public string mouseSensitivity = "mouse_sensitivity";
        public string verticalSensitivity = "vertical_look";
        public string horizontalSensitivity = "horizontal_look";
        public string invertLook = "invert_look";

        private float deltaInputX;
        private float deltaInputY;
        private bool lookInverted;
        private bool lockLook;

        private Vector2 lerpRotation;
        private float lerpSpeed;
        private bool doLerpLook;

        Vector2 clampRange;
        Quaternion originalRotation;

        [HideInInspector]
        public bool bodyClamp = false;

        [HideInInspector]
        public Quaternion playerOriginalRotation;

        void Awake()
        {
            player = transform.root.gameObject;
        }

        void Start()
        {
            if (!isLocalCamera)
            {
                // mainCamera = Utilities.MainPlayerCamera();
            }
            else
            {
                mainCamera = mainCameraGameObject.GetComponent<Camera>();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Make the rigid body not change rotation
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }

            originalRotation = transform.localRotation;
            playerOriginalRotation = player.transform.localRotation;
        }
        

        void Update()
        {
            //timekeeper.UpdateTime();

            if (!lockLook)
            {
                doLerpLook = false;

                // Vector2 lookDelta = InputHandler.ReadInput<Vector2>("Look", "PlayerExtra");
                // deltaInputX = lookDelta.x;
                // deltaInputY = lookInverted ? lookDelta.y * -1 : lookDelta.y;
            }
            else if (doLerpLook)
            {
                rotationX = Mathf.LerpAngle(rotationX, lerpRotation.x, Time.deltaTime * (lerpSpeed * 1.5f));
                rotationY = Mathf.LerpAngle(rotationY, lerpRotation.y, Time.deltaTime * lerpSpeed);
            }

            if (Cursor.lockState == CursorLockMode.None)
                return;

            if (axes == RotationAxes.MouseXAndY)
            {
                // Read the mouse input axis
                rotationX += (deltaInputX * sensitivityX / 30 * mainCamera.fieldOfView + offsetX);
                rotationY += (deltaInputY * sensitivityY / 30 * mainCamera.fieldOfView + offsetY);

                rotationX = ClampAngle(rotationX, minimumX, maximumX);

                if (bodyClamp)
                {
                    rotationX = ClampBodyAngle(rotationX);
                }

                rotationY = ClampAngle(rotationY, minimumY, maximumY);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);

                Quaternion playerRotation = playerOriginalRotation * xQuaternion;
                Quaternion lookRotation = originalRotation * yQuaternion;

                if (smoothLook)
                {
                    player.transform.localRotation = Quaternion.Slerp(player.transform.localRotation, playerRotation, smoothTime * Time.deltaTime);
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, lookRotation, smoothTime * Time.deltaTime);
                }
                else
                {
                    player.transform.localRotation = playerRotation;
                    transform.localRotation = lookRotation;
                }
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotationX += (deltaInputX * sensitivityX / 60 * mainCamera.fieldOfView + offsetX);
                rotationX = ClampAngle(rotationX, minimumX, maximumX);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            else
            {
                rotationY += (deltaInputY * sensitivityY / 60 * mainCamera.fieldOfView + offsetY);
                rotationY = ClampAngle(rotationY, minimumY, maximumY);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
            }

            offsetY = 0F;
            offsetX = 0F;
        }

        public void LockLook(bool state)
        {
            lockLook = state;
        }

        public void LerpLook(Vector2 rotation, float speed, bool lockLook)
        {
            this.lockLook = lockLook;
            lerpRotation = rotation;
            lerpSpeed = speed;

            deltaInputX = 0;
            deltaInputY = 0;

            StartCoroutine(LerpLookWait());
        }

        IEnumerator LerpLookWait()
        {
            yield return new WaitForEndOfFrame();
            doLerpLook = true;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            float newAngle = FixAngle(angle);
            return Mathf.Clamp(newAngle, min, max);
        }

        public void SetClampRange(float min, float max)
        {
            float y = rotationX;

            float angle1 = FixAngle(y + min);
            float angle2 = FixAngle(y + max);

            clampRange = new Vector2(angle1, angle2);
        }

        public float ClampBodyAngle(float angle)
        {
            return Mathf.Clamp(angle, clampRange.x, clampRange.y);
        }

        public Vector2 GetRotation()
        {
            return new Vector2(rotationX, rotationY);
        }

        public Vector2 GetInputDelta()
        {
            return new Vector2(deltaInputX, deltaInputY);
        }

        public void SetRotation(Vector2 rotation)
        {
            rotationX = rotation.x;
            rotationY = rotation.y;
        }
        /// <summary>
        /// Correct the Angle
        /// </summary>
        public static float FixAngle(float angle)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;

            return angle;
        }
        
    }
}