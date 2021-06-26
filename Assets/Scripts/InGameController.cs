using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour
{
    public GameObject playerCar;
    [HideInInspector]
    public int racerType = 1;
    [HideInInspector]
    public string playerCarColor = "white";//maybe an int
    public bool openingSequenceSeen = false;
    public GameObject pauseScreen;
    public GameObject optionsScreen;
    [SerializeField]
    private AudioSource BGM;
    [SerializeField]
    private Slider volume;

    // Start is called before the first frame update
    private void Awake()
    {
        InGameController[] objs = FindObjectsOfType<InGameController>();
        if(objs.Length > 1)
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        volume.onValueChanged.AddListener(delegate { OnDrag(volume.value); });
    }
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {

            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseUnpause();
            }
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //get build index from scene to change music
        switch (scene.buildIndex)
        {//todo replace the resources loading
            case 0://main menu
                BGM.clip = Resources.Load<AudioClip>("Sounds/GameTheme");
                break;
            case 1://tutorial
                BGM.clip = Resources.Load<AudioClip>("Sounds/TutorialTrack");
                BGM.Play();
                break;
            case 2://circle 1
                BGM.clip = Resources.Load<AudioClip>("Sounds/TrackSongs/CircleSong");
                BGM.Play();
                break;
            case 3://butterfly 2
                BGM.clip = Resources.Load<AudioClip>("Sounds/TrackSongs/Track2");
                BGM.Play();
                break;
            case 4://wave 3 
                BGM.clip = Resources.Load<AudioClip>("Sounds/TrackSongs/Track3");
                BGM.Play();
                break;
            case 5://dollar 4
                BGM.clip = Resources.Load<AudioClip>("Sounds/TrackSongs/DollarSong");
                BGM.Play();
                break;
        }
    }
    public void PauseUnpause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        if(Time.timeScale == 0)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            CloseOptions();
            pauseScreen.SetActive(false);
        }
    }
    public void Restart()
    {
        //reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PauseUnpause();
    }
    public void MainMenu()
    {
        //go back to main menu
        SceneManager.LoadScene(0);
        PauseUnpause();
    }
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }
    void OnDrag(float val)
    {
        BGM.volume = val;
        BGM.volume = Mathf.Clamp(BGM.volume, 0, 1);
    }
}
