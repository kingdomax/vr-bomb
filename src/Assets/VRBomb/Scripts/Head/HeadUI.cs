using UnityEngine;
using System.Collections;
using UnityEngine.UI;		// Must declare this, when trying to access UI component


// 1.This class grants privilege to access all UI object.
// 2.This class also contains utility method for ui manipulation.
public class HeadUI : MonoBehaviour 
{
	// Other UI
	public GameObject Menu;
	public GameObject Message;
	public GameObject HintPanel;
	public GameObject Indicator;

	
	// Direct reference to UI-Step1
	public GameObject Step1;
	public GameObject Description1_1;
	public GameObject Description1_2;
	public GameObject Description1_3;
	
	// Direct reference to UI-Step2
	public GameObject Step2;
	public GameObject Description2_1;
	public GameObject Description2_2;

	// Direct reference to UI-Step3
	public GameObject Step3;
	public GameObject Description3_1;

	// Direct reference to UI-Step4
	public GameObject Step4;
	public GameObject Description4_1;
	public GameObject Description4_2;

	// Direct reference to UI-Step5
	public GameObject Step5;
	public GameObject Description5_1;


	void Start()
	{
		StartCoroutine(FadeOutMessage());	// Fade out welcome message
	}


	IEnumerator FadeOutMessage()
	{
		// Display "welcome" for 4 minutes
		yield return new WaitForSeconds(4.0f);		
		for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
			Message.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
			yield return null;
		}
		Message.GetComponent<Text>().text = "";
		Message.GetComponent<Text>().CrossFadeAlpha(255, 0.1f, true);
	}


	public void UiStep1(int JustFinishDescription)
	{
		switch(JustFinishDescription)
		{
			case 1	:	Description1_1.transform.GetChild(0).gameObject.SetActive(true);
						Description1_2.SetActive(true);
						break;
			case 2	: 	Description1_2.transform.GetChild(0).gameObject.SetActive(true);
						Description1_3.SetActive(true);
						break;
			case 3 : 	Description1_3.transform.GetChild(0).gameObject.SetActive(true);
						StartCoroutine(FadeOutHint(1));
						break;
		}
	}


	public void UiStep2(int JustFinishDescription)
	{
		switch(JustFinishDescription)
		{
			case 1	:	Description2_1.transform.GetChild(0).gameObject.SetActive(true);
						Description2_2.SetActive(true);
						break;
			case 2	: 	Description2_2.transform.GetChild(0).gameObject.SetActive(true);
						StartCoroutine(FadeOutHint(2));
						break;

		}
	}
	
	
	public void UiStep3(int JustFinishDescription)
	{
		switch(JustFinishDescription)
		{
			case 1	:	Description3_1.transform.GetChild(0).gameObject.SetActive(true);
						StartCoroutine(FadeOutHint(3));
						break;
		}
	}
	
	
	public void UiStep4(int JustFinishDescription)
	{
		switch(JustFinishDescription)
		{
			case 1	:	Description4_1.transform.GetChild(0).gameObject.SetActive(true);
						Description4_2.SetActive(true);
						break;
			case 2	:	Description4_2.transform.GetChild(0).gameObject.SetActive(true);
						StartCoroutine(FadeOutHint(4));
						break;
		}
	}
	
	
	public void UiStep5(int JustFinishDescription)
	{
		switch(JustFinishDescription)
		{
			case 1	:	Description5_1.transform.GetChild(0).gameObject.SetActive(true);
						StartCoroutine(FadeOutHint(5));
						
						// -------------Pop up menu in FadeOut(5) because we want to wait for a second before--------------------
						// HintPanel.SetActive(false);
						// complete.setactive(true);
						// instruction.setactive(false);
						//Menu.SetActive(true);
						//OVRManager.display.RecenterPose();
						//HeadMain.EnableCheckGesture = true;
						break;
		}
	}


	// Fading out all current descriptions and display next step
	IEnumerator FadeOutHint(int Step)
	{
		switch(Step)
		{
			case 1	: 	for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
							Description1_1.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_2.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_3.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Step1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_2.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_3.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							yield return null;
						}
						Step1.SetActive(false);
						Indicator.GetComponent<Text>().text = "2/5";
						Step2.SetActive(true);
						break;

			case 2	: 	for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
							Description2_1.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description2_2.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Step2.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description2_1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description2_2.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							yield return null;
						}
						Step2.SetActive(false);
						Indicator.GetComponent<Text>().text = "3/5";
						Step3.SetActive(true);
						break;

			case 3	:  	for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
							Description3_1.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Step3.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description3_1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							yield return null;
						}
						Step3.SetActive(false);
						Indicator.GetComponent<Text>().text = "4/5";
						Step4.SetActive(true);
						break;

			case 4	: 	for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
							Description4_1.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description4_2.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Step4.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description4_1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description4_2.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							yield return null;
						}
						Step4.SetActive(false);
						Indicator.GetComponent<Text>().text = "5/5";
						Step5.SetActive(true);
						break;

			case 5	: 	for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
							Description5_1.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Step5.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description5_1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							yield return null;
						}
						Step5.SetActive(false);
						Indicator.GetComponent<Text>().text = "DONE !!";
						yield return new WaitForSeconds(2.0f);
						
						// -----------Pop up menu when finished last assembly step--------------------
						HintPanel.SetActive(false);
						// complete.setactive(true);
						// instruction.setactive(false);
						Menu.SetActive(true);
						OVRManager.display.RecenterPose();
						HeadMain.EnableCheckGestureUI = true;
						break;
		}
	}


	public void DisplayMessage(string ObjName)
	{
		switch(ObjName)
		{
			case "RetainingClip"	:	Message.GetComponent<Text>().text = "retaining clip";	break;
			case "StopScrew"		:	Message.GetComponent<Text>().text = "stop screw";		break;
			case "Tray"				:	Message.GetComponent<Text>().text = "tray";				break;
			case "DelayElement"		:	Message.GetComponent<Text>().text = "delay element";	break;
			case "ArmingWire"		:	Message.GetComponent<Text>().text = "arming wire";		break;
			case "ScrewDriver"		:	Message.GetComponent<Text>().text = "screw driver";		break;
			default 				:	Message.GetComponent<Text>().text = ObjName;			break;
		}
	}


	/*public void ToggleInterface(bool B)
	{
		if(B){
			Message.SetActive(true);
			HintPanel.SetActive (true);
		}else{
			Message.SetActive(false);
			HintPanel.SetActive (false);
		}
	}*/
}
