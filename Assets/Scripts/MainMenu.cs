using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject start;
    public GameObject level;
    public GameObject intro;
    private bool hasIntroPlayer = false;
    private bool playIntro = false;
    private bool isPlaying = false;
    [SerializeField]
    private AudioSource introSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playIntro && !hasIntroPlayer)
        {
            introSound.Play();
            isPlaying = true;
            hasIntroPlayer = true;
            playIntro = false;
        }
        if (isPlaying && Input.GetKey(KeyCode.Space))
        {
            isPlaying = false;
            //stop playing sound
            introSound.Stop();
            LevelSelect();
        }
        if (!introSound.isPlaying && hasIntroPlayer)
        {
            isPlaying = false;
            intro.SetActive(false);
            LevelSelect();
        }
    }
    public void EnterLevelSelect()
    {
        playIntro = true;
        start.SetActive(false);
        intro.SetActive(true);
    }
    public void LevelSelect()
    {
        level.SetActive(true);
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
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
