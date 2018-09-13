using UnityEngine;
using System.Collections;
using Leap;


// 1. This class performs other input event such as KeyPressed, EnableLeapGestures, CheckHand or etc.
// 2. This class contains utility method for HeadAssembly scene.
public class HeadMain : MonoBehaviour 
{
	// Grant privilege to access other class
	public GameObject HeadUI;
	public GameObject HeadAssemblyObject;
	public GameObject Menu;
	
	bool IsMenuActive;
	public static bool EnableCheckGestureUI;
	
	Controller LeapMotion;

	void Start()
	{
		Debug.Log ("HeadMain.cs_Initialize.HeadAsemblyScene");

		EnableCheckGestureUI = false;
		IsMenuActive = false;
		LeapMotion = new Controller();

		LeapMotion.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		LeapMotion.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 20.0f);	// default = 30.0f
		LeapMotion.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", 0.5f);	
		LeapMotion.Config.SetFloat("Gesture.ScreenTap.MinDistance", 0.5f);			// default = 1.0f

		LeapMotion.Config.Save();

	}
	
	
	void OnApplicationQuit()
	{
		Debug.Log ("HeadMain.cs_Quit.HeadAsemblyScene");
		Application.Quit ();
	}


	// Check user's input every frame
	void Update () 
	{
		CheckMoveObject();
		CheckRotateObject();
		//CheckPause();
		CheckRecenter();
		CheckLoadScene();
		CheckScreenTap();
		CheckResetAccessories();
		//CheckToggleHint();
	}


	/*void CheckToggleHint()
	{
		if(Input.GetKeyDown("h"))
		{
			Debug.Log ("HeadMain.cs_KeyDown.H");

			if(HintIsActive){
				HintIsActive = false;
				HeadUI.GetComponent<HeadUI>().ToggleInterface(false);	// Deactivated hint interfaces
				// Deactivated particle effects
				HeadAssemblyObject.GetComponent<HeadAssembly>().ToggleLightEffect(false);	// Deactivated PointLight object
				// Activated "Warning Message" object
			}else{
				HintIsActive = true;
				HeadUI.GetComponent<HeadUI>().ToggleInterface(true);						// Activated hint interfaces
				// Activated particle effects
				HeadAssemblyObject.GetComponent<HeadAssembly>().ToggleLightEffect(true);	// Activated PointLight object
				// Deactivated "Warning Message" object
			}
		}
	}*/


	void CheckResetAccessories()
	{
		if(Input.GetKeyDown("a")){
			Debug.Log ("HeadMain.cs_KeyDown.A");
			HeadAssemblyObject.GetComponent<HeadAssembly>().ResetAcessories();
		}
	}


	/*
	// Display PAUSE menu by pressing 'ESC'
	void CheckPause()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(IsMenuActive){	// true = close it
				IsMenuActive = false;

				EnableCheckGestureUI = false;
				// instruction.setactive(false);
				Menu.SetActive(false);
				IsMenuActive = false;
			}else{				// false = open it
				IsMenuActive = true;

				// complete.setactive(false);
				// instruction.setactive(true);
				Menu.SetActive(true);
				OVRManager.display.RecenterPose();
				EnableCheckGestureUI = true;
			}
		}
	}
	*/


	// Loading any scene again
	void CheckLoadScene()
	{
		if(Input.GetKeyDown(KeyCode.F1)){
			Debug.Log ("HeadMain.cs_KeyDown.F1");
			Application.LoadLevel("tutorial");
		}
		if(Input.GetKeyDown(KeyCode.F2)){
			Debug.Log ("HeadMain.cs_KeyDown.F2");
			Application.LoadLevel("headAssembly");
		}
		if(Input.GetKeyDown(KeyCode.F3)){
			Debug.Log ("HeadMain.cs_KeyDown.F3");
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


	// Check if there is any screen tap gesture existed on screen
	void CheckScreenTap()
	{
		if(EnableCheckGestureUI)
		{
			GestureList GL = LeapMotion.Frame ().Gestures();

			for(int i=0; i<GL.Count; i++)
			{
				if(GL[i].IsValid && GL[i].Type == Gesture.GestureType.TYPE_SCREEN_TAP && GL[i].State == Gesture.GestureState.STATE_STOP && new ScreenTapGesture(GL[i]).Progress == 1.0f)
				{
					Debug.Log ("HeadMain.cs_Detected.ScreenTapGesture");

					/*if(TriggerScreenTap.HandAtScrewDriverClone1){
						TriggerScreenTap.HandAtScrewDriverClone1 = false;
						Debug.Log ("HeadMain.cs_Detected.ScreenTapGesture.ScrewDriverClone1");
						TriggerScreenTap.AlreadyScreenTapAtScrewDriverClone1 = true;
						break;
					}*/

					/*if(TriggerScreenTap2.HandAtRetainingClipClone1){
						TriggerScreenTap2.HandAtRetainingClipClone1 = false;
						Debug.Log ("HeadMain.cs_Detected.ScreenTapGesture.RetainingClipClone1");
						TriggerScreenTap2.AlreadyScreenTapAtRetainingClipClone1 = true;
						break;
					}*/

					/*if(TriggerMenuButton.HandAtMenuButton){
						EnableCheckGestureUI = false;
						TriggerMenuButton.HandAtMenuButton = false;
						Debug.Log ("HeadMain.cs_Detected.ScreenTapGesture.MenuButton");
						Menu.transform.GetChild(1).GetComponent<TriggerMenuButton>().Tap();
						// Don't forget "Back to main menu" statement
						Application.Quit ();
						break;
					}*/

					if(TriggerRestartButton.HandAtRestartButton){
						EnableCheckGestureUI = false;
						TriggerRestartButton.HandAtRestartButton = false;
						Debug.Log ("HeadMain.cs_Detected.ScreenTapGesture.RestartButton");
						Menu.transform.GetChild(2).GetComponent<TriggerRestartButton>().Tap();
						Application.LoadLevel("headAssembly");
						break;
					}

					if(TriggerNextButton.HandAtNextButton){
						EnableCheckGestureUI = false;
						TriggerNextButton.HandAtNextButton = false;
						Debug.Log ("HeadMain.cs_Detected.ScreenTapGesture.NextButton");
						Menu.transform.GetChild(3).GetComponent<TriggerNextButton>().Tap();
						Application.LoadLevel("tailAssembly");
						break;
					}
				}
			}
		}
	}


	// Move main object in scene by pressing 2,4,6,8,+,-
	void CheckMoveObject()
	{
		if(Input.GetKeyDown(KeyCode.KeypadPlus)){
			Debug.Log ("HeadMain.cs_KeyDown.KeypadPlus");

			// acces to other gameobject's script, for using methods
			HeadAssembly TempScript = (HeadAssembly)HeadAssemblyObject.GetComponent(typeof(HeadAssembly));
			Vector3 NewPosition = TempScript.GetObjectPosition();
			NewPosition.z -= 0.5f;

			TempScript.MoveObjectTo(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.KeypadMinus)){
			Debug.Log ("HeadMain.cs_KeyDown.KeypadMinus");
			HeadAssembly TempScript = (HeadAssembly)HeadAssemblyObject.GetComponent(typeof(HeadAssembly));
			Vector3 NewPosition = TempScript.GetObjectPosition();
			NewPosition.z += 0.5f;
			TempScript.MoveObjectTo(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.Keypad4)){
			Debug.Log ("HeadMain.cs_KeyDown.Keypad4");
			HeadAssembly TempScript = (HeadAssembly)HeadAssemblyObject.GetComponent(typeof(HeadAssembly));
			Vector3 NewPosition = TempScript.GetObjectPosition();
			NewPosition.x -= 0.5f;
			TempScript.MoveObjectTo(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.Keypad6)){
			Debug.Log ("HeadMain.cs_KeyDown.Keypad6");
			HeadAssembly TempScript = (HeadAssembly)HeadAssemblyObject.GetComponent(typeof(HeadAssembly));
			Vector3 NewPosition = TempScript.GetObjectPosition();
			NewPosition.x += 0.5f;
			TempScript.MoveObjectTo(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.Keypad8)){
			Debug.Log ("HeadMain.cs_KeyDown.Keypad8");
			HeadAssembly TempScript = (HeadAssembly)HeadAssemblyObject.GetComponent(typeof(HeadAssembly));
			Vector3 NewPosition = TempScript.GetObjectPosition();
			NewPosition.y += 0.5f;		
			TempScript.MoveObjectTo(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.Keypad2)){
			Debug.Log ("HeadMain.cs_KeyDown.Keypad2");
			HeadAssembly TempScript = (HeadAssembly)HeadAssemblyObject.GetComponent(typeof(HeadAssembly));
			Vector3 NewPosition = TempScript.GetObjectPosition();
			NewPosition.y -= 0.5f;
			TempScript.MoveObjectTo(NewPosition);
		}
	}


	// Rotate main object by pressing 5, stop by releasing 5 again
	void CheckRotateObject()
	{
		if(Input.GetKeyDown(KeyCode.Keypad5)){
			Debug.Log ("HeadMain.cs_KeyDown.Keypad5");
			HeadAssembly TempScript = (HeadAssembly)HeadAssemblyObject.GetComponent(typeof(HeadAssembly));
			TempScript.StartCoroutine(TempScript.RotateMainObject());
		}
		if(Input.GetKeyUp(KeyCode.Keypad5)){
			Debug.Log ("HeadMain.cs_KeyUp.Keypad5");
			HeadAssembly TempScript = (HeadAssembly)HeadAssemblyObject.GetComponent(typeof(HeadAssembly));
			TempScript.StopAllCoroutines();
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
}
