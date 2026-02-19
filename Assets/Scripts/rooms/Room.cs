using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Text Adventure/Room")]
public class Room : ScriptableObject
{
    public string roomName;
    [TextArea]
    public string description;
    public Exit[] exits;
}

