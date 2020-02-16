using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameStateManager : MonoBehaviour
{
    public GameObject Visuals;
    public GameObject TimeUpCanvas;
    public GameObject VictoryCanvas;
    public GameObject GameOverCanvas;
    public Timer timer;
    //public Button TimeUpRetry;
    //public Button TimeUpMenu;
    //public Button GameOverRetry;
    //public Button GameOverMenu;
    //public Camera MainCamera;
    //public Scene GameplayScene;
    //public Scene MenuScene;

    private void Awake()
    {
        
    }

    public void victory()
    {
        Visuals.SetActive(false);
        VictoryCanvas.SetActive(true);
        timer.isOn = false;
    }

    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
        Visuals.SetActive(false);
        timer.isOn = false;
    }
    public void TimeUp()
    {
        TimeUpCanvas.SetActive(true);
        Visuals.SetActive(false);
    }

    public void TimeUpRetryPress()
    {
        TimeUpCanvas.SetActive(false);
        Visuals.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void ButtonVictoryNext()
    {
        if (GameState.remainingLifes < 5)
            GameState.remainingLifes++;
        GameState.levelCompleted++;
        VictoryCanvas.SetActive(false);
        Visuals.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }

    public void ButtonMenuPress()
    {
        TimeUpCanvas.SetActive(false);
        GameOverCanvas.SetActive(false);
        Visuals.SetActive(false);
        SceneManager.LoadScene("Menu");
    }

    public void GameOverRetryPress()
    {
        GameOverCanvas.SetActive(false);
        Visuals.SetActive(true);
        GameState.ResetGameState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
