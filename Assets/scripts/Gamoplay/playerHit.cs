using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHit : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float moveSpeed;
    public int hp = 3;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
      
    }
    public void FixedUpdate()
    {
        rb.velocity = new Vector2(icemonstercontroller.directiontotheplayer.x, icemonstercontroller.directiontotheplayer.y) * moveSpeed;
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerSettings.life -= 20;
            Destroy(this.gameObject);
        }
      

    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(redlight());


        }

    }
    IEnumerator redlight()
    {
        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        hp--;
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
