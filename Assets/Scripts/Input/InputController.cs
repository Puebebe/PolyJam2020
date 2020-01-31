using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    private PlayerInput Input;
    private bool pressing = false;
    public UnityEvent Pressed;
    public UnityEvent Canceled;
    public UnityEvent DoublePressed;

    private void Awake()
    {
        Input = new PlayerInput();
        Input.Enable();
        Input.InGame.Press.performed += ctx => StartPressing();
        Input.InGame.DoublePress.performed += ctx => DoublePress();
        Input.InGame.Press.canceled += ctx => EndPressing();
    }

    private void StartPressing()
    {
        pressing = true;
        Pressed.Invoke();
    }

    private void EndPressing()
    {
        pressing = false;
        Canceled.Invoke();
    }

    private void DoublePress()
    {
        pressing = true;
        DoublePressed.Invoke();
        pressing = false;
    }

    public Vector2 InputToWorldPosition()
    {
        if (pressing)
        {
            Vector2 inputPos = Input.InGame.InputPosition.ReadValue<Vector2>();
            Vector2 worldPos = Cam.ScreenToWorldPoint(inputPos);
            return worldPos;
        }
        else
        {
            Debug.LogError("Tried to read input but there is no input");
            return Vector2.zero;
        }
        
    }


}
