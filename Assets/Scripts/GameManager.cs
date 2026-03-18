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
    }

    public void Save()
    {
        SaveState gameState = new SaveState();
        gameState.currentRoom = NavigationManager.instance.currentRoom.name;
        //add inventory saving here

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/player.save");
        Debug.Log(Application.persistentDataPath);
        bf.Serialize(file, gameState);
        file.Close();
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/player.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.save", FileMode.Open);
            SaveState gameState = (SaveState) bf.Deserialize(file);
            file.Close();

            //Add inventory loading here
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
