using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IPlatform
{
    [SerializeField]
    private List<Material> breakMaterials;

    [SerializeField]
    private float jumpForce = 0f;

    [SerializeField]
    private float breakTimeOnSeconds = 3f;

    [SerializeField]
    private float destroyTimeAfterBreak = 1f;


    private bool isTouched;

    public void BallTouch(Rigidbody ballRigidbody)
    {
        isTouched = true;
    }


    float timeAfterFirstTouch = 1;

    private void Update()
    {
        if (!isTouched)
            return;

        timeAfterFirstTouch -= Time.deltaTime;

        if (timeAfterFirstTouch < 0)
        {
            if (breakMaterials.Count == 0)
            {
                Destroy(gameObject);
                return;
            }

            gameObject.GetComponent<Renderer>().material = breakMaterials[0];
            breakMaterials.RemoveAt(0);

            timeAfterFirstTouch = breakMaterials.Count == 0 ? destroyTimeAfterBreak : 1;
        }
    }
}
