using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    [HideInInspector]
    public GameObject playerCar;
    [HideInInspector]
    public string playerCarColor;//maybe an int
    public bool openingSequenceSeen = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //reload current scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                //go back to main menu
                SceneManager.LoadScene(0);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseUnpause();
            }
        }
    }

    public void PauseUnpause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}
