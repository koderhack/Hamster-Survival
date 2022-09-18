using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Runtime;
using UnityEngine.Events;

public class ruchgracza : MonoBehaviour
{

    public CharacterController2D controller;

    float horizontalMove = 0f;

    public float runSpeed = 40f;
    public TMP_Text textkolowrotek;
    [SerializeField] private Animator animator;
    [SerializeField] private string animationwheel = "Wheelanim";
    public float blockDestroyTime = 1;
    bool jump = false;
    bool crouch = false;
    public Grid grid;
    public Tilemap hightlist;
    public TileBase tilehigh;
    public Tilemap tilemain;
    public Tilemap tiletwo;
    private Vector3Int previousTileCoordinate;
    public TileBase[] tilebuild;
    public List<TileBase> tilenotdestroy;
    private int id;
    public Dropdown dropdownitem;
    public static bool buildAllowed;
    public GameObject lightorginal;
    public bool lampbuildingallowed;
    GameObject[] clones;
    GameObject light1;
    public AudioSource musicplay;

    public AudioClip[] musicclips;
    private AudioClip currentclip;
    public Sprite[] imgblocks;
    public int currentid;
    public GameObject[] buttonsimggameobject;
    public int[] idblock;
    public Color greencolor;
    public int imageindex;
    private GameObject buttonclicked;
    int jFound;
    int kFound;
    public Color pustykolor;
    bool soundPlaying = false;
    public Animator animatorplayer;
    public int layerindex;
    public GameObject chomik;
    public GameObject textlayer;
   
    private TileBase currentile;
    public Text alerttext;
    private bool isinvenntoryfull;
    public TileBase tiledestroy;
    private Item currentitem;
    public float done = 0.0f;
    public Tilemap tilemapdestroy;
    [SerializeField] private UI_inventory uiInventory;
    public Text lifetxt;
    public GameObject groundcheck;


    private void Awake()
    {
      
       
    }

    // Start is called before the first frame update
    void Start()
    {
        currentitem = null;
        layerindex = 1;
        alerttext.text = "";
       
        //dzia³anie efektu

        textkolowrotek.text = "";
        buildAllowed = true;
        lampbuildingallowed = true;
        if(WorldSettings.creative == true)
        {
            PokazObrazki(0);
        }
        else
        {

        }
      
        textlayer.GetComponent<Text>().text = layerindex.ToString();
        
    }
  
    
    public void OnMouseOver()
    {
        buildAllowed = false;

    }
    public void OnMouseExit()
    {
        buildAllowed = true;
    }

   
    // Update is called once per frame
    void Update()
    {

     

        textlayer.GetComponent<Text>().text = layerindex.ToString();
        clones = GameObject.FindGameObjectsWithTag("LampLight");


        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseTileCoords = grid.WorldToCell(mouseWorldPos);
       
        //sp_highlight.transform.position = mouseTileWorldPos;
        hightlist.SetTile(mouseTileCoords, tilehigh);

        if (mouseTileCoords != previousTileCoordinate)
        {
            hightlist.SetTile(previousTileCoordinate, null);
            hightlist.SetTile(mouseTileCoords, tilehigh);
            previousTileCoordinate = mouseTileCoords;

        }

       

        // Debug.Log("mouseWorldPos: " + mouseWorldPos + " | mouseTileCoords: " + mouseTileCoords + " | mouseTileWorldPos: " + mouseTileWorldPos);
        if (clones.Length == 1000)
        {
            lampbuildingallowed = false;
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animatorplayer.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;

        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        //destroy
        
         if (Input.GetKey(KeyCode.Mouse0))
            {
                if (buildAllowed == true)
                {
           

                //sp_highlight.transform.position = mouseTileWorldPos;
              
               
                foreach (var tile in tilenotdestroy)
                    {
                    if (tilenotdestroy.Contains(tilemain.GetTile(mouseTileCoords)))
                    {

                    }
                    else
                    {
                        if (tilemain.GetTile(mouseTileCoords) == tilebuild[6])
                        {
                            foreach (var clone2 in clones)
                            {
                                if (clone2.transform.position == mouseTileCoords)
                                {
                                    Destroy(clone2);
                                }
                            }

                        }
                        if (tiletwo.GetTile(mouseTileCoords) == tilebuild[6])
                        {
                            foreach (var clone2 in clones)
                            {
                                if (clone2.transform.position == mouseTileCoords)
                                {
                                    Destroy(clone2);
                                }
                            }

                        }
                      
                        SoundEffects(mouseTileCoords);
                        if (WorldSettings.creative == false)
                        {
                            if (layerindex == 1)
                            {
                                Item itemtoadd = new Item();
                                itemtoadd.itemtile = tilemain.GetTile(mouseTileCoords);
                                itemtoadd.itemtype = Item.ItemType.Block;


                                foreach (var img in imgblocks)
                                {
                                    if (tilemain.GetTile(mouseTileCoords) != null)
                                    {
                                        if (img.name == tilemain.GetTile(mouseTileCoords).name)
                                        {
                                            itemtoadd.sprite = img;

                                            if (isinvenntoryfull == false)
                                            {
                                                
                                                currentitem = itemtoadd;
                                               // if (tilemain.GetTile(mouseTileCoords).name != "fire")
                                               // {
                                                    StartCoroutine(waiter(itemtoadd.damagesec(),itemtoadd,mouseTileCoords));
                                                //}
                                               /* else
                                                {
                                                    tilemain.SetTile(mouseTileCoords, null);
                                                    tiletwo.SetTile(mouseTileCoords, null);
                                               }*/
                                                
                                                
                                               
                                               

                                            }
                                           
                                        }
                                    }

                                }


                            }
                            else
                            {
                                Item itemtoadd = new Item();
                                itemtoadd.itemtile = tiletwo.GetTile(mouseTileCoords);
                                itemtoadd.itemtype = Item.ItemType.Block;


                                foreach (var img in imgblocks)
                                {
                                    if (tiletwo.GetTile(mouseTileCoords) != null)
                                    {
                                        if (img.name == tiletwo.GetTile(mouseTileCoords).name)
                                        {
                                            itemtoadd.sprite = img;
                                            if (isinvenntoryfull == false)
                                            {
                                                currentitem = itemtoadd;

                                                if (tiletwo.GetTile(mouseTileCoords).name != "fire")
                                                {
                                                    StartCoroutine(waiter(itemtoadd.damagesec(), itemtoadd, mouseTileCoords));
                                                }
                                                else
                                                {
                                                    tilemain.SetTile(mouseTileCoords, null);
                                                    tiletwo.SetTile(mouseTileCoords, null);
                                                }
                                            }
                                           
                                           
                                        }
                                    }
                                }

                            }
                          


                        }
                        else
                        {
                            tilemain.SetTile(mouseTileCoords, null);
                            tiletwo.SetTile(mouseTileCoords, null);
                        }
                       
                       





                    }
                    }

                }




            }

        foreach (var item in inventory.GetItemList())
        {
            if (inventory.GetItemList().ToArray().Length <= 5 )
            {
                isinvenntoryfull = false;
                alerttext.text = "";
            }
            else
            {

                isinvenntoryfull = true;
                alerttext.text = "Full inventory";
            }
        }

        if(WorldSettings.creative == false)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
            StartCoroutine(Sleep());
            }

        }
       
       
        //build
        if (Input.GetKey(KeyCode.Mouse1))
        {

            if (buildAllowed == true)
            {
                

                foreach (var tile in tilenotdestroy)
                {
                    if (tilenotdestroy.Contains(tilemain.GetTile(mouseTileCoords)))
                    {

                    }
                    else
                    {
                        TileBase lastTile = tilemain.GetTile(mouseTileCoords);
                        if (lastTile == tilebuild[6])
                        {
                            foreach (var clone2 in clones)
                            {
                                if (clone2.transform.position == mouseTileCoords)
                                {
                                    Destroy(clone2);
                                }
                            }
                        }
                        TileBase lastTile2 = tiletwo.GetTile(mouseTileCoords);
                        if (lastTile2 == tilebuild[6])
                        {
                            foreach (var clone2 in clones)
                            {
                                if (clone2.transform.position == mouseTileCoords)
                                {
                                    Destroy(clone2);
                                }
                            }
                        }
                        
                        if (tilebuild[id] != tilebuild[6] || currentile != tilebuild[6])
                        {

                            // tilemain.SetTile(mouseTileCoords, tilebuild[id]);
                            if (layerindex == 1)
                            {
                                if (WorldSettings.creative == true)
                                {
                                    tilemain.SetTile(mouseTileCoords, tilebuild[id]);
                                }
                                else
                                {
                                    if(lastTile == null || lastTile2 == null)
                                    {
                                        if (inventory.GetItem(currentile) != null)
                                        {
                                            tilemain.SetTile(mouseTileCoords, currentile);
                                            inventory.DeleteItem(currentile);
                                            uiInventory.RefreshInventoryItems();
                                        }
                                    }
                                   
                                  
                                        

                                    


                                   
                                   
                                      
                                    
                                   

                                }
                               
                            }
                            else
                            {
                                if (WorldSettings.creative == true)
                                {
                                    tiletwo.SetTile(mouseTileCoords, tilebuild[id]);
                                }
                                else
                                {
                                    tiletwo.SetTile(mouseTileCoords, currentile);
                                }
                            }

                        }
                        if (tilebuild[id] == tilebuild[6] || currentile == tilebuild[6] && lampbuildingallowed == true)
                        {

                            // tilemain.SetTile(mouseTileCoords, tilebuild[id]);
                            if (layerindex == 1)
                            {
                                if (WorldSettings.creative == true)
                                {
                                    tilemain.SetTile(mouseTileCoords, tilebuild[id]);
                                }
                                else
                                {
                                    tilemain.SetTile(mouseTileCoords, currentile);
                                }

                            }
                            else
                            {
                                if (WorldSettings.creative == true)
                                {
                                    tiletwo.SetTile(mouseTileCoords, tilebuild[id]);
                                }
                                else
                                {
                                    tiletwo.SetTile(mouseTileCoords, currentile);
                                }
                            }

                            var clone = Instantiate(lightorginal, mouseTileCoords, Quaternion.identity);

                            foreach (var clone1 in clones)
                            {
                                if (clone1.transform.position == clone.transform.position)
                                {
                                    Destroy(clone1);
                                }
                            }

                            // Destroy(clone);

                        }

                    }
                }
            }


        }
    


        if (Input.mouseScrollDelta == Vector2.down)
        {
            if (WorldSettings.creative == true)
            {
                imageindex -= 5;
                if (imageindex < 0)
                {
                    imageindex = imgblocks.Length / 5 * 5;
                    if (imgblocks.Length % 5 == 0)
                    {
                        imageindex -= 5;
                    }

                }
                PokazObrazki(imageindex);
            }
        }
        if (Input.mouseScrollDelta == Vector2.up)
        {
            if(WorldSettings.creative == true)
            {
                imageindex += 5;
                 if (imageindex > imgblocks.Length)
                {
                imageindex = 0;
                }

                PokazObrazki(imageindex);
            }
           
        }

     if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            musicplay.Stop();
        }
        lifetxt.text = $"{PlayerSettings.life}%";
      
        var cellPos = tilemain.WorldToCell(groundcheck.transform.position);
        var tile1 = tilemain.GetTile(cellPos);
        if (tile1 != null)
        {
            if (tile1 == tilebuild[12])
            {
                PlayerSettings.life--;
            }
        }




    }
     IEnumerator Sleep()
    {
       
        if(cykldniainocy.isday)
        {
            
            animatorplayer.SetBool("isSleeping", true);
            yield return new WaitForSeconds(5);
            cykldniainocy.sleepcontrolleractive = true;
            PlayerSettings.life = 100;

            animatorplayer.SetBool("isSleeping", false);

             cykldniainocy.timer = 600;
        }
           
           
      
       

    }
    
    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;


    }
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        crouch = false;
       /* if (collision.tag == "Wheel")
        {

            textkolowrotek.text = "Press U to Use";
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (!Input.GetKeyDown(KeyCode.Escape))
                {
                    animator.Play(animationwheel);
                    AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
                    float currentTime = animState.normalizedTime % 1;
                    if (currentTime == 63)
                    {
                        animator.Play(animationwheel);
                    }


                }

            }

        }
       */

    }
    IEnumerator waiter(float sek, Item item,Vector3Int mousepos)
    {
        buildAllowed = false;
        tilemapdestroy.SetTile(mousepos, tiledestroy);
        yield return new WaitForSeconds(sek);
        
        inventory.AddItem(item);
        uiInventory.RefreshInventoryItems();
        tilemain.SetTile(mousepos, null);
        tiletwo.SetTile(mousepos, null);
        buildAllowed = true;
        tilemapdestroy.SetTile(mousepos, null);


    }
    public void OnCrouching(bool isCrouching)
    {
        animatorplayer.SetBool("IsCrouching", isCrouching);
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
      /*  if (collision.tag == "Wheel")
        {
            textkolowrotek.text = "";

        }
      */
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
       /* if (collision.tag == "Wheel")
        {
            textkolowrotek.text = "Press U to Use";
            if (Input.GetKeyDown(KeyCode.U))
            {
                if (!Input.GetKeyDown(KeyCode.Escape))
                {
                    animator.Play(animationwheel);
                    AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
                    float currentTime = animState.normalizedTime % 1;
                    if (currentTime == 63)
                    {
                        animator.Play(animationwheel);
                    }







                }

            }*/
            crouch = false;
        }

    
    public static void EnterMenu()
    {
        buildAllowed = false;
    }
    public static void ExitMenu()
    {
        buildAllowed = true;
    }
  public void layerbtnclicked()
    {
        if(layerindex == 1)
        {
            layerindex = 2;
            textlayer.GetComponent<Text>().text = layerindex.ToString();
        }
        else
        {
            layerindex = 1;
            textlayer.GetComponent<Text>().text = layerindex.ToString();
        }
    }

    bool IsInDistance(Transform a, Transform b, float maxDistance)
    {
        return Vector3.Distance(a.position, b.position) <= maxDistance;
    }

    public void SoundEffects(Vector3Int mouseTileCoords1)
    {

        

     
        
            if (tilemain.GetTile(mouseTileCoords1) == tilebuild[0] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[0])
            {
            //musicplay.PlayOneShot(musicclips[0]);
            musicplay.clip = musicclips[0];
            musicplay.Play();
            }
            else if (tilemain.GetTile(mouseTileCoords1) == tilebuild[1] || tilemain.GetTile(mouseTileCoords1) == tilebuild[4] || tilemain.GetTile(mouseTileCoords1) == tilebuild[6] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[1] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[4] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[6])
            {
            // musicplay.PlayOneShot(musicclips[1]);
            musicplay.clip = musicclips[1];
            musicplay.Play();
        }
            else if (tilemain.GetTile(mouseTileCoords1) == tilebuild[2] || tilemain.GetTile(mouseTileCoords1) == tilebuild[3] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[2] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[3])
            {
            // musicplay.PlayOneShot(musicclips[2]);
            musicplay.clip = musicclips[2];
            musicplay.Play();
        }

            else if (tilemain.GetTile(mouseTileCoords1) == tilebuild[5] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[5])
            {
            // musicplay.PlayOneShot(musicclips[3]);
            musicplay.clip = musicclips[3];
            musicplay.Play();
        }

            else if (tilemain.GetTile(mouseTileCoords1) == tilebuild[7] || tilemain.GetTile(mouseTileCoords1) == tilebuild[8] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[7] || tiletwo.GetTile(mouseTileCoords1) == tilebuild[8] )
            {
            //musicplay.PlayOneShot(musicclips[4]);
            musicplay.clip = musicclips[4];
            musicplay.Play();
        }

        
  










    }

    //map.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), null);

    public void BtnClicked(Image image)
    {
        
       
        if(WorldSettings.creative == false)
        {
            foreach (var item in inventory.GetItemList())
             {
                if(item.sprite == image.sprite)
                {
                    currentile = item.itemtile;
                    
                }
            }
        }
        else
        {
            for (int i = 0; i < imgblocks.Length; i++)
            {
               if(image.sprite == imgblocks[i])
                {
                    id = i;
                    Debug.Log(id);
                }
                
                
            }
               
            
             
            
        }
        //for each object in the array..
       
            
        
       
       
    }


       
        private void PokazObrazki(int index)
        {
            for (int i = 0; i < buttonsimggameobject.Length; i++)
            {
                Image image = buttonsimggameobject[i].GetComponent<Image>();
            buttonsimggameobject[i].gameObject.GetComponentInParent<Button>().interactable = true;
                if (i + index < imgblocks.Length && i + index >= 0) 
                { 
                image.sprite = imgblocks[i + index];
                image.color =pustykolor;
                
                 }
                else
                {
                    image.sprite = null;
                image.color = greencolor;
                buttonsimggameobject[i].gameObject.GetComponentInParent<Button>().interactable = false;
                }







            }

        }
      /* public void OnCrouch()
        {
        BoundsInt bounds = tilemain.cellBounds;

        //create a new leveldata
        

        //loop trougth the bounds of the tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //get the tile on the position
                if(transform.position == new Vector3Int(x, y, 0))
                {
                    tilemain.SetTile(new Vector3Int(x, y, 0),null);
                }
                
                //find the temp tile in the custom tiles list
               
            }
        }
    }*/
    }



