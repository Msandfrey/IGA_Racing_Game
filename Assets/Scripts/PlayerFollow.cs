using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 upOffset;
    public float behindOffset;
    public float smoothBrainFactor = .1f;
    public bool lookAtOn = false;
    private void Start()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = followTarget.position + upOffset + (-followTarget.forward * behindOffset);
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothBrainFactor);
        transform.position = smoothPos;
        if (lookAtOn) { transform.LookAt(followTarget); }
    }
}
