using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 offset;
    private Vector3 lastPosition;
    private void Start()
    {
        lastPosition = followTarget.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = lastPosition + offset;
        lastPosition = followTarget.position;
    }
}
