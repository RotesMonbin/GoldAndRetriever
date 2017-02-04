using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worms : MonoBehaviour {

    public Rigidbody2D rb;
    private int direction;
    public float speed = 3;
    public LayerMask decor;
    public GameObject head;
    public GameObject eye;
    public Animator anime;

    private bool falling = false;

    // Use this for initialization
    void Start () {
        direction =-(int)this.transform.localScale.x;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (rb)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
        wallDetection();
        edgeDetection();

    }
    void edgeDetection()
    {
        if(!Physics2D.OverlapCircle(new Vector2(head.transform.position.x, head.transform.position.y)
            ,0.1f, decor)){
            if (!falling)
            {
                changeDirection();
                falling = true;
            }
        }
        else
        {
            falling = false;
        }
    }
    void wallDetection()
    {
        if (Physics2D.OverlapCircle(new Vector2(eye.transform.position.x, eye.transform.position.y)
            , 0.1f, decor))
        {
            changeDirection();
        }
    }
    void changeDirection()
    {
        this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y,
            this.transform.localScale.z);
        direction = -direction;
    }
    public void dead()
    {
        Destroy(this.GetComponent<Rigidbody2D>());
        if (anime)
        {
            anime.SetBool("dead", true);
        }
    }
}
