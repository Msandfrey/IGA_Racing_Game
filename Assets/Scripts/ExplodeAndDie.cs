using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAndDie : MonoBehaviour
{
    private ParticleSystem a;
    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a.isStopped)
        {
            Destroy(gameObject);
        }
    }
    
}