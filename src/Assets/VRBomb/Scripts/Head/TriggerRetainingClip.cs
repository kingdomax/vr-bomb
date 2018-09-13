using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP4-HeadAssembly"
public class TriggerRetainingClip : MonoBehaviour 
{
	[HideInInspector]
	public bool RetainingClipConnected;


	void Start () 
	{
		RetainingClipConnected = false;
	}


	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.name=="RetainingClip"){
			Debug.Log ("TriggerRetainingClipe.cs_HeadAssembly.Step4.RetainingClipConnected");
			RetainingClipConnected = true;
		}
	}
}
