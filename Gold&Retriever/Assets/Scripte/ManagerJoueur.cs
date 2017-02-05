using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ManagerJoueur : MonoBehaviour {

	// Vie : 
	private GameObject Joueur1; 

	private int lifeJ1; 
	private int lifeJ1Max= 3; 

	private GameObject Joueur2; 
	private int lifeJ2; 
	private int lifeJ2Max= 3; 

	// cash
	private int cashJ1; 
	private int cashJ2; 

	private Text hudJ1Cash; 
	private Text hudJ2Cash; 

	// Bombe :
	private int bombeJ1; 
	private int bombeJ2; 


	private Text hudJ1Bombe; 
	private Text hudJ2Bombe; 

	private Life lifeScript_J1;  
	private Life lifeScript_J2;

    // Gem :
    private bool gem;
    private GemmeBleuHUD gemGlobal;

    // lave
    private Lave lave;

    // Scene 
    private int scene = 0;
    static ManagerJoueur me;

    void Awake()
    {
        if (me != null)
            DestroyImmediate(this.gameObject);
        else {
            me = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Use this for initialization
    void Start () {

        Joueur1 = GameObject.Find("P1----------------------------------");
        Joueur2 = GameObject.Find("P2-----------------------------------");

        hudJ1Bombe = GameObject.Find("BombeJ1").GetComponent<Text>();
        hudJ2Bombe = GameObject.Find("BombeJ2").GetComponent<Text>();

        hudJ1Cash = GameObject.Find("CashJ1").GetComponent<Text>();
        hudJ2Cash = GameObject.Find("CashJ2").GetComponent<Text>();

        lifeScript_J1 = GameObject.Find("LifeHUDJ1").GetComponent<Life>();
        lifeScript_J2 = GameObject.Find("LifeHUDJ2").GetComponent<Life>();

        gemGlobal = GameObject.Find("GEMME").GetComponent<GemmeBleuHUD>();
        lave = GameObject.Find("LaveDeplacement").GetComponent<Lave>();

        hudJ1Bombe.text = "" + bombeJ1;
        hudJ2Bombe.text = "" + bombeJ2;

        hudJ1Cash.text = "" + cashJ1;
        hudJ2Cash.text = "" + cashJ2;
        gem = false;
        lifeJ1 = 3; 
		lifeJ2 = 3;
		lifeJ1Max = 3;
		lifeJ2Max = 3;

        bombeJ1 = 2;
        bombeJ2 = 2;
        hudJ1Bombe.text = "" + bombeJ1; 
		hudJ2Bombe.text = "" + bombeJ2; 

        cashJ1 = 0;
        cashJ2 = 0;
        
		hudJ1Cash.text = "" + cashJ1; 
		hudJ2Cash.text = "" + cashJ2; 
 	}
	
	// Update is called once per frame
	void Update () {
        
    }
		

	#region cash 
	public void cashUp(int cash, bool J1actif)
	{
		if (J1actif)
			cashJ1 += cash; 
		else 
			cashJ2 += cash; 	
		
		hudJ1Cash.text = "" + cashJ1;
		hudJ2Cash.text = "" + cashJ2;
	}

    #endregion

    #region gem 
    public void gemUp()
    {
        gem = true;
        gemGlobal.gemaff();
        lave.laveLancement(); 
    }

    public bool possedeGem()
    {
        return gem; 
    }

    #endregion

    #region life
    public void lifeDown(int lifeReduice ,  bool J1actif) 
	{

		if (J1actif) 
		{
			//if (lifeJ1 - lifeReduice)
			lifeJ1 -= lifeReduice; 
			lifeScript_J1.LifeAff (); 
		
		} else 
		{
			lifeJ2 -= lifeReduice; 
			lifeScript_J2.LifeAff(); 

		}
	}

	public void lifeUp(int lifeInscrease ,  bool J1actif) 
	{
		if (J1actif) 
		{
          
			lifeJ1 += lifeInscrease;
            lifeScript_J1.LifeAff();
        } else 
		{
			lifeJ2 += lifeInscrease;
            lifeScript_J1.LifeAff();
        }
	}

	public int getLifeMax(bool J1actif)
	{
		return J1actif ? lifeJ1Max : lifeJ2Max;
	}

	public int getLife(bool J1actif)
	{
		return J1actif ? lifeJ1 : lifeJ2;
	}
	#endregion

	#region Bombe
	public void bombeUp(int bombe, bool J1actif)
	{
		if (J1actif)
			bombeJ1 += bombe; 
		else 
			bombeJ2 += bombe; 	

		hudJ1Bombe.text = "" + bombeJ1;
		hudJ2Bombe.text = "" + bombeJ2;
	}

	public void bombeDown(int bombe, bool J1actif)
	{
		if (J1actif)
			bombeJ1 -= bombe; 
		else 
			bombeJ2 -= bombe; 	

		hudJ1Bombe.text = "" + bombeJ1;
		hudJ2Bombe.text = "" + bombeJ2;
	}

	public bool nbBombeOk(bool J1actif)
	{

		if (J1actif) {
			if (bombeJ1 > 0)
				return true;
			else
				return false; 
		} else {
			if (bombeJ2 > 0)
				return true;
			else
				return false; 
		}
	
	}
    #endregion


    #region Gestion Scene 

    // scene 0 ==> menu, scene = 1 gameplay niveau 1
    public void changementScene(int sceneID)
    {
        scene = sceneID; 
        if (sceneID == 1)
        {
            SceneManager.LoadScene("SceneJeuAleatoire1");
        }
        else if(sceneID == 0)
        {
            SceneManager.LoadScene("SceneMenu");
        }
    }

    public void chargementDonnee()
    {

        if(Joueur1 == null)
        {
            Joueur1 = GameObject.Find("P1----------------------------------");
            Joueur2 = GameObject.Find("P2-----------------------------------");

            hudJ1Bombe = GameObject.Find("BombeJ1").GetComponent<Text>();
            hudJ2Bombe = GameObject.Find("BombeJ2").GetComponent<Text>();

            hudJ1Cash = GameObject.Find("CashJ1").GetComponent<Text>();
            hudJ2Cash = GameObject.Find("CashJ2").GetComponent<Text>();

            lifeScript_J1 = GameObject.Find("LifeHUDJ1").GetComponent<Life>();
            lifeScript_J2 = GameObject.Find("LifeHUDJ2").GetComponent<Life>();

            gemGlobal = GameObject.Find("GEMME").GetComponent<GemmeBleuHUD>();
            lave = GameObject.Find("LaveDeplacement").GetComponent<Lave>();

            hudJ1Bombe.text = "" + bombeJ1;
            hudJ2Bombe.text = "" + bombeJ2;

            hudJ1Cash.text = "" + cashJ1;
            hudJ2Cash.text = "" + cashJ2;

        }
      
    }
    #endregion
}
