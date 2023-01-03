using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LadderMovement : MonoBehaviour
{
    public float vertical;
    public float speed;
    public static bool isLadder;
    public static bool isClimbing;
    public static bool moveindicator;

    [SerializeField] private Rigidbody2D rb;
    public void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder)
        {
            isClimbing= true;
            moveindicator= false;
        }
        else
        {
            moveindicator = true;
            isClimbing = false;
        }
        if(isClimbing && Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Horizontal") == 1)
        {
            isLadder = false;
            isClimbing = false;
             moveindicator = true;
        }
    }
    public void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);

        }
        else
        {
            rb.gravityScale = 3f;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.tag == "tilemap")
        {
            
            var cellPos2 = collision.gameObject.GetComponent<Tilemap>().WorldToCell(new Vector3(transform.position.x + 1, transform.position.y, 0));
   
            var tile2 = collision.gameObject.GetComponent<Tilemap>().GetTile(cellPos2);

         
            if (tile2 != null)
            {
                if (tile2.name == "drabina")
                {
                    
                    isLadder = true;
                }
                else
                {
                    isLadder = false;
                   isClimbing = false;
                    moveindicator = true;
                }
            }
            else
            {
                isLadder = false;
                isClimbing = false;
                moveindicator = true;
            }

           
               
                 
                
            
        }


    }
    
}
