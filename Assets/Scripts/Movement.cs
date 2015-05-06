using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () {transform.Translate (Vector2.right *Time.deltaTime*speed);
	
	}
}
