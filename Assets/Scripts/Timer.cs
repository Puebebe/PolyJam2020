using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float RemainingTime { get; set; }
    public bool isOn = false;

    public void StartTimer()
    {
        isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
            RemainingTime -= Time.deltaTime;
    }
}
