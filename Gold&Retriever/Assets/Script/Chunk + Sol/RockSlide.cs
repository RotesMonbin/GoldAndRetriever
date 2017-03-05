using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSlide : MonoBehaviour
{

    public GameObject smallRock;
    public GameObject bigRock;

    public List<GameObject> smallRocks;
    public List<GameObject> bigRocks;


    public void Awake()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject tmp;
        int speedX;
        if (collision.gameObject.tag == "Player")
        {
            foreach (GameObject sr in smallRocks)
            {
                speedX = Random.Range(-3, 3);
                tmp = Instantiate(smallRock);
                tmp.transform.position = sr.transform.position;
                tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, -20);
                Destroy(sr);
            }

            foreach (GameObject br in bigRocks)
            {
                speedX = Random.Range(-3, 3);
                tmp = Instantiate(bigRock);
                tmp.transform.position = br.transform.position;
                tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(speedX, -20);
                Destroy(br);
            }

            Destroy(this);
        }

    }
}
