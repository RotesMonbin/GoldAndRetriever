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

        int rLigne =  Random.Range(2, 5);
        int rColonne = Random.Range(3, 5);

        int sens = 1; // -1 = inverser 

        int indiceLigne = 40;
        int indiceColonne = 1;

        InstanciateGameObjRandom(TabDebGrotte , new Vector3(0,1,0) , 1 , false);


        for (int i = 1;  i <=  rColonne; i++)
        {
            if (sens == 1)
            {
                // ligne 
                for (int j = 1; j <= rLigne; j++)
                {
                    int r = Random.Range(0, TabMilieu.Count); 
                    InstanciateGameObjRandom(TabMilieu[r], new Vector3(indiceLigne, indiceColonne, 0), sens, false);
                    indiceLigne = indiceLigne + 40;
                }

                int rCoin = Random.Range(0, TabCoin.Count);
                InstanciateGameObjRandom(TabCoin[rCoin], new Vector3(indiceLigne, indiceColonne, 0), -sens, true);

            }
            else
            {
                indiceLigne = indiceLigne - 41;
                // ligne Autre sens
                for (int j = 1; j <= rLigne; j++)
                {
                    int r = Random.Range(0, TabMilieu.Count);
                    InstanciateGameObjRandom(TabMilieu[r], new Vector3(indiceLigne, indiceColonne, 0), sens, false);
                    indiceLigne = indiceLigne - 40;
                }

                int rCoin = Random.Range(0, TabCoin.Count);
                InstanciateGameObjRandom(TabCoin[rCoin], new Vector3(indiceLigne+20, indiceColonne, 0), -sens, true);
                
            }
            sens = sens == 1 ? -1 : 1;
            indiceColonne = indiceColonne - 21;
        }

       

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
            position.x += 20;

        gameobjTemp.transform.localScale = temp;
        gameobjTemp.transform.position = position;
    }
}
