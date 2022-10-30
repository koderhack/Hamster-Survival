using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters ;
using System.Security.Cryptography;
using Newtonsoft.Json;

public class BaseFunc : MonoBehaviour
{
    public static BaseFunc Instance { get; private set; }
    
    public List<CustomTile> tiles = new List<CustomTile>();
   
    private Transform pozycja;
    GameObject[] clones;
    private Tilemap mapa;
    private Tilemap mapa2;
    private string keyword;
    private void Awake()
    {
        keyword = "84448940";
        // If there is an instance, and it's not me, delete myself.
        DontDestroyOnLoad(this.gameObject);
        
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

  


    public void Savelevel(Tilemap mapa,string worldname)
    {
        //get the bounds of the tilemap
        BoundsInt bounds = mapa.cellBounds;

        //create a new leveldata
        LevelData levelData = new LevelData();

        //loop trougth the bounds of the tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //get the tile on the position
                TileBase temp = mapa.GetTile(new Vector3Int(x, y, 0));
                //find the temp tile in the custom tiles list
                CustomTile temptile = tiles.Find(t => t.tile == temp);

                //if there's a customtile associated with the tile
                if (temptile != null)
                {
                    //add the values to the leveldata
                    levelData.tiles.Add(temptile.id);
                    levelData.poses_x.Add(x);
                    levelData.poses_y.Add(y);
                }
            }
        }

        //save the data as a json
        string json = JsonUtility.ToJson(levelData, true);
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path,"Base");
        path = Path.Combine(path, "world.json");
        File.WriteAllText(path, json);

        //debug
       // Debug.Log("Level was saved");
    }
    public void Savelevel2(Tilemap mapa2, string worldname1)
    {
        //get the bounds of the tilemap
        BoundsInt bounds = mapa2.cellBounds;

        //create a new leveldata
        LevelData levelData = new LevelData();

        //loop trougth the bounds of the tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //get the tile on the position
                TileBase temp = mapa2.GetTile(new Vector3Int(x, y, 0));
                //find the temp tile in the custom tiles list
                CustomTile temptile = tiles.Find(t => t.tile == temp);

                //if there's a customtile associated with the tile
                if (temptile != null)
                {
                    //add the values to the leveldata
                    levelData.tiles.Add(temptile.id);
                    levelData.poses_x.Add(x);
                    levelData.poses_y.Add(y);
                }
            }
        }

        //save the data as a json
        string json = JsonUtility.ToJson(levelData, true);
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname1));
        path = Path.Combine(path, "Base");
        path = Path.Combine(path, "worldlayer2.json");
        File.WriteAllText(path, json);

        //debug
        // Debug.Log("Level was saved");
    }
    public void LoadLevel(Tilemap mapa, TileBase tilelight, GameObject lightoryginal,string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "Base");
        path = Path.Combine(path, "world.json");
        if (File.Exists(path))
        {


            //load the json file to a leveldata
            string json = File.ReadAllText(path);
            LevelData data = JsonUtility.FromJson<LevelData>(json);

            //clear the tilemap
            mapa.ClearAllTiles();

            //place the tiles
            for (int i = 0; i < data.tiles.Count; i++)
            {
                mapa.SetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0), tiles.Find(t => t.id == data.tiles[i]).tile);
                TileBase tile1 = mapa.GetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0));
                if (tile1 == tilelight)
                {
                    Instantiate(lightoryginal, new Vector3Int(data.poses_x[i], data.poses_y[i], 0), Quaternion.identity);
                }
            }

            //debug
           // Debug.Log("Level was loaded");
        }
    }
    public void LoadLevel2(Tilemap mapa, TileBase tilelight1, GameObject lightoryginal1, string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "Base");
        path = Path.Combine(path, "worldlayer2.json");
        if (File.Exists(path))
        {


            //load the json file to a leveldata
            string json = File.ReadAllText(path);
            LevelData data = JsonUtility.FromJson<LevelData>(json);

            //clear the tilemap
            mapa.ClearAllTiles();

            //place the tiles
            for (int i = 0; i < data.tiles.Count; i++)
            {
                mapa.SetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0), tiles.Find(t => t.id == data.tiles[i]).tile);
                TileBase tile1 = mapa.GetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0));
                if (tile1 == tilelight1)
                {
                    Instantiate(lightoryginal1, new Vector3Int(data.poses_x[i], data.poses_y[i], 0), Quaternion.identity);
                }
            }

            //debug
            // Debug.Log("Level was loaded");
        }
    }
    public void DeleteLevel(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, worldname);
        path = Path.Combine(path, "Base");
        path = Path.Combine(path, "world.json");
        File.Delete(path);

    }
    public void DeleteLevel2(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, worldname);
        path = Path.Combine(path, "Base");
        path = Path.Combine(path, "worldlayer2.json");
        File.Delete(path);

    }

    public void GenerateStartFile(Tilemap mapa)
    {
        BoundsInt bounds = mapa.cellBounds;

        //create a new leveldata
        LevelData levelData = new LevelData();

        //loop trougth the bounds of the tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //get the tile on the position
                TileBase temp = mapa.GetTile(new Vector3Int(x, y, 0));
                //find the temp tile in the custom tiles list
                CustomTile temptile = tiles.Find(t => t.tile == temp);

                //if there's a customtile associated with the tile
                if (temptile != null)
                {
                    //add the values to the leveldata
                    levelData.tiles.Add(temptile.id);
                    levelData.poses_x.Add(x);
                    levelData.poses_y.Add(y);
                }
            }
        }

        //save the data as a json
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.persistentDataPath + "/startLevel.json", json);

        //debug
        //Debug.Log("Start Level was saved");
    }
    public void GenerateStartFile2(Tilemap mapa2)
    {
        BoundsInt bounds = mapa2.cellBounds;

        //create a new leveldata
        LevelData levelData = new LevelData();

        //loop trougth the bounds of the tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                //get the tile on the position
                TileBase temp = mapa2.GetTile(new Vector3Int(x, y, 0));
                //find the temp tile in the custom tiles list
                CustomTile temptile = tiles.Find(t => t.tile == temp);

                //if there's a customtile associated with the tile
                if (temptile != null)
                {
                    //add the values to the leveldata
                    levelData.tiles.Add(temptile.id);
                    levelData.poses_x.Add(x);
                    levelData.poses_y.Add(y);
                }
            }
        }

        //save the data as a json
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.persistentDataPath + "/startLevel2.json", json);

        //debug
        //Debug.Log("Start Level was saved");
    }
    public void LoadStartLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/startLevel.json"))
        {


            //load the json file to a leveldata
            string json = File.ReadAllText(Application.persistentDataPath + "/startLevel.json");
            LevelData data = JsonUtility.FromJson<LevelData>(json);

            //clear the tilemap
            mapa.ClearAllTiles();

            //place the tiles
            for (int i = 0; i < data.tiles.Count; i++)
            {
                mapa.SetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0), tiles.Find(t => t.id == data.tiles[i]).tile);

            }

            //debug
            //Debug.Log("Start Level was loaded");
        }
    }
    public void LoadStartLevel2()
    {
        if (File.Exists(Application.persistentDataPath + "/startLevel2.json"))
        {


            //load the json file to a leveldata
            string json = File.ReadAllText(Application.persistentDataPath + "/startLevel2.json");
            LevelData data = JsonUtility.FromJson<LevelData>(json);

            //clear the tilemap
            mapa2.ClearAllTiles();

            //place the tiles
            for (int i = 0; i < data.tiles.Count; i++)
            {
                mapa2.SetTile(new Vector3Int(data.poses_x[i], data.poses_y[i], 0), tiles.Find(t => t.id == data.tiles[i]).tile);

            }

            //debug
            //Debug.Log("Start Level was loaded");
        }
    }
    public void DeleteStartLevel()
    {
        if (File.Exists(Application.persistentDataPath + "/startLevel.json"))
        {
            File.Delete(Application.persistentDataPath + "/startLevel.json");
        }
        if (File.Exists(Application.persistentDataPath + "/startLevel2.json"))
        {
            File.Delete(Application.persistentDataPath + "/startLevel2.json");
        }
    }
    public void GenerateWorldStructure(string worldname)
    {

        string path = Path.Combine(Application.persistentDataPath, $"{SecurityCheck(worldname)}");
        path = Path.Combine(path, "Base");
        Directory.CreateDirectory(path);
       // Debug.Log("World STRUCKTURE GENERATED");
    }
    public void DeleteWorldStructure(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, worldname);
        path = Path.Combine(path, "Base");
        Directory.Delete(path);
        path = Path.Combine(Application.persistentDataPath, worldname);
        Directory.Delete(path);

    }
    public void SaveSettings(string worldname)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "worldsettings.dat");
        FileStream file = File.Create(path);
        SaveData data = new SaveData();
        data.worldnamesave = WorldSettings.worldname;
        data.creativesave = WorldSettings.creative;
        data.settingssave = WorldSettings.settings;
        bf.Serialize(file, data);
        file.Close();
       // Debug.Log("Settings Saved!");
    }
    public void LoadSettings(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "worldsettings.dat");
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            WorldSettings.worldname = data.worldnamesave;
            WorldSettings.creative = data.creativesave;
            WorldSettings.settings = data.settingssave;
           // Debug.Log("Settings loaded!");
        }
        else
        {
           // Debug.LogError("There is no save data!");
        }


    }
    public void DeleteSettings(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "worldsettings.dat");
        File.Delete(path);
    }
    public void LoadSettingsToInfo(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "worldsettings.dat");
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            
            WorldInfo.worldname = data.worldnamesave;
            WorldInfo.creative = data.creativesave;
            WorldInfo.settings = data.settingssave;
           // Debug.Log("Settings loaded!");
           
        }
        else
        {
            //Debug.Log("There is no save data!");
        }


    }
    public void WriteSettings(string worldname, bool creative, bool[] settings)
    {
        WorldSettings.worldname = worldname;
        WorldSettings.creative = creative;
        WorldSettings.settings = settings;
    }
    public string SecurityCheck(string worldname)
    {
      
        char[] chars = Path.GetInvalidPathChars();
        
        foreach (char someChar in chars)
        {
            
            if (worldname.Contains(someChar))
            {
                Regex.Replace(worldname,$"{chars}","", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
           
            
            return worldname;
        }
        return null;
    }
    public void WritePlayerSettings()
    {
        PlayerSettings.life = 100;
        PlayerSettings.hunger = 100;
        PlayerSettings.sport = 100;
        PlayerSettings.level = 0;
    }
    public void SavePlayerSettings(string worldname)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "playersettings.dat");
        FileStream file = File.Create(path);
        PlayerData data = new PlayerData();
        data.life = PlayerSettings.life;
        data.sport  = PlayerSettings.sport;
        data.hunger = PlayerSettings.hunger;
        data.level = PlayerSettings.level;
      
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadPlayerSettings(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "playersettings.dat");
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file); 
            PlayerSettings.life = data.life;
            PlayerSettings.hunger = data.hunger;
         
            PlayerSettings.sport = data.sport;
            PlayerSettings.level = data.level;
            file.Close();
          
            
            // Debug.Log("Settings loaded!");
        }
        else
        {
            // Debug.LogError("There is no save data!");
        }


    }
    public void DeletePlayerSettings(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "playersettings.dat");
        File.Delete(path);
    }
    public void SaveInventory(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "inventory.json");

       
       
        if(inventory.itemList != null)
        {
            InventoryDataSave invsave = new InventoryDataSave();
            invsave.inventorysave = new List<ItemSave>();
            foreach (Item item in inventory.itemList)
            {
                

                ItemSave itemsave = new ItemSave();
                itemsave.amount = item.amount;
                itemsave.itemtype = ItemSave.ItemType.Block;
                string name = item.itemtile.name;
                itemsave.tilepath = $"Tiles/{name}";
                itemsave.spritepath = name;
              
                invsave.inventorysave.Add(itemsave);






            }
            string json = JsonConvert.SerializeObject(invsave);

            //  string encryptedjson = EncryptDecrypt(json);

            File.WriteAllText(path, json);
        }
        
       


    }
    public void LoadInventory(string worldname,UI_inventory uiinventory)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "inventory.json");
        if (File.Exists(path))
        {
            string encryptedjson = File.ReadAllText(path);
            //string json = EncryptDecrypt(encryptedjson);
            InventoryDataSave invsave1 = JsonConvert.DeserializeObject<InventoryDataSave>(encryptedjson);
            inventory.itemList = new List<Item>();
        
            foreach (var itemsave in invsave1.inventorysave)
                {
                    Item item = new Item();
            
                    item.amount = itemsave.amount;
                    item.itemtype = (Item.ItemType)itemsave.itemtype;
                    item.itemtile = Resources.Load<TileBase>(itemsave.tilepath);
                    item.sprite = Resources.Load<Sprite>(itemsave.spritepath);
                    inventory.AddItem(item);
                
                uiinventory.RefreshInventoryItems();
                
            }
         
           
          
           
        }
        else
        {
            inventory.itemList = new List<Item>();
        }
    }
    public void DeleteInventory(string worldname)
    {
        string path = Path.Combine(Application.persistentDataPath, SecurityCheck(worldname));
        path = Path.Combine(path, "inventory.json");
        File.Delete(path);
    }
    private string EncryptDecrypt(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ keyword[i % keyword.Length]);
        }
        return result;
    }
    //ogólne metody

    public void ResetStartLevel(Tilemap mapa,Tilemap mapa2)
    {
        DeleteStartLevel();
        GenerateStartFile(mapa);
        GenerateStartFile2(mapa2);
    }
    public void SaveWorld(Tilemap mapa,Tilemap mapa2,string worldname)
    {
        SaveSettings(worldname);
        SavePlayerSettings(worldname);
        SaveInventory(worldname);
        Savelevel(mapa,worldname);
        Savelevel2(mapa2, worldname);
    }
    public void LoadWorld(Tilemap mapa,Tilemap mapa2, TileBase tilelight, GameObject lightoryginal,string worldname,UI_inventory uiinventory)
    {
        LoadSettings(worldname);
        LoadPlayerSettings(worldname);
        LoadInventory(worldname,uiinventory);
        LoadLevel(mapa, tilelight, lightoryginal,worldname);
        LoadLevel2(mapa2, tilelight, lightoryginal, worldname);
    }
    public void CreateWorld(string worldname, bool creative,bool[] settings)
    {
         mapa = GameObject.FindGameObjectWithTag("tilemap").GetComponent<Tilemap>();
        mapa2 = GameObject.FindGameObjectWithTag("tilemap2").GetComponent<Tilemap>(); 
        inventory.itemList = new List<Item>();
        string name = SceneManager.GetActiveScene().name;
       // Debug.Log(name);
        GenerateWorldStructure(worldname);
            LoadStartLevel();
        LoadStartLevel2();
            WriteSettings(worldname, creative, settings);
        WritePlayerSettings();
  
        
        
    }
    public void DeleteWorld(string worldname)
    {
        DeleteLevel(worldname);
        DeleteLevel2(worldname);
        DeleteSettings(worldname);
        DeletePlayerSettings(worldname);
        DeleteInventory(worldname);
        DeleteWorldStructure(worldname);
    }
    public List<CustomTile> GetTiles()
    {
        return tiles;
    }
    public CustomTile FindById(string id)
    {
      
        return tiles.Find(x => x.id == id);
    }
   
   
}
[Serializable]
class SaveData
{
  public string worldnamesave;
  public bool creativesave;
  public bool[] settingssave;
}
[Serializable]
class PlayerData
{
    public float life;
    public float hunger;
    public float sport;
    public int level;
}
public class LevelData
{
    public List<string> tiles = new List<string>();
    public List<int> poses_x = new List<int>();
    public List<int> poses_y = new List<int>();
}
[Serializable]
public class InventoryDataSave
{
    public List<ItemSave> inventorysave;



}
[Serializable]
public class ItemSave
{
 

    public enum ItemType
    {
        Block,
        Weapon,


    }
    public ItemType itemtype;
    public int amount;
    public string spritepath;
    public string tilepath;
  


}