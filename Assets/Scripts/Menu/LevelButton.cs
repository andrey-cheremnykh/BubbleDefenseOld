using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] GameObject disableImage;
    [SerializeField] GameObject completeImage;
    TMP_Text buttonText;
    int levelIndex;
   
    public void SetButtonParams(int buttonIndex)
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        int levelsCompleted = PlayerPrefs.GetInt("levels-open") + 1;
        levelIndex = buttonIndex;
        buttonText.text = $"Level {levelIndex}";
        GetComponent<Button>().onClick.AddListener(LoadLevel    );
        if (levelIndex < levelsCompleted) Complete();
        else if (levelIndex < levelsCompleted + 1) Current();
        else Disable();
    }

    private void Disable()
    {
        GetComponent<Button>().interactable = false;
        completeImage.SetActive(false);
        disableImage.SetActive(true);
    }private void Current()
    {
        GetComponent<Button>().interactable = false;
        completeImage.SetActive(false);
        disableImage.SetActive(false);
    }
    void Complete()
    {
        completeImage.SetActive(true);
        disableImage.SetActive(false);
    }
    void LoadLevel()
    {

    }
}
