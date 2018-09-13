using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean.
// As this result, TailAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP1-TailAssembly"
public class TriggerSafetyElement : MonoBehaviour 
{
	[HideInInspector]
	public bool SafetyElementConnected;

	
	void Start () 
	{
		SafetyElementConnected = false;
	}
	

	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.name=="ColliderOfSafetyElement"){
			Debug.Log ("TriggerSafetyElement.cs_TailAssembly.Step1.SafetyElementConnected");
			SafetyElementConnected = true;
		}
	}
}
