using UnityEngine;
using System.Collections;


// Control fading intensity light 0-8
public class LerpingLight : MonoBehaviour 
{
	float TargetIntensity;
	float Speed;
	float ChangeMargin;


	void OnEnable()
	{
		// check whether is this head or tail scene
		TargetIntensity = 2.0f;
		Speed = 1.0f;
		ChangeMargin = 0.2f;
		StartCoroutine(Fading());
	}


	IEnumerator Fading() 
	{
		while(this.gameObject.activeSelf)
		{
			light.intensity = Mathf.Lerp(light.intensity, TargetIntensity, Speed*Time.deltaTime);
		
			if(Mathf.Abs(TargetIntensity - light.intensity) < ChangeMargin)
			{
				// If the target intensity is high
				if(TargetIntensity == 2.0f){
					TargetIntensity = 0f;	// Then set the target to low.
				}else{
					TargetIntensity = 2.0f;	// Otherwise set the targer to high.
				}
			}
			yield return null;
		}
	}
}
