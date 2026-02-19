using UnityEngine;

[CreateAssetMenu(fileName = "Exit", menuName = "Text Adventure/Exit")]
public class Exit : ScriptableObject
{
    public enum Direction {north, south, east, west};
    public Direction direction;
    [TextArea]
    public string description;
    public Room room; //room that the exit is attached to

}
