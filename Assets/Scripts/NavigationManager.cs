using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;

    public Room startingRoom;
    public Room currentRoom;

    private Dictionary<string, Room> exitRooms = new Dictionary<string, Room>();
    
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
        currentRoom = startingRoom;
        Unpack();
    }

    void Unpack()
    {
        exitRooms.Clear();
        string description = currentRoom.description;
        foreach(Exit e in currentRoom.exits)
        {  
           description += " " + e.description;
           exitRooms.Add(e.direction.ToString(), e.room);
        }
        InputManager.instance.UpdateStory(description);
    }

    public bool Switch(string direction)
    {
        if (exitRooms.ContainsKey(direction))
        {
            currentRoom = exitRooms[direction];
            InputManager.instance.UpdateStory("You go " +  direction);
            Unpack();
            return true;
        }
        return false;
    }
}
