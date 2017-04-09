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
    private bool destroyOnKill = false;

    private float timeNoGravity;

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

        if (!(Mathf.Abs(rb.velocity.y + rb.velocity.x) < 2))
        {
            if (rb.isKinematic == false)
            {
                Vector2 dir = rb.velocity;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            Collider2D coll = Physics2D.OverlapBox(box.transform.position, box.size, 0);
            if (coll != null)
            {
                //Faire une classe mère pour les ennemies
                if (coll.gameObject.layer == LayerMask.NameToLayer("Enemies"))
                {
                    if (destroyOnKill)
                        Destroy(this.gameObject);
                    Monstre m = coll.GetComponent<Monstre>();
                    if (m != null)
                    {
                        m.dead();
                    }
                }
                else if (coll.gameObject.layer == LayerMask.NameToLayer("decors"))
                {
                    this.enabled = false;
                    rb.velocity = new Vector2(0, 0);
                    rb.angularVelocity = 0;
                    rb.gravityScale = 0;
                    rb.isKinematic = true;
                }

            }

        }
        if (rb.isKinematic == true)
        {
            if (timeNoGravity == 0)
            {
                rb.isKinematic = false;
            }
            else
            {
                timeNoGravity = timeNoGravity - Time.deltaTime <= 0 ? 0 : timeNoGravity - Time.deltaTime;
            }
        }
    }

    public void tirNormal(float direction, float indicePuissance)
    {
        rb.AddForce(new Vector2((100 + (indicePuissance > 0.5f ? 200 : 50) * indicePuissance) * direction, 20 + (indicePuissance > 0.5f ? 150 : 50) * (1 - indicePuissance)));
    }

    public void tirSpear(float direction, float indicePuissance)
    {
        timeNoGravity = 0.2f;
        rb.isKinematic = true;
        if (direction < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0,180);
        }

        rb.velocity = (new Vector2((15+15*indicePuissance) * direction, 0));
    }

    public void tirHaut(float indicePuissance)
    {

        rb.AddForce(new Vector2(1, 100 + 100 * indicePuissance));
    }
}
