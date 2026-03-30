using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> inventory = new List<string>();

    public List<Room> itemRooms = new List<Room>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        NavigationManager.instance.onRestart += ResetGame; //no parentheses, just points event to function
        Load();
    }



    void ResetGame()
    {
        inventory.Clear();
        ResetItems();

    }

    void ResetItems()
    {
        foreach (Room room in itemRooms)
        {
            if(room.name == "Key")
            {
                inventory.Add("key");
            }
            else if (room.name == "Fountain")
            {
                inventory.Add("coin");
            }
            else if (room.name == "Kitchen")
            {
                inventory.Add("knife");
            }
            else if (room.name == "Orb")
            {
                inventory.Add("orb");
            }
        }
    }

    public void Save()
    {
        SaveData gameState = new SaveData();
        gameState.currentRoom = NavigationManager.instance.currentRoom.name;

        foreach (var item in inventory)
        {
            gameState.currentInventory.Add(item);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save.save");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(file, gameState);
        file.Close();
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/save.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.save", FileMode.Open);
            SaveData gameState = (SaveData)bf.Deserialize(file);
            
            file.Close();
            //inventory
            foreach (var item in gameState.currentInventory)
            {
                inventory.Add(item);
            }

            Room room = NavigationManager.instance.GetRoomByName(gameState.currentRoom);
            if(room != null)
            {
                NavigationManager.instance.SwitchRoom(room);
            }
            else
            {
                NavigationManager.instance.GameRestart();
            }
        }
    }
}
