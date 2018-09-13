using UnityEngine;
using System.Collections;
using Leap.Interact;


// 1.This class validates the assembly, if it's correct, performs some actions.
// 2.This class include methods which manipulate any objects in TailAssembly scene.
public class TailAssembly : MonoBehaviour 
{
	//[HideInInspector]
	public int CurrentStep;
	[HideInInspector]
	public GameObject CurrentMainObject;

	// Direct reference other script
	public GameObject TailUI;

	// Direct reference of accessories
	public GameObject SafetyElement;
	public GameObject ScrewDriver;
	public GameObject StopScrew;
	public GameObject ArmingPin;
	public GameObject Clip;

	// Direct reference a Tail scene's main object
	public GameObject TailFuse;

	// Direct reference Step1's component
	public GameObject TriggerSafetyElement;
	public GameObject TriggerStopScrew;
	public GameObject StopScrewClone1;
	public GameObject StopScrewClone2;
	public GameObject SafetyElementClone1;
	public GameObject SafetyElementClone2;
	public GameObject ScrewDriverClone1;

	// Direct reference Step2's component
	public GameObject ClipClone1;
	public GameObject ArmingPinSlot;

	// Direct reference Step3's component
	public GameObject ArmingPinClone1;
	public GameObject ArmingPinClone2;
	public GameObject Firework;

	
	void Start () 
	{
		CurrentStep = 1;
		CurrentMainObject = TailFuse;
		TriggerSafetyElement.SetActive(true);	// Active 1st triggered in TailAssembly.scene
		SafetyElement.transform.GetChild(1).gameObject.SetActive(true);
		IncreaseMagneticDistance(SafetyElement);

		/*// DEBUG
		CurrentStep = 5;
		CurrentMainObject = TailFuse;
		TriggerTray.SetActive(true);*/
	}


	// Update is called once per frame
	void Update () 
	{
		switch(CurrentStep){
			case 1	:	Step1();	break;
			case 2	:	Step2();	break;
			case 3	:	Step3();	break;
		}
	}


	void Step1()
	{
		// If trigger sub1, perform something........
		if(TriggerSafetyElement.GetComponent<TriggerSafetyElement>().SafetyElementConnected){

			// First, set boolean to false, for preventing these statements run again.
			TriggerSafetyElement.GetComponent<TriggerSafetyElement>().SafetyElementConnected = false;

			// Then, perform specific action
			SafetyElement.SetActive(false);
			TriggerSafetyElement.SetActive(false);
			SafetyElementClone1.SetActive(true);
			TriggerStopScrew.SetActive (true);
			IncreaseMagneticDistance(StopScrew);
			Debug.Log ("TailAssembly.cs_Finish.Step1.1");
			TailUI.GetComponent<TailUI>().UiStep1(1);
		}

		// If trigger sub2, perform something........
		if(TriggerStopScrew.GetComponent<TriggerStopScrew>().StopScrewConnected){

			TriggerStopScrew.GetComponent<TriggerStopScrew>().StopScrewConnected = false;

			StopScrew.SetActive(false);
			TriggerStopScrew.SetActive(false);
			StopScrewClone1.SetActive(true);
			IncreaseMagneticDistance(ScrewDriver);
			Debug.Log ("TailAssembly.cs_Finish.Step1.2");
			TailUI.GetComponent<TailUI>().UiStep1(2);
		}

		// If trigger sub3, perform something........
		if(StopScrewClone1.GetComponent<TriggerScrewDriver>().ScrewDriverConnected){

			StopScrewClone1.GetComponent<TriggerScrewDriver>().ScrewDriverConnected = false;
			
			ScrewDriver.SetActive(false);
			StopScrewClone1.transform.GetChild(1).gameObject.SetActive(false);	// disable highlight object
			StopScrewClone1.GetComponent<BoxCollider>().isTrigger = false;
			ScrewDriverClone1.SetActive(true);
			StartCoroutine(EnableIsTrigger(ScrewDriverClone1));
			Debug.Log ("TailAssembly.cs_Finish.Step1.3");
			TailUI.GetComponent<TailUI>().UiStep1(3);
		}

		// If trigger sub4, perform something........
		if(TriggerScreenTap.AlreadyScreenTapAtScrewDriverClone1){

			TriggerScreenTap.AlreadyScreenTapAtScrewDriverClone1 = false;

			ScrewDriverClone1.GetComponent<BoxCollider>().isTrigger = false;
			ScrewDriverClone1.transform.GetChild(0).gameObject.SetActive(false);

			StartCoroutine(BruteForceStep1_4());
			Debug.Log ("TailAssembly.cs_Finish.Step1.4");
		}
	}


	void Step2()
	{
		if(SafetyElementClone2.GetComponent<TriggerClip>().ClipConnected){

			SafetyElementClone2.GetComponent<TriggerClip>().ClipConnected = false;

			Clip.SetActive(false);
			SafetyElementClone2.transform.GetChild(0).gameObject.SetActive(false);
			SafetyElementClone2.GetComponent<BoxCollider>().isTrigger = false;
			ClipClone1.SetActive(true);
			ArmingPinSlot.transform.GetChild(0).gameObject.SetActive(true);
			ArmingPinSlot.GetComponent<BoxCollider>().isTrigger = true;
			ArmingPin.transform.GetChild(1).gameObject.SetActive(true);
			IncreaseMagneticDistance(ArmingPin);

			CurrentStep++;
			Debug.Log ("TailAssembly.cs_Finish.Step2.1");
			TailUI.GetComponent<TailUI>().UiStep2(1);
		}
	}


	void Step3()
	{
		// If trigger sub1, perform something........
		if(ArmingPinSlot.GetComponent<TriggerArmingPin>().ArmingPinConnected){

			ArmingPinSlot.GetComponent<TriggerArmingPin>().ArmingPinConnected = false;

			ArmingPin.SetActive(false);
			ArmingPinSlot.transform.GetChild(0).gameObject.SetActive(false);
			ArmingPinSlot.GetComponent<BoxCollider>().isTrigger = false;
			ArmingPinClone1.SetActive(true);
			StartCoroutine(EnableIsTrigger(ArmingPinClone1));
			Debug.Log ("TailAssembly.cs_Finish.Step3.1");
			TailUI.GetComponent<TailUI>().UiStep3(1);
		}


		// If trigger sub2, perform something........
		if(TriggerScreenTap3.AlreadyScreenTapAtArmingPinClone1){

			TriggerScreenTap3.AlreadyScreenTapAtArmingPinClone1 = false;

			ArmingPinClone1.transform.GetChild(0).gameObject.SetActive(false);
			ArmingPinClone1.GetComponent<BoxCollider>().isTrigger = false;
			StartCoroutine(BruteForceStep3_2());

			CurrentMainObject = null;
			CurrentStep = 0;
			Debug.Log ("TailAssembly.cs_Finish.Step3.2");
			Debug.Log ("TailAssembly.cs_Finish.AllStep");
		}
	}


	IEnumerator BruteForceStep1_4()
	{
		Debug.Log ("TailAssembly.cs_Invoke.BruteForceStep1_4()");

		while(Vector3.Distance (StopScrewClone1.transform.localPosition, new Vector3(0.118f, 2.4f, -0.369f)) > 0.01f){
			StopScrewClone1.transform.Rotate(Vector3.up, 3.0f);	
			StopScrewClone1.transform.localPosition = Vector3.Lerp(StopScrewClone1.transform.localPosition, 
			                                            new Vector3(0.118f, 2.4f, -0.369f), 
			                                            1.0f*Time.deltaTime);
			yield return null;
		}

		StopScrewClone1.SetActive(false);
		StopScrewClone2.SetActive(true);
		SafetyElementClone1.SetActive(false);
		SafetyElementClone2.SetActive(true);
		Clip.transform.GetChild(1).gameObject.SetActive(true);
		IncreaseMagneticDistance(Clip);
		Clip.transform.GetChild(1).gameObject.SetActive(true);
		// Set up next step
		CurrentStep++;
		TailUI.GetComponent<TailUI>().UiStep1(4);
		yield return new WaitForSeconds(2.0f);
		SafetyElementClone2.transform.GetChild(0).gameObject.SetActive(true);
	}


	IEnumerator BruteForceStep3_2()
	{
		Debug.Log ("TailAssembly.cs_Invoke.BruteForceStep3_2()");
		while(Vector3.Distance (ArmingPinClone1.transform.localPosition, new Vector3(-0.062f, 3f, 1.42f)) > 0.01f){
			ArmingPinClone1.transform.localPosition = Vector3.Lerp(ArmingPinClone1.transform.localPosition, 
			                                            			new Vector3(-0.062f, 3f, 1.42f), 
			                                            			1.0f*Time.deltaTime);
			yield return null;
		}
		
		for(float i=0.0f; i<1.0f; i+=Time.deltaTime/2.5f){
			ArmingPinClone1.transform.Rotate(Vector3.left, 0.25f);	
			yield return null;
		}
		
		for(float i=0.0f; i<1.0f; i+=Time.deltaTime/2.5f){
			ArmingPinClone1.transform.Rotate(Vector3.right, 0.25f);	
			yield return null;
		}

		ArmingPinClone1.SetActive(false);
		ArmingPinClone2.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		Firework.SetActive(true);
		TailFuse.GetComponent<Rigidbody>().isKinematic = false;
		TailFuse.GetComponent<Rigidbody>().AddForce(transform.forward);
		TailUI.GetComponent<TailUI>().UiStep3(2);
	}


	IEnumerator EnableIsTrigger(GameObject G)
	{
		yield return new WaitForSeconds(2.5f);
		G.GetComponent<BoxCollider>().isTrigger = true;
	}


	void IncreaseMagneticDistance(GameObject G)
	{
		G.GetComponent<LeapInteraction>().MagneticDistance = 50f;
	}


	void SetTransform(GameObject G, Vector3 LocalPosition, Vector3 LocalEulerAngle)
	{
		G.transform.localPosition = LocalPosition;
		G.transform.localEulerAngles = LocalEulerAngle;
	}


	public void ResetAcessories()
	{
		SetTransform (SafetyElement, new Vector3(0.528f, 0.41f, 0.13f), new Vector3(0f, 0f, 0f));
		SetTransform (StopScrew, new Vector3(-0.48f, 0.3f, 0.18f), new Vector3(0f, 0f, 0f));
		SetTransform (ScrewDriver, new Vector3(-4f, 0.2f, 1.25f), new Vector3(90f, 90f, 0f));
		SetTransform (Clip, new Vector3(2.3f, 0.33f, 0.38f), new Vector3(90f, 180f, 0f));
		SetTransform (ArmingPin, new Vector3(-1.82f, 0.32f, 0.28f), new Vector3(90f, 270f, 0f));
	}


	public void MoveMainObject(Vector3 NewPosition)
	{
		CurrentMainObject.transform.localPosition = Vector3.MoveTowards(CurrentMainObject.transform.localPosition, NewPosition, 1.5f);
	}
	
	
	public Vector3 GetMainObjectPosition()
	{
		return CurrentMainObject.transform.localPosition;
	}


	// For rotate main object
	public IEnumerator RotateMainObject()
	{
		while(true){
			CurrentMainObject.transform.Rotate(Vector3.up, 1.7f);	
			yield return null;
		}
	}
}
