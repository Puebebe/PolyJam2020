using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Timer : MonoBehaviour
{
    public UnityEvent Tick;
    private float elapsedTime = 0f;
    private bool running = false;

    public void Set(float tickDelay)
    {
        Stop();
        running = true;
        this.StartCoroutine(tick(tickDelay));
    }

    private IEnumerator tick(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (running)
        {
            elapsedTime += delay;
            Tick.Invoke();
            this.StartCoroutine(tick(delay));
        }
    }

    public void Stop()
    {
        elapsedTime = 0f;
        running = false;
        this.StopAllCoroutines();
    }

    public float ElapsedTime
    {
        get
        {
            return elapsedTime;
        }
    }


}
