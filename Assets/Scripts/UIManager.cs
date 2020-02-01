﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private Image timerUI;
    [SerializeField] private ParticleSystem Particles;
    [SerializeField] private Vector3 ParticlesStartPos;
    [SerializeField] private Vector3 ParticlesEndPos;
    [SerializeField] private Camera Cam;

    // Update is called once per frame
    void Update()
    {
        if (timer.isOn)
        {
            float progress = timer.RemainingTime / LevelManager.BasicTime;
            timerUI.fillAmount = progress;



            Particles.transform.position = Vector3.Lerp(Cam.ScreenToWorldPoint(CanvasToResolution(ParticlesEndPos)), Cam.ScreenToWorldPoint(CanvasToResolution(ParticlesStartPos)), progress);
            Particles.transform.position = new Vector3(Particles.transform.position.x, Particles.transform.position.y, 0f);

        }
        else
        {
            if (Particles.isPlaying)
            {
                Particles.Stop();
            }
        }
            
    }

    private Vector3 CanvasToResolution(Vector3 input)
    {
        float width = (input.x / 1920f) * Screen.width;
        float height = (input.y / 1080f) * Screen.height;

        return new Vector3(width, height, input.z);
    }
}
