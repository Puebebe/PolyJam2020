using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasMover : MonoBehaviour
{
    [SerializeField] private SkarpetkasFinder Finder;
    [SerializeField] private SkarpetkasLayerer Layerer;
    [SerializeField] private float PickReach;
    [SerializeField] private InputController Input;
    private Vector3 PickOffset = Vector3.zero;
    private GameObject PickedSkarpetka = null;
    private bool Picked = false;

    public void AttemptToPickSkarpetka()
    {
        Vector3 MousePos = Input.InputToWorldPosition();
        Transform closestSkarpetka = null;
        GameObject[] Skarpetkas = Finder.FindSkarpetkas();
        for (int i = 0; i < Skarpetkas.Length; i++)
        {
            Transform skarpetka = Skarpetkas[i].transform;
            float distance = Vector3.Distance(skarpetka.position, MousePos);
            if (distance <= PickReach && ( closestSkarpetka == null || distance < Vector3.Distance(closestSkarpetka.position, MousePos) ))
            {
                closestSkarpetka = skarpetka;
            }
        }

        if (closestSkarpetka != null)
        {
            PickedSkarpetka = closestSkarpetka.gameObject;
            PickOffset = closestSkarpetka.position - MousePos;
            Picked = true;
            Layerer.LayerSkarpetkas(Skarpetkas, PickedSkarpetka);
            StartCoroutine(MoveSkarpetka());
        }
    }

    public void DropSkarpetka()
    {
        Picked = false;
        PickedSkarpetka = null;
    }

    private IEnumerator MoveSkarpetka()
    {
        while (Picked)
        {
            PickedSkarpetka.transform.position = (Vector3)Input.InputToWorldPosition() + PickOffset;
            yield return new WaitForEndOfFrame();
        }
        PickedSkarpetka = null;
    }

    public GameObject GetPickedSkarpetka()
    {
        return PickedSkarpetka;
    }

}
