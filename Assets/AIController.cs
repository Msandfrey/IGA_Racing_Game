using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Follow))]
public class AIController : MonoBehaviour
{
    [HideInInspector]
    public Follow pathFollow;
    private float timer;
    public float delayToStart;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(180, 0, 0);
        pathFollow = GetComponent<Follow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= delayToStart)
        {
            timer += Time.deltaTime;
            return;
        }
        pathFollow.IncreaseSpeed(.001f, 30, 60);
    }
}
