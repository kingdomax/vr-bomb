using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leap.Interact;


public class TailHandCallBack : MonoBehaviour 
{


	public GameObject TailUI;
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
				if(lastHovered.name.Equals("ArmingPin") || lastHovered.name.Equals("Clip")){
					lastHovered.transform.GetChild(1).gameObject.GetComponent<ParticleEmitter>().emit = false;
				}
				if(lastHovered.name.Equals("SafetyElement") || lastHovered.name.Equals("StopScrew")){
					lastHovered.transform.GetChild(0).renderer.material = freeMaterial;
				}else{
					lastHovered.renderer.material = freeMaterial;
				}
				TailUI.GetComponent<TailUI>().DisplayMessage("");
			}
			if (gameObject)
			{
				if(gameObject.name.Equals("ArmingPin") || gameObject.name.Equals("Clip")){
					gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleEmitter>().emit = false;
				}
				if(gameObject.name.Equals("SafetyElement") || gameObject.name.Equals("StopScrew")){
					gameObject.transform.GetChild(0).renderer.material = hoverMaterial;
				}else{
					gameObject.renderer.material = hoverMaterial;
				}
				TailUI.GetComponent<TailUI>().DisplayMessage(gameObject.name);
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
			if(gameObject.name.Equals("ArmingPin") || gameObject.name.Equals("Clip")){
				gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleEmitter>().emit = true;
			}
			if(gameObject.name.Equals("SafetyElement") || gameObject.name.Equals("StopScrew")){
				gameObject.transform.GetChild(0).renderer.material = heldMaterial;
			}else{
				gameObject.renderer.material = heldMaterial;
			}
			TailUI.GetComponent<TailUI>().DisplayMessage("");
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
			if(gameObject.name.Equals("ArmingPin") || gameObject.name.Equals("Clip")){
				gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleEmitter>().emit = false;
			}
			if(gameObject.name.Equals("SafetyElement") || gameObject.name.Equals("StopScrew")){
				gameObject.transform.GetChild(0).renderer.material = freeMaterial;
			}else{
				gameObject.renderer.material = freeMaterial;
			}
			TailUI.GetComponent<TailUI>().DisplayMessage("");
		}
	}
	
	
	public void OnHoldingUpdates(Holding holding) {}
	void Update () {}
}
