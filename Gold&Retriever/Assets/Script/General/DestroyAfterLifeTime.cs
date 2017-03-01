using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyAfterLifeTime : MonoBehaviour {

    [SerializeField]
    private float lifeTime; 

    // Use this for initialization
    void Start () {
        Destroy(this.gameObject, lifeTime); 
	}
	
}
