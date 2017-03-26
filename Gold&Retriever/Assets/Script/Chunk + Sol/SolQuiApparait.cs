using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolQuiApparait : MonoBehaviour
{

    [SerializeField]
    private LayerMask player;

    [SerializeField]
    private GameObject box;

    [SerializeField]
    private List <GameObject> listPlateform;

    [SerializeField]
    private float timeBtwSpawn = 1;

    private bool actif = false; 
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Physics2D.OverlapBox(new Vector2(box.transform.position.x, box.transform.position.y), new Vector2(1, 0.2330246f), 0, player) && !actif)
        {
            actif = true; 
            StartCoroutine(PlateformeGogo());
        }
    }


    IEnumerator PlateformeGogo()
    {
        for(int i = 0; i < listPlateform.Count + 3; i++)
        {
            yield return new WaitForSeconds(timeBtwSpawn);
            if (i < listPlateform.Count ) 
                listPlateform[i].SetActive(true); 
            if(i > 2)
                listPlateform[i-3].SetActive(false);
        }
        actif = false; 


    }

}
