using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum objectUtile{
    none,
    arc,
    spear
}
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

    // Object actuel
    private objectUtile objetUtileActuelJ1 = objectUtile.none;
    private objectUtile objetUtileActuelJ2 = objectUtile.none;
    private HudObjectUtile HUDitemJ1 ;
    private HudObjectUtile HUDitemJ2 ;

    private ChargementArrow chargementBarreJ2;

    public bool test = false;

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
        if (!test)
        {
            changementScene(1);
        }

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
    public void cashDown(int cash, bool J1actif)
    {
        if (J1actif)
            cashJ1 -= cash;
        else
            cashJ2 -= cash;

        hudJ1Cash.text = "" + cashJ1;
        hudJ2Cash.text = "" + cashJ2;
    }

    public int get_cash(bool J1actif)
    {
        return J1actif ? cashJ1 : cashJ2;  
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
            lifeScript_J2.LifeAff();
        }
    }

    public int getLifeMax(bool J1actif)
    {
        return J1actif ? lifeJ1Max : lifeJ2Max;
    }

    public void setLifeMax(int lifeplus , bool J1actif)
    {
        
        if(J1actif)
            lifeJ1Max += lifeplus; 
        else
            lifeJ2Max += lifeplus;

        lifeUp(lifeplus, J1actif); 
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

            HUDitemJ1 = GameObject.Find("obj DedansJ1").GetComponent<HudObjectUtile>();
            HUDitemJ2 = GameObject.Find("obj DedansJ2").GetComponent<HudObjectUtile>();

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

            chargementBarreJ2 = GameObject.Find("hudItemCaseLoadArrow").GetComponent<ChargementArrow>(); 

            hudJ1Bombe.text = "" + bombeJ1;
            hudJ2Bombe.text = "" + bombeJ2;

            hudJ1Cash.text = "" + cashJ1;
            hudJ2Cash.text = "" + cashJ2;

            changementItemUtile(objectUtile.spear, true);
            changementItemUtile(objectUtile.arc, false);
        }

    }
    #endregion

    #region Object utile , ex : arc ...
    public void changementItemUtile(objectUtile obj, bool J1actif)
    {
        if(J1actif)
        {
            objetUtileActuelJ1 = obj;
            HUDitemJ1.changement(obj);
        }else
        {
            objetUtileActuelJ2 = obj;
            HUDitemJ2.changement(obj);
        }
    }

    public void rechargementArrow(float tempsRecharge)
    {
        chargementBarreJ2.lancementChargement(tempsRecharge);
    }

    #endregion 
}
