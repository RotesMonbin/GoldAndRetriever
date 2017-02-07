using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendeurGestion : MonoBehaviour {

    [SerializeField]
    private GameObject texte;

    [SerializeField]
    private List<GameObject> listObject;

    [SerializeField]
    private GameObject postionObject;

    // Use this for initialization
    void Start () {
        int r = Random.Range(0, listObject.Count);
        GameObject go = Instantiate(listObject[r]);
        go.transform.position = new Vector2(postionObject.transform.position.x + 2, postionObject.transform.position.y);
        go.transform.parent = postionObject.transform;

        r = Random.Range(0, listObject.Count);
        go = Instantiate(listObject[r]);
        go.transform.position = new Vector2 (postionObject.transform.position.x + 4, postionObject.transform.position.y) ;
        go.transform.parent = postionObject.transform;

        r = Random.Range(0, listObject.Count);
        go = Instantiate(listObject[r]);
        go.transform.position = new Vector2(postionObject.transform.position.x + 6, postionObject.transform.position.y);
        go.transform.parent = postionObject.transform;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
