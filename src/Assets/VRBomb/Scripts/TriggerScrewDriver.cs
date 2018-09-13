using UnityEngine;
using System.Collections;




// If the trigger have happened, this class will set new value of boolean.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP1-HeadAssembly", "STEP1-TailAssembly"
public class TriggerScrewDriver : MonoBehaviour 
{
	[HideInInspector]
	public bool ScrewDriverConnected;


	void Start () 
	{
		ScrewDriverConnected = false;
	}
	

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name=="ColliderOfScrewDriver")
		{
			if(Application.loadedLevelName.Equals("headAssembly")){
				Debug.Log ("TriggerScrewDriver.cs_HeadAssembly.Step1.ScrewDriverConnected");
			}else if(Application.loadedLevelName.Equals("bodyAssembly")){
				Debug.Log ("TriggerScrewDriver.cs_BodyAssembly.StepX.ScrewDriverConnected");
			}else{
				Debug.Log ("TriggerScrewDriver.cs_TailAssembly.Step1.ScrewDriverConnected");
			}
			ScrewDriverConnected = true;
		}
	}
}
