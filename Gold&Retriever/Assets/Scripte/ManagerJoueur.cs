using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerJoueur : MonoBehaviour {

	// Vie : 
	[SerializeField]
	private GameObject Joueur1; 
	private int lifeJ1; 

	[SerializeField]
	private GameObject Joueur2; 
	private int lifeJ2; 

	// cash
	private int cashJ1; 
	private int cashJ2; 

	[SerializeField]
	private Text hudJ1Cash; 
	[SerializeField]
	private Text hudJ2Cash; 

	// Use this for initialization
	void Start () {
		
		lifeJ1 = 3; 
		lifeJ2 = 3; 
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
			lifeJ1 += lifeReduice; 
		} else 
		{
			lifeJ2 += lifeReduice; 
		}
	}
	#endregion
}
