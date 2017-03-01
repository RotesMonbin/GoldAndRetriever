using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonDouble : MonoBehaviour {

    [SerializeField]
    private GameObject porte ;

    [SerializeField]
    private bool actif1 = false ;

    public bool actif2 = false ;

    [SerializeField]
    private Transform traFini;

    private Transform traBase;

    [SerializeField]
    private List<GameObject> DestroyS;

    [SerializeField]
    private float timeActif = 0.5f;

    // Use this for initialization
    void Start () {
        traBase = porte.transform; 

    }
	
	// Update is called once per frame
	void Update () {
        testles2(); // a surment enlever 

    }

    public void setActif1()
    {
        actif1 = true;
        testles2(); 
    }
    public void setActif2()
    {
        actif2 = true;
        testles2(); 
    }

    public void setDesActif1()
    {
        actif1 = false;
        testles2();
    }
    public void setDesActif2()
    {
        actif2 = false;
        testles2(); 
    }

    void testles2()
    {


        if ((!actif1 || !actif2) && porte.transform.position != traBase.position)
        {

            porte.transform.position = Vector2.Lerp(traFini.position, traBase.position, Time.deltaTime * timeActif);
        }

        if (actif1 && actif2)
        {
            porte.transform.position = Vector2.Lerp(traBase.position, traFini.position, Time.deltaTime * timeActif); 
        }
    }
}
