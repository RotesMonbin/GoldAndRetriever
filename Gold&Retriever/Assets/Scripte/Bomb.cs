﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float timeToBlow;
    public float timeToBlink;
    public Animator anime;
    public GameObject blowRadius;
    public LayerMask decor;

    private float timer=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > timeToBlink && !anime.GetBool("blink"))
        {
            anime.SetBool("blink", true);
        }

        if (timer > timeToBlow)
        {
            anime.SetBool("blow", true);
            Blow();
        }

	}

    void Blow()
    {
        Collider2D[] blocks = Physics2D.OverlapCircleAll(new Vector2(blowRadius.transform.position.x, blowRadius.transform.position.y)
            , 2, decor);
        Destroy(this.gameObject.GetComponent<Rigidbody2D>());
        foreach(Collider2D c in blocks)
        {
            if (c.tag == "Breakable")
            {
                Destroy(c.gameObject);
            }
        }

    }

    void Destruction()
    {
        Destroy(this.gameObject);
    }
}