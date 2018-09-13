using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP1-HeadAssembly", "STEP1-TailAssembly"
public class TriggerStopScrew : MonoBehaviour 
{
	[HideInInspector]
	public bool StopScrewConnected;
	

	void Start () 
	{
		StopScrewConnected = false;
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name=="StopScrew")
		{
			if(Application.loadedLevelName.Equals("headAssembly")){
				Debug.Log ("TriggerStopScrew.cs_HeadAssembly.Step1.StopScrewConnected");
			}else if(Application.loadedLevelName.Equals("bodyAssembly")){
				Debug.Log ("TriggerStopScrew.cs_BodyAssembly.StepX.StopScrewConnected");
			}else{
				Debug.Log ("TriggerStopScrew.cs_TailAssembly.Step1.StopScrewConnected");
			}
			StopScrewConnected = true;
		}
	}
}
