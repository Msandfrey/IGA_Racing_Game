using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    public float smoothBrainFactor = .1f;
    private void Start()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = followTarget.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothBrainFactor);
        transform.position = smoothPos;
    }
}
