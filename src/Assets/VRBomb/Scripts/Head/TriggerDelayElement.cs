using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP2-HeadAssembly"
public class TriggerDelayElement : MonoBehaviour 
{
	[HideInInspector]
	public bool DelayElementConnected;


	void Start () 
	{
		DelayElementConnected = false;
	}

	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.name=="DelayElement"){
			Debug.Log ("TriggerDelayElement.cs_HeadAssembly.Step2.DelayElementConnected");
			DelayElementConnected = true;
		}
	}
}
