using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasPile : MonoBehaviour
{
    [SerializeField] private GameObject[] Skarpetkas;
    [SerializeField] private Transform SkarpetkasParent;
    [SerializeField] private SpriteRenderer PileRenderer;
    [SerializeField] private Sprite[] PileStates;
    private int nextSkarpetkaIndex = 0;

    public SkarpetkaController SpawnSkarpetka(Vector3 pos)
    {
        if (SkarpetkasLeft > 0)
        {
            GameObject skarpetka = Instantiate(Skarpetkas[nextSkarpetkaIndex],pos,Quaternion.identity,SkarpetkasParent);
            SkarpetkaController controller = skarpetka.GetComponent<SkarpetkaController>();

            nextSkarpetkaIndex++;
            return controller;
        }
        else
        {
            Debug.LogError("No more skarpetkas in the pile");
            return null;
        }
    }

    public int SkarpetkasLeft
    {
        get
        {
            return Skarpetkas.Length - nextSkarpetkaIndex;
        }
    }

    public void InitializePile(GameObject[] skarpetkas)
    {
        Skarpetkas = skarpetkas;
        nextSkarpetkaIndex = 0;
    }

    private void UpdateState()
    {
        Debug.LogError("TODO");
    }
}
