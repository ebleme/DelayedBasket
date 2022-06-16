using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragRotate : MonoBehaviour
{
    const string DragTag = "Platform";

    [SerializeField]
    private Vector3 rotateVector = Vector3.zero;


    //[SerializeField]
    //private float mouseDragSpeed = .1f;

    //private Vector3 velocity = Vector3.zero;


    GameObject dragObject;

    PlatformInputControls inputControls;

    private void Awake()
    {
        inputControls = new PlatformInputControls();

    }

    private void OnEnable()
    {
        inputControls.Enable();
        inputControls.General.RotateLeft.performed += RotateLeft;
        inputControls.General.RotateRight.performed += RotateRight;

        inputControls.General.Drag.performed += MousePressed;
        inputControls.General.Drag.canceled += MouseClick_canceled;
    }

    private void OnDisable()
    {

        inputControls.Disable();
        inputControls.General.RotateLeft.performed -= RotateLeft;
        inputControls.General.RotateRight.performed -= RotateRight;
        inputControls.General.Drag.performed -= MousePressed;
        inputControls.General.Drag.canceled -= MouseClick_canceled;
    }

    private void MouseClick_canceled(InputAction.CallbackContext obj)
    {
        dragObject = null;
        Cursor.visible = true;
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.CompareTag(DragTag))
                return;

            dragObject = hit.collider.gameObject;
            Cursor.visible = false;


            StartCoroutine(Drag(hit.collider.gameObject, context));
        }
    }

    public void RotateLeft(InputAction.CallbackContext obj)
    {
        dragObject?.transform.Rotate(rotateVector);
    }

    public void RotateRight(InputAction.CallbackContext obj)
    {
        dragObject?.transform.Rotate(-1 * rotateVector);
    }

    private IEnumerator Drag(GameObject obj, InputAction.CallbackContext context)
    {
        while (context.performed)
        {
            #region Gives 3D drag perspective
            //float distance = Vector3.Distance(obj.transform.position, Camera.main.transform.position);
            //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            //obj.transform.position = Vector3.SmoothDamp(obj.transform.position, ray.GetPoint(distance), ref velocity, mouseDragSpeed); 
            #endregion

            #region 2D Drag perspective Only X and Y

            Vector2 pos =  Mouse.current.position.ReadValue();
            Vector3 position = new Vector3()
            {
                x = pos.x,
                y = pos.y,
                z = Camera.main.WorldToScreenPoint(obj.transform.position).z
            };

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            dragObject.transform.position = worldPosition; 

            #endregion

            // yields until to next frame
            yield return null;
        }
    }


    #region Old Select & Drag & Rotate

    //void Update()
    //{
    //    SelectDeselect();

    //    Drag();

    //    Rotate();
    //}

    private void SelectDeselect()
    {
        if (Input.GetMouseButton(0))
        {
            if (dragObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag(DragTag))
                        return;

                    dragObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
        }
        else
        {
            dragObject = null;
            Cursor.visible = true;
        }
    }

    private void Drag()
    {
        if (dragObject != null)
        {
            Vector3 position = new Vector3()
            {
                x = Input.mousePosition.x,
                y = Input.mousePosition.y,
                z = Camera.main.WorldToScreenPoint(dragObject.transform.position).z
            };

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            dragObject.transform.position = worldPosition;
        }
    }

    private void Rotate()
    {
        if (dragObject == null)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            dragObject.transform.Rotate(rotateVector);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            dragObject.transform.Rotate(-1 * rotateVector);
        }
    }

    private RaycastHit CastRay()
    {

        // +++ ScreenToWorldPoint fonksiyonunda objenin cameradan kesin uzaklýðýnýn bilinmesi gerekli
        Vector3 screenMousePosFar = new Vector3()
        {
            x = Input.mousePosition.x,
            y = Input.mousePosition.y,
            z = Camera.main.farClipPlane
        };

        Vector3 screenMousePosNear = new Vector3()
        {
            x = Input.mousePosition.x,
            y = Input.mousePosition.y,
            z = Camera.main.nearClipPlane
        };

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        // ---


        RaycastHit hit;

        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    } 

    #endregion
}
