using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasPile : MonoBehaviour
{
    [SerializeField] private SkarpetkasFinder skarpetkasFinder;
    [SerializeField] private SockGenerator sockGenerator;
    [SerializeField] private List<GameObject> Skarpetkas;
    //[SerializeField] private Transform SkarpetkasParent;
    [SerializeField] private SpriteRenderer PileRenderer;
    [SerializeField] private Sprite[] PileStates;
    private int RemovedSkarpetkas = 0;

    public SkarpetkaController SpawnSkarpetka(Vector3 pos)
    {
        if (Skarpetkas.Count > 0)
        {
            GameObject skarpetka = Skarpetkas[0];
            Skarpetkas.RemoveAt(0);
            skarpetka.transform.position = pos;
            skarpetka.SetActive(true);
            SkarpetkaController controller = skarpetka.GetComponent<SkarpetkaController>();

            RemovedSkarpetkas++;
            UpdateState();
            return controller;
        }
        else
        {
            Debug.LogError("No more skarpetkas in the pile");
            return null;
        }
    }

    public void RemovePairedSocks(Sock sock1, Sock sock2)
    {
        Sock pairSock1, pairSock2;
        pairSock1 = pairSock2 = null;

        for (int i = 0; i < Skarpetkas.Count; i++)
        {
            if (Skarpetkas[i].GetComponent<Sock>().Equals(sock1))
            {
                pairSock1 = Skarpetkas[i].GetComponent<Sock>();
                Debug.Log("pairSock1 found");
            }
            if (Skarpetkas[i].GetComponent<Sock>().Equals(sock2))
            {
                pairSock2 = Skarpetkas[i].GetComponent<Sock>();
                Debug.Log("pairSock2 found");
            }
        }

        if (pairSock1 != null)
        {
            Skarpetkas.Remove(pairSock1.gameObject);
            Destroy(pairSock1.gameObject);
        }
        else
        {
            GameObject[] skarpetkaas = skarpetkasFinder.FindSkarpetkas();
            if (skarpetkaas.Length == 0)
                Debug.LogError("Finder found nothing");

            for (int i = 0; i < skarpetkaas.Length; i++)
            {
                if (skarpetkaas[i].GetComponent<Sock>().Equals(sock1))
                    pairSock1 = skarpetkaas[i].GetComponent<Sock>();
            }
            Destroy(pairSock1.gameObject);
        }
        if (pairSock2 != null)
        {
            Skarpetkas.Remove(pairSock2.gameObject);
            Destroy(pairSock2.gameObject);
        }
        else
        {
            GameObject[] skarpetkaas = skarpetkasFinder.FindSkarpetkas();
            if (skarpetkaas.Length == 0)
                Debug.LogError("Finder found nothing");
            for (int i = 0; i < skarpetkaas.Length; i++)
            {
                if (skarpetkaas[i].GetComponent<Sock>().Equals(sock2))
                    pairSock2 = skarpetkaas[i].GetComponent<Sock>();
            }

            Destroy(pairSock2.gameObject);
        }
    }

    public int SkarpetkasLeft
    {
        get
        {
            return (GameState.socksPairsForLevel * 2) - RemovedSkarpetkas;
        }
    }

    public void InitializePile(GameObject[] skarpetkas)
    {
        Skarpetkas = new List<GameObject>();
        Skarpetkas.AddRange(skarpetkas);
        RemovedSkarpetkas = 0;
        UpdateState();
    }

    private void UpdateState()
    {
        float progress = (float)SkarpetkasLeft / ((float)GameState.socksPairsForLevel * 2f);
        int chosenIndex = 0;
        for (int i = 0; i < PileStates.Length; i++)
        {
            float scale = ((float)i - 1f) / ((float)PileStates.Length - 1f);
            //Debug.Log("P: " + progress + " S: " + scale);
            if (progress > scale)
            {
                chosenIndex = i;
            }
        }
        if (progress == 0)
        {
            PileRenderer.sprite = PileStates[0];
        }
        else
        {
            PileRenderer.sprite = PileStates[chosenIndex];
        }

    }

    void Start()
    {
        InitializePile(sockGenerator.GenerateSockPile(GameState.socksPairsForLevel));
    }
}
