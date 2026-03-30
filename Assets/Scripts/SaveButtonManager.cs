using UnityEngine;
using UnityEngine.UI;

public class SaveButtonManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Button saveButton;
    public AudioSource saveSound;

    void Start()
    {
        saveButton = GetComponent<Button>();
        saveButton.onClick.AddListener(Save);    
    }

    void Save()
    {
        GameManager.instance.Save();
        saveSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
