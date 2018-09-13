using UnityEngine;
using System.Collections;
using Leap;


// 1. This class performs other input event such as KeyPressed, EnableLeapGestures, CheckHand or etc.
// 2. This class contains utility method for TailAssembly scene.
public class TailMain : MonoBehaviour 
{

	public GameObject Menu;
	public GameObject TailAssembly;
	public GameObject TailUI;

	bool IsMenuActive;
	Controller LeapMotion;
	public static bool EnableCheckGestureUI;


	void Start () 
	{
		Debug.Log ("TailMain.cs_Initialize.TailAsemblyScene");

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
		Debug.Log ("TailMain.cs_Quit.TailAsemblyScene");
		Application.Quit ();
	}


	void Update () 
	{
		CheckLoadScene();
		CheckRecenter();
		CheckResetAccessories();
		CheckMoveObject();
		CheckRotateObject();
		CheckScreenTap();
	}


	// Rotate main object by pressing 5, stop by releasing 5 again
	void CheckRotateObject()
	{
		if(Input.GetKeyDown(KeyCode.Keypad5)){
			Debug.Log ("TailMain.cs_KeyDown.Keypad5");
			TailAssembly.GetComponent<TailAssembly>().StartCoroutine(TailAssembly.GetComponent<TailAssembly>().RotateMainObject());
		}
		if(Input.GetKeyUp(KeyCode.Keypad5)){
			Debug.Log ("TailMain.cs_KeyUp.Keypad5");
			TailAssembly.GetComponent<TailAssembly>().StopAllCoroutines();
		}
	}


	// Move main object in scene by pressing 2,4,6,8,+,-
	void CheckMoveObject()
	{
		if(Input.GetKeyDown(KeyCode.KeypadPlus)){
			Debug.Log ("TailMain.cs_KeyDown.KeypadPlus");

			Vector3 NewPosition = TailAssembly.GetComponent<TailAssembly>().GetMainObjectPosition();
			NewPosition.z -= 0.5f;
			
			TailAssembly.GetComponent<TailAssembly>().MoveMainObject(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.KeypadMinus)){
			Debug.Log ("TailMain.cs_KeyDown.KeypadMinus");
			Vector3 NewPosition = TailAssembly.GetComponent<TailAssembly>().GetMainObjectPosition();
			NewPosition.z += 0.5f;
			TailAssembly.GetComponent<TailAssembly>().MoveMainObject(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.Keypad4)){
			Debug.Log ("TailMain.cs_KeyDown.Keypad4");
			Vector3 NewPosition = TailAssembly.GetComponent<TailAssembly>().GetMainObjectPosition();
			NewPosition.x -= 0.5f;
			TailAssembly.GetComponent<TailAssembly>().MoveMainObject(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.Keypad6)){
			Debug.Log ("TailMain.cs_KeyDown.Keypad6");
			Vector3 NewPosition = TailAssembly.GetComponent<TailAssembly>().GetMainObjectPosition();
			NewPosition.x += 0.5f;
			TailAssembly.GetComponent<TailAssembly>().MoveMainObject(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.Keypad8)){
			Debug.Log ("TailMain.cs_KeyDown.Keypad8");
			Vector3 NewPosition = TailAssembly.GetComponent<TailAssembly>().GetMainObjectPosition();
			NewPosition.y += 0.5f;		
			TailAssembly.GetComponent<TailAssembly>().MoveMainObject(NewPosition);
		}
		if(Input.GetKeyDown(KeyCode.Keypad2)){
			Debug.Log ("TailMain.cs_KeyDown.Keypad2");
			Vector3 NewPosition = TailAssembly.GetComponent<TailAssembly>().GetMainObjectPosition();
			NewPosition.y -= 0.5f;
			TailAssembly.GetComponent<TailAssembly>().MoveMainObject(NewPosition);
		}
	}


	void CheckResetAccessories()
	{
		if(Input.GetKeyDown("a")){
			Debug.Log ("TailMain.cs_KeyDown.A");
			TailAssembly.GetComponent<TailAssembly>().ResetAcessories();
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
					Debug.Log ("TailMain.cs_Detected.ScreenTapGesture");
					
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
						Debug.Log ("TailMain.cs_Detected.ScreenTapGesture.RestartButton");
						Menu.transform.GetChild(2).GetComponent<TriggerRestartButton>().Tap();
						Application.LoadLevel("tailAssembly");
						break;
					}
					
					if(TriggerNextButton.HandAtNextButton){
						EnableCheckGestureUI = false;
						TriggerNextButton.HandAtNextButton = false;
						Debug.Log ("TailMain.cs_Detected.ScreenTapGesture.NextButton");
						Menu.transform.GetChild(3).GetComponent<TriggerNextButton>().Tap();
						Application.LoadLevel("headAssembly");
						break;
					}
				}
			}
		}
	}


	// Loading any scene again
	void CheckLoadScene()
	{
		if(Input.GetKeyDown(KeyCode.F1)){
			Debug.Log ("TailMain.cs_KeyDown.F1");
			Application.LoadLevel("tutorial");
		}
		if(Input.GetKeyDown(KeyCode.F2)){
			Debug.Log ("TailMain.cs_KeyDown.F2");
			Application.LoadLevel("headAssembly");
		}
		if(Input.GetKeyDown(KeyCode.F3)){
			Debug.Log ("TailMain.cs_KeyDown.F3");
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
}
