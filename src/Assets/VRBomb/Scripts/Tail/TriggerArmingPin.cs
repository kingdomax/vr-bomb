using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean.
// As this result, TailAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP3-TailAssembly"
public class TriggerArmingPin : MonoBehaviour 
{
	[HideInInspector]
	public bool ArmingPinConnected;

	
	void Start () 
	{
		ArmingPinConnected = false;
	}

	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.name=="ColliderOfArmingPin"){
			Debug.Log ("TriggerArmingPin.cs_TailAssembly.Step3.ArmingPinConnected");
			ArmingPinConnected = true;
		}
	}
}
