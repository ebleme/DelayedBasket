using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IPlatform
{
    public void BallTouch(Rigidbody ballRigidbody)
    {
        Debug.Log("Touched to Stone");
    }
}
