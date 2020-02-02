using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    //public Button playButton;
    //public Button exitButton;
    //bool isHover;
    //float smooth = 100;

    // Update is called once per frame
    //void Update()
    //{
    //    smooth -= 1f;
    //    if (isHover)
    //    {
    //        smooth = 100;
    //        SmoothQuaternion(playButton.gameObject,playButton.transform.rotation, Quaternion.Euler(0, 0, 110));

    //        SmoothQuaternion(exitButton.gameObject, exitButton.transform.rotation, Quaternion.Euler(0, 0, 110));

    //    }
    //    else
    //    {
    //        SmoothQuaternion(playButton.gameObject,playButton.transform.rotation, Quaternion.Euler(0, 0, 100));

    //        SmoothQuaternion(exitButton.gameObject, exitButton.transform.rotation, Quaternion.Euler(0, 0, 100));
    //    }
    //}
    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    isHover = true;
    //    Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    isHover = false;
    //}
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
    //public void SmoothQuaternion(GameObject setTo,Quaternion from, Quaternion to)
    //{
    //    smooth = Mathf.Max(smooth, 0);
    //    setTo.transform.rotation = Quaternion.RotateTowards(from, to, smooth * Time.deltaTime);

    //}
}
