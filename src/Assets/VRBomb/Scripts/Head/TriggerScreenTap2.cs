using UnityEngine;
using System.Collections;


// If the trigger have happened, HeadMain.cs will check the exist of screen tap gesture (CheckScreenTap()), and set new value of boolean.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP4-HeadAssembly"
public class TriggerScreenTap2 : MonoBehaviour 
{
	//public static bool HandAtRetainingClipClone1;
	public static bool AlreadyScreenTapAtRetainingClipClone1;

	
	void Start () 
	{
		//HandAtRetainingClipClone1 = false;
		AlreadyScreenTapAtRetainingClipClone1 = false;
	}
	

	void OnTriggerStay(Collider other)
	{
		if(HeadMain.IsHand(other.gameObject.name)){
			AlreadyScreenTapAtRetainingClipClone1 = true;
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		AlreadyScreenTapAtRetainingClipClone1 = false;
	}


	/*void OnTriggerStay(Collider other)
	{
		if(HeadMain.IsHand(other.gameObject.name)){
			HandAtRetainingClipClone1 = true;
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		HandAtRetainingClipClone1 = false;
	}*/
}
