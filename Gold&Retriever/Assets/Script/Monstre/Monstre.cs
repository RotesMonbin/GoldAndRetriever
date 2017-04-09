using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monstre : MonoBehaviour {

    public Transform headTop;
    public abstract void dead();
    public bool isUnderPlayer(Vector3 playerFeetPosition)
    {
        return playerFeetPosition.y > headTop.position.y;
    }
}
