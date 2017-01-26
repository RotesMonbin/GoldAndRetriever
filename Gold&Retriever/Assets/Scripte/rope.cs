using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour {

	public float speed; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		/*if (other.tag == "Player" && Input.GetKey (KeyCode.UpArrow)) {
			other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, speed); 
			other.GetComponent<Rigidbody2D> ().gravityScale = 0; 
		} else if (other.tag == "Player" && Input.GetKey (KeyCode.DownArrow)) {
			other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -speed); 
			other.GetComponent<Rigidbody2D> ().gravityScale = 0; 
		} else {
			other.GetComponent<Rigidbody2D> ().gravityScale = 1.2f; 
		}*/
	}
}
