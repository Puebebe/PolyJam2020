using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarpetkasMover : MonoBehaviour
{
    [SerializeField] private SkarpetkasFinder Finder;
    [SerializeField] private SkarpetkasLayerer Layerer;
    [SerializeField] private float PickReach;
    [SerializeField] private float InsertReach;
    [SerializeField] private InputController Input;
    [SerializeField] private SkarpetkasPile Pile;
    [SerializeField] private Transform BasketPosition;
    private Vector3 PickOffset = Vector3.zero;
    private GameObject PickedSkarpetka = null;
    private SkarpetkaController PickedController = null;
    private bool Picked = false;
    private Vector3 StartPos;

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
            StartPos = closestSkarpetka.position;
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
        if (PickedController != null)
        {
            if (Vector3.Distance(PickedController.transform.position, BasketPosition.position) <= InsertReach)
            {
                if (PickedController.Pair == null)
                {
                    PickedController.MoveTo(StartPos);
                }
                else
                {
                    InsertIntoBasket(PickedController);
                }
            }
            else
            {
                if (PickedController.Pair == null)
                {
                    PickedController.AttemptPairing();
                }
            }
            
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

    private void InsertIntoBasket(SkarpetkaController sock)
    {
        if (sock.Pair == null)
        {
            Debug.LogError("WTF?");
            return;
        }

        Sock firstSock = sock.GetComponent<Sock>();
        Sock secondSock = sock.Pair.GetComponent<Sock>();

        if (firstSock.Equals(secondSock))
        {
            //TODO beautiful animation of successful paired socks
            //Pile.RemovePairedSocks(firstSock, secondSock);
            Destroy(firstSock.gameObject);
            Destroy(secondSock.gameObject);
        }
        else
        {
            //TODO animation of wrong paired socks
            sock.MoveTo(StartPos);
            //lifes--;
        }
    }

}
