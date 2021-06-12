using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint2 : MonoBehaviour
{
    private TutManager tMan;
    // Start is called before the first frame update
    void Start()
    {
        tMan = GetComponentInParent<TutManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        tMan.EndTut();
        gameObject.SetActive(false);
    }
}
