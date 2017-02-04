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

        int rLigne = 2; Random.Range(2, 7);
        int rColonne = 2; Random.Range(2, 5);

        int sens = 1; // -1 = inverser 

        int pointeurLigne = 40;
        int pointeurColonne = 0;

        for(int iColonne = 0; iColonne < rColonne; iColonne++ )
        {
            if(sens == -1 )
            {
                pointeurLigne = pointeurLigne - 20;
                // LIGNE +-------------------------------
                for (int iLigne = rLigne - 1 ; iLigne >= 0; iLigne--)
                {
                    pointeurLigne = pointeurLigne * iLigne + (iLigne == 0 ? 79 : 0);
                    int randomMilieu = Random.Range(0, TabMilieu.Count);
                    InstanciateGameObjRandom(TabMilieu[randomMilieu], new Vector3(pointeurLigne, pointeurColonne, 0), sens, true);
                    
                }
            }
            else
            {
                // LIGNE +-------------------------------
                for (int iLigne = 0; iLigne < rLigne; iLigne++)
                {
                    if (iColonne == 0 && iLigne == 0)
                        InstanciateGameObjRandom(TabDebGrotte, new Vector3(0, 0, 0), sens, false);
                    else
                    {
                        pointeurLigne = pointeurLigne * iLigne;
                        int randomMilieu = Random.Range(0, TabMilieu.Count);
                        InstanciateGameObjRandom(TabMilieu[randomMilieu], new Vector3(pointeurLigne, pointeurColonne, 0), sens, false);
                    }
                }
            }
           

            sens = sens == 1 ? -1 : 1;

            pointeurLigne = pointeurLigne + 79;
            // COLONNEEE+------------------------------ DRoite
            if (rColonne > 1 && iColonne < rColonne - 1 )
            {
                pointeurLigne += 20;
                int randomCoin = Random.Range(0, TabCoin.Count);
                InstanciateGameObjRandom(TabCoin[randomCoin], new Vector3(pointeurLigne, pointeurColonne, 0), sens, true);
                pointeurColonne = pointeurColonne + HAUTEURMAX - 1;
            }
            
        }
                
         if(sens == 1)
            InstanciateGameObjRandom(TabFin, new Vector3(pointeurLigne -198, pointeurColonne, 0), sens, false);
        else 
            InstanciateGameObjRandom(TabFin, new Vector3(pointeurLigne , pointeurColonne, 0), sens,false);

    }
	
	// Update is called once per frame
	void Update () {
	}

    void InstanciateGameObjRandom(GameObject Go , Vector3 position, int direction, bool coin)
    {
        GameObject gameobjTemp = Instantiate(Go);

        Vector3 temp = gameobjTemp.transform.localScale;
        temp.x = direction;


        if (direction == -1 && !coin)
            position.x += 40;
        if (direction == -1 && coin)
            position.x -= 40;

        gameobjTemp.transform.localScale = temp;
        gameobjTemp.transform.position = position;
    }
}
