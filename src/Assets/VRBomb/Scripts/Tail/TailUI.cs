using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TailUI : MonoBehaviour 
{
	// Other UI
	public GameObject Message;
	public GameObject Menu;
	public GameObject Indicator;
	public GameObject HintPanel;

	
	// Direct reference to UI-Step1
	public GameObject Step1;
	public GameObject Description1_1;
	public GameObject Description1_2;
	public GameObject Description1_3;
	public GameObject Description1_4;
	
	// Direct reference to UI-Step2
	public GameObject Step2;
	public GameObject Description2_1;
	
	// Direct reference to UI-Step3
	public GameObject Step3;
	public GameObject Description3_1;
	public GameObject Description3_2;


	void Start()
	{
		StartCoroutine(FadeOutMessage());	// Fade out welcome message
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
						Description1_4.SetActive(true);
						break;
			case 4 : 	Description1_4.transform.GetChild(0).gameObject.SetActive(true);
						StartCoroutine(FadeOutHint(1));
						break;
		}
	}


	public void UiStep2(int JustFinishDescription)
	{
		switch(JustFinishDescription)
		{
			case 1	:	Description2_1.transform.GetChild(0).gameObject.SetActive(true);
						StartCoroutine(FadeOutHint(2));
						break;
		}
	}


	public void UiStep3(int JustFinishDescription)
	{
		switch(JustFinishDescription)
		{
			case 1	:	Description3_1.transform.GetChild(0).gameObject.SetActive(true);
						Description3_2.SetActive(true);
						break;
			case 2	: 	Description3_2.transform.GetChild(0).gameObject.SetActive(true);
						StartCoroutine(FadeOutHint(3));
						break;		
		}
	}


	IEnumerator FadeOutHint(int Step)
	{
		switch(Step)
		{
			case 1	: 	for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
							Description1_1.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_2.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_3.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_4.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Step1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_2.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_3.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description1_4.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							yield return null;
						}
						Step1.SetActive(false);
						Indicator.GetComponent<Text>().text = "2/3";
						Step2.SetActive(true);
						break;

			case 2 : 	for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
							Description2_1.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Step2.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description2_1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							yield return null;
						}
						Step2.SetActive(false);
						Indicator.GetComponent<Text>().text = "3/3";
						Step3.SetActive(true);
						break;

			case 3 : 	for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
							Description3_1.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description3_2.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Step3.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description3_1.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							Description3_2.GetComponent<Text>().CrossFadeAlpha(0, 1.0f, false);
							yield return null;
						}
						Step3.SetActive(false);
						Indicator.GetComponent<Text>().text = "DONE !!";
						yield return new WaitForSeconds(2.0f);
						
						// -----------Pop up menu when finished last assembly step--------------------
						HintPanel.SetActive(false);
						// complete.setactive(true);
						// instruction.setactive(false);
						Menu.SetActive(true);
						OVRManager.display.RecenterPose();
						TailMain.EnableCheckGestureUI = true;
						break;
		}
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


	public void DisplayMessage(string ObjName)
	{
		switch(ObjName)
		{
			case "SafetyElement"	:	Message.GetComponent<Text>().text = "safety element";	break;
			case "StopScrew"		:	Message.GetComponent<Text>().text = "stop screw";		break;
			case "ArmingPin"		:	Message.GetComponent<Text>().text = "arming pin";		break;
			case "Clip"				:	Message.GetComponent<Text>().text = "clip";				break;
			case "ScrewDriver"		:	Message.GetComponent<Text>().text = "screw driver";		break;
			default 				:	Message.GetComponent<Text>().text = ObjName;			break;
		}
	}
}
