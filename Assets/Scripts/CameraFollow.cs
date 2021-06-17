using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.rotation = target.rotation;
        transform.Rotate(10, 0, 0);
    }
}
