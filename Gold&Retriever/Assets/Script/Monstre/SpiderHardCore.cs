using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderHardCore : MonoBehaviour
{
    [Header("Rigide Body")]
    [SerializeField]
    private Rigidbody2D rb;
    private int direction;

    [SerializeField]
    [Header("Associer à l'araigné")]
    private float speed = 3;
    [SerializeField]
    private float timeAvantDescent;
    [SerializeField]
    private float distanceDeNonAgro = 5;

    private float timeTempDescent = 0;
    //private float timeTempAntiBug = 0;

    [Header("Layer sur quoi il est")]
    [SerializeField]
    private LayerMask decor;
    [SerializeField]
    private LayerMask playerMask;

    [Header("Box monstre")]
    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject headBas;
    [SerializeField]
    private GameObject eye;

    [Header("Animation")]
    [SerializeField]
    private Animator anime;


    [Header("detection Collider")]
    [SerializeField]
    private float distanceDetection;
    private bool falling = false;
    private bool modRage = false; // lorsqu'il voit le joueur il passe en mode je suis le joueur 

    private GameObject Player = null;


    // Use this for initialization
    void Start()
    {
        direction = -(int)this.transform.localScale.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!modRage)
        {
            detectionDessus();
        }
        else
        {
            if (((Player.transform.position.x - this.transform.position.x) < -distanceDeNonAgro) ||
            ((Player.transform.position.x - this.transform.position.x) > distanceDeNonAgro))
                modRage = false;

            timeTempDescent += Time.deltaTime;
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (timeAvantDescent < timeTempDescent)
            {
                rb.gravityScale = 1.2f;
            }
        }

        if (modRage)
        {
            float direction2 = Mathf.Clamp(Player.transform.position.x - this.transform.position.x, -1, 1);
            rb.velocity = new Vector2(direction2 * speed, rb.velocity.y);
        }
        else
        {
            if (rb)
            {
                rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            }
            wallDetection();
            edgeDetection();
        }
    }

    #region Patrouille
    void edgeDetection()
    {
        if (!Physics2D.OverlapCircle(new Vector2(head.transform.position.x, head.transform.position.y)
             , 0.1f, decor) &&
            !Physics2D.OverlapCircle(new Vector2(headBas.transform.position.x, headBas.transform.position.y)
             , 0.1f, decor))
        {
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
        if (!modRage)
        {
            this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y,
            this.transform.localScale.z);
            direction = -direction;
        }
    }
    #endregion

    #region detection
    void detectionDessus()
    {

        Vector2 point = eye.transform.position;
        float xPoint = point.x;
        float yPoint = point.y + 0.7f * -1;

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(xPoint, yPoint), Vector2.down, distanceDetection, playerMask);

        if (hit)
        {
            //Debug.DrawLine(new Vector2(xPoint, yPoint), hit.transform.position, Color.blue);
            modeCharge(hit.collider.gameObject);
        }
    }
    void modeCharge(GameObject Player_)
    {
        Player = Player_;
        modRage = true;
        timeTempDescent = 0;
    }
    #endregion


    #region dead
    public void dead()
    {
        this.gameObject.layer = 0;
        Destroy(this.gameObject);
        if (anime)
        {
            anime.SetBool("dead", true);
        }
    }
    #endregion  
}
