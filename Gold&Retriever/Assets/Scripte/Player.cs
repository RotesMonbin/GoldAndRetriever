using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float speed = 10f;
	public float jumpPower = 10f;

	public Rigidbody2D rb; 
	public GameObject feet ;
    public GameObject face ;
    public CapsuleCollider2D capsuleCollider;


	public LayerMask isJumpable ;
    public LayerMask enemieLayer;
    public LayerMask spikeLayer;

    public float jumpDuration = 0.2f ;
	public Animator animator ;
    public GameObject bomb;
    public float bombThrow;
    public float invincibleTime;

    public SpriteRenderer sprite;

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
    private float invincible=0;
    private int lastBlinkNumber;
    


    private List<GameObject> listRope; 


	public bool onLadder { get ; set; } 

	[SerializeField]
	private float climbSpeed; 

	private ManagerJoueur managerJoueur ; 

	[SerializeField]
	private Camera cameraShakeJ1 ;
	[SerializeField]
	private Camera cameraShakeJ2 ; 

	// Prefab : 
	public GameObject rope ; 
	public GameObject ropeDeb ; 

    // Use this for initialization
    void Start () {
		managerJoueur = GameObject.Find ("ManagerJoueur").GetComponent<ManagerJoueur> ();
		listRope = new List<GameObject>() ; 
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (J1actif)
        {
            if(managerJoueur.getLife(true) <= 0)
            {
                animator.SetBool("Dead", true);
                this.dontMove = true;
            }
        }
        else
        {
            if (managerJoueur.getLife(false) <= 0)
            {
                animator.SetBool("Dead", true);
                this.dontMove = true;
            }
        }
		animator.SetBool ("Walk", 	rb.velocity.x != 0.0f ); // walk
		animator.SetBool ("Jump", 	rb.velocity.y != 0.0f ); // walk
        DirectionControl();
        SautControl();
		touchLadder();
        BombControl();
        SpikeColision();
        // touche action 
        actionOn ();
        enemieColision();

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

        //invincibilityBlink
        if (invincible > 0)
        {
            blinkAnimation();
        }
        else
        {
            this.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }


	}

    #region ToucheBomb
    void BombControl()
    {
        //BOMBE
        if (!dontMove)
        {
            if (Input.GetKeyDown(toucheBomb))
            {

                if (managerJoueur.nbBombeOk(J1actif))
                {
                    GameObject b = GameObject.Instantiate(bomb);
                    b.transform.position = this.transform.position;
                    if (Input.GetKey(toucheAccroupi))
                    {
                        b.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                    }
                    else
                    {
                        b.GetComponent<Rigidbody2D>().velocity = new Vector2(bombThrow * this.transform.localScale.x, bombThrow);
                    }
                    managerJoueur.bombeDown(1, J1actif);
                }
            }
        }
    }
    #endregion

    #region ToucheSaut
    void SautControl()
    {
        // PIED SAUT 
        if (!dontMove)
        {
            if (Input.GetKey(toucheSaut)
            && (Physics2D.OverlapBox(new Vector2(feet.transform.position.x, feet.transform.position.y), new Vector2(0.728853f, 0.1633179f), 0, isJumpable)))
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            if (isTouchingGround())
                animator.SetBool("Jump", false);
            else
                animator.SetBool("Jump", true);

            if (rb.velocity.y == 0.0f)
                animator.SetFloat("vitesse", Mathf.Abs(rb.velocity.x));

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetBool("crush", true);
                managerJoueur.lifeDown(1, J1actif);
            }
            else
                animator.SetBool("crush", false); 
        }
    }
    #endregion

    #region TouchesDirection

    void DirectionControl()
    {
        Vector3 temp = this.transform.localScale;
        if (!dontMove)
        {
            if (Input.GetKey(toucheGauche) && rb.velocity.x > -5.0f)
            {
                temp.x = -1;
                this.transform.localScale = temp;
                if (!isFacingWall())
                {
                    rb.velocity += new Vector2(-speed, 0);
                }

            }
            else if (Input.GetKey(toucheDroite) && rb.velocity.x < 5.0f)
            {
                temp.x = 1;
                this.transform.localScale = temp;
                if (!isFacingWall())
                {
                    rb.velocity += new Vector2(+speed, 0);
                }
            }
            else
            {
                if (isTouchingGround())
                {
                    if (rb.velocity.x > 0)
                    {
                        if (rb.velocity.x - speed <= 0)
                        {
                            rb.velocity -= new Vector2(rb.velocity.x, 0);
                        }
                        else
                        {
                            rb.velocity += new Vector2(-speed, 0);
                        }
                    }
                    else
                    {
                        if (rb.velocity.x + speed >= 0)
                        {
                            rb.velocity += new Vector2(rb.velocity.x, 0);
                        }
                        else
                        {
                            rb.velocity += new Vector2(speed, 0);
                        }
                    }
                }
                else
                {
                    rb.velocity -= new Vector2(rb.velocity.x / 20, 0);
                }
            }
        }
    }
    #endregion

    #region Touche action
    void actionOn() 
	{
		//Action
		if (Input.GetKeyDown(toucheAction))
		{
			if (!J1actif) { // fille 
				if (isTouchingGround ()) {
					ropeAction (); 
					rb.velocity = new Vector2 (0, 0); // a voir si enlever ( fréinage direct) 
					dontMove = true;
				}
			} else {
				doAction();

			}

		}

		if (Input.GetKeyUp (toucheAction)) {
			if (!J1actif) {
				if (isTouchingGround ()) {
					ropeActionReverse (); 
					dontMove = false;
				}
			}
		}
	}
	#endregion 

	#region Collision 2D
	// ENEMIES
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ennemies" &&  coll.contacts [0].normal.y > 0.5) {
			Destroy (coll.gameObject); 
			rb.velocity += new Vector2 (0, jumpPower*5);
			animator.SetBool ("Saut", true);
		}

		if (coll.gameObject.tag == "Coins") {
			managerJoueur.GetComponent<ManagerJoueur> ().cashUp (1, J1actif); 
			Destroy (coll.gameObject); 
		}	

		if (coll.gameObject.tag == "bombeCaisse") {
			managerJoueur.GetComponent<ManagerJoueur> ().bombeUp (1, J1actif); 
			Destroy (coll.gameObject); 
		}
        if (coll.gameObject.tag == "Enemie")
        {
            dead();
        }
    }
		
	// Death zone 
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "deathZone") {
			//manager.ChangementMort (); 
			dead (); 
		}
        if (coll.gameObject.tag == "Enemie" )
        {
            dead();
        }

		if (coll.gameObject.tag == "GemmeBleu" )
		{
			cameraShakeJ1.GetComponent<CameraShake> ().gem = true;
			cameraShakeJ2.GetComponent<CameraShake> ().gem = true; 

			Destroy (coll.gameObject); 
		}


    }

    #endregion

    #region Damage
    void enemieColision()
    {
        if (invincible != 0)
        {
            invincible = invincible - Time.deltaTime<0 ?0: invincible - Time.deltaTime;
        }
        Collider2D[] cols = Physics2D.OverlapCapsuleAll(capsuleCollider.transform.position, capsuleCollider.size, capsuleCollider.direction, 0,enemieLayer);
        foreach (Collider2D c in cols)
        {
            if (c.GetComponent<Worms>())
            {
                if (c.transform.position.y > feet.transform.position.y+0.5)
                {
                    if (invincible == 0) {
                        invincible = invincibleTime;
                        takeDamage(c.transform.position.x > feet.transform.position.x ? -1 : 1);
                    }
                }
                else
                {
                    if (invincible == 0)
                    {
                        this.rb.velocity += new Vector2(0, 4);
                        c.GetComponent<Worms>().dead();
                    }
                }
            }
        }
    }
    void SpikeColision()
    {
        if (Physics2D.OverlapBox(new Vector2(feet.transform.position.x, feet.transform.position.y)
            , new Vector2(0.728853f, 0.1633179f), 0, spikeLayer) && rb.velocity.y<-0.5)
        {
            managerJoueur.lifeDown(managerJoueur.getLife(J1actif), J1actif);
        }

    }
    void takeDamage(int direction)
    {
        this.rb.velocity+=new Vector2 (direction * 4, 4);
        managerJoueur.lifeDown(1, J1actif);
        lastBlinkNumber = (int)(5 * invincible);
    }
    public void dead()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name); 
	}
    #endregion

    #region Porte obj
    void moveHeldObject()
    {
        Transform t = heldObject.transform;
        t.position = new Vector3(this.transform.position.x + 0.2f * this.transform.localScale.x,
            this.transform.position.y + 0.2f, this.transform.position.z);
        t.localScale = this.transform.localScale;
    }

    void doAction()
    {

        if (holdSomething)
        {
            moveHeldObject();
            if (heldObject.tag == "Player")
            {
                heldObject.GetComponent<Player>().animator.SetBool("Held", false);
                heldObject.GetComponent<Player>().dontMove = false;
            }
            heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
            heldObject.GetComponent<Rigidbody2D>().velocity = new Vector2(20 * this.transform.localScale.x, 10);
            holdSomething = false;
        }
        else
        {
            Collider2D col = null;
            Collider2D[] cols = Physics2D.OverlapCapsuleAll(capsuleCollider.transform.position, capsuleCollider.size, capsuleCollider.direction, 0);
            if (cols.Length > 1)
            {
                foreach (Collider2D c in cols)
                {
                    if (c.gameObject != this.gameObject)
                    {
                        if (c.gameObject.tag == "Player" || c.gameObject.tag == "Pickup") { 
                            col = c;
                        }
                    }
                }
                if (col)
                {
                    if (col.gameObject.tag == "Player")
                    {
                        col.gameObject.GetComponent<Player>().animator.SetBool("Held", true);
                        col.gameObject.GetComponent<Player>().dontMove = true;
                    }
                    heldObject = col.gameObject;
                    heldObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    holdSomething = true;
                }
            }
        }
    }
	#endregion

	#region Rope Action 
	void ropeAction()
	{
		GameObject ropei = Instantiate (ropeDeb); 
		Vector3 temp = this.transform.position; 

		Vector3 temp2 = this.transform.localScale; 
		temp2.x = direction(); 
		ropei.transform.localScale = temp2; 
		temp.x = temp.x + direction() * 0.8f;
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

			ropei.transform.localScale = temp2; 
			temp.x = temp.x + direction() * 0.8f;
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
		if (onLadder) {
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
	#endregion 

	#region Utile : direction, toucheGround...
	// Direction du joueur ; 1 regarde à droite -1 à gauche 
	float direction() 
	{
		return this.transform.localScale.x;
	}

	// si les pied touch le sol
	bool isTouchingGround()
	{
		return (Physics2D.OverlapBox (new Vector2 (feet.transform.position.x, feet.transform.position.y), new Vector2 (0.728853f, 0.1633179f), 0, isJumpable));
	}

    bool isFacingWall()
    {
        return (Physics2D.OverlapCircle(new Vector2(face.transform.position.x, face.transform.position.y), 0.2f, isJumpable));
    }

    void blinkAnimation()
    {
        if (lastBlinkNumber>(int)(5 * invincible))
        {
            lastBlinkNumber = (int)(5 * invincible);
            this.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, this.GetComponentInChildren<SpriteRenderer>().color.a==1?0:1);
        }
        
    }
    #endregion

}

