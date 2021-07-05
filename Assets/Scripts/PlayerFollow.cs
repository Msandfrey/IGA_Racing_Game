using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 upOffset;
    public float behindOffset;
    [Range(0, 1)]
    public float smoothBrainFactor = .4f;
    public bool lookAtOn = false;
    private void Start()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = followTarget.position + upOffset + (-followTarget.forward * behindOffset);
        float blend = 1f - Mathf.Pow(1f - smoothBrainFactor, Time.deltaTime * 30);//30 is the framerate (should be)
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, blend);
        transform.position = smoothPos;
        if (lookAtOn) { transform.LookAt(followTarget); }
    }
}
