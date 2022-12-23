using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class CraftableItem : ScriptableObject
{
    public string name1;
    public int count;
    public string name2;
    public int count2;
    public Sprite newsprite;
    public TileBase newtile;

}
