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

    public GameObject laveGame; // un seul à gerer le parent de tout les prefabLave
    public GameObject prefabLave;

    // Use this for initialization
    void Start () {

        int maxLigne = 4;
        int maxColonne = 4;

        int rLigne = Random.Range(2, maxLigne);
        int rColonne = Random.Range(2, maxColonne);

        int sens = 1; // -1 = inverser 

        int indiceLigne = 40;
        int indiceColonne = 1;

        InstanciateGameObjRandom(TabDebGrotte , new Vector3(0,1,0) , 1 , 0);
        
        for (int i = 1;  i <=  rColonne; i++)
        {
            rLigne = Random.Range(2, maxLigne);
            if (sens == 1)
            {
                // ligne 
                for (int j = 1; j <= rLigne; j++)
                {
                    int r = Random.Range(0, TabMilieu.Count); 
                    InstanciateGameObjRandom(TabMilieu[r], new Vector3(indiceLigne, indiceColonne, 0), sens, 0);
                    indiceLigne = indiceLigne + 40;
                }

                int rCoin = Random.Range(0, TabCoin.Count);
                InstanciateGameObjRandom(TabCoin[rCoin], new Vector3(indiceLigne, indiceColonne, 0), -sens, 1);

            }
            else
            {
               // indiceLigne = indiceLigne;
                // ligne Autre sens
                for (int j = 1; j <= rLigne; j++)
                {
                    indiceLigne = indiceLigne - 40;
                    int r = Random.Range(0, TabMilieu.Count);
                    InstanciateGameObjRandom(TabMilieu[r], new Vector3(indiceLigne, indiceColonne, 0), -sens, 0);
                    
                }

                int rCoin = Random.Range(0, TabCoin.Count);
                InstanciateGameObjRandom(TabCoin[rCoin], new Vector3(indiceLigne-20, indiceColonne, 0), -sens, 1);
                
            }
            sens = sens == 1 ? -1 : 1;
            indiceColonne = indiceColonne - 21;
        }
        
        InstanciateGameObjRandom(TabFin, new Vector3(indiceLigne , indiceColonne, 0), -sens, 2);


        indiceColonne = indiceColonne - 1; 
        int posXLave = -20;
        laveGame.transform.position = new Vector3(posXLave, indiceColonne); 
        for (int i= 0; i < rColonne + 4; i++)
        {
            LaveSpawn(new Vector3(posXLave, indiceColonne ), 0);
            posXLave += 40;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
	}

    void InstanciateGameObjRandom(GameObject Go , Vector3 position, int direction, int milCoinFin)
    {
        GameObject gameobjTemp = Instantiate(Go);

        Vector3 temp = gameobjTemp.transform.localScale;
        temp.x = direction;


       /* if (direction == -1 && milCoinFin == 0)
            position.x += 40;*/

        if (direction == -1 && milCoinFin == 1)
            position.x += 20;

        if (direction == -1 && milCoinFin == 2)
            position.x += 40;   
        if (direction == 1 && milCoinFin == 2)
            position.x -= 40;


        gameobjTemp.transform.localScale = temp;
        gameobjTemp.transform.position = position;
    }

    void LaveSpawn(Vector3 position, int timeLave)
    {
        GameObject gameobjTemp = Instantiate(prefabLave);
        gameobjTemp.transform.parent = laveGame.transform; 
        gameobjTemp.transform.position = position;

    }
}
