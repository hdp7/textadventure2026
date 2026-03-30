using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;

    public Room startingRoom;
    public Room currentRoom;
    public List<Room> rooms;

    private Dictionary<string, Room> exitRooms = new Dictionary<string, Room>();
    

    public Exit toKeyNorth;

    public delegate void Restart();
    public event Restart onRestart;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //currentRoom = startingRoom;
        //Unpack();
    }

    void ResetItems()
    {
        foreach (Room room in rooms)
        {
            if (room.roomName == "Key")
            {
                Debug.Log("added key back to key.");
                if (room.items.Count == 0)
                {
                    room.items.Add("key");
                }
                room.description = "A shiny key is visible...";
            }
            else if (room.roomName == "Fountain")
            {

                Debug.Log("added coin back to fountain.");
                if (room.items.Count == 0)
                {
                    room.items.Add("coin");
                }
                room.description = "A coin glimmers from the fountain...";
            }
            else if (room.roomName == "Kitchen")
            {
                Debug.Log("added knife back to kitchen.");
                if (room.items.Count == 0)
                {
                    room.items.Add("knife");
                }
                room.description = "A sharp knife lies imbedded in a chopping board...";
            }
            else if (room.roomName == "Orb")
            {
                Debug.Log("added orb back to orb.");
                if (room.items.Count == 0)
                {
                    room.items.Add("orb");
                }
                room.description = "A glowing orbs lights the room...";
            }
        }
    }


    public Room GetRoomByName(string name)
    {
        foreach(Room room in rooms)
        {
            if(room.name == name) return room;
        }
        return null;
    }

    public void Unpack()
    {
        string description = currentRoom.description;
     
        exitRooms.Clear(); //clears current room data
        
        foreach (Exit e in currentRoom.exits)
        {
            if (!e.isHidden)
            {
                description += " " + e.description;
                exitRooms.Add(e.direction.ToString(), e.room);
            }
        }
            
        InputManager.instance.UpdateStory(description);

        if (currentRoom.name == "dragon")
        {
            GameRestart();
        }
    }
    public void SwitchRoom(Room room)
    {
        currentRoom = room;
        Unpack();
    }
    public bool Switch(string direction)
    {
        if (exitRooms.ContainsKey(direction))
        {
            if (GameManager.instance.inventory.Contains("key") || !GetExit(direction).isLocked)
            {
                currentRoom = exitRooms[direction];
                InputManager.instance.UpdateStory("You go " + direction);
                Unpack();
                return true;
            } else if(GetExit(direction).isLocked)
            {
                InputManager.instance.UpdateStory("The door to the " + direction + " is locked.");
                return false;
            }
            {
                return false;
            }
        }
        return false;
    }

    Exit GetExit(string direction)
    {
        foreach (Exit e in currentRoom.exits)
        {
            if (e.direction.ToString() == direction)
                return e;
        }
        return null;
    }

    public bool GetItem(string item)
    {
        bool isFound = false;
        foreach (string i in currentRoom.items)
        {
            if (i == item)
            {
                isFound = true;
            }
        }
        if (isFound)
        {
            if (item == "orb")
            {
                toKeyNorth.isHidden = false;
                currentRoom.items.Remove(item);
                currentRoom.description = "This room used to have a blue glow...";
            }
            else if (item == "knife")
            {
                currentRoom.items.Remove(item);
                currentRoom.description = "A large gash lies where the knife once laid...";
            }
            else if (item == "coin")
            {
                currentRoom.items.Remove(item);
                currentRoom.description = "The fountain shimmers a bit less without the coin...";
            }
        }
        return isFound; //item not found
    }
    

    public void GameRestart()
    { 
        onRestart.Invoke();
        ResetItems();
        currentRoom = startingRoom;
        toKeyNorth.isHidden = true;
        Unpack();
        GameManager.instance.Save();
        Debug.Log("Game reset.");
    }
}
