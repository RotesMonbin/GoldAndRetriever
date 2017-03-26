using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubChunk : MonoBehaviour {

    public List<GameObject> SubChunks;

    // Use this for initialization
    void Awake () {
        int ind = Random.Range(0, SubChunks.Count);
        GameObject instance= Instantiate(SubChunks[ind].gameObject,this.transform.parent.transform);
        instance.transform.position = this.gameObject.transform.position;
        Destroy(this.gameObject);
	}
}
