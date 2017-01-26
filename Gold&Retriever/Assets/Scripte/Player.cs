using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float speed = 10f;
	public float jumpPower = 10f;

	public Rigidbody2D rb; 
	public GameObject feet ;
    public CapsuleCollider2D capsuleCollider;


	public LayerMask isJumpable ; 
	public float jumpDuration = 0.2f ;
	public Animator animator ;
    public GameObject bomb;
    public float bombThrow;

	private float jumpTime = 0f ; 
	private bool isJumping = false;

    private bool holdSomething=false;
    private GameObject heldObject;

    // Touche clavier :
	public bool J1actif; 

    public KeyCode toucheDroite; 
	public KeyCode toucheGauche;
	public KeyCode toucheHaut;
	public KeyCode toucheSaut;
    public KeyCode toucheBomb;
    public KeyCode toucheAccroupi;
    public KeyCode toucheAction;

	private bool dontMove = false ; 

	private List<GameObject> listRope; 


	public bool onLadder { get ; set; } 

	[SerializeField]
	private float climbSpeed; 

	//private Manager manager ; 


	// Prefab : 
	public GameObject rope ; 
	public GameObject ropeDeb ; 

    // Use this for initialization
    void Start () {
		//manager = GameObject.Find ("Manager").GetComponent<Manager> ();
		listRope = new List<GameObject>() ; 
	}
	
	// Update is called once per frame
	void Update ()
	{

		animator.SetBool ("Walk", 	rb.velocity.x != 0.0f); // walk
		animator.SetBool ("Jump", 	rb.velocity.y != 0.0f); // walk

		Vector3 temp = this.transform.localScale;
		if (!dontMove) {
			if (Input.GetKey (toucheGauche) && rb.velocity.x > -5.0f) {
				rb.velocity += new Vector2 (-speed, 0); 
				temp.x = -1;
				this.transform.localScale = temp; 

			} else if (Input.GetKey (toucheDroite) && rb.velocity.x < 5.0f) {
				rb.velocity += new Vector2 (+speed, 0); 
				temp.x = 1;
				this.transform.localScale = temp; 
			} else {
				if (rb.velocity.x > 0) {
					if (rb.velocity.x - speed <= 0) {
						rb.velocity -= new Vector2 (rb.velocity.x, 0);
					} else {
						rb.velocity += new Vector2 (-speed, 0);
					}
				} else {
					if (rb.velocity.x + speed >= 0) {
						rb.velocity += new Vector2 (rb.velocity.x, 0);
					} else {
						rb.velocity += new Vector2 (speed, 0);
					}
				}
			}

			//BOMBE
			if (Input.GetKeyDown (toucheBomb)) {
				GameObject b = GameObject.Instantiate (bomb);
				b.transform.position = this.transform.position;
				if (Input.GetKey (toucheAccroupi)) {
					b.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);

				} else {
					b.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bombThrow * this.transform.localScale.x, bombThrow);
				}
			}
			// PIED SAUT 
			if (Input.GetKey (toucheSaut)
			    && (Physics2D.OverlapBox (new Vector2 (feet.transform.position.x, feet.transform.position.y), new Vector2 (0.728853f, 0.1633179f), 0, isJumpable)))
				rb.velocity = new Vector2 (rb.velocity.x, jumpPower);

			if (isTouchingGround())
				animator.SetBool ("Jump", false);
			else
				animator.SetBool ("Jump", true);
		
			if (rb.velocity.y == 0.0f)
				animator.SetFloat ("vitesse", Mathf.Abs (rb.velocity.x));  

			if (Input.GetKey (KeyCode.DownArrow))
				animator.SetBool ("crush", true);
			else
				animator.SetBool ("crush", false);

		
			touchLadder (); 

		}


		// touche action 
		actionOn (temp.x);  

        // DEPLACE OBJET TENU
        if (holdSomething)
        {
            moveHeldObject();
        }
		if (Physics2D.OverlapBox(new Vector2 (feet.transform.position.x, feet.transform.position.y), new Vector2 (0.728853f, 0.1633179f),0,isJumpable))
			animator.SetBool ("Jump", false);
		else 
			animator.SetBool ("Jump", true);
	
		if (rb.velocity.y == 0.0f)
			animator.SetFloat ("vitesse", Mathf.Abs (rb.velocity.x));  

	}
		
	#region Touche action
	void actionOn(float direction) 
	{
		//Action
		if (Input.GetKeyDown(toucheAction))
		{
			if (!J1actif) { // fille 
				if (isTouchingGround ()) {
					ropeAction (direction); 
					rb.velocity = new Vector2 (0, 0); // a voir si enlever ( fréinage direct) 
					dontMove = true;
				}
			} else {
				doAction();

			}

		}

		if (Input.GetKeyUp (toucheAction)) {
			if (!J1actif) {
				ropeActionReverse (); 
				dontMove = false;
			}


		}

	}
	#endregion 

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

    void moveHeldObject()
    {
        Transform t = heldObject.transform;
        t.position = new Vector3(this.transform.position.x + 0.2f * this.transform.localScale.x,
            this.transform.position.y + 0.2f, this.transform.position.z);
        t.localScale = this.transform.localScale;
    }

    void doAction()
    {
        Collider2D col = null;
        Collider2D[] cols = Physics2D.OverlapCapsuleAll(capsuleCollider.transform.position, capsuleCollider.size, capsuleCollider.direction, 0);

        if (cols.Length>1)
        {
            foreach (Collider2D c in cols)
            {
                if (c.gameObject != this.gameObject)
                {
                    col = c;
                }
            }
            if (holdSomething)
            {
                //col.gameObject.GetComponent<Player>(); activate
                heldObject.GetComponent<Rigidbody2D>().velocity = new Vector2(20 * this.transform.localScale.x, 10);
                holdSomething = false;
                //heldObject = null;
            }
            else
            {
                if (col.gameObject.tag == "Player")
                {
                    heldObject = col.gameObject;
                    //col.gameObject.GetComponent<Player>(); inactivate
                    holdSomething = true;
                }
            }
        }
       
    }
	// si les pied touch le sol
	bool isTouchingGround()
	{
		return (Physics2D.OverlapBox (new Vector2 (feet.transform.position.x, feet.transform.position.y), new Vector2 (0.728853f, 0.1633179f), 0, isJumpable));
	}

	void ropeAction(float direction)
	{
		GameObject ropei = Instantiate (ropeDeb); 
		Vector3 temp = this.transform.position; 

		Vector3 temp2 = this.transform.localScale; 
		temp2.x = direction; 
		ropei.transform.localScale = temp2; 
		temp.x = temp.x + direction * 0.8f;
		temp.y = temp.y - 0.373f;
		ropei.transform.position = temp; 
		listRope.Add (ropei); 
	
		Vector3 tempWhile = ropei.transform.position ; 
		float ftemp = 0.0f; 

		while(!Physics2D.OverlapBox (new Vector2 (tempWhile.x,tempWhile.y - 1), new Vector2 (0.2409988f, 0.358952f), 0, isJumpable) && ftemp != -10.0f)
		{
			ftemp -= 1.0f; 
			ropei = Instantiate (rope); 
			temp = this.transform.position; 
			temp2 = this.transform.localScale; 

			ropei.transform.parent = listRope [0].transform; 
				
			temp2.x = direction; 
			ropei.transform.localScale = temp2; 
			temp.x = temp.x + direction * 0.8f;
			temp.y = temp.y - 0.373f + ftemp;
			ropei.transform.position = temp; 

			listRope.Add (ropei); 
			tempWhile = ropei.transform.position ; 
		}


		BoxCollider2D c2D = listRope [0].AddComponent<BoxCollider2D>() ;
		c2D.isTrigger = true;
		c2D.size = new Vector2 (0.25f,listRope.Count); 
		c2D.offset = new Vector2(0, -listRope.Count / 2 ) ; 
	}

	void ropeActionReverse()
	{
		Debug.Log ("lalalalalalal : "+ listRope.Count ); 
		for(int i = 1; i < listRope.Count ; i++)
			Destroy (listRope[i]); 
		Destroy (listRope[0]); 
		listRope.Clear ();
	}	


	void touchLadder() 
	{
		Collider2D[] cols = Physics2D.OverlapCapsuleAll(capsuleCollider.transform.position, capsuleCollider.size, capsuleCollider.direction, 0);

		onLadder = false; 
		foreach (Collider2D c in cols)
			if (c.gameObject.tag == "rope") 
				onLadder = true; 
		
		animator.SetBool ("Climb", onLadder);		
		if (onLadder && J1actif) {
			if (Input.GetKey (toucheHaut))
				rb.velocity = new Vector2 (0, speed * climbSpeed); 
			else if (Input.GetKey (toucheAccroupi))
				rb.velocity = new Vector2 (0, -speed * climbSpeed); 
		
			rb.gravityScale = 0;
		} else {
			rb.gravityScale = 1.2f;
			animator.SetBool ("Climb", false);
		}

	}
		
}

