using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Debug Script
public class TestAnything : MonoBehaviour 
{

	
	void Update()
	{
		if(Input.GetKeyDown("z")){
			StartCoroutine(test());
		}
	}


	IEnumerator test()
	{
		Debug.Log ("TestAnything.cs_KeyDown.Z");
		for(float time=0.0f; time<1.0f; time+=Time.deltaTime/1.0f){
			this.gameObject.GetComponent<RawImage>().CrossFadeAlpha(0, 1.0f, false);
			yield return null;
		}
	}


}
