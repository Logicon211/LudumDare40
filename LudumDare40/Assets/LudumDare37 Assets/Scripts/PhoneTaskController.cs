using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTaskController : MonoBehaviour {

	private TaskController parentController;
	private bool taskComplete = false;

	public AudioClip phone_ringing;
	public AudioClip conversation_clip;
	//This should come from the object in scene
	public AudioSource phoneAudio;
	private bool ringing;

	private GameObject phoneInstructions;
	private GameObject phoneInstructionsDone;

	// Use this for initialization
	void Start () {
		phoneInstructions = transform.Find ("PhoneInstructions").gameObject;
		phoneInstructionsDone = transform.Find ("PhoneInstructionsDone").gameObject;

		phoneInstructionsDone.SetActive (false);

		parentController = GameObject.FindObjectOfType<TaskController> ();
		ringing = false;
		//audio = gameObject.GetComponent<AudioSource>();

	}

	// Update is called once per frame
	void Update () {
		//15 seconds after light is turned on, start the phone ringing
		//while (!answered) {
		//}
	}
		
	public void StartRinging(){
		//Start playing audio clip

		Debug.Log ("PHONE: start playing ringing");
		phoneAudio.clip = phone_ringing;
		phoneAudio.PlayDelayed(0.5f);
		ringing = true;
	}

	public bool CheckRinging(){
		return ringing;
	}

	public void StartCall(){

		if (ringing){
			ringing = false;
				phoneAudio.Stop ();

				Debug.Log ("PHONE: start playing clip");
				//Start audio clip recording for conversation
				phoneAudio.PlayOneShot (conversation_clip);

				//while(audiosource.isplaying()){
				//}
				Invoke ("PhoneTaskComplete", 5f);
		}
	}

	private void PhoneTaskComplete() {
		
		Debug.Log ("Phone task complete");
		parentController.TriggerPhoneTaskComplete ();

		phoneInstructionsDone.SetActive (true);
		phoneInstructions.SetActive (false);

	}
		
}