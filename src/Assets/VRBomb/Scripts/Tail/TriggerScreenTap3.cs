using UnityEngine;
using System.Collections;


// If the trigger have happened, TailMain.cs will check the exist of screen tap gesture (CheckScreenTap()), and set new value of boolean.
// As this result, TailAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP3-TailAssembly"
public class TriggerScreenTap3 : MonoBehaviour 
{
	public static bool AlreadyScreenTapAtArmingPinClone1;

	
	void Start () 
	{
		AlreadyScreenTapAtArmingPinClone1 = false;
	}

	
	void OnTriggerStay(Collider other)
	{
		if(TailMain.IsHand(other.gameObject.name)){
			AlreadyScreenTapAtArmingPinClone1 = true;
		}
	}
	

}
