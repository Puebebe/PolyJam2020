using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasMover : MonoBehaviour
{
    [SerializeField] private SkarpetkasFinder Finder;
    [SerializeField] private SkarpetkasLayerer Layerer;
    [SerializeField] private float PickReach;
    [SerializeField] private InputController Input;
    [SerializeField] private SkarpetkasPile Pile;
    private Vector3 PickOffset = Vector3.zero;
    private GameObject PickedSkarpetka = null;
    private SkarpetkaController PickedController = null;
    private bool Picked = false;

    public void AttemptToPickSkarpetka()
    {
        Vector3 MousePos = Input.InputToWorldPosition();
        
        Transform closestSkarpetka = null;
        SkarpetkaController closestController = null;

        GameObject skarpetka = Finder.FindClosestSkarpetka(MousePos, null);
        if (skarpetka != null)
        {
            closestController = skarpetka.GetComponent<SkarpetkaController>();
        }

        if (Pile.SkarpetkasLeft > 0 && Vector3.Distance(MousePos, Pile.transform.position) <= PickReach)
        {
            //Debug.Log("Pile in range and has skarpetkas");
            if (closestController == null || Vector3.Distance(MousePos, closestController.transform.position) > Vector3.Distance(MousePos, Pile.transform.position))
            {
                //Debug.Log("Mover wants skarpetka");
                closestController = Pile.SpawnSkarpetka(MousePos);
                closestSkarpetka = closestController.transform;
            }
        }
        else if (closestController != null && Vector3.Distance(closestController.transform.position, MousePos) <= PickReach)
        {
            closestSkarpetka = closestController.transform;
        }

        if (closestSkarpetka != null)
        {
            PickedSkarpetka = closestSkarpetka.gameObject;
            PickedController = PickedSkarpetka.GetComponent<SkarpetkaController>();
            PickOffset = closestSkarpetka.position - MousePos;
            Picked = true;
            Layerer.LayerSkarpetkas(null, PickedSkarpetka, true);
            StartCoroutine(MoveSkarpetka());
        }
    }

    public void DropSkarpetka()
    {
        Picked = false;
        if (PickedController != null && PickedController.Pair == null)
        {
            PickedController.AttemptPairing();
        }
        PickedSkarpetka = null;
        PickedController = null;
    }

    private IEnumerator MoveSkarpetka()
    {
        while (Picked)
        {
            PickedController.MoveTo((Vector3)Input.InputToWorldPosition() + PickOffset);
            yield return new WaitForEndOfFrame();
        }
    }

    public GameObject GetPickedSkarpetka()
    {
        return PickedSkarpetka;
    }

    public SkarpetkaController GetPickedController()
    {
        return PickedController;
    }

    public void AttemptToUnPairSkarpetkas()
    {
        //Debug.Log("Mover attempting to unpair");
        Vector3 MousePos = Input.InputToWorldPosition();
        GameObject Closest = Finder.FindClosestSkarpetka(MousePos, null);
        if (Closest != null)
        {
            SkarpetkaController ClosestController = Closest.GetComponent<SkarpetkaController>();
            if (Vector3.Distance(Closest.transform.position, MousePos) <= PickReach && ClosestController.Pair != null)
            {
                ClosestController.AttemptToUnPair();
            }
        }
    }

}
