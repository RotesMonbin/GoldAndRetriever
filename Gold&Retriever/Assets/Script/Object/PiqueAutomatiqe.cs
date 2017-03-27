using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiqueAutomatiqe : MonoBehaviour
{
    [SerializeField]
    private float distance;
    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private float timeEnBas = 3;
    private float tempTimeBas = 0;

    private float timeActif;
    private bool posbool;
    private Vector3 posInit;

    private float timeDelta = 0;

    private bool bool_actif_timer = false;

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
        
       // Debug.Log("time : " + tempTimeBas);
        if (tempTimeBas > 0.01f && bool_actif_timer )
        {
            tempTimeBas -= Time.deltaTime;
            bool_actif_timer = false;
        }
        else
        {
            
            float dt = 0.1f * speed;
            float posYMax = posInit.y - distance;
            float posYActu = this.transform.position.y;
            

            if (posYActu > posYMax && !posbool )
            {
                this.transform.position = new Vector2(this.transform.position.x, posYActu - dt);
                timeActif += dt;

            }
            else
            {
                posbool = true;
                bool_actif_timer = false;
                tempTimeBas = timeEnBas;
            }


            if (posbool && posYActu < posInit.y)
            {
                this.transform.position = new Vector2(this.transform.position.x, posYActu + dt);       
                timeActif += dt;
              
            }
            else
            {
                posbool = false;
                bool_actif_timer = true;

            }

        }


    }




    #region dead
    public void dead()
    {
        this.gameObject.layer = 0;
        Destroy(this.gameObject);

    }
    #endregion
}
