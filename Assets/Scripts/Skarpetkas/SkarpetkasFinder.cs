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

            Skarpetkas = result;

            return result;
        }
    }

    public GameObject FindClosestSkarpetka(Vector3 position, GameObject ignore)
    {
        GameObject closestSkarpetka = null;
        GameObject[] Skarpetkas = FindSkarpetkas();
        for (int i = 0; i < Skarpetkas.Length; i++)
        {
            if (Skarpetkas[i] != ignore)
            {
                Transform skarpetka = Skarpetkas[i].transform;
                float distance = Vector3.Distance(skarpetka.position, position);
                if (closestSkarpetka == null || distance < Vector3.Distance(closestSkarpetka.transform.position, position))
                {
                    closestSkarpetka = skarpetka.gameObject;
                }
            }
        }
        return closestSkarpetka;
    }
}
