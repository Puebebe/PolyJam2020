using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] Image timerUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.isOn)
            timerUI.fillAmount = timer.RemainingTime / LevelManager.BasicTime;
    }
}
