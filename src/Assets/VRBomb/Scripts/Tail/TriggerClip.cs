using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean.
// As this result, TailAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP2-TailAssembly"
public class TriggerClip : MonoBehaviour 
{
	[HideInInspector]
	public bool ClipConnected;

	
	void Start () 
	{
		ClipConnected = false;
	}


	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.name=="ColliderOfClip"){
			Debug.Log ("TriggerClip.cs_TailAssembly.Step2.ClipConnected");
			ClipConnected = true;
		}
	}
}
