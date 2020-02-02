using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkaController : MonoBehaviour
{
    public SkarpetkaController Pair = null;
    private static float PairingDistance = 0.5f;
    [SerializeField] private static SkarpetkasFinder Finder;

    public void MoveTo(Vector3 destination)
    {
        Vector3 offset = destination - transform.position;
        transform.position += offset;
        if (Pair != null)
        {
            Pair.transform.position += offset;
        }
    }

    public void AttemptPairing()
    {
        if (Pair == null)
        {
            if (Finder == null)
            {
                Finder = GameObject.FindObjectOfType<SkarpetkasFinder>();
            }

            GameObject[] Skarpetkas = Finder.FindSkarpetkas();

            for (int i = 0; i < Skarpetkas.Length; i++)
            {
                if (Vector3.Distance(Skarpetkas[i].transform.position, this.gameObject.transform.position) <= PairingDistance)
                {
                    SkarpetkaController other = Skarpetkas[i].GetComponent<SkarpetkaController>();
                    if (other.Pair == null && other != this && Pair == null)
                    {
                        this.Pair = other;
                        other.Pair = this;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Already Paired");
        }
    }

    public void AttemptToUnPair()
    {
        //Debug.Log("Skarpetka attempting to unpair");
        if (Pair != null)
        {
            Pair.Pair = null;
            this.Pair = null;
        }
        else
        {
            Debug.LogError("Not paired");
        }
    }

}
