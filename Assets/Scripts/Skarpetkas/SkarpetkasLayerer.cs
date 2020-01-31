﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasLayerer : MonoBehaviour
{
    [SerializeField] SkarpetkasFinder Finder;

    public void LayerSkarpetkas(GameObject[] Skarpetkas = null, GameObject PrioritySkarpetka = null)
    {
        if (Skarpetkas == null)
        {
            Skarpetkas = Finder.FindSkarpetkas();
        }

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
            for (int j = renderers.Length-1; j > -1; j--)
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