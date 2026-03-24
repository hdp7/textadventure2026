using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextSize : MonoBehaviour
{

    public TMP_Text storyText; // the story 
    public TMP_InputField userInput; // the input field object
    public TMP_Text inputText; // part of the input field where user enters response
    public TMP_Text placeHolderText;

    public Slider textSize;
    private float fontSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fontSize = PlayerPrefs.GetFloat("fontSize", textSize.value);
        textSize.onValueChanged.AddListener(UpdateSize);
    }

    // Update is called once per frame
    void UpdateSize(float value)
    {
       ChangeSize(value);
    }

    void ChangeSize(float value)
    {
        inputText.fontSize = value;
        placeHolderText.fontSize = value;
        storyText.fontSize = value; 
        fontSize = PlayerPrefs.GetFloat("fontSize", value);
        PlayerPrefs.SetFloat("fontSize", value);
        PlayerPrefs.Save();
    }
}
