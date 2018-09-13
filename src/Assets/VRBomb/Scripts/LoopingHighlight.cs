using UnityEngine;
using System.Collections;


// Looping display particle at period of time.
// Used in "SparkleRising" particle
public class LoopingHighlight : MonoBehaviour 
{
	/*ParticleEmitter PE;
	IEnumerator coroutine;
	

	void OnEnable() 
	{
		PE = this.gameObject.GetComponent<ParticleEmitter>();
		coroutine = Highlight();
		StartCoroutine(coroutine);
	}


	void OnDisable()
	{
		StopCoroutine(coroutine);
	}


	IEnumerator Highlight()
	{
		while(true){
			PE.emit = false;
			yield return new WaitForSeconds(2.0f);

			PE.emit = true;
			yield return new WaitForSeconds(1.5f);
		}
	}*/
}
