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
        public float JumpForce; //The force that the player will jump with
        public float WalkSpeed; //The speed at which the player will walk
        public float RunSpeed; //The speed at which the player will run
        public Vector2 MouseSensitivity; //How fast the player rotation is, relative to input values

        public float MaxMouseX; //How high the player can look
        public float MinMouseX; //How low the player can look
         
        public LayerMask GroundCheckMask; //The mask used for ignore certain colliders when checking for grounded

        public bool IsGrounded //Finds whether or not the player is on the ground
        {
            get { return Physics.Raycast(transform.position, -transform.up, 0.51f, GroundCheckMask); }
        }

        public Transform CameraTransform; //The transform component on the camera

        Rigidbody rigidbody; //The rigidbody on the player, for movement
        bool isRunning; //Are we running

        float xRot; //Current rotation on the x axis (affects camera transform)
        float yRot; //Current rotation on the y axis (affects player transform)
        Vector3 direction; //The direction we are to move in

        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

            if (!InputManager.IsInitialised) //Init input manager if not already initialised
                InputManager.Initialise();
        }

        void Update()
        {
            CameraRotation();
            Movement();
        }

        void CameraRotation()
        {
            float mouseX = InputManager.GetAxis("Look Horizontal"); //Get "MOUSE X"
            float mouseY = InputManager.GetAxis("Look Vertical"); //Get "MOUSE Y"
            
            yRot += mouseX * MouseSensitivity.x * Time.deltaTime; //Apply mouse x to yRot

            xRot -= mouseY * MouseSensitivity.y * Time.deltaTime;//Apply mouse y to xRot
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
