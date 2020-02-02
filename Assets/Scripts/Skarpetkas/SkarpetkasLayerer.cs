using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasLayerer : MonoBehaviour
{
    [SerializeField] SkarpetkasFinder Finder;

    public void LayerSkarpetkas(GameObject[] Skarpetkas = null, GameObject PrioritySkarpetka = null, bool forceUpdate = false)
    {

        

        if (Skarpetkas == null)
        {
            Skarpetkas = Finder.FindSkarpetkas(forceUpdate);
        }

        Debug.Log("Found: " + Skarpetkas.Length + " skarpetkas");

        int baseLayer = 0;
        int priorityLayer = 0;

        if (PrioritySkarpetka != null)
        {
            baseLayer = -2;
            PrioritySkarpetka.transform.SetAsFirstSibling();
        }

        for (int i = 0; i < Skarpetkas.Length; i++)
        {
            SpriteRenderer[] renderers = Skarpetkas[i].GetComponentsInChildren<SpriteRenderer>();
            for (int j = 0; j < renderers.Length; j++)
            {
                if (Skarpetkas[i] != PrioritySkarpetka)
                {
                    renderers[j].sortingOrder = baseLayer;
                    baseLayer--;
                }
                else
                {
                    renderers[j].sortingOrder = priorityLayer;
                    priorityLayer--;
                }              
            }
        }

    }

    public void Layerskarpetkas(GameObject PrioritySkarpetka)
    {
        LayerSkarpetkas(null, PrioritySkarpetka);
    }

    public void LayerSkarpetkas(GameObject[] Skarpetkas)
    {
        LayerSkarpetkas(Skarpetkas, null);
    }

    public void LayerSkarpetkas()
    {
        LayerSkarpetkas(null, null);
    }
}
