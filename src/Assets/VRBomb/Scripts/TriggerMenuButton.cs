using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TriggerMenuButton : MonoBehaviour 
{
	public static bool HandAtMenuButton;
	Image Button;
	Text T;


	void OnEnable () 
	{
		Button = this.gameObject.GetComponent<Image>();
		T = this.gameObject.transform.GetChild(0).GetComponent<Text>();
		HandAtMenuButton = false;
	}


	void OnTriggerStay(Collider other)
	{
		if(HeadMain.IsHand(other.gameObject.name)){
			HandAtMenuButton = true;
			Button.fillCenter = true;
			T.color = new Color32(0, 255, 255, 255);	// Cyan color
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		HandAtMenuButton = false;
		Button.fillCenter = false;
		T.color = new Color32(0, 0, 0, 255);	// Black color
	}


	public void Tap()
	{
		this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
		Button.color = new Color32(0, 255, 255, 255);
		Button.fillCenter = true;
		T.color = new Color32(0, 255, 255, 255);
	}
}
