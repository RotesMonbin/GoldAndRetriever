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
    private LayerMask enemy;

    [SerializeField]
    private BoxCollider2D box;

    [SerializeField]
    private bool arrowOrNot = false; 

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

            Collider2D coll = Physics2D.OverlapBox(box.transform.position, box.size, 0, enemy);
            if (coll != null)
            {
                //Faire une classe mère pour les ennemies
                if (arrowOrNot)
                    Destroy(this.gameObject); 
                Destroy(coll.gameObject);
            }
            
        }
    }

    public void tirNormal(float direction, float indicePuissance)
    {
        rb.AddForce(new Vector2((100 + (indicePuissance > 0.5f ? 200 : 50 ) * indicePuissance) * direction, 20 + (indicePuissance > 0.5f ? 150 : 50) * (1 - indicePuissance)));
    }

    public void tirHaut(float indicePuissance)
    {
    
        rb.AddForce(new Vector2(1, 100 + 100 * indicePuissance));
    }
}
