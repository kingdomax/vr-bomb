using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TriggerTailButton : MonoBehaviour 
{

	public GameObject Message;
	
	Image Button;
	Text T;
	Color32 HoverColor;
	string Text;
	public static bool HandAtTailButton;
	
	
	void OnEnable () 
	{
		HoverColor = new Color32(39, 176, 126, 255); 	// Green color
		Text = "go to tail assembly";
		HandAtTailButton = false;
		Button = this.gameObject.GetComponent<Image>();
		T = this.gameObject.transform.GetChild(0).GetComponent<Text>();
	}
	
	
	void OnTriggerStay(Collider other)
	{
		if(TutorialController.IsHand(other.gameObject.name)){
			HandAtTailButton = true;
			Button.fillCenter = true;
			T.color = HoverColor;
			Message.GetComponent<Text>().text = Text;
		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		HandAtTailButton = false;
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
