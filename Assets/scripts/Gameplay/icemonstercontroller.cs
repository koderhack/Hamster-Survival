using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icemonstercontroller : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;

    public float moveSpeed;
    public static Vector3 directiontotheplayer;
    private Vector3 LocalScale;
    public float jumpforce;
    public static int killedEnemies;
    int hp = 7;
    AudioSource source;
    public AudioClip monsterroar;
    public AudioClip shottingice;
    public GameObject icecube;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        AdditionalSettings.killedmobs = killedEnemies;

        rb = GetComponent<Rigidbody2D>();

        LocalScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
        source = GameObject.FindGameObjectWithTag("source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveEnemy();
        
        if (hp <= 0)
        {
            Destroy(this.gameObject);
            killedEnemies+= 1;
            
        }
     
    }
    public void Update()
    {

        if (player.transform.position.x + 5 >= transform.position.x)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0f)
            {
                directiontotheplayer = (player.transform.position - transform.position).normalized;

                Vector3 pozycja = new Vector3(transform.position.x, transform.position.y, 0);

                Instantiate(icecube, pozycja, Quaternion.identity);
                //dŸwiêk kuli
                source.clip = shottingice;
                source.Play();
                Timer = 10f;
            }
        }
    }
    private void LateUpdate()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(LocalScale.x, LocalScale.y, LocalScale.z);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-LocalScale.x, LocalScale.y, LocalScale.z);
        }
    }
    private void MoveEnemy()
    {
        directiontotheplayer = (player.transform.position - transform.position).normalized;

        if (player.transform.position.x + 5 >= transform.position.x)
        {
            rb.velocity = new Vector2(directiontotheplayer.x, 0) * moveSpeed;
            if (source.isPlaying == false)
            {
                source.clip =  monsterroar;
                source.Play();
            }
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
