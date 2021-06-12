using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint1 : MonoBehaviour
{

    private TutManager tMan;
    // Start is called before the first frame update
    void Start()
    {
        tMan = GetComponentInParent<TutManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            tMan.PassTurn();
            gameObject.SetActive(false);
        }
    }
}
