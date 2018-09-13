using UnityEngine;
using System.Collections;
using Leap.Interact;


// 1.This class validates the assembly, if it's correct, performs some actions.
// 2.This class include methods which manipulate any objects in HeadAssembly scene.
public class HeadAssembly : MonoBehaviour 
{
	[HideInInspector]
	public int CurrentStep;
	[HideInInspector]
	public GameObject CurrentMainObject;
	
	// Other object
	public GameObject HeadUI;
	
	// Direct reference of accessories
	public GameObject DelayElement;
	public GameObject ScrewDriver;
	public GameObject StopScrew;
	public GameObject ArmingWire;
	public GameObject Tray;
	public GameObject RetainingClip;
	
	// Direct reference of HEAD-Step1&2 objects
	public GameObject NozeFuse;
	public GameObject TriggerStopScrew;
	public GameObject StopScrewClone1;
	public GameObject StopScrewClone1Sprite;
	public GameObject StopScrewClone2;
	public GameObject ScrewDriverClone1;
	public GameObject Lockpin;	
	public GameObject TriggerDelayElement;
	
	// Direct reference of HEAD-Step3 objects
	public GameObject NozeFuseStopScrewDelayElement;
	public GameObject TriggerArmingWire;
	
	// Direct reference of HEAD-Step4&5 objects
	public GameObject NozeFuseStopScrewDelayElementWire;
	public GameObject TriggerRetainingClip;
	public GameObject RetainingClipClone1;
	public GameObject RetainingClipClone2;
	public GameObject TriggerTray;
	
	// Direct reference of HEAD-Result
	public GameObject NozeFuseInTray;
	public GameObject Firework;


	void Start()
	{
		// REAL
		CurrentStep = 1;
		CurrentMainObject = NozeFuse;
		TriggerStopScrew.SetActive(true);	// Active 1st triggered in HeadAssembly.scene
		IncreaseMagneticDistance(StopScrew);

		/*// DEBUG
		CurrentStep = 5;
		CurrentMainObject = NozeFuseStopScrewDelayElementWire;
		TriggerTray.SetActive(true);*/
	}
	
	
	void Update()
	{
		switch(CurrentStep){
			case 1	:	Step1();	break;
			case 2	:	Step2();	break;
			case 3	:	Step3();	break;
			case 4	:	Step4();	break;
			case 5	:	Step5();	break;
		}
	}


	void Step1()
	{
		// If trigger sub1, perform something........
		if(TriggerStopScrew.GetComponent<TriggerStopScrew>().StopScrewConnected){

			// First, set boolean to false, for preventing these statements run again.
			TriggerStopScrew.GetComponent<TriggerStopScrew>().StopScrewConnected = false;	

			StopScrew.SetActive(false);
			TriggerStopScrew.SetActive(false);
			StopScrewClone1.SetActive(true);
			HeadUI.GetComponent<HeadUI>().UiStep1(1);
			IncreaseMagneticDistance(ScrewDriver);
		}

		// If trigger sub2, perform something........
		if(StopScrewClone1Sprite.GetComponent<TriggerScrewDriver>().ScrewDriverConnected){

			StopScrewClone1Sprite.GetComponent<TriggerScrewDriver>().ScrewDriverConnected = false;

			StopScrewClone1Sprite.transform.GetChild(0).gameObject.SetActive(false);	// deactive highlight particle
			ScrewDriver.SetActive(false);
			ScrewDriverClone1.SetActive(true);
			/* Change screen tap gesture to delay and only tap
			 * HeadMain.EnableCheckGesture = true; */
			StartCoroutine(EnableIsTrigger(ScrewDriverClone1));
			HeadUI.GetComponent<HeadUI>().UiStep1(2);	
		}

		// If trigger sub3, perform something........
		if(TriggerScreenTap.AlreadyScreenTapAtScrewDriverClone1){

			TriggerScreenTap.AlreadyScreenTapAtScrewDriverClone1 = false;

			//HeadMain.EnableCheckGesture = false;
			ScrewDriverClone1.GetComponent<BoxCollider>().isTrigger = false;
			ScrewDriverClone1.transform.GetChild(0).gameObject.SetActive(false);	
			StartCoroutine(RotateObject(StopScrewClone1, Vector3.right, 3.0f, 3.0f));
			Debug.Log ("HeadAssembly.cs_Invoke.BruteForceStep1_3()");
			/* Put next 9 statements in BruteForceStep1_3(), because we want those to run after rotate coroutine
			ScrewDriverClone1.SetActive(false);
			StopScrewClone1.SetActive(false);
			StopScrewClone2.SetActive(true);
			SetTransform(CurrentMainObject, new Vector3(6.3f, 2.7f, 4.5f), new Vector3(90f, 180f, 270f));
			HeadUI.GetComponent<HeadUI>().UiStep1(3);
			IncreaseMagneticDistance(DelayElement);
			Lockpin.GetComponent<BoxCollider>().enabled = true;
			Lockpin.transform.GetChild(0).gameObject.SetActive(true);
			CurrentStep++;
			 */

			// We don't setup new main object in the transition of step1 - step2.
			Debug.Log ("HeadAssembly.cs_Finish.Step1");
		}
	}


	void Step2()
	{
		// If trigger sub1, perform something........
		if(Lockpin.GetComponent<TriggerHover>().HoverAtLockpin){
			Lockpin.transform.GetChild(0).gameObject.SetActive(false);
			TriggerDelayElement.SetActive(true);
			HeadUI.GetComponent<HeadUI>().UiStep2(1);
		}else{
			Lockpin.transform.GetChild(0).gameObject.SetActive(true);
			TriggerDelayElement.SetActive(false);
		}

		// If trigger sub2, perform something........
		if(TriggerDelayElement.GetComponent<TriggerDelayElement>().DelayElementConnected){

			// First, set boolean to false, for preventing these statements run again.
			Lockpin.GetComponent<TriggerHover>().HoverAtLockpin = false;
			TriggerDelayElement.GetComponent<TriggerDelayElement>().DelayElementConnected = false;

			Lockpin.GetComponent<BoxCollider>().enabled = false;
			Lockpin.transform.GetChild(0).gameObject.SetActive(false);
			TriggerDelayElement.SetActive(false);
			DelayElement.SetActive (false);

			// Setup next step(transition of step2 - step3)
			SetNewMainObject(NozeFuseStopScrewDelayElement);
			TriggerArmingWire.SetActive(true);
			HeadUI.GetComponent<HeadUI>().UiStep2(2);
			CurrentStep++;
			Debug.Log ("HeadAssembly.cs_Finish.Step2");
			//SetTransform(CurrentMainObject, new Vector3(6.3f, 2.7f, 4.5f), new Vector3(90f, 180f, 270f));
			ArmingWire.transform.GetChild (1).gameObject.SetActive(true);
			IncreaseMagneticDistance(ArmingWire);
		}
	}


	void Step3()
	{
		if(TriggerArmingWire.GetComponent<TriggerArmingWire>().ArmingWireConnected){

			// First, set boolean to false, for preventing these statements run again.
			TriggerArmingWire.GetComponent<TriggerArmingWire>().ArmingWireConnected = false;

			TriggerArmingWire.SetActive(false);
			ArmingWire.SetActive(false);

			// Setup next step(transition of step3 - step4)
			SetNewMainObject (NozeFuseStopScrewDelayElementWire);
			TriggerRetainingClip.SetActive(true);
			HeadUI.GetComponent<HeadUI>().UiStep3(1);
			CurrentStep++;
			Debug.Log ("HeadAssembly.cs_Finish.Step3");
			//SetTransform(CurrentMainObject, new Vector3(6.3f, 2.7f, 4.5f), new Vector3(90f, 180f, 270f));
			IncreaseMagneticDistance(RetainingClip);
		}
	}


	void Step4()
	{
		// If trigger sub1, perform something........
		if(TriggerRetainingClip.GetComponent<TriggerRetainingClip>().RetainingClipConnected){

			// First, set boolean to false, for preventing these statements run again.
			TriggerRetainingClip.GetComponent<TriggerRetainingClip>().RetainingClipConnected = false;

			TriggerRetainingClip.SetActive(false);
			RetainingClip.SetActive(false);
			RetainingClipClone1.SetActive(true);
			/* Change screen tap gesture to delay and only tap
			 * HeadMain.EnableCheckGesture = true; */
			StartCoroutine(EnableIsTrigger(RetainingClipClone1));
			HeadUI.GetComponent<HeadUI>().UiStep4(1);
		}

		// If trigger sub2, perform something........
		if(TriggerScreenTap2.AlreadyScreenTapAtRetainingClipClone1){

			TriggerScreenTap2.AlreadyScreenTapAtRetainingClipClone1 = false;

			//HeadMain.EnableCheckGesture = false;
			RetainingClipClone1.GetComponent<BoxCollider>().isTrigger = false;
			RetainingClipClone1.transform.GetChild(0).gameObject.SetActive(false);
			StartCoroutine(LerpObject(RetainingClipClone1, 
			                          new Vector3(RetainingClipClone1.transform.localPosition.x, RetainingClipClone1.transform.localPosition.y, 1.8f), 
			                          1.25f));	
			Debug.Log ("HeadAssembly.cs_Invoke.BruteForceStep4_2()");
			/*Put next 5 statements in BruteForceStep4_2(), because we want those to run after lerp coroutine
			RetainingClipClone1.SetActive(false);
			RetainingClipClone2.SetActive(true);
			TriggerTray.SetActive(true);
			HeadUI.GetComponent<HeadUI>().UiStep4(2);
			SetTransform(CurrentMainObject, new Vector3(6.3f, 2.7f, 4.5f), new Vector3(90f, 180f, 270f));
			 */

			// We don't setup new main object in the transition of step4 - step5.
			CurrentStep++;
			Debug.Log ("HeadAssembly.cs_Finish.Step4");
		}
	}


	void Step5()
	{
		// If trigger sub1, perform something........
		if(TriggerTray.GetComponent<TriggerTray>().TrayCapped){

			// First, set boolean to false, for preventing these statements run again.
			TriggerTray.GetComponent<TriggerTray>().TrayCapped = false;

			TriggerTray.SetActive(false);
			Tray.SetActive(false);

			SetNewMainObject(NozeFuseInTray);
			CurrentMainObject.transform.localPosition 		=	new Vector3(CurrentMainObject.transform.localPosition.x-1.5f,
			                                                        		CurrentMainObject.transform.localPosition.y,
			                                                        		CurrentMainObject.transform.localPosition.z+14f);
			CurrentMainObject.transform.localEulerAngles 	= 	new Vector3(CurrentMainObject.transform.localEulerAngles.x,
			                                                           		CurrentMainObject.transform.localEulerAngles.y-90f,
			                                                           		CurrentMainObject.transform.localEulerAngles.z);
			Firework.SetActive(true);
			HeadUI.GetComponent<HeadUI>().UiStep5(1);
			CurrentMainObject = null;
			CurrentStep = 0;
			Debug.Log ("HeadAssembly.cs_Finish.AllStep");
		}
	}


	void SetNewMainObject(GameObject NewObject)
	{
		Vector3 PositionMainObject = CurrentMainObject.transform.localPosition;
		Vector3 RotationMainObject = CurrentMainObject.transform.localEulerAngles;
		CurrentMainObject.SetActive(false);
		CurrentMainObject = NewObject;
		CurrentMainObject.transform.localPosition = PositionMainObject;
		CurrentMainObject.transform.localEulerAngles = RotationMainObject;
		CurrentMainObject.SetActive(true);
	}


	public void MoveObjectTo(Vector3 NewPosition)
	{
		CurrentMainObject.transform.position = Vector3.MoveTowards(CurrentMainObject.transform.position, NewPosition, 1.5f);
	}


	public Vector3 GetObjectPosition()
	{
		return CurrentMainObject.transform.position;
	}


	// For rotate main object
	public IEnumerator RotateMainObject()
	{
		while(true){
			CurrentMainObject.transform.Rotate(Vector3.up, 1.7f);	
			yield return null;
		}
	}


	// For rotate "any" object 
	IEnumerator RotateObject(GameObject G, Vector3 Direction, float Speed, float Duration)
	{
		for(float i=0.0f; i<1.0f; i+=Time.deltaTime/Duration){
			G.transform.Rotate(Direction, Speed);	
			yield return null;
		}
		BruteForceStep1_3();
	}


	void BruteForceStep1_3()
	{
		ScrewDriverClone1.SetActive(false);
		StopScrewClone1.SetActive(false);
		StopScrewClone2.SetActive(true);
		SetTransform(CurrentMainObject, new Vector3(6.3f, 2.7f, 4.5f), new Vector3(90f, 180f, 270f));
		HeadUI.GetComponent<HeadUI>().UiStep1(3);
		IncreaseMagneticDistance(DelayElement);
		Lockpin.GetComponent<BoxCollider>().enabled = true;
		Lockpin.transform.GetChild(0).gameObject.SetActive(true);
		CurrentStep++;
	}


	IEnumerator LerpObject(GameObject G, Vector3 Destination, float Fraction)
	{
		while(Vector3.Distance (G.transform.localPosition, Destination) > 0.03f){
			G.transform.localPosition = Vector3.Lerp(G.transform.localPosition, Destination, Fraction*Time.deltaTime);
			yield return null;
		}
		BruteForceStep4_2();
	}

	
	void BruteForceStep4_2()
	{
		RetainingClipClone1.SetActive(false);
		RetainingClipClone2.SetActive(true);
		TriggerTray.SetActive(true);
		HeadUI.GetComponent<HeadUI>().UiStep4(2);
		//SetTransform(CurrentMainObject, new Vector3(6.3f, 2.7f, 4.8f), new Vector3(90f, 180f, 270f));
	}


	IEnumerator EnableIsTrigger(GameObject G)
	{
		yield return new WaitForSeconds(2.5f);
		G.GetComponent<BoxCollider>().isTrigger = true;
	}


	void SetTransform(GameObject G, Vector3 LocalPosition, Vector3 LocalEulerAngle)
	{
		G.transform.localPosition = LocalPosition;
		G.transform.localEulerAngles = LocalEulerAngle;
	}


	/*public void SetTransformAccessories(GameObject G, Transform T)
	{
		G.transform.localPosition = T.localPosition;
		G.transform.localEulerAngles = T.localEulerAngles;
	}*/


	public void ResetAcessories()
	{
		SetTransform (StopScrew, new Vector3(-0.001f, 0.3f, 0.287f), new Vector3(270f, 0f, 0f));
		SetTransform (ScrewDriver, new Vector3(-4f, 0.2f, 1.25f), new Vector3(90f, 90f, 0f));
		SetTransform (DelayElement, new Vector3(1.6f, 0.4f, -0.28f), new Vector3(270f, 0f, 0f));
		SetTransform (RetainingClip, new Vector3(-1.47f, 0.24f, -0.35f), new Vector3(0f, 0f, 0f));
		SetTransform (ArmingWire, new Vector3(0.43f, 0.23f, 1.3f), new Vector3(0f, 270f, 180f));
		SetTransform (Tray, new Vector3(4.11f, 0.82f, -2.9f), new Vector3(0f, 0f, 0f));
	}


	void IncreaseMagneticDistance(GameObject G)
	{
		G.GetComponent<LeapInteraction>().MagneticDistance = 50f;
	}


	/*public void ToggleLightEffect(bool B)
	{
		if(B){
			TriggerTray.transform.GetChild(0).gameObject.SetActive(true);
		}else{
			TriggerTray.transform.GetChild(0).gameObject.SetActive(false);
		}
	}*/
}
