using UnityEngine;
using System.Collections;


// If the trigger have happened, HeadMain.cs will check the exist of screen tap gesture (CheckScreenTap()), and set new value of boolean.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP1-HeadAssembly", "STEP1-TailAssembly"
public class TriggerScreenTap : MonoBehaviour 
{	
	public static bool AlreadyScreenTapAtScrewDriverClone1;


	void Start()
	{
		AlreadyScreenTapAtScrewDriverClone1 = false;
	}


	void OnTriggerStay(Collider other)
	{
		if(Application.loadedLevelName.Equals("headAssembly"))
		{
			if(HeadMain.IsHand(other.gameObject.name)){
				AlreadyScreenTapAtScrewDriverClone1 =  true;
			}
		}
		else
		{
			if(TailMain.IsHand(other.gameObject.name)){
				AlreadyScreenTapAtScrewDriverClone1 =  true;
			}
		}
	}


	void OnTriggerExit(Collider other)
	{
		AlreadyScreenTapAtScrewDriverClone1 = false;
	}




}
