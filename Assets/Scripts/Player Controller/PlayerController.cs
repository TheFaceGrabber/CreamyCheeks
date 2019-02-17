using UnityEngine;
using System.Collections.Generic;
using CreamyCheaks.Input;

namespace CreamyCheaks.PlayerController
{
    /// <summary>
    /// Used to control the player, i.e. walk, run, jump, etc.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Information")]
        public float JumpForce; //The force that the player will jump with
        public float WalkSpeed; //The speed at which the player will walk
        public float RunSpeed; //The speed at which the player will run

        [Header("Input Information")]
        public Vector2 MouseSensitivity; //How fast the player rotation is, relative to mouse input values
        public Vector2 GamepadSensitivity; //How fast the player rotation is, relative to gamepad input values

        public float MaxMouseX; //How high the player can look
        public float MinMouseX; //How low the player can look

        [Header("Head Bob Information")]
        public float MaxBobWalk; //How much the camera will move when walk
        public float MaxBobRun; //How much the camera will move when running
        public float BobSpeed; //How fast the bob will run

        [Header("Misc")]
        public LayerMask GroundCheckMask; //The mask used for ignore certain colliders when checking for grounded

        public GameObject Flashlight;

        public bool IsGrounded //Finds whether or not the player is on the ground
        {
            get { return Physics.Raycast(transform.position, -transform.up, 1.01f, GroundCheckMask); }
        }

        public Transform CameraTransform; //The transform component on the camera

        Rigidbody rigidbody; //The rigidbody on the player, for movement
        bool isRunning; //Are we running

        float xRot; //Current rotation on the x axis (affects camera transform)
        float yRot; //Current rotation on the y axis (affects player transform)
        Vector3 direction; //The direction we are to move in

        private float bobStartHeight; //The starting height of the camera (0.8)
        private float bobTimer; //The current timer for head bob

        private bool allowInput;

        void Start()
        {
            allowInput = true;
            rigidbody = GetComponent<Rigidbody>();

            bobStartHeight = CameraTransform.localPosition.y;

            if (!InputManager.IsInitialised) //Init input manager if not already initialised
                InputManager.Initialise();
        }

        public void SetAllowInput(bool t)
        {
            allowInput = t;
            if (t)
                rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            else
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;

            GetComponent<PlayerInteract>().enabled = t;
        }

        void Update()
        {
            if (allowInput)
            {
                CameraRotation();
                Movement();
                HeadBob();
            }
        }

        void HeadBob()
        {
            if (direction != Vector3.zero) //Find if we are moving at all, only bob if we are
            {
                bobTimer += BobSpeed * Time.deltaTime; //Add to the timer

                float m = isRunning ? MaxBobRun : MaxBobWalk; //If we're running, use the run value, if not, use the walk value
                float avg = (InputManager.GetAxis("Move Forward") + InputManager.GetAxis("Move Sideways")) / 2; //Average out inputs
                m*= avg; //times the multiplier by the average input
                float bobY = Mathf.Sin(bobTimer) * m; //Calculate the vertical bob
                float bobX = Mathf.Cos(bobTimer / 2) * m; //Calculate the horizontal bob
                CameraTransform.localPosition =
                    new Vector3(bobX, bobStartHeight + bobY, CameraTransform.localPosition.z); //Add it all together and apply to the camera
            }
        }

        void CameraRotation()
        {
            float mouseX = InputManager.GetAxis("Look Horizontal"); //Get "MOUSE X"
            float mouseY = InputManager.GetAxis("Look Vertical"); //Get "MOUSE Y"

            var inputType = InputManager.GetLastInputType();

            var Sens = inputType == InputType.Keyboard ? MouseSensitivity : GamepadSensitivity;

            yRot += mouseX * Sens.x * Time.deltaTime; //Apply mouse x to yRot

            xRot -= mouseY * Sens.y * Time.deltaTime;//Apply mouse y to xRot
            xRot = Mathf.Clamp(xRot, MinMouseX, MaxMouseX); //Clamp xRot between min and max values

            CameraTransform.localEulerAngles = new Vector3(xRot, CameraTransform.localEulerAngles.y, CameraTransform.localEulerAngles.z); //Apply xRot to camera

            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRot, transform.localEulerAngles.z); //Apply yRot to player
        }

        void Movement()
        {
            isRunning = InputManager.GetButton("Run"); //Find if the run button is being held

            float v = InputManager.GetAxis("Move Forward"); //Get "VERTICAL"
            float h = InputManager.GetAxis("Move Sideways"); //Get "HORIZONTAL"
            //Create direction vector
            direction = new Vector3(h,0,v);
            direction = transform.TransformDirection(direction);
            direction *= isRunning ? RunSpeed : WalkSpeed;

            if (IsGrounded)
            {
                //IF we are on the ground and jump has been pressed, apply jump force
                if (InputManager.GetButtonDown("Jump"))
                {
                    direction.y = JumpForce;
                }
            }

            //Finally, add it all together and apply directional values to the rigidbody
            rigidbody.velocity = new Vector3(direction.x, rigidbody.velocity.y + direction.y,direction.z);
        }
    }
}
