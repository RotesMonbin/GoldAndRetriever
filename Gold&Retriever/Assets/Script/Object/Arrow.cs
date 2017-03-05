using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float force;

    [SerializeField]
    private float amplitude;



    [SerializeField]
    private LayerMask decors;
    [SerializeField]
    private GameObject box;



    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tirNormal(float direction)
    {
        rb.gravityScale = force / 5;
        rb.velocity = new Vector2((rb.velocity.x + force) * direction, Mathf.Sin(rb.velocity.y) * force + amplitude);

        this.transform.localScale = new Vector2(this.transform.localScale.x * direction, this.transform.localScale.y); 
        
        if (Physics2D.OverlapBox(new Vector2(box.transform.position.x, box.transform.position.y), new Vector2(0.123973f, 0.1235231f), 0, decors))
        {
            rb.isKinematic = true; 
        }

    }
}
