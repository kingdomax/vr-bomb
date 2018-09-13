using UnityEngine;
using System.Collections;


// Used in "Spark" particle
public class OneTimeHighlight : MonoBehaviour 
{
	// One time emited 
	void OnEnable() 
	{
		this.gameObject.GetComponent<ParticleEmitter>().Emit ();
	}
}
