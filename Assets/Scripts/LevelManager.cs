using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Timer timer;
    public static float BasicTime { private set; get; } = 10;   //TODO change to 60 sec

    // Start is called before the first frame update
    public void StartNextLevel()
    {
        Debug.Log("Level number: " + (GameState.levelCompleted + 1));

        float multiplier = 0.8f;
        timer.RemainingTime = BasicTime * Mathf.Pow(multiplier, GameState.levelCompleted);
        timer.StartTimer();

        Debug.Log("Time for level: " + timer.RemainingTime);
    }

    public void ReplayLevel()
    {

    }

    void Start()
    {
        //TODO move call StartNextLevel to button OnClick
        StartNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.RemainingTime < 0)
        {
            timer.isOn = false;
        }
    }
}
