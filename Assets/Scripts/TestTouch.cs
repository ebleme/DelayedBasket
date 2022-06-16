using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTouch : MonoBehaviour
{
    private TouchInputManager touchInputManager;
    private Camera cameraMain;

    private void Awake()
    {
        touchInputManager = TouchInputManager.Instance;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        touchInputManager.OnStartTouch += Move;
    }

    private void OnDisable()
    {
        touchInputManager.OnStartTouch -= Move;
    }

    private void Move(Vector2 position, float time)
    {
        Vector3 screenCoordinates = new Vector3(position.x, position.y, cameraMain.nearClipPlane);

        Vector3 worldCoordinated = cameraMain.ScreenToWorldPoint(screenCoordinates);

        worldCoordinated.z = 0;
        transform.position = worldCoordinated;
    }
}
