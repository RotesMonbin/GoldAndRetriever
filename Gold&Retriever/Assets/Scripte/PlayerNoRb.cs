using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoRb : MonoBehaviour
{


    public float acceleration;
    public float walkingMaxSpeed;
    public float runningMaxSpeed;
    public float jumpPower;
    public float throwPower;
    private float jumpOnEnemy = 20;

    public BoxCollider2D feet;
    public BoxCollider2D face;
    public BoxCollider2D head;
    public BoxCollider2D boxCollider;
    public float gravityScale;


    public LayerMask decorLayer;
    public LayerMask enemieLayer;
    public LayerMask spikeLayer;
    public LayerMask objectLayer;

    public float jumpDuration = 0.2f;
    public Animator animator;
    public GameObject bomb;
    public float bombThrow;
    public float invincibleTime;

    public SpriteRenderer sprite;

    public float speedFallDamage;

    private bool isJumping = false;

    private bool gameOver = false;
    private bool holdSomething = false;
    private GameObject heldObject;

    private Vector2 speed;

    // Touche clavier :
    public bool J1actif;

    public KeyCode toucheDroite;
    public KeyCode toucheGauche;
    public KeyCode toucheHaut;
    public KeyCode toucheSaut;
    public KeyCode toucheBomb;
    public KeyCode toucheAccroupi;
    public KeyCode toucheAction;

    public string manetteSaut;
    public string manetteBombe;
    public string manetteAction;
    public string manetteAxeX;
    public string manetteAxeY;
    public string triggAxis;

    private bool dontMove = false;

    private float invincible = 0;
    private int lastBlinkNumber;
    private bool damageNextTimeOnGround;

    private List<GameObject> listRope;
    private List<GameObject> objectsLaunched;

    public bool onLadder { get; set; }

    [SerializeField]
    private float climbSpeed;

    private ManagerJoueur managerJoueur;

    [SerializeField]
    private Camera cameraShakeJ1;
    [SerializeField]
    private Camera cameraShakeJ2;

    // Prefab : 
    public GameObject rope;
    public GameObject ropeDeb;

    // Use this for initialization
    void Start()
    {
        speed = new Vector2(0, 0);
        managerJoueur = GameObject.Find("ManagerJoueur").GetComponent<ManagerJoueur>();

        listRope = new List<GameObject>();
        objectsLaunched = new List<GameObject>();

        managerJoueur.chargementDonnee();

    }

    // Update is called once per frame
    void Update()
    {

        if (J1actif)
        {
            if (managerJoueur.getLife(true) <= 0)
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
        animator.SetBool("Walk", speed.x != 0.0f); // walk
        animator.SetBool("Jump", speed.y != 0.0f); // Jump

        // DEPLACE OBJET TENU
        if (holdSomething)
        {
            moveHeldObject();
        }
        if (isTouchingGround())
            animator.SetBool("Jump", false);
        else
            animator.SetBool("Jump", true);

        //invincibilityBlink
        if (invincible > 0 && managerJoueur.getLife(J1actif) > 0)
        {
            blinkAnimation();
        }
        else
        {
            sprite.color = new Color(1, 1, 1, 1);
        }


    }

    private void FixedUpdate()
    {
        Gravity();
        HeadColision();
        FaceColision();


        UpdatePosition();

        DirectionControl();
        SautControl();
        touchLadder();
        BombControl();
        SpikeColision();
        DamageOnThrow();
        CleanObjectLaunched();
        actionOn();
        enemieColision();
        DamageOnFall();
        GameOver();
    }
    #region RB manager

    private void UpdatePosition()
    {
        this.transform.Translate(speed * Time.deltaTime);
    }

    private bool AlreadyOnGround = false;
    private void Gravity()
    {
        if (!isTouchingGround())
        {
            speed -= new Vector2(0, 9.81f * gravityScale * Time.deltaTime);
            AlreadyOnGround = false;
        }
        else if (!AlreadyOnGround)
        {
            speed.y = 0;
            Collider2D coll = Physics2D.OverlapBox(new Vector2(feet.transform.position.x, feet.transform.position.y), feet.size, 0, decorLayer);
            Vector3 closestPoint=coll.bounds.ClosestPoint(this.transform.position);
            float dist = closestPoint.y - (feet.transform.position.y - feet.size.y/2);
            if (dist > 0)
            {
                this.transform.Translate(0, dist, 0);
            }
            AlreadyOnGround = true;
        }
    }

    private bool AlreadyFacingWall = false;
    private void FaceColision()
    {
        if (!isFacingWall())
        {
            AlreadyFacingWall = false;
        }
        else if (!AlreadyFacingWall)
        {
            speed.x = 0;
            AlreadyFacingWall = true;
        }
    }

    private bool AlreadyTouchingRoof = false;
    private void HeadColision()
    {
        if (!isTouchingRoof())
        {
            AlreadyTouchingRoof = false;
        }
        else if (!AlreadyTouchingRoof)
        {
            speed.y = 0;
            Collider2D coll = Physics2D.OverlapBox(new Vector2(head.transform.position.x, head.transform.position.y), head.size, 0, decorLayer);
            Vector3 closestPoint = coll.bounds.ClosestPoint(this.transform.position);
            float dist = (head.transform.position.y - head.size.y / 2) - closestPoint.y;
            if (dist > 0)
            {
                this.transform.Translate(0, -dist, 0);
            }
            AlreadyTouchingRoof = true;
        }
    }

    #endregion

    #region ToucheBomb
    void BombControl()
    {
        //BOMBE
        if (!dontMove)
        {
            if (Input.GetKeyDown(toucheBomb) || Input.GetButtonDown(manetteBombe))
            {

                if (managerJoueur.nbBombeOk(J1actif))
                {
                    GameObject b = GameObject.Instantiate(bomb);
                    objectsLaunched.Add(b);
                    b.transform.position = this.transform.position;
                    if (Input.GetKey(toucheAccroupi) || manetteDown())
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
            if ((Input.GetKey(toucheSaut) || Input.GetButton(manetteSaut)) && isTouchingGround())
                speed = new Vector2(speed.x, jumpPower);

            if (isTouchingGround())
                animator.SetBool("Jump", false);
            else
                animator.SetBool("Jump", true);

        }
    }
    #endregion

    #region TouchesDirection


    void DirectionControl()
    {
        Vector3 temp = this.transform.localScale;

        if (!dontMove)
        {
            if ((Input.GetKey(toucheGauche) || manetteLeft()))
            {
                temp.x = -1;
                this.transform.localScale = temp;
                if (!isFacingWall())
                {
                    if ((manetteRT() && speed.x > -runningMaxSpeed) || speed.x > -walkingMaxSpeed)
                    {
                        speed -= new Vector2(acceleration * Time.deltaTime, 0);
                    }
                }


            }
            else if ((Input.GetKey(toucheDroite) || manetteRight()))
            {
                temp.x = 1;
                this.transform.localScale = temp;
                if (!isFacingWall())
                {
                    if ((manetteRT() && speed.x < runningMaxSpeed) || speed.x < walkingMaxSpeed)
                    {
                        speed += new Vector2(acceleration * Time.deltaTime, 0);
                    }

                }

            }
            else
            {
                if (isTouchingGround() && speed.x != 0)
                {
                    if ((speed.x > 0 && speed.x - acceleration * Time.deltaTime <= 0) || (speed.x < 0 && speed.x + acceleration * Time.deltaTime >= 0))
                    {
                        speed = new Vector2(0, speed.y);
                    }
                    else if (speed.x > 0)
                    {
                        speed += new Vector2(-acceleration * Time.deltaTime, 0);
                    }
                    else
                    {
                        speed += new Vector2(acceleration * Time.deltaTime, 0);
                    }
                }
                else
                {
                    speed -= new Vector2(speed.x / 20, 0);
                }
            }
        }
    }
    #endregion

    #region Touche action
    void actionOn()
    {
        //Action
        if (Input.GetKeyDown(toucheAction) || Input.GetButtonDown(manetteAction))
        {
            if (!J1actif)
            { // fille 
                if (Input.GetKey(toucheAccroupi) || manetteDown())
                {
                    if (isTouchingGround())
                    {
                        ropeAction();
                        speed = new Vector2(0, 0); // a voir si enlever ( fréinage direct) 
                        dontMove = true;
                    }
                }
                else
                {
                    pickUpItem();
                }
            }
            else
            {
                pickUpItem();

            }

        }

        if (Input.GetKeyUp(toucheAction) || Input.GetButtonUp(manetteAction))
        {
            if (!J1actif)
            {
                if (isTouchingGround())
                {
                    ropeActionReverse();
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

        if (coll.gameObject.tag == "Coins")
        {
            managerJoueur.GetComponent<ManagerJoueur>().cashUp(1, J1actif);
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "bombeCaisse")
        {
            managerJoueur.GetComponent<ManagerJoueur>().bombeUp(1, J1actif);
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "coeur de vie" && managerJoueur.getLifeMax(J1actif) != managerJoueur.getLife(J1actif))
        {
            managerJoueur.GetComponent<ManagerJoueur>().lifeUp(1, J1actif);
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.tag == "Transition Jeu Menu" && managerJoueur.possedeGem())
        {
            managerJoueur.changementScene(3);
            managerJoueur.gemUp();
        }

    }

    // Death zone 
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "GemmeBleu")
        {
            cameraShakeJ1.GetComponent<CameraShake>().gem = true;
            cameraShakeJ2.GetComponent<CameraShake>().gem = true;
            managerJoueur.gemUp();


            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "Lave")
        {
            takeDamage(4);
        }

        if (coll.gameObject.tag == "Transition Menu Jeu")
        {
            managerJoueur.changementScene(2);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {

        if (coll.CompareTag("objetBuy"))
        {
            if (Input.GetKeyDown(toucheAction) || Input.GetButtonDown(manetteAction))
            {
                coll.GetComponent<ObjectSeller>().gestion_cash(J1actif);
            }
        }
    }
    #endregion

    #region Damage
    void enemieColision()
    {
        if (invincible != 0)
        {
            invincible = invincible - Time.deltaTime < 0 ? 0 : invincible - Time.deltaTime;
        }
        Collider2D[] cols = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size, 0, enemieLayer);
        foreach (Collider2D c in cols)
        {
            if (c.GetComponent<Worms>())
            {
                if (c.transform.position.y > feet.transform.position.y + 0.6)
                {
                    if (invincible == 0)
                    {
                        invincible = invincibleTime;
                        takeDamage(c.transform.position.x > feet.transform.position.x ? -1 : 1);
                    }
                }
                else
                {
                    if (invincible < 1.5)
                    {
                        speed = new Vector2(speed.x, jumpOnEnemy);
                        c.GetComponent<Worms>().dead();
                    }
                }
            }
            if (c.GetComponent<Frog>())
            {
                if (c.transform.position.y > feet.transform.position.y + 0.6)
                {
                    if (invincible == 0)
                    {
                        invincible = invincibleTime;
                        takeDamage(c.transform.position.x > feet.transform.position.x ? -1 : 1);
                    }
                }
                else
                {
                    if (invincible < 1.5)
                    {
                        speed = new Vector2(speed.x, jumpOnEnemy);
                        c.GetComponent<Frog>().dead();
                    }
                }
            }
        }
    }
    void SpikeColision()
    {
        if (Physics2D.OverlapBox(new Vector2(feet.transform.position.x, feet.transform.position.y)
            , feet.size, 0, spikeLayer) && speed.y < -0.5)
        {
            killPlayer();
        }

    }

    public void killPlayer()
    {
        managerJoueur.lifeDown(managerJoueur.getLife(J1actif), J1actif);
    }

    void takeDamage(int direction)
    {
        speed += new Vector2(direction * 4, 4);
        managerJoueur.lifeDown(1, J1actif);
        lastBlinkNumber = (int)(5 * invincible);
    }

    void GameOver()
    {
        if (managerJoueur.getLife(true) <= 0 || managerJoueur.getLife(false) <= 0)
        {
            this.dontMove = true;
            gameOver = true;
        }
        if (gameOver && (Input.GetButtonDown(manetteSaut) || Input.GetKeyDown(toucheSaut)))
        {
            managerJoueur.changementScene(1);
        }
    }

    public void DamageOnThrow()
    {
        bool next = false;
        if (invincible == 0)
        {
            Collider2D[] cols = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size, 0, objectLayer);
            foreach (Collider2D c in cols)
            {
                next = false;
                foreach (GameObject obj in objectsLaunched)
                {
                    if (c.gameObject == obj)
                    {
                        next = true;
                    }
                }
                if (!next)
                {
                    Rigidbody2D rb_tmp = c.GetComponent<Rigidbody2D>();
                    if (rb_tmp)
                    {
                        if (Mathf.Abs(rb_tmp.velocity.x) + Mathf.Abs(rb_tmp.velocity.y) > 1)
                        {
                            invincible = invincibleTime;
                            takeDamage(c.transform.position.x > this.transform.position.x ? -1 : 1);
                        }
                    }
                }
            }
        }
    }

    public void CleanObjectLaunched()
    {
        bool toDelete = true;
        List<GameObject> objectsToDelete = new List<GameObject>();
        Collider2D[] cols = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size, 0, objectLayer);
        foreach (GameObject obj in objectsLaunched)
        {
            foreach (Collider2D c in cols)
            {
                if (obj == c.gameObject)
                {
                    toDelete = false;
                }
            }
            if (toDelete)
            {
                objectsToDelete.Add(obj);
            }
        }
        foreach (GameObject obj in objectsToDelete)
        {
            objectsLaunched.Remove(obj);
        }
    }

    private void DamageOnFall()
    {
        if (speed.y < -speedFallDamage)
        {
            damageNextTimeOnGround = true;
        }
        if (isTouchingGround() && damageNextTimeOnGround)
        {
            invincible = invincibleTime;
            takeDamage((int)-this.transform.localScale.x);
            damageNextTimeOnGround = false;
        }
    }
    #endregion

    #region Porte obj
    void moveHeldObject()
    {
        if (!heldObject)
        {
            holdSomething = false;
        }
        else
        {
            Transform t = heldObject.transform;
            t.position = new Vector3(this.transform.position.x + 0.2f * this.transform.localScale.x,
                this.transform.position.y + 0.2f, this.transform.position.z);
            t.localScale = this.transform.localScale;
        }

    }

    void pickUpItem()
    {

        if (holdSomething)
        {
            moveHeldObject();
            if (heldObject.tag == "Player")
            {
                heldObject.GetComponent<Player>().animator.SetBool("Held", false);
                heldObject.GetComponent<Player>().dontMove = false;
            }
            if (heldObject.gameObject.tag == "Box")
            {
                heldObject.GetComponent<boxRandom>().held = false;
                heldObject.GetComponent<boxRandom>().thrown = true;
            }
            objectsLaunched.Add(heldObject);
            heldObject.GetComponent<Rigidbody2D>().isKinematic = false;
            heldObject.GetComponent<Rigidbody2D>().velocity = new Vector2(throwPower * this.transform.localScale.x, throwPower / 2);
            holdSomething = false;
        }
        else
        {
            Collider2D col = null;
            Collider2D[] cols = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size, 0);
            if (cols.Length > 1)
            {
                foreach (Collider2D c in cols)
                {
                    if (c.gameObject != this.gameObject)
                    {
                        if ((c.gameObject.tag == "Player" && J1actif) || c.gameObject.tag == "Pickup" || c.gameObject.tag == "Box")
                        {
                            col = c;
                        }
                    }
                }
                if (col)
                {
                    if (col.gameObject.tag == "Box")
                    {
                        col.GetComponent<boxRandom>().held = true;
                    }
                    if (col.gameObject.tag == "Player")
                    {
                        col.gameObject.GetComponent<Player>().animator.SetBool("Held", true);
                        col.gameObject.GetComponent<Player>().dontMove = true;
                    }
                    heldObject = col.gameObject;
                    heldObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
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
        GameObject ropei = Instantiate(ropeDeb);
        Vector3 temp = this.transform.position;

        Vector3 temp2 = this.transform.localScale;
        temp2.x = direction();
        ropei.transform.localScale = temp2;
        temp.x = temp.x + direction() * 0.8f;
        temp.y = temp.y - 0.373f;
        ropei.transform.position = temp;
        listRope.Add(ropei);

        Vector3 tempWhile = ropei.transform.position;
        float ftemp = 0.0f;

        while (!Physics2D.OverlapBox(new Vector2(tempWhile.x, tempWhile.y - 1), new Vector2(0.2409988f, 0.358952f), 0, decorLayer) && ftemp != -10.0f)
        {
            ftemp -= 1.0f;
            ropei = Instantiate(rope);
            temp = this.transform.position;
            temp2 = this.transform.localScale;

            ropei.transform.parent = listRope[0].transform;

            ropei.transform.localScale = temp2;
            temp.x = temp.x + direction() * 0.8f;
            temp.y = temp.y - 0.373f + ftemp;
            ropei.transform.position = temp;

            listRope.Add(ropei);
            tempWhile = ropei.transform.position;
        }


        BoxCollider2D c2D = listRope[0].AddComponent<BoxCollider2D>();
        c2D.isTrigger = true;
        c2D.size = new Vector2(0.25f, listRope.Count);
        c2D.offset = new Vector2(0, -listRope.Count / 2);
    }

    void ropeActionReverse()
    {
        if (listRope.Count > 0)
        {
            for (int i = 1; i < listRope.Count; i++)
                Destroy(listRope[i]);
            Destroy(listRope[0]);
            listRope.Clear();
        }
    }

    void touchLadder()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(boxCollider.transform.position, boxCollider.size, 0);

        onLadder = false;
        foreach (Collider2D c in cols)
            if (c.gameObject.tag == "rope")
                onLadder = true;

        animator.SetBool("Climb", onLadder);
        if (onLadder)
        {
            if (Input.GetKey(toucheHaut) || manetteUp())
                speed = new Vector2(0, acceleration * climbSpeed);
            else if (Input.GetKey(toucheAccroupi) || manetteDown())
                speed = new Vector2(0, -acceleration * climbSpeed);

            //rb.gravityScale = 0; Gravity scale ------------------------------------------------------------------------------
        }
        else
        {
            //rb.gravityScale = gravityScale; Gravity scale ------------------------------------------------------------------------------
            animator.SetBool("Climb", false);
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
        return (Physics2D.OverlapBox(new Vector2(feet.transform.position.x, feet.transform.position.y), feet.size, 0, decorLayer));
    }

    bool isTouchinGroundRayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 5, decorLayer);
        this.transform.Translate(0, 1f, 0);
        Debug.DrawRay(transform.position, -Vector2.up, Color.black);
        return hit.collider != null;
    }



    bool isFacingWall()
    {
        return (Physics2D.OverlapBox(new Vector2(face.transform.position.x, face.transform.position.y), face.size, 0, decorLayer));
    }

    bool isTouchingRoof()
    {
        return (Physics2D.OverlapBox(new Vector2(head.transform.position.x, head.transform.position.y), head.size, 0, decorLayer));
    }

    void blinkAnimation()
    {
        if (lastBlinkNumber > (int)(5 * invincible))
        {
            lastBlinkNumber = (int)(5 * invincible);
            sprite.color = new Color(1, 1, 1, sprite.color.a == 1 ? 0 : 1);
        }
    }

    bool manetteDown()
    {
        return Input.GetAxis(manetteAxeY) > 0.5;
    }
    bool manetteUp()
    {
        return Input.GetAxis(manetteAxeY) < -0.5;
    }
    bool manetteLeft()
    {
        return Input.GetAxis(manetteAxeX) < -0.5;
    }
    bool manetteRight()
    {
        return Input.GetAxis(manetteAxeX) > 0.5;
    }
    bool manetteRT()
    {
        return Input.GetAxis(triggAxis) < -0.5;
    }
    #endregion

}
