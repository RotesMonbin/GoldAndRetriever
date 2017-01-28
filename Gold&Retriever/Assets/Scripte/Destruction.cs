using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour {

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
