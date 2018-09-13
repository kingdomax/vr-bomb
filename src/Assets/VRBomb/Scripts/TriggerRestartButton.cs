using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class TriggerRestartButton : MonoBehaviour 
{
	Image Button;
	Text T;
	Color32 HoverColor;
	public static bool HandAtRestartButton;


	void OnEnable () 
	{
		if(Application.loadedLevelName.Equals("headAssembly")){
			HoverColor = new Color32(0, 255, 255, 255);	// Cyan color
		}else{
			HoverColor = new Color32(209, 255, 0, 255); // Yellow color
		}

		HandAtRestartButton = false;
		Button = this.gameObject.GetComponent<Image>();
		T = this.gameObject.transform.GetChild(0).GetComponent<Text>();
	}
	
	
	void OnTriggerStay(Collider other)
	{
		if(HeadMain.IsHand(other.gameObject.name) || TailMain.IsHand(other.gameObject.name)){
			HandAtRestartButton = true;
			Button.fillCenter = true;
			T.color = HoverColor;
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		HandAtRestartButton = false;
		Button.fillCenter = false;
		T.color = new Color32(0, 0, 0, 255);	// Black color
	}


	public void Tap()
	{
		this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
		Button.color = HoverColor;
		Button.fillCenter = true;
		T.color = HoverColor;
	}
}
