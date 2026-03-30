using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextSize : MonoBehaviour
{

    public TMP_Text storyText; // the story 
    public TMP_InputField userInput; // the input field object
    public TMP_Text inputText; // part of the input field where user enters response
    public TMP_Text placeHolderText;// placeholder

    public Slider textSize;
    private float fontSize;

    void Start()
    {
        fontSize = PlayerPrefs.GetFloat("fontSize", textSize.value);
        textSize.onValueChanged.AddListener(ChangeSize);
        textSize.value = fontSize;
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
