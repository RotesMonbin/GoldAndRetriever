using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float timeToBlow;
    public float timeToBlink;
    public Animator anime;
    public GameObject blowRadius;
    public LayerMask decor;

    [SerializeField]
    public GameObject bombeParticule; 

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
            GameObject particulego = Instantiate(bombeParticule);
            Vector2 temp = this.transform.position;
            particulego.transform.position = temp;
            Blow();
        }

	}

    void Blow()
    {
        Collider2D[] blocks = Physics2D.OverlapCircleAll(new Vector2(blowRadius.transform.position.x, blowRadius.transform.position.y)
            , 2);
        Destroy(this.gameObject.GetComponent<Rigidbody2D>());
        foreach(Collider2D c in blocks)
        {
            if (c.tag == "Breakable")
            {
                Destroy(c.gameObject);
            }
            if(c.tag == "Player")
            {
                c.GetComponent<PlayerNoRb>().speed = new Vector2((c.transform.position.x - this.transform.position.x)*20, (c.transform.position.y - this.transform.position.y)*20);
                c.GetComponent<PlayerNoRb>().killPlayer();

            }
            if(c.tag == "Enemie")
            {
                Destroy(c.gameObject);
            }
        }
        Destroy(this.gameObject);

    }
}
