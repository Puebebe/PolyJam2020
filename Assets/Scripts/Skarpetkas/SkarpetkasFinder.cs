using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasFinder : MonoBehaviour
{
    [SerializeField] Transform SkarpetkasParent;
    private int frame = -1;
    private GameObject[] Skarpetkas;

    public GameObject[] FindSkarpetkas()
    {
        if (Time.frameCount == frame)
        {
            return Skarpetkas;
        }
        else
        {
            frame = Time.frameCount;

            GameObject[] result = new GameObject[SkarpetkasParent.childCount];
            for (int i = 0; i < SkarpetkasParent.childCount; i++)
            {
                result[i] = SkarpetkasParent.GetChild(i).gameObject;
            }

            return result;
        }
    }
}
