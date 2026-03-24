using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData
{
    public string currentRoom;
    public List<string> currentInventory = new List<string>();
}

