using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private Image timerUI;
    [SerializeField] private ParticleSystem Particles;
    [SerializeField] private Vector3 ParticlesStartPos;
    [SerializeField] private Vector3 ParticlesMidPoint;
    [SerializeField] private Vector3 ParticlesEndPos;
    [SerializeField] private Camera Cam;
    [SerializeField] private GameObject[] LifeSocks;
    [SerializeField] private GameObject BigLifeSockPrefab;
    [SerializeField] private SkarpetkasFinder Finder;

    // Update is called once per frame
    void Update()
    {
        if (timer.isOn)
        {
            float progress = timer.RemainingTime / LevelManager.BasicTime;
            timerUI.fillAmount = progress;

            if (progress > 0.5f)
            {
                Particles.transform.position = Vector3.Lerp(Cam.ScreenToWorldPoint(CanvasToResolution(ParticlesMidPoint)), Cam.ScreenToWorldPoint(CanvasToResolution(ParticlesStartPos)), (progress - 0.5f) * 2f);
            }
            else
            {
                Particles.transform.position = Vector3.Lerp(Cam.ScreenToWorldPoint(CanvasToResolution(ParticlesEndPos)), Cam.ScreenToWorldPoint(CanvasToResolution(ParticlesMidPoint)), progress * 2f);
            }
            
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

    public void WrongInsertion()
    {
        GameObject FirstLifeSock = LifeSocks[GameState.remainingLifes];
        GameObject SecondLifeSock = LifeSocks[GameState.remainingLifes + 1];

        Vector3 FirstSockStart = Cam.ScreenToWorldPoint(CanvasToResolution( new Vector3(1920f, 1080f, 10f) - FirstLifeSock.transform.position));
        Vector3 SecondSockStart = Cam.ScreenToWorldPoint(CanvasToResolution( new Vector3(1920f, 1080f, 10f)- SecondLifeSock.transform.position));

        Debug.Log("FSS: " + FirstSockStart);
        Debug.Log("SSS: " + SecondSockStart);

        GameObject FirstBigLifeSock = Instantiate(BigLifeSockPrefab, FirstSockStart,Quaternion.identity,this.transform);
        GameObject SecondBigLifeSock = Instantiate(BigLifeSockPrefab, SecondSockStart, Quaternion.identity, this.transform);

        GameObject[] BigRegularSocks = GameObject.FindGameObjectsWithTag("BigRegularSock");

        FirstBigLifeSock.GetComponent<BigSkarpetkasController>().Appear(1f);
        SecondBigLifeSock.GetComponent<BigSkarpetkasController>().Appear(1f);

        BigRegularSocks[0].GetComponent<BigSkarpetkasController>().GoToAndDie(new Vector3(-6.25f, -2f,0f), 2f);
        BigRegularSocks[1].GetComponent<BigSkarpetkasController>().GoToAndDie(new Vector3(-2f, -2f, 0f), 2f);

        //FirstBigLifeSock.GetComponent<BigSkarpetkasController>().GoToAndDie(new Vector3(-6.25f, -2f, 0f), 2f);
        //SecondBigLifeSock.GetComponent<BigSkarpetkasController>().GoToAndDie(new Vector3(-2f, -2f, 0f), 2f);

        RefreshLifeSocks();
    }

    public void RefreshLifeSocks()
    {
        ClearLifeSocks();
        for (int i = 0; i < LifeSocks.Length && i < GameState.remainingLifes; i++)
        {
            LifeSocks[i].SetActive(true);
        }
    }

    private void ClearLifeSocks()
    {
        for (int i = 0; i < LifeSocks.Length; i++)
        {
            LifeSocks[i].SetActive(false);
        }
    }
}
