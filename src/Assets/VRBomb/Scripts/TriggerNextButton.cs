using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TriggerNextButton : MonoBehaviour 
{
	public GameObject Message;

	Image Button;
	Text T;
	Color32 HoverColor;
	string Text;
	public static bool HandAtNextButton;
	
	
	void OnEnable () 
	{
		if(Application.loadedLevelName.Equals("headAssembly")){
			HoverColor = new Color32(0, 255, 255, 255);	// Cyan color
			Text = "go to tail assembly";
		}else{
			HoverColor = new Color32(209, 255, 0, 255); // Yellow color
			Text = "go to head assembly";
		}

		HandAtNextButton = false;
		Button = this.gameObject.GetComponent<Image>();
		T = this.gameObject.transform.GetChild(0).GetComponent<Text>();
	}
	
	
	void OnTriggerStay(Collider other)
	{
		if(HeadMain.IsHand(other.gameObject.name) || TailMain.IsHand(other.gameObject.name)){
			HandAtNextButton = true;
			Button.fillCenter = true;
			T.color = HoverColor;
			Message.GetComponent<Text>().text = Text;
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		HandAtNextButton = false;
		Button.fillCenter = false;
		T.color = new Color32(0, 0, 0, 255);	// Black color
		Message.GetComponent<Text>().text = "";
	}


	public void Tap()
	{
		this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
		Button.color = HoverColor;
		Button.fillCenter = true;
		T.color = HoverColor;
		Message.GetComponent<Text>().text = "";
	}
}
