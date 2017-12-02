using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		private float maxSpeed;
		private float forwardSpeed;
		private float slowdown;
		public GameObject target;
		public float rotateSpeed = 5;
        
        private void Start()
        {

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			maxSpeed = 100;
			slowdown = 0;
			forwardSpeed = 0;
			m_Cam = Camera.main.transform;

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
			transform.parent = target.transform;
			transform.LookAt(target.transform);
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


			

		void LateUpdate() {
			float horizontal = Input.GetAxis("Mouse X") * 4;
			//float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
			target.transform.Rotate(0, horizontal, 0);
			//m_Cam.transform.RotateAround (target.transform.position, Vector3.up, horizontal);
			//m_Cam.transform.RotateAround (target.transform.position, Vector3.right, vertical);
		}

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
  


			//keyboard inputs
			//h is horizontal (-1 left, 1 rigth)
			//v is vertical, only allowed to be 1, not sure how we will handle backing up
			float v = 0;
			float h = 0;
			if (Input.GetKey (KeyCode.W)) {
				v += 1;
				slowdown = 0;
				Debug.Log ("checking forward speed" + forwardSpeed + " vs max speed " + maxSpeed);
				if (forwardSpeed < maxSpeed) {
					forwardSpeed += 1;
					if (forwardSpeed > maxSpeed) {
						forwardSpeed = maxSpeed;
					}
				}

			} else {
				if(forwardSpeed >0){
					forwardSpeed =- slowdown;
					slowdown += 1+ slowdown;
					if (forwardSpeed < 0) {
						forwardSpeed = 0;
					}
				}
			}
			//Debug.Log ("forwardSpeed: " + forwardSpeed);
			//Debug.Log ("slowdown Speed: " + slowdown);
			if(Input.GetKey(KeyCode.A)){
				h+=-1;
			}
			if(Input.GetKey(KeyCode.D)){
				h+=1;
			}

            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				m_Move = v*m_CamForward*forwardSpeed + h*m_Cam.right;
        
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }



		public void maxSpeedChange(int SpeedVariation){
			maxSpeed += SpeedVariation;
		}


    }
}
