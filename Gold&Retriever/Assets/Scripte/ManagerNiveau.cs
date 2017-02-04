using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerNiveau : MonoBehaviour {

    // chunk milieu = 40 largeur , 20 hauteur 
    // chunk Coin = 20 largeur , 40 hauteur 
    private int HAUTEURMAX = -20;
    private int LARGEURMAX = 40;
    // Tableau de prefable Milieu de map : 
    public List<GameObject> TabMilieu;

    // Tableau de prefable coin de map : 
    public List<GameObject> TabCoin;


    // Tableau de prefable coin de map : 
    public GameObject TabDeb;
    public GameObject TabDebGrotte;

    public GameObject TabFin;

    // Player 1 
    public GameObject Player1;  
	public GameObject Player2;

    public GameObject laveGame; // un seul à gerer le parent de tout les prefabLave
    public GameObject prefabLave;

    // Use this for initialization
    void Start () {

        int rLigne = Random.Range(2, 7);
        int rColonne = 2; Random.Range(2, 5);

        int sens = 1; // -1 = inverser 

        int HauteurColonne = HAUTEURMAX;
        int tailleLigne = LARGEURMAX;

        for (int i = 0; i < rColonne;  i++)
        {
            HauteurColonne = i * HauteurColonne;
            // ligne 0 

            tailleLigne = 0;
            for (int j = 0; j < rLigne; j++)
            {
                if (i == 0 && j == 0)
                    InstanciateGameObjRandom(TabDebGrotte, new Vector3(0, 0, 0), sens);
                else
                {
                    tailleLigne = j * LARGEURMAX;

                    Vector3 vLignetemp = new Vector3(tailleLigne, HauteurColonne, 0);

                    int randLigne = Random.Range(0, TabMilieu.Count);
                    InstanciateGameObjRandom(TabMilieu[randLigne], vLignetemp, sens);
                }
              
            }

            tailleLigne += LARGEURMAX + 20;
            Debug.Log(" LIGNE : " + tailleLigne);
            sens = sens == -1 ? 1 : -1;
            int randColonne = Random.Range(0, TabCoin.Count );
            Vector3 vCointemp = new Vector3(tailleLigne, HauteurColonne, 0);

            InstanciateGameObjRandom(TabCoin[randColonne], vCointemp, sens);

            HauteurColonne += HAUTEURMAX;
        }


        /*
        Instantiate (TabDebGrotte).transform.position = new Vector3(0,1,0);
        Instantiate (TabMilieu [0]).transform.position = new Vector3(40,1,0); 

		Instantiate (TabMilieu [1]).transform.position = new Vector3(40,-20,0); 

		GameObject i3 = Instantiate (TabCoin [0]); 

		Vector3 temp = i3.transform.localScale; 
		temp.x = -1; 
		i3.transform.localScale = temp; 
	
		i3.transform.position = new Vector3(100,1,0); 

	
		//Instantiate (TabMilieu [1]).transform.position = new Vector3(40,-20,0); 
	
		Instantiate (TabCoin [0]).transform.position = new Vector3(20,-20,0); */

	}
	
	// Update is called once per frame
	void Update () {
	}

    void InstanciateGameObjRandom(GameObject Go , Vector3 position, int direction)
    {
        GameObject gameobjTemp = Instantiate(Go);

        Vector3 temp = gameobjTemp.transform.localScale;
        temp.x = direction;

 
        gameobjTemp.transform.localScale = temp;
        gameobjTemp.transform.position = position;
    }
}
