using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP3-HeadAssembly"
public class TriggerArmingWire : MonoBehaviour 
{
	[HideInInspector]
	public bool ArmingWireConnected;

	
	void Start () 
	{
		ArmingWireConnected = false;
	}


	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.name=="ColliderOfArmingWire"){
			Debug.Log ("TriggerArmingWire.cs_HeadAssembly.Step3.ArmingWireConnected");
			ArmingWireConnected = true;
		}
	}
}
