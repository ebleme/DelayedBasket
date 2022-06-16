using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class TouchInputManager : Singleton<TouchInputManager>
{
    private TouchInputs touchInputs;

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;

    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private void Awake()
    {
           touchInputs = new TouchInputs();
    }

    private void OnEnable()
    {
        touchInputs.Enable();
    }

    private void OnDisable()
    {
        touchInputs.Disable();  
    }

    private void Start()
    {
        touchInputs.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchInputs.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch started " + touchInputs.Touch.TouchPosition.ReadValue<Vector2>());

        if (OnStartTouch != null)
        {
            OnStartTouch(touchInputs.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Ended");

        if (OnEndTouch != null)
        {
            OnEndTouch(touchInputs.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }
}
