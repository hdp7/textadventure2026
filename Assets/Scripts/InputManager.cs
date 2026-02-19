using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;


public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public TMP_Text storyText; // the story 
    public TMP_InputField userInput; // the input field object
    public TMP_Text inputText; // part of the input field where user enters response
    public TMP_Text placeHolderText; // part of the input field for initial placeholder text
    
    private string story; // holds the story to display
    private List<string> commands = new List<string>(); //Holds all commands

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
        story = storyText.text;
        userInput.onEndEdit.AddListener(GetInput);
        commands.Add("go");
        commands.Add("get");

    }

    void GetInput(string input)
    {
        char[] delims = { ' ' };
        userInput.text = "";
        userInput.ActivateInputField();

        if (input != "")
        {
            string[] parts = input.ToLower().Split(delims); //parts[0] = command, parts[1] = direction/item to pick up

            if (commands.Contains(parts[0]))
            {
                UpdateStory(input);
                if (parts[0] == "go")
                {
                    if (NavigationManager.instance.Switch(parts[1]))
                    {
                        //Come back to later...
                    }
                    else
                    {
                        UpdateStory("Direction Does not exist, try again");
                    }
                }
                if (parts[0] == "get")
                {
                    if (NavigationManager.instance.GetItem(parts[1]))
                    {
                        GameManager.instance.inventory.Add(parts[1]);
                        UpdateStory("The " + parts[1] + " was added to your inventory");
                    }
                    else
                    {
                        UpdateStory("The " + parts[1] + " was not found.");
                    }
                }
            }
            else
            {
                UpdateStory("Invalid command, try again.");
            }
        }
    }

    public void UpdateStory(string msg)
    {
        story += "\n" + msg;
        storyText.text = story;
    }
}
