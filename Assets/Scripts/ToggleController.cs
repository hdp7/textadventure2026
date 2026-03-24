using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Image background;
    public TMP_Text storyText;
    public Image inputBackground;
    public TMP_Text inputText;
    public TMP_Text placeholderText;
    public Text toggleText;
    private bool darkmode;
    private Toggle toggle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //if(PlayerPrefs.HasKey("darkmode"))
        darkmode = PlayerPrefs.GetInt("darkmode", 1) == 1 ? true : false;
        PlayerPrefs.Save();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(UpdateTheme);
    }

    void UpdateTheme(bool isChecked)
    {
        darkmode = isChecked;
        PlayerPrefs.SetInt("darkmode", darkmode ? 1 : 0);
        SetTheme();
    }


    void SetTheme()
    {

        if (darkmode)
        {
            toggle.isOn = true; //check box
            background.color = Color.black;
            inputBackground.color = Color.black;
            storyText.color = Color.white;
            inputText.color = Color.white;
            placeholderText.color = Color.white;
            toggleText.color = Color.white;
        }
        else
        {
            toggle.isOn = false; //uncheck box
            background.color = Color.white;
            inputBackground.color = Color.white;
            storyText.color = Color.black;
            inputText.color = Color.black;
            placeholderText.color = Color.black;
            toggleText.color = Color.black;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
