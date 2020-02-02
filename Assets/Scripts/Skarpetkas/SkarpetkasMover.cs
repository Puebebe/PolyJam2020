using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkarpetkasMover : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private SkarpetkasFinder Finder;
    [SerializeField] private SkarpetkasLayerer Layerer;
    [SerializeField] private float PickReach;
    [SerializeField] private float InsertReach;
    [SerializeField] private InputController Input;
    [SerializeField] private SkarpetkasPile Pile;
    [SerializeField] private Transform BasketPosition;
    public UnityEvent PickedSock;
    public UnityEvent DropedSock;
    public UnityEvent PairedSocks;
    public UnityEvent UnPairedSocks;
    public UnityEvent CorrectInsertion;
    public UnityEvent WrongInsertion;
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
            if (closestController == null || Vector3.Distance(MousePos, closestController.transform.position) > Vector3.Distance(MousePos, Pile.transform.position))
            {
                closestController = Pile.SpawnSkarpetka(MousePos);
                closestSkarpetka = closestController.transform;
            }
            else
            {
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
            if (closestController.Pair != null && closestController.Pair.GetComponent<SpriteRenderer>().sortingOrder > closestController.gameObject.GetComponent<SpriteRenderer>().sortingOrder)
            {
                Layerer.LayerSkarpetkas(null, PickedSkarpetka, true);
                Layerer.LayerSkarpetkas(null,closestController.Pair.gameObject,false);
            }
            else
            {
                Layerer.LayerSkarpetkas(null, PickedSkarpetka, true);
            }
            PickedSock.Invoke();
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
                    WrongInsertion.Invoke();
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
                    if (PickedController.Pair == null)
                    {
                        DropedSock.Invoke();
                    }
                    else
                    {
                        PairedSocks.Invoke();
                    }
                }
                else
                {
                    DropedSock.Invoke();
                }
                
            }
            
        }
        PickedSkarpetka = null;
        PickedController = null;
    }

    private IEnumerator MoveSkarpetka()
    {
        //if (przegranko) return;

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

                if (ClosestController.Pair == null)
                {
                    UnPairedSocks.Invoke();
                }
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
            Destroy(firstSock.gameObject);
            Destroy(secondSock.gameObject);
            CorrectInsertion.Invoke();

            GameState.remainingSocksPairs--;

            if (GameState.remainingSocksPairs <= 0)
            {
                //Win level
                gameStateManager.victory();
            }
        }
        else
        {
            //TODO animation of wrong paired socks
            //sock.MoveTo(StartPos);
            Pile.RemovePairedSocks(firstSock, secondSock);
            Destroy(firstSock.gameObject);
            Destroy(secondSock.gameObject);

            WrongInsertion.Invoke();
            GameState.remainingLifes -= 2;
            FindObjectOfType<UIManager>().UpdateLifes(GameState.remainingLifes);
            GameState.remainingSocksPairs -= 2;

            if (GameState.remainingLifes <= 0)
            {
                gameStateManager.GameOver();
            }
        }
    }

}
