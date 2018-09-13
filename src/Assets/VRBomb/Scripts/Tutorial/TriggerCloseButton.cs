using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TriggerCloseButton : MonoBehaviour 
{
	Image Button;
	Text T;
	Color32 HoverColor;
	string Text;
	public static bool HandAtCloseButton;

	
	void Start () 
	{
		HoverColor = new Color32(39, 176, 126, 255); 	// Green color
		HandAtCloseButton = false;
		Button = this.gameObject.GetComponent<Image>();
		T = this.gameObject.transform.GetChild(0).GetComponent<Text>();
	}


	void OnTriggerStay(Collider other)
	{
		if(TutorialController.IsHand(other.gameObject.name)){
			HandAtCloseButton = true;
			Button.fillCenter = true;
			T.color = HoverColor;
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		HandAtCloseButton = false;
		Button.fillCenter = false;
		T.color = new Color32(0, 0, 0, 255);	// Black color
	}
}
