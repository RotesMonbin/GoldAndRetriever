using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private LayerMask decors;
    [SerializeField]
    private GameObject box;

    /*[SerializeField]
    private Transform sprite;
    */
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!(Mathf.Abs(rb.velocity.y + rb.velocity.x) < 2 ))
        {
            Vector2 dir = rb.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void tirNormal(float direction, float indicePuissance)
    {
        indicePuissance = 1 - indicePuissance;
        rb.AddForce(new Vector2((300 + 300 * indicePuissance) * direction, 50 + 100 * (1 - indicePuissance)));
    }

    public void tirHaut(float indicePuissance)
    {
        rb.AddForce(new Vector2(1, 300 + 100 * indicePuissance));
    }
}
