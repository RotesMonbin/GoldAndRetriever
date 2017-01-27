using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerJoueur : MonoBehaviour {

	// Vie : 
	[SerializeField]
	private GameObject Joueur1; 
	private int lifeJ1; 
	private int lifeJ1Max= 3; 

	[SerializeField]
	private GameObject Joueur2; 
	private int lifeJ2; 
	private int lifeJ2Max= 3; 

	// cash
	private int cashJ1; 
	private int cashJ2; 

	[SerializeField]
	private Text hudJ1Cash; 
	[SerializeField]
	private Text hudJ2Cash; 

	// Bombe :
	private int bombeJ1; 
	private int bombeJ2; 

	[SerializeField]
	private Text hudJ1Bombe; 
	[SerializeField]
	private Text hudJ2Bombe; 

	// Use this for initialization
	void Start () {
		
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

	#region life
	public void lifeDown(int lifeReduice ,  bool J1actif) 
	{
		if (J1actif) 
		{
			lifeJ1 -= lifeReduice; 
		} else 
		{
			lifeJ2 -= lifeReduice; 
		}
	}

	public void lifeUp(int lifeInscrease ,  bool J1actif) 
	{
		if (J1actif) 
		{
			lifeJ1 += lifeInscrease; 
		} else 
		{
			lifeJ2 += lifeInscrease; 
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
}
