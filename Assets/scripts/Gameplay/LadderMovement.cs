using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;
    public void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing= true;
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
            var cellPos = collision.gameObject.GetComponent<Tilemap>().WorldToCell(transform.position);
            var tile1 = collision.gameObject.GetComponent<Tilemap>().GetTile(cellPos);
            if (tile1 != null)
            {
                if (tile1.name == "drabina")
                {
                    isLadder = true;
                }
            }
        }


    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "tilemap")
        {
            var cellPos = collision.gameObject.GetComponent<Tilemap>().WorldToCell(transform.position);
            var tile1 = collision.gameObject.GetComponent<Tilemap>().GetTile(cellPos);
            if (tile1 != null)
            {
                if (tile1.name == "drabina")
                {
                    isLadder = false;
                    isClimbing = false;
                }
            }
        }
    }
}
