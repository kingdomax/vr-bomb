using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP5-HeadAssembly"
public class TriggerTray : MonoBehaviour 
{
	[HideInInspector]
	public bool TrayCapped;


	void Start () 
	{
		TrayCapped = false;
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name=="ColliderOfTray"){
			Debug.Log ("TriggerTray.cs_HeadAssembly.Step5.TrayCapped");
			TrayCapped = true;
		}
	}
}
