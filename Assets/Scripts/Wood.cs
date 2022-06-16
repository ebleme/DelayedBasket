using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour, IPlatform
{
    [SerializeField]
    private Vector3 jumpForce = Vector3.zero;

    public void BallTouch(Rigidbody ballRigidbody)
    {
        ballRigidbody.AddForce(jumpForce, ForceMode.Impulse);
    }
   
}
