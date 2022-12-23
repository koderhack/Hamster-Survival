using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
     
    public float moveSpeed;
    private Vector3 directiontotheplayer;
    private Vector3 LocalScale;
    public float jumpforce;
    AudioSource source;
    public AudioClip zombieroar;
    public GameObject player;
    int hp = 3;
    float timer;
    float interval = 2;
    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("source").GetComponent<AudioSource>();    
        rb = GetComponent<Rigidbody2D>();
        
        LocalScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
    
    }

    private void FixedUpdate()
    {
        
        MoveEnemy();

        if (hp <= 0)
        {
            Destroy(this.gameObject);
            WorldOptions.killedEnemies++;
            
        }

    }
    private void MoveEnemy()
    {
        directiontotheplayer = (player.transform.position - transform.position).normalized;
       
        if (player.transform.position.x + 5 >= transform.position.x || player.transform.position.y + 5 >= transform.position.y)
        {
            rb.velocity = new Vector2(directiontotheplayer.x, 0) * moveSpeed;
            timer += Time.deltaTime;
            if (timer >= interval)
            {
            

                if (source.isPlaying == false)
                {
                source.clip = zombieroar;
                source.Play();
                }






                timer -= interval;
                interval = UnityEngine.Random.Range(2, 5);
            }
          

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
        if (source.isPlaying == false)
        {
            source.clip = zombieroar;
            source.Play();
        }
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

}
