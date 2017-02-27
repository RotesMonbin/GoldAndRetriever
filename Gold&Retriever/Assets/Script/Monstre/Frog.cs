using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    public Rigidbody2D rb;
    public BoxCollider2D feet;
    public LayerMask decors;
    public Animator anime;

    public float timeToJump;
    private float timer;
    private int direction;
    // Use this for initialization
    void Start()
    {
        Transform parent = this.gameObject.transform.parent;
        if (parent)
        {
            direction = -(int)parent.localScale.x;
        }
        else
        {
            direction = -1;
        }

        timer = timeToJump;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb)
        {
            if (timer <= 0 && isTouchingGround())
            {
                int r = Random.Range(0, 2);
                this.transform.localScale = new Vector3(r == 0 ? -1 : 1, 1, 1);
                rb.velocity = new Vector2(direction*this.transform.localScale.x * 4, 20);
                timer = timeToJump;
            }

            if (isTouchingGround())
            {
                timer -= Time.deltaTime;
            }
            anime.SetBool("jump", !isTouchingGround());
        }
    }

    bool isTouchingGround()
    {
        return (Physics2D.OverlapBox(new Vector2(feet.transform.position.x, feet.transform.position.y), feet.size, 0, decors));
    }

    public void dead()
    {
        this.gameObject.layer = 0;
        Destroy(this.GetComponent<Rigidbody2D>());
        if (anime)
        {
            anime.SetBool("dead", true);
        }
    }
}
