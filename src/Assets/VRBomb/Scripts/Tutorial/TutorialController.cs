using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;


public class TutorialController : MonoBehaviour 
{
	// Tutorial Panel
	public GameObject TutorialPanel;
	public GameObject One;
	public GameObject Two;
	public GameObject Three;
	public GameObject Four;
	public GameObject CloseButton;

	// Menu Panel
	public GameObject Menu;

	// Game Message
	public GameObject Message;
	// Shape Objects
	public GameObject GameObjects;

	// Leap Motion Controller
	Controller LeapMotion;

	
	void Start () 
	{
		Debug.Log ("TutorialController.cs_Initialize.TutorialScene");

		LeapMotion = new Controller();
		
		LeapMotion.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		LeapMotion.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 20.0f);	// default = 30.0f
		LeapMotion.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", 0.5f);	
		LeapMotion.Config.SetFloat("Gesture.ScreenTap.MinDistance", 0.5f);			// default = 1.0f
		
		LeapMotion.Config.Save();

		// Execute tutorial panel here
		StartCoroutine(WalkThrough());
	}


	void Update () 
	{
		/*if(Input.GetKeyDown(KeyCode.Keypad1)){
			Debug.Log ("TutorialController.cs_KeyDown.Keypad1");
			Four.SetActive(false);
			One.SetActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Keypad2)){
			Debug.Log ("TutorialController.cs_KeyDown.Keypad2");
			One.SetActive(false);
			Two.SetActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Keypad3)){
			Debug.Log ("TutorialController.cs_KeyDown.Keypad3");
			Two.SetActive(false);
			Three.SetActive(true);
		}
		if(Input.GetKeyDown(KeyCode.Keypad4)){
			Debug.Log ("TutorialController.cs_KeyDown.Keypad4");
			Three.SetActive(false);
			Four.SetActive(true);
		}*/
		CheckRecenter();
		CheckScreenTap();
		CheckLoadScene();
	}

	
	void CheckLoadScene()
	{
		if(Input.GetKeyDown(KeyCode.F1)){
			Debug.Log ("TutorialController.cs_KeyDown.F1");
			Application.LoadLevel("tutorial");
		}
		if(Input.GetKeyDown(KeyCode.F2)){
			Debug.Log ("TutorialController.cs_KeyDown.F2");
			Application.LoadLevel("headAssembly");
		}
		if(Input.GetKeyDown(KeyCode.F3)){
			Debug.Log ("TutorialController.cs_KeyDown.F3");
			Application.LoadLevel("tailAssembly");
		}
	}


	// Reset Headpose
	void CheckRecenter()
	{
		if(Input.GetKeyDown("r")){
			Debug.Log ("HeadMain.cs_KeyDown.R");
			OVRManager.display.RecenterPose();
		}
	}


	// Is it leap motion's hand?
	public static bool IsHand(string ObjName)
	{
		if(ObjName.Equals ("bone1") || ObjName.Equals ("bone2") || ObjName.Equals ("bone3") || ObjName.Equals ("palm")){
			return true;
		}else{
			return false;
		}
	}


	// Check if there is any screen tap gesture existed on screen
	void CheckScreenTap()
	{
		GestureList GL = LeapMotion.Frame ().Gestures();
		
		for(int i=0; i<GL.Count; i++)
		{
			if(GL[i].IsValid && GL[i].Type == Gesture.GestureType.TYPE_SCREEN_TAP && GL[i].State == Gesture.GestureState.STATE_STOP && new ScreenTapGesture(GL[i]).Progress == 1.0f)
			{
				Debug.Log ("HeadMain.cs_Detected.ScreenTapGesture");

				if(TriggerCloseButton.HandAtCloseButton){
					TriggerCloseButton.HandAtCloseButton = false;
					Debug.Log ("TutorialController.cs_Detected.ScreenTapGesture.CloseButton");
					TutorialPanel.SetActive(false);
					Menu.SetActive(true);
					break;
				}

				if(TriggerHeadButton.HandAtHeadButton){
					TriggerHeadButton.HandAtHeadButton = false;
					Debug.Log ("TutorialController.cs_Detected.ScreenTapGesture.HeadButton");
					Menu.transform.GetChild(1).GetComponent<TriggerHeadButton>().Tap();
					Application.LoadLevel("headAssembly");
					break;
				}
				
				if(TriggerTailButton.HandAtTailButton){
					TriggerTailButton.HandAtTailButton = false;
					Debug.Log ("TutorialController.cs_Detected.ScreenTapGesture.TailButton");
					Menu.transform.GetChild(2).GetComponent<TriggerTailButton>().Tap();
					Application.LoadLevel("tailAssembly");
					break;
				}
			}
		}
	}


	IEnumerator WalkThrough()
	{
		// Display "first tutorial" is for 20 seconds.
		Debug.Log ("TutorialController.cs_Tutorial1.Active");
		yield return new WaitForSeconds(20.0f);
		for(float time=0.0f; time<1.5f; time+=Time.deltaTime/1.0f){
			CloseButton.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.5f, false);
			CloseButton.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(0, 1.5f, false);
			One.GetComponent<RawImage>().CrossFadeAlpha(0, 1.5f, false);
			yield return null;
		}
		One.SetActive(false);
		Debug.Log ("TutorialController.cs_Tutorial1.DeActive");


		// Display "second tutorial" is for 15 seconds.
		Debug.Log ("TutorialController.cs_Tutorial2.Active");
		Two.SetActive(true);
		CloseButton.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(255, 0.1f, false);
		CloseButton.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(255, 0.1f, false);
		yield return new WaitForSeconds(15.0f);
		for(float time=0.0f; time<1.5f; time+=Time.deltaTime/1.0f){
			CloseButton.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.5f, false);
			CloseButton.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(0, 1.5f, false);
			Two.GetComponent<RawImage>().CrossFadeAlpha(0, 1.5f, false);
			yield return null;
		}
		Two.SetActive(false);
		Debug.Log ("TutorialController.cs_Tutorial2.DeActive");


		// Display "third tutorial" is for 30 seconds.
		Debug.Log ("TutorialController.cs_Tutorial3.Active");
		Three.SetActive(true);
		CloseButton.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(255, 0.1f, false);
		CloseButton.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(255, 0.1f, false);
		GameObjects.SetActive(true);
		yield return new WaitForSeconds(30.0f);
		for(float time=0.0f; time<1.5f; time+=Time.deltaTime/1.0f){
			CloseButton.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.5f, false);
			CloseButton.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(0, 1.5f, false);
			Three.GetComponent<RawImage>().CrossFadeAlpha(0, 1.5f, false);
			yield return null;
		}
		Three.SetActive(false);
		GameObjects.SetActive(false);
		Debug.Log ("TutorialController.cs_Tutorial3.DeActive");


		// Display "forth tutorial" is for 20 seconds.
		Debug.Log ("TutorialController.cs_Tutorial4.Active");
		Four.SetActive(true);
		CloseButton.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(255, 0.1f, false);
		CloseButton.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(255, 0.1f, false);
		yield return new WaitForSeconds(20.0f);
		for(float time=0.0f; time<1.5f; time+=Time.deltaTime/1.0f){
			CloseButton.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.5f, false);
			CloseButton.GetComponent<UnityEngine.UI.Image>().CrossFadeAlpha(0, 1.5f, false);
			Four.GetComponent<RawImage>().CrossFadeAlpha(0, 1.5f, false);
			yield return null;
		}
		Four.SetActive(false);
		CloseButton.SetActive(false);
		Debug.Log ("TutorialController.cs_Tutorial4.DeActive");


		// Display "menu panel" for selecting scene
		Menu.SetActive(true);
	}


	public void DisplayMessage(string ObjName)
	{
		switch(ObjName){
			case "Cube"		:	Message.GetComponent<Text>().text = "cube";		break;
			case "Capsule"	:	Message.GetComponent<Text>().text = "capsule";	break;
			case "Sphere"	:	Message.GetComponent<Text>().text = "sphere";	break;
			default 		:	Message.GetComponent<Text>().text = ObjName;	break;
		}
	}
}
