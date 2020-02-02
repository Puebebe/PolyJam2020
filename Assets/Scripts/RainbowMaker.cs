using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowMaker : MonoBehaviour
{
    public Camera MainCamera;
    public GameObject image;
    public float wildnessFactor = 0.1f;
    public float variationFactor = 0.2f;
    public float iAmSpeed = 0.02f;
    public float calmFactor = 0.05f;    
    SpriteRenderer SR;
    public float HueVelocity = 0;
    public float HueAcceleration = 0;
    public float Hue, Sat, V;
    float Red, Green, Blue;
    public float wH, wS, wV;
    float wR, wG, wB;
    float dt = 0.01f;
    public bool LETSGETWILD = false;
    void Awake()
    {
        SR = image.GetComponent<SpriteRenderer>();
        Color.RGBToHSV(SR.color, out Hue, out Sat, out V);
        Color.RGBToHSV(MainCamera.backgroundColor, out wH, out wS, out wV);
    }

    void Update()
    {
        if (LETSGETWILD)
        {
            Hue += iAmSpeed * HueVelocity * dt;
            if (Hue > 1) Hue -= 1;
            if (Hue < 0) Hue += 1;
            HueVelocity += HueAcceleration * dt - calmFactor * HueVelocity * iAmSpeed;
            HueAcceleration += Random.Range(-variationFactor, variationFactor) - calmFactor * HueAcceleration;
            //camera.backgroundColor
            SR.color = Color.HSVToRGB(Hue, Sat, V);
        }

        wH += wildnessFactor * dt;
        if (wH > 1) wH -= 1;
        if (wH < 0) wH += 1;
        MainCamera.backgroundColor = Color.HSVToRGB(wH, wS, wV);

    }
}
