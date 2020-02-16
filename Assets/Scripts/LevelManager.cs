using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Timer timer;
    public UIManager uiManager;

    private bool levelWasFailed = false;
    private const float BASIC_TIME = 60;
    public static float TimeForLevel { private set; get; } = BASIC_TIME;

    // Start is called before the first frame update
    public void StartLevel()
    {
        Debug.Log("Level number: " + (GameState.levelCompleted + 1));

        float multiplier = 0.8f;
        timer.RemainingTime = BASIC_TIME * Mathf.Pow(multiplier, GameState.levelCompleted);
        TimeForLevel = timer.RemainingTime;
        timer.StartTimer();

        GameState.remainingSocksPairs = GameState.socksPairsForLevel;
        uiManager.UpdateLifes(GameState.remainingLifes);

        Debug.Log("Socks pairs: " + GameState.remainingSocksPairs);
        Debug.Log("Time for level: " + TimeForLevel);
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        //TODO move call StartNextLevel to button OnClick
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (!levelWasFailed && timer.RemainingTime < 0)
        {
            //Fail level
            levelWasFailed = true;
            timer.isOn = false;
            GameState.remainingLifes--;
            uiManager.UpdateLifes(GameState.remainingLifes);

            if (GameState.remainingLifes <= 0)
                GetComponent<GameStateManager>().GameOver();
            else
                GetComponent<GameStateManager>().TimeUp();
        }
    }
}
