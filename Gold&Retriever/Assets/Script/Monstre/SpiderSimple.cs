using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSimple : Monstre
{ 
    [Header("Animation")]
    [SerializeField]
    private Animator anime;

    [Header("Associer à l'araigné")]
    [SerializeField]
    private float distance;
    [SerializeField]
    private float speed = 3;

    private float timeActif;
    private bool posbool;
    private Vector3 posInit;

    [SerializeField]
    private float modeSpider = 1;

    private float timeDelta = 0;

    [SerializeField]
    private GameObject web; 
    // Use this for initialization
    void Start()
    {
        timeActif = 0;
        posInit = this.transform.position;
        posbool = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dt = 0.1f * speed;
        float posYMax = posInit.y - distance;
        float posYActu = this.transform.position.y;

        if (posYActu > posYMax && !posbool)
        {
            this.transform.position = new Vector2(this.transform.position.x, posYActu - dt);
            if(modeSpider == 1)
            {
                web.transform.localScale = new Vector2(web.transform.localScale.x, web.transform.localScale.y + (speed * 0.05f) / 0.5f);
                web.transform.position = new Vector2(web.transform.position.x, web.transform.position.y + (speed * 0.025f) / 0.5f);
            }
            
            timeActif += dt;
        }
        else
        {
            posbool = true;
        }


        if (posbool && posYActu < posInit.y)
        {

            this.transform.position = new Vector2(this.transform.position.x, posYActu + dt);
            if (modeSpider == 1)
            {
                web.transform.localScale = new Vector2(web.transform.localScale.x, web.transform.localScale.y - (speed * 0.05f) / 0.5f);
                web.transform.position = new Vector2(web.transform.position.x, web.transform.position.y - (speed * 0.025f) / 0.5f);
            }

            timeActif += dt;
        }
        else
        {
            posbool = false;
        }
        
    }

  


    #region dead
    public override void dead()
    {
        this.gameObject.layer = 0;
        Destroy(this.gameObject);

    }
    #endregion
}
