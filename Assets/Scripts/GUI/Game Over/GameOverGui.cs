using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverGui : MonoBehaviour
{
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject pausePanel;
    bool isPanelShown;  

    // Start is called before the first frame update
    void Start()
    { 
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        CastleHealth castle = FindObjectOfType<CastleHealth>();
        castle.onDestroy += ShowLosePanel;
        EnemySpawner spawn = FindObjectOfType<EnemySpawner>();
        spawn.onWin += ShowWinPanel;
    }

    public void ShowLosePanel()
    {
        if (isPanelShown == true) return;
        losePanel.SetActive(true);
        isPanelShown = true;
        Time.timeScale = 0;
    }
    public void ShowWinPanel()
    {
        if (isPanelShown == true) return;
        winPanel.SetActive(true);
        isPanelShown = true;
        Time.timeScale = 0;
    }
    public void ShowPausePanel()
    {
        if (isPanelShown == true) return;
        pausePanel.SetActive(true);
        isPanelShown = true;
        Time.timeScale = 0;
    }
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
        isPanelShown = true;
        Time.timeScale = 1;
    }
    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
    public void RestartLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    } 
    public void LoadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
    }
    public  void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
