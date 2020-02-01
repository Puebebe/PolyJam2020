using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDebuger : MonoBehaviour
{
    [SerializeField] TimerOld timer;

    private void Start()
    {
        timer.Set(1f);
    }

    public void Yell()
    {
        Debug.Log("timer time: " + timer.ElapsedTime);
        if (timer.ElapsedTime > 10f)
        {
            timer.Stop();
        }
    }



}
