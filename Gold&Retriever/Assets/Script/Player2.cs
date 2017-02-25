using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour {

	public float speed = 10f;
	public float jumpPower = 10f;

	public Rigidbody2D rb; 
	public GameObject feet ;

	private bool isJumping = false;
	public LayerMask isJumpable ; 
	public float jumpDuration = 0.2f ;
	private float jumpTime = 0f ; 
	public Animator animator ;

	//private Manager manager ; 
	// Use this for initialization
	void Start () {
		//manager = GameObject.Find ("Manager").GetComponent<Manager> ();
	}

	// Update is called once per frame
	void Update ()
	{
		animator.SetBool ("Walk", rb.velocity.x != 0.0f); // walk
		//animator.SetBool ("Jump", 	rb.velocity.y != 0.0f); // walk


		Vector3 temp = this.transform.localScale;
		if (Input.GetKey (KeyCode.Q) && rb.velocity.x > -5.0f) {
			rb.velocity += new Vector2 (-speed, 0); 
			temp.x = -1;
			this.transform.localScale = temp; 

		}

		if (Input.GetKey (KeyCode.D) && rb.velocity.x < 5.0f) {
			rb.velocity += new Vector2 (+speed, 0); 
			temp.x = 1;
			this.transform.localScale = temp; 
		}

		// PIED SAUT 
		if (Input.GetKey (KeyCode.Space)
			&& (Physics2D.OverlapBox(new Vector2 (feet.transform.position.x, feet.transform.position.y), new Vector2 (0.728853f, 0.1633179f),0,isJumpable)))
			rb.velocity = new Vector2 (rb.velocity.x, jumpPower);

		if (Physics2D.OverlapBox(new Vector2 (feet.transform.position.x, feet.transform.position.y), new Vector2 (0.728853f, 0.1633179f),0,isJumpable))
			animator.SetBool ("Jump", false);
		else 
			animator.SetBool ("Jump", true);

		if (rb.velocity.y == 0.0f)
			animator.SetFloat ("vitesse", Mathf.Abs (rb.velocity.x));  

		if (Input.GetKey (KeyCode.DownArrow))
			animator.SetBool ("crush", true);
		else
			animator.SetBool ("crush", false);
	}

	// ENEMIES
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ennemies" &&  coll.contacts [0].normal.y > 0.5) {
			Destroy (coll.gameObject); 
			rb.velocity += new Vector2 (0, jumpPower*5);
			animator.SetBool ("Saut", true);
		}
		if (coll.gameObject.tag == "Ennemies" &&  coll.contacts [0].normal.y < 0.5)
			dead (); 

		if (coll.gameObject.tag == "Champi") {
			rb.velocity += new Vector2 (0, jumpPower*3);
			animator.SetBool ("Saut", true);
		}
	}


	// Death zone 
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "deathZone") {
			//manager.ChangementMort (); 
			dead (); 
		}
	}


	void dead()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name); 
	}
}

