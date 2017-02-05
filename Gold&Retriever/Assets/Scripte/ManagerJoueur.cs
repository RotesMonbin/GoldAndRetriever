using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ManagerJoueur : MonoBehaviour
{

    // Vie : 
    private GameObject Joueur1;

    private int lifeJ1 = 3;
    private int lifeJ1Max = 3;

    private GameObject Joueur2;
    private int lifeJ2 = 3;
    private int lifeJ2Max = 3;

    // cash
    private int cashJ1 = 0;
    private int cashJ2 = 0;

    private Text hudJ1Cash;
    private Text hudJ2Cash;

    // Bombe :
    private int bombeJ1 = 2;
    private int bombeJ2 = 2;


    private Text hudJ1Bombe;
    private Text hudJ2Bombe;

    private Life lifeScript_J1;
    private Life lifeScript_J2;

    private GameObject hudJ1GameOver;
    private GameObject hudJ2GameOver;

    // Gem :
    private bool gem = false;
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
        else
        {
            me = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    // Use this for initialization
    void Start()
    {
        changementScene(1);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Restart()
    {
        lifeJ1 = 3;
        lifeJ1Max = 3;

        lifeJ2 = 3;
        lifeJ2Max = 3;

        // cash
        cashJ1 = 0;
        cashJ2 = 0;

        // Bombe :
        bombeJ1 = 2;
        bombeJ2 = 2;

        // Gem :
        gem = false;
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
    public void lifeDown(int lifeReduice, bool J1actif)
    {

        if (J1actif)
        {
            lifeJ1 -= lifeReduice;
            lifeScript_J1.LifeAff();
        }
        else
        {
            lifeJ2 -= lifeReduice;
            lifeScript_J2.LifeAff();
        }

        if (lifeJ1 <= 0 || lifeJ2 <= 0)
        {
            hudJ1GameOver.SetActive(true);
            hudJ2GameOver.SetActive(true);
        }
    }

    public void lifeUp(int lifeInscrease, bool J1actif)
    {
        if (J1actif)
        {

            lifeJ1 += lifeInscrease;
            lifeScript_J1.LifeAff();
        }
        else
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

        if (J1actif)
        {
            if (bombeJ1 > 0)
                return true;
            else
                return false;
        }
        else
        {
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
        if (sceneID == 0)
        {
            SceneManager.LoadScene("SceneStart");
        }
        else if (sceneID == 1)
        {
            Restart();
            SceneManager.LoadScene("SceneMenu");
        }
        else if (sceneID == 2)
        {
            SceneManager.LoadScene("SceneJeuAleatoire1");
        }
        else if (sceneID == 3)
        {
            SceneManager.LoadScene("SceneMenu fin");
        }
    }

    public void chargementDonnee()
    {

        if (Joueur1 == null)
        {
            Joueur1 = GameObject.Find("P1----------------------------------");
            Joueur2 = GameObject.Find("P2-----------------------------------");

            hudJ1Bombe = GameObject.Find("BombeJ1").GetComponent<Text>();
            hudJ2Bombe = GameObject.Find("BombeJ2").GetComponent<Text>();

            hudJ1Cash = GameObject.Find("CashJ1").GetComponent<Text>();
            hudJ2Cash = GameObject.Find("CashJ2").GetComponent<Text>();

            if (hudJ1GameOver == null)
            {
                hudJ1GameOver = GameObject.Find("GameOverJ1");
                hudJ2GameOver = GameObject.Find("GameOverJ2");
                hudJ1GameOver.SetActive(false);
                hudJ2GameOver.SetActive(false);
            }

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
