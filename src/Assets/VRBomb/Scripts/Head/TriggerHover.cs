using UnityEngine;
using System.Collections;


// If the trigger have happened, this class will set new value of boolean, change material color and change position of lockpin.
// As this result, HeadAssembly.cs automatically knows the changes and performs some action though. 
// Used in "STEP2-HeadAssembly"
public class TriggerHover : MonoBehaviour 
{

	[HideInInspector]
	public bool HoverAtLockpin;
	public Material HoverMat;

	Material OriginalMaterial;

	
	void Start () 
	{
		HoverAtLockpin = false;
		OriginalMaterial = this.gameObject.renderer.material;
	}


	void OnTriggerStay(Collider other)
	{
		if(HeadMain.IsHand(other.gameObject.name)){
			Debug.Log ("TriggerHover.cs_HeadAssembly.Step2.HoverAtLockpin");
			HoverAtLockpin = true;
			this.gameObject.renderer.material = HoverMat;
			this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y, 0.25f);
		}
	}


	void OnTriggerExit(Collider other)
	{
		HoverAtLockpin = false;
		this.gameObject.renderer.material = OriginalMaterial;
		this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y, 0.0f);
	}


}
