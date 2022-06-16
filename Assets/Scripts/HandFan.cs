using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFan : MonoBehaviour, IPlatform
{
    [SerializeField]
    private Vector3 jumpForce = Vector3.zero;

    public void BallTouch(Rigidbody ballRigidbody)
    {

        jumpForce = jumpForce + new Vector3(transform.localRotation.x, 0, 0);
        ballRigidbody.AddForce(jumpForce, ForceMode.Acceleration);
    }
}
