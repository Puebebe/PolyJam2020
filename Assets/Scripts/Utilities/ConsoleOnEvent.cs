using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleOnEvent : MonoBehaviour
{
    [SerializeField] private bool Active = true;
    public void Print(string input)
    {
        if (Active)
        {
            Debug.Log(input);
        }
    }
}
