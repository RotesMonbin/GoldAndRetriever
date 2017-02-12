using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectSeller : MonoBehaviour {

    [SerializeField]
    private int price;

    [SerializeField]
    [Header("1 : coeur, 2 : bombe, 3 : arc, 4 : ...")]
    private int numeroObject;

    private ManagerJoueur managerJoueur;

    // Use this for initialization
    void Start () {
        managerJoueur = GameObject.Find("ManagerJoueur").GetComponent<ManagerJoueur>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    #region gestion argent 
    public void gestion_cash(bool J1actif)
    {
        if (price <= managerJoueur.get_cash(J1actif)) 
        {
            procedureAchat(J1actif); 
        }else
        {
            //Debug.Log("Pas assez d'argent !!!"); 
            // afficher le mec qui dis pas assez d'argent bordel ! 
        }
    }

    void procedureAchat(bool J1actif)
    {
        // life max up 
        if(numeroObject == 1)
        {
            managerJoueur.cashDown(price, J1actif);
            managerJoueur.setLifeMax(1, J1actif);
            Destroy(this.gameObject); 
        }

        // bombe
        if (numeroObject == 2)
        {
            managerJoueur.cashDown(price, J1actif);
            managerJoueur.bombeUp(1, J1actif); 
            Destroy(this.gameObject);
        }
        if (numeroObject == 3)
        {
            managerJoueur.cashDown(price, J1actif);
            managerJoueur.changementItemUtile(objectUtile.arc, J1actif);
            Destroy(this.gameObject);
        }

    }

    #endregion
}
