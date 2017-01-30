using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour {

    public GameObject toDestroy;
    private void DestroyThis()
    {
        Destroy(toDestroy);
    }
}
