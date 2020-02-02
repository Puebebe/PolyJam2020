using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasPile : MonoBehaviour
{
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

    /*
    public void RemovePairedSocks(Sock sock1, Sock sock2)
    {
        Skarpetkas.Remove(sock1.gameObject);
        Skarpetkas.Remove(sock2.gameObject);
    }
    */

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
