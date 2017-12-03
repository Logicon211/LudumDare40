using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        //private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_CamSideways;
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		public float maxSpeed;
		public float currentMaxSpeed;
		private float forwardSpeed;
		private float sideSpeed;
		private float slowdown;
		public GameObject target;
		public float rotateSpeed = 5;
		public GameObject collisionCube;
		private Rigidbody collisionCubeRigidbody;
		private BoxCollider cubeCollider;
		public bool charging;
		public GameObject speedLines;
		private float chargeCooldown =2f;

		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_JumpPower = 12f;
		[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.3f;


		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Crouching;

		public int maxNumBabies = 5;
		public int currentNumBabies = 0;

		public AudioClip jumpNoise;
		public AudioClip footstep;

		public Slider chargeSlider;

		private AudioSource audiosource;


		void Start()
		{
			collisionCubeRigidbody = collisionCube.GetComponent<Rigidbody> ();
			collisionCubeRigidbody.mass = 0;
			cubeCollider = collisionCube.GetComponent<BoxCollider> ();
			cubeCollider.enabled = false;
			collisionCube.SetActive (false);
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;

			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			m_OrigGroundCheckDistance = m_GroundCheckDistance;

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			maxSpeed = 10;
			currentMaxSpeed = 10;
			slowdown = 0;
			forwardSpeed = 0;
			sideSpeed = 10;
			m_Cam = Camera.main.transform;

            // get the third person character ( this should never be null due to require component )
            //m_Character = GetComponent<ThirdPersonCharacter>();
			transform.parent = target.transform;
			transform.LookAt(target.transform);

			audiosource = GetComponent<AudioSource> ();
			speedLines.SetActive(false);
			charging = false;
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

			chargeCooldown -= 0.01f;
			chargeSlider.value = Math.Min (1.5f, 1.5f - chargeCooldown);
			chargeSlider.enabled = false;

			//keyboard inputs
			//h is horizontal (-1 left, 1 rigth)
			//v is vertical, only allowed to be 1, not sure how we will handle backing up
			float v = 0;
			float h = 0;

			if (Input.GetKey (KeyCode.W)) {
				v += 1;
				slowdown = 0;
				//Debug.Log ("checking forward speed" + forwardSpeed + " vs max speed " + maxSpeed);
				if (forwardSpeed < currentMaxSpeed) {
					forwardSpeed += 0.5f;
					if (forwardSpeed > currentMaxSpeed) {
						forwardSpeed = currentMaxSpeed;
					}
				}

			} 


			if (Input.GetKey (KeyCode.S)) {
				v += -1;
				slowdown = 0;
				//Debug.Log ("checking forward speed" + forwardSpeed + " vs max speed " + maxSpeed);
				if (forwardSpeed < currentMaxSpeed) {
					forwardSpeed += 0.5f;
					if (forwardSpeed > currentMaxSpeed) {
						forwardSpeed = currentMaxSpeed;
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

			if (Input.GetMouseButton (1) && m_IsGrounded && chargeCooldown <0) {
				chargeAttack ();
				chargeCooldown = 1.4f;
			}

            // calculate move direction to pass to character
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_CamSideways = Vector3.Scale(m_Cam.right, new Vector3(1, 0, 1)).normalized;
			//Debug.Log ("h: " + h);
			m_Move = v*m_CamForward*forwardSpeed + h*m_CamSideways*sideSpeed;
        
			//Vector3.Angle (m_CamForward, m_Move);
			//transform.Rotation(Vector3.Angle (m_CamForward, m_Move));
			//transform.rot
            
			if (m_Move != Vector3.zero) {
				//Debug.Log ("m_Move is zero");
				transform.rotation = Quaternion.Slerp (
					transform.rotation,
					Quaternion.LookRotation (m_Move),
					Time.deltaTime * 5f
				);
			} else {
				transform.rotation = Quaternion.Slerp (
				transform.rotation,
					Quaternion.LookRotation (m_CamForward),
				Time.deltaTime * 5f
				);
			}

			// pass all parameters to the character control script
            Move(m_Move, m_Jump);
            m_Jump = false;
        }



		public void maxSpeedChange(){
			currentMaxSpeed = maxSpeed * (1 - (currentNumBabies * 0.15f));
		}


		public void Move(Vector3 move, bool jump)
		{
			if (m_IsGrounded && Time.deltaTime > 0 && !charging) {
				//Vector3 v;
				//Debug.Log ("move.x: " + move.x + "   move.y: " + move.y + "     move.z: " + move.z);
				// we preserve the existing y part of the current velocity.
				//move = m_Rigidbody.velocity.y;
				//m_Rigidbody = 
				//v = m_Rigidbody.velocity;
				//v.y = m_Rigidbody.velocity.y;
				//v.x = move.x;
				//v.z = move.z;
				m_Rigidbody.velocity = move;
				Vector3 v = new Vector3 (m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
				if(v.magnitude > currentMaxSpeed)
				{
					v = v.normalized * currentMaxSpeed;
					v.y = m_Rigidbody.velocity.y;
					m_Rigidbody.velocity=v;
				}

				//Debug.Log ("GROUND rigidbody.x: " + m_Rigidbody.velocity.x + "   rigidbody.z: " + m_Rigidbody.velocity.z);



			} else if(Time.deltaTime > 0 && !charging) {
				move.x = m_Rigidbody.velocity.x + (move.x *0.2f);
				move.z = m_Rigidbody.velocity.z + (move.z *0.2f);
				move.y = m_Rigidbody.velocity.y; 
				//move.x = Mathf.Clamp(move.x, -maxSpeed, maxSpeed);
				//move.z = Mathf.Clamp(move.z, -maxSpeed, maxSpeed);
				//Debug.Log ("MOVE.Y: " + move.y +"  MOVE.X: " + move.y + "  maxspeed: " + maxSpeed);
				m_Rigidbody.velocity = move;
				Vector3 v = new Vector3 (m_Rigidbody.velocity.x, 0, m_Rigidbody.velocity.z);
				if(v.magnitude > currentMaxSpeed)
				{
					v = v.normalized * currentMaxSpeed;
					v.y = m_Rigidbody.velocity.y;
					m_Rigidbody.velocity=v;
				}


				//m_Rigidbody.velocity.x = Mathf.Clamp(m_Rigidbody.velocity.x, -maxSpeed, maxSpeed);
				//m_Rigidbody.velocity.y = Mathf.Clamp(m_Rigidbody.velocity.y, -maxSpeed, maxSpeed);
			}


			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			//m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			//ApplyExtraTurnRotation();

			// control and velocity handling is different when grounded and airborne:
			if (m_IsGrounded)
			{
				HandleGroundedMovement(jump);
			}
			else
			{
				HandleAirborneMovement();
			}


			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}


		void HandleAirborneMovement()
		{
			// apply extra gravity from multiplier:
			Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
			m_Rigidbody.AddForce(extraGravityForce);

			m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
		}


		void HandleGroundedMovement(bool jump)
		{
			// check whether conditions are right to allow a jump:
			if (jump && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				// jump!
				audiosource.PlayOneShot(jumpNoise);
				m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
				m_IsGrounded = false;
				m_Animator.applyRootMotion = false;
				m_GroundCheckDistance = 0.2f;
			}
		}


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;

			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));

			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(gameObject.transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
				m_Animator.applyRootMotion = true;
			}
			else
			{
				m_IsGrounded = false;
			//	Debug.Log ("WE ARE IN THE AIR");
			//	Debug.Log ("transform.position.y: " + transform.position.y);
			//m_GroundNormal = Vector3.up;
			//	m_Animator.applyRootMotion = false;
			}
		}

		void OnCollisionEnter(Collision collision) {
			if (collision.gameObject.tag == "Baby") {
				if (!collision.gameObject.GetComponent<BabyController> ().flying) {
					//Attach baby to Craig
					if (currentNumBabies < maxNumBabies) {
						currentNumBabies++;
						maxSpeedChange ();
						Destroy (collision.gameObject);
					}
				}
			}
		}


		void chargeAttack(){
			Debug.Log ("charging");
			charging = true;
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			collisionCubeRigidbody.isKinematic = false;
			collisionCubeRigidbody.mass = 300;
			cubeCollider.enabled = true;
			collisionCube.SetActive (true);
			//m_Rigidbody.drag = 0;
			speedLines.SetActive(true);
			m_Rigidbody.velocity = (40f * transform.forward);
			StartCoroutine("ChargingTimer");


		}




		void UpdateAnimator(Vector3 move)
		{
			// update the animator parameters
			m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
			m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
			m_Animator.SetBool("Crouch", m_Crouching);
			m_Animator.SetBool("OnGround", m_IsGrounded);
			if (!m_IsGrounded)
			{
				m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
			}

			// calculate which leg is behind, so as to leave that leg trailing in the jump animation
			// (This code is reliant on the specific run cycle offset in our animations,
			// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
			float runCycle =
				Mathf.Repeat(
					m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
			float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
			if (m_IsGrounded)
			{
				m_Animator.SetFloat("JumpLeg", jumpLeg);
			}

			// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
			// which affects the movement speed because of the root motion.
			if (m_IsGrounded && move.magnitude > 0)
			{
				m_Animator.speed = m_AnimSpeedMultiplier;
			}
			else
			{
				// don't use that while airborne
				m_Animator.speed = 1;
			}
		}




		IEnumerator ChargingTimer()
		{
			print("CHARGING");
			yield return new WaitForSeconds(0.3f);
			print("DONE CHARGING");
			charging = false;
			//collisionCubeRigidbody.
			cubeCollider.enabled = false;
			collisionCubeRigidbody.isKinematic = true;
			collisionCubeRigidbody.mass = 0;
			collisionCube.SetActive (false);
			//m_Rigidbody.drag = 0.05f;
			speedLines.SetActive(false);

		}

    }
}
