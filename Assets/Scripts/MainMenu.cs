using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject start;
    public GameObject level;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelSelect()
    {
        start.SetActive(false);
        level.SetActive(true);
    }
    public void MattTest()
    {
        SceneManager.LoadScene("MattTest");//rename later maybe
    }
    public void Butterfly()
    {
        SceneManager.LoadScene("ButterflyTest");//rename later maybe
    }
    public void Wave()
    {
        SceneManager.LoadScene("WaveTest");//rename later maybe
    }
}
