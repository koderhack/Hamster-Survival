using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
     
    public float moveSpeed;
    private Vector3 directiontotheplayer;
    private Vector3 LocalScale;
    public float jumpforce;
    public Collider2D collider;
    public static int killedEnemies;
    public GameObject player;
    int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        Enemy.killedEnemies = AdditionalSettings.killedmobs;
        rb = GetComponent<Rigidbody2D>();
        
        LocalScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
    
    }

    private void FixedUpdate()
    {
        AdditionalSettings.killedmobs = killedEnemies;
        MoveEnemy();
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            killedEnemies++;
            
        }

    }
    private void MoveEnemy()
    {
        directiontotheplayer = (player.transform.position - transform.position).normalized;
       
        if (player.transform.position.x + 5 >= transform.position.x || player.transform.position.y + 5 >= transform.position.y)
        {
            rb.velocity = new Vector2(directiontotheplayer.x, 0) * moveSpeed;
        }
       
        
       
       
    }

   

    // Update is called once per frame
    private void LateUpdate()
    {
        if(rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(LocalScale.x, LocalScale.y, LocalScale.z);
        }
        else if(rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-LocalScale.x, LocalScale.y, LocalScale.z);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerSettings.life--;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerSettings.life--;
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
