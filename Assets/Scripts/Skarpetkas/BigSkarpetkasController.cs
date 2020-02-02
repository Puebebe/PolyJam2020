using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSkarpetkasController : MonoBehaviour
{
    private Vector3 Destination;
    private Vector3 Initial;
    private float TimeNeeded;
    private float passedTime = 0f;

    public void Appear(float duration)
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(ScaleUp(duration));
    }

    public void GoToAndDie(Vector3 destination , float time)
    {
        Initial = transform.position;
        Destination = destination;
        TimeNeeded = time;
        passedTime = 0f;
        StartCoroutine(move());
    }

    private IEnumerator move()
    {
        while (passedTime < TimeNeeded)
        {
            transform.position = Vector3.Lerp(Initial, Destination, passedTime / TimeNeeded);
            passedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }

    private IEnumerator ScaleUp(float time)
    {
        float timepassed = 0f;
        while (timepassed < time)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, passedTime / time);
            passedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.one;
    }
}
