using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Timer timer;

    private bool levelWasFailed = false;

#if UNITY_EDITOR
    public static float BasicTime { private set; get; } = 10;
#else
    public static float BasicTime { private set; get; } = 60;
#endif

    // Start is called before the first frame update
    public void StartLevel()
    {
        Debug.Log("Level number: " + (GameState.levelCompleted + 1));

        float multiplier = 0.8f;
        timer.RemainingTime = BasicTime * Mathf.Pow(multiplier, GameState.levelCompleted);
        timer.StartTimer();

        Debug.Log("Time for level: " + timer.RemainingTime);
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
            
            if (GameState.remainingLifes <= 0)
            {
                //Game over
            }
        }
    }
}
