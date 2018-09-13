using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Interact;

public class MyHandCallback : MonoBehaviour 
{
	public GameObject HeadUI;
	public Material freeMaterial;
	public Material hoverMaterial;
	public Material heldMaterial;
	
	private GameObject lastHovered = null;


	void Start () 
	{
		UnityUtil.Scene.OnHoldingHoverOver += new Scene.HoldingNotification(OnHoldingHovers);
		UnityUtil.Scene.OnHoldingStarts += new Scene.HoldingNotification(OnHoldingStarts);
		UnityUtil.Scene.OnHoldingUpdates += new Scene.HoldingNotification(OnHoldingUpdates);
		UnityUtil.Scene.OnHoldingEnds += new Scene.HoldingNotification(OnHoldingEnds);
	}


	public void OnHoldingHovers(Holding holding) 
	{
		Body body = holding.Body;
		GameObject gameObject = null;
		if (body != null && body.IsValid())
		{
			gameObject = UnityUtil.BodyMapper.FirstOrDefault(x => x.Value.BodyId.ptr == body.BodyId.ptr).Key;
		}
		if (lastHovered != gameObject)
		{
			if (lastHovered)
			{
				if(lastHovered.name.Equals("ArmingWire")){
					lastHovered.transform.GetChild(1).gameObject.GetComponent<ParticleEmitter>().emit = false;
				}
				if(lastHovered.name.Equals("RetainingClip") || lastHovered.name.Equals("StopScrew") || lastHovered.name.Equals("DelayElement")){
					lastHovered.transform.GetChild(0).renderer.material = freeMaterial;
				}else{
					lastHovered.renderer.material = freeMaterial;
				}
				HeadUI.GetComponent<HeadUI>().DisplayMessage("");
			}
			if (gameObject)
			{
				if(gameObject.name.Equals("ArmingWire")){
					gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleEmitter>().emit = false;
				}
				if(gameObject.name.Equals("RetainingClip") || gameObject.name.Equals("StopScrew") || gameObject.name.Equals("DelayElement")){
					gameObject.transform.GetChild(0).renderer.material = hoverMaterial;
				}else{
					gameObject.renderer.material = hoverMaterial;
				}
				HeadUI.GetComponent<HeadUI>().DisplayMessage(gameObject.name);
			}
			lastHovered = gameObject;
		}
	}


	public void OnHoldingStarts(Holding holding) {
		Body body = holding.Body;
		GameObject gameObject = null;
		if (body != null && body.IsValid())
		{
			gameObject = UnityUtil.BodyMapper.FirstOrDefault(x => x.Value.BodyId.ptr == body.BodyId.ptr).Key;
		}
		if (gameObject)
		{
			if(gameObject.name.Equals("ArmingWire")){
				gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleEmitter>().emit = true;
			}
			if(gameObject.name.Equals("RetainingClip") || gameObject.name.Equals("StopScrew") || gameObject.name.Equals("DelayElement")){
				gameObject.transform.GetChild(0).renderer.material = heldMaterial;
			}else{
				gameObject.renderer.material = heldMaterial;
			}
			HeadUI.GetComponent<HeadUI>().DisplayMessage("");
		}
	}


	public void OnHoldingEnds(Holding holding) 
	{
		Body body = holding.Body;
		GameObject gameObject = null;
		if (body != null && body.IsValid())
		{
			gameObject = UnityUtil.BodyMapper.FirstOrDefault(x => x.Value.BodyId.ptr == body.BodyId.ptr).Key;
		}
		if (gameObject)
		{
			if(gameObject.name.Equals("ArmingWire")){
				gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleEmitter>().emit = false;
			}
			if(gameObject.name.Equals("RetainingClip") || gameObject.name.Equals("StopScrew") || gameObject.name.Equals("DelayElement")){
				gameObject.transform.GetChild(0).renderer.material = freeMaterial;
			}else{
				gameObject.renderer.material = freeMaterial;
			}
			HeadUI.GetComponent<HeadUI>().DisplayMessage("");
		}
	}


	public void OnHoldingUpdates(Holding holding) {}
	void Update () {}
}
