using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    const string PlatformTag = "Platform";

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(PlatformTag))
        {
            collision.gameObject.GetComponent<IPlatform>().BallTouch(rb);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag(PlatformTag))
    //    {
    //        other.gameObject.GetComponent<IPlatform>().BallTouch(rb);
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(PlatformTag))
        {
            other.gameObject.GetComponent<IPlatform>().BallTouch(rb);
        }
    }
}
