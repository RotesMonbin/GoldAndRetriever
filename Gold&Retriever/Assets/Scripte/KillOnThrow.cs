using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnThrow : MonoBehaviour
{

    public CapsuleCollider2D capsColl;
    public LayerMask isObject;
    public float speedToKill = 1;
    public Animator animator;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        objectCollision();
    }

    private void objectCollision()
    {
        Collider2D[] cols = Physics2D.OverlapCapsuleAll(capsColl.transform.position, capsColl.size, capsColl.direction, 0, isObject);
        foreach (Collider2D c in cols)
        {
            Rigidbody2D rb_tmp = c.GetComponent<Rigidbody2D>();
            if (rb_tmp)
            {
                if (Mathf.Abs(rb_tmp.velocity.x) + Mathf.Abs(rb_tmp.velocity.y) > speedToKill)
                {
                    Destroy(this.GetComponent<Rigidbody2D>());
                    if (animator)
                    {
                        animator.SetBool("dead", true);
                    }
                }
            }
        }
    }
}
