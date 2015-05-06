using UnityEngine;
using System.Collections;


public class ParticleOnDestroy : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col)
	{ 
		GetComponent<ParticleSystem>().enableEmission=true;
	}
}
