using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject title;
    //buttons
    public GameObject start;
    public GameObject creditButt;
    public GameObject instructionsButt;
    public GameObject tutorial;
    //intro scene
    public GameObject intro;
    //credits
    public GameObject credit;
    //controls
    public GameObject instructions;
    public GameObject instructions3DObject;
    //level page
    public GameObject level;
    public GameObject level3DObject;
    public GameObject circle;
    public GameObject butter;
    public GameObject dollar;
    public GameObject wave;
    private int currLevel = 1;
    //cars
    public GameObject car;
    public GameObject car3DObject;
    [SerializeField]
    private int currCar = 1;
    private int currColor = 1;

    private bool hasIntroPlayer = false;
    private bool playIntro = false;
    private bool isPlaying = false;
    [SerializeField]
    private AudioSource introSound;
    [SerializeField]
    private AudioSource menuBGM;
    private InGameController GM;

    //sounds
    AudioSource UISounds;
    [SerializeField]
    private AudioClip xSound;
    [SerializeField]
    private AudioClip backSound;
    [SerializeField]
    private AudioClip selectSound;
    [SerializeField]
    private AudioClip carLevelSound;
    private string SceneToLoad;
    // Start is called before the first frame update
    void Start()
    {
        GM = FindObjectOfType<InGameController>();
        currCar = GM.racerType;
        switch (GM.playerCarColor)
        {
            case "white":
                currColor = 1;
                break;
            case "yellow":
                currColor = 2;
                break;
            case "orange":
                currColor = 3;
                break;
            case "magenta":
                currColor = 4;
                break;
            case "red":
                currColor = 5;
                break;
            default:
                currColor = 1;
                break;
        }
        UISounds = GM.UI;
        menuBGM = GM.BGM;
        introSound = GM.VFX;
        introSound.clip = Resources.Load<AudioClip>("Sounds/Narration/GameIntro");
    }

    // Update is called once per frame
    void Update()
    {
        if(playIntro && !hasIntroPlayer)
        {
            hasIntroPlayer = true;
            playIntro = false;
            menuBGM.Pause();
            introSound.Play();
            isPlaying = true;
        }
        if (isPlaying && (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)))
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
            FindObjectOfType<InGameController>().openingSequenceSeen = true;
            menuBGM.Play();
            LevelSelect();
            hasIntroPlayer = false;
        }
    }
    public void Credits()
    {
        //play sound
        UISounds.clip = selectSound;
        UISounds.Play();
        //show credits
        start.SetActive(false);
        creditButt.SetActive(false);
        tutorial.SetActive(false);
        instructionsButt.SetActive(false);
        credit.SetActive(true);
    }
    public void BackFromCredits()
    {
        //play sound
        UISounds.clip = xSound;
        UISounds.Play();
        //todo go back to previous
        //just go to level select for now
        credit.SetActive(false);
        creditButt.SetActive(true);
        tutorial.SetActive(true);
        instructionsButt.SetActive(true);
        start.SetActive(true);
    }
    public void BackToLevels()
    {
        //play sound
        UISounds.clip = backSound;
        UISounds.Play();
        start.SetActive(false);
        level.SetActive(true);
        level3DObject.SetActive(true);
        car.SetActive(false);
        car3DObject.SetActive(false);
    }
    public void EnterLevelSelect()
    {
        //play sound
        UISounds.clip = selectSound;
        UISounds.Play();
        if (GM.openingSequenceSeen)
        {
            start.SetActive(false);
            creditButt.SetActive(false);
            tutorial.SetActive(false);
            instructionsButt.SetActive(false);
            LevelSelect();
        }
        else
        {
            playIntro = true;
            start.SetActive(false);
            creditButt.SetActive(false);
            tutorial.SetActive(false);
            instructionsButt.SetActive(false);
            intro.SetActive(true);
        }
    }
    public void LevelSelect()
    {
        DisplayCar(currCar);
        DisplayCarColor(currCar, currColor);
        title.SetActive(false);
        level.SetActive(true);
        level3DObject.SetActive(true);
    }
    public void BackToStart()
    {
        //play sound
        UISounds.clip = backSound;
        UISounds.Play();
        title.SetActive(true);
        start.SetActive(true);
        creditButt.SetActive(true);
        tutorial.SetActive(true);
        instructionsButt.SetActive(true);
        level.SetActive(false);
        level3DObject.SetActive(false);
        currLevel = 1;
    }
    public void EnterControls()
    {
        //play sound
        UISounds.clip = selectSound;
        UISounds.Play();
        title.SetActive(false);
        start.SetActive(false);
        creditButt.SetActive(false);
        tutorial.SetActive(false);
        instructionsButt.SetActive(false);
        instructions.SetActive(true);
        instructions3DObject.SetActive(true);
    }
    public void LeaveControls()
    {
        //play sound
        UISounds.clip = backSound;
        UISounds.Play();
        title.SetActive(true);
        start.SetActive(true);
        creditButt.SetActive(true);
        tutorial.SetActive(true);
        instructionsButt.SetActive(true);
        instructions.SetActive(false);
        instructions3DObject.SetActive(false);
    }
    public void Tutorial()
    {
        //play sound
        UISounds.clip = selectSound;
        UISounds.Play();
        SceneManager.LoadScene("Tutorial");
        FindObjectOfType<InGameController>().racerType = 1;
        FindObjectOfType<InGameController>().playerCarColor = "red";
    }
    public void LeftLevel()
    {
        //play sound
        UISounds.clip = carLevelSound;
        UISounds.Play();
        currLevel--;
        if(currLevel <= 0)
        {
            currLevel = 4;
        }
        DisplayLevel(currLevel);
    }
    public void RightLevel()
    {
        //play sound
        UISounds.clip = carLevelSound;
        UISounds.Play();
        currLevel++;
        if (currLevel > 4)
        {
            currLevel = 1;
        }
        DisplayLevel(currLevel);
    }
    void DisplayLevel(int level)
    {
        switch (level)
        {//see about removing resources.load todo
            case 1:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Circuit_Track_1");
                level3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Circuit_Track_Material");
                level3DObject.transform.localScale = new Vector3(5, 5, 5);
                level3DObject.transform.localEulerAngles = new Vector3(180, 90, -90);
                //circle
                circle.SetActive(true);
                butter.SetActive(false);
                dollar.SetActive(false);
                wave.SetActive(false);
                break;
            case 2:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Prototype_Butterfly_Track_2");
                level3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Butterfly_Track_Material");
                level3DObject.transform.localScale = new Vector3(.06f, .06f, .06f);
                level3DObject.transform.localEulerAngles = new Vector3(180, 90, -90);
                //butter
                circle.SetActive(false);
                butter.SetActive(true);
                dollar.SetActive(false);
                wave.SetActive(false);
                break;
            case 3:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Prototype_Dollar_Track_2");
                level3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Dollar_Track_Material");
                level3DObject.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                level3DObject.transform.localEulerAngles = new Vector3(90, 90, -90);
                //dollar
                circle.SetActive(false);
                butter.SetActive(false);
                dollar.SetActive(true);
                wave.SetActive(false);
                break;
            case 4:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Prototype_Wave_Track_2");
                level3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Wave_Track_Material");
                level3DObject.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);
                level3DObject.transform.localEulerAngles = new Vector3(90, 90, -90);
                //wave
                circle.SetActive(false);
                butter.SetActive(false);
                dollar.SetActive(false);
                wave.SetActive(true);
                break;
            default:
                level3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Circuit_Track_1");
                level3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Circuit_Track_Material");
                //circle
                circle.SetActive(true);
                butter.SetActive(false);
                dollar.SetActive(false);
                wave.SetActive(false);
                break;
        }
    }
    public void LeftCar()
    {
        //play sound
        UISounds.clip = carLevelSound;
        UISounds.Play();
        currCar--;
        if (currCar <= 0)
        {
            currCar = 4;
        }
        DisplayCar(currCar);
        DisplayCarColor(currCar, currColor);
    }
    public void RightCar()
    {
        //play sound
        UISounds.clip = carLevelSound;
        UISounds.Play();
        currCar++;
        if (currCar > 4)
        {
            currCar = 1;
        }
        DisplayCar(currCar);
        DisplayCarColor(currCar, currColor);
    }
    void DisplayCar(int car)
    {
        switch (car)
        {//see about removing resources.load todo
            case 1://racer
                car3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Starter_Car_2");
                FindObjectOfType<InGameController>().racerType = 1;
                break;
            case 2://agile
                car3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Agile_Car_1");
                FindObjectOfType<InGameController>().racerType = 2;
                break;
            case 3://heavy
                car3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Heavy_Car_1");
                FindObjectOfType<InGameController>().racerType = 3;
                break;
            case 4://speed
                car3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Speed_Car_1");
                FindObjectOfType<InGameController>().racerType = 4;
                break;
            default:
                car3DObject.GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Starter_Car_2");
                FindObjectOfType<InGameController>().racerType = 1;
                break;
        }
    }
    public void UpCar()
    {
        currColor--;
        if (currColor <= 0)
        {
            currColor = 5;
        }
        DisplayCarColor(currCar, currColor);
    }
    public void DownCar()
    {
        currColor++;
        if (currColor > 5)
        {
            currColor = 1;
        }
        DisplayCarColor(currCar, currColor);
    }
    void DisplayCarColor(int carType, int color)
    {
        switch (carType)
        {
            case 1:
                Car1Color(color);
                break;
            case 2:
                Car2Color(color);
                break;
            case 3:
                Car3Color(color);
                break;
            case 4:
                Car4Color(color);
                break;
            default:
                Car1Color(color);
                break;
        }
    }
    //racer colors
    void Car1Color(int color)
    {
        switch (color)
        {//see about removing resources.load todo
            case 1://white
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Racer_1_Material_White");
                FindObjectOfType<InGameController>().playerCarColor = "white";
                break;
            case 2://gold
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_1_Material_Gold");
                FindObjectOfType<InGameController>().playerCarColor = "yellow";
                break;
            case 3://orange
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_1_Material_Orange");
                FindObjectOfType<InGameController>().playerCarColor = "orange";
                break;
            case 4://pink
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_1_Material_Pink");
                FindObjectOfType<InGameController>().playerCarColor = "magenta";
                break;
            case 5://red
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_1_Material_Red");
                FindObjectOfType<InGameController>().playerCarColor = "red";
                break;
            default://white
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Racer_1_Material_White");
                FindObjectOfType<InGameController>().playerCarColor = "white";
                break;
        }
    }
    //agile colors
    void Car2Color(int color)
    {//see about removing resources.load todo
        switch (color)
        {
            case 1://white
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Racer_2_Material_White");
                FindObjectOfType<InGameController>().playerCarColor = "white";
                break;
            case 2://gold
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_2_Material_Gold");
                FindObjectOfType<InGameController>().playerCarColor = "yellow";
                break;
            case 3://orange
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_2_Material_Orange");
                FindObjectOfType<InGameController>().playerCarColor = "orange";
                break;
            case 4://pink
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_2_Material_Pink");
                FindObjectOfType<InGameController>().playerCarColor = "magenta";
                break;
            case 5://red
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_2_Material_Red");
                FindObjectOfType<InGameController>().playerCarColor = "red";
                break;
            default://white
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Racer_2_Material_White");
                FindObjectOfType<InGameController>().playerCarColor = "white";
                break;
        }
    }
    //heavy colors
    void Car3Color(int color)
    {
        switch (color)
        {//see about removing resources.load todo
            case 1://white
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Racer_3_Material_White");
                FindObjectOfType<InGameController>().playerCarColor = "white";
                break;
            case 2://gold
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_3_Material_Gold");
                FindObjectOfType<InGameController>().playerCarColor = "yellow";
                break;
            case 3://orange
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_3_Material_Orange");
                FindObjectOfType<InGameController>().playerCarColor = "orange";
                break;
            case 4://pink
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_3_Material_Pink");
                FindObjectOfType<InGameController>().playerCarColor = "magenta";
                break;
            case 5://red
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_3_Material_Red");
                FindObjectOfType<InGameController>().playerCarColor = "red";
                break;
            default://white
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Racer_3_Material_White");
                FindObjectOfType<InGameController>().playerCarColor = "white";
                break;
        }
    }
    //speed color
    void Car4Color(int color)
    {
        switch (color)
        {//see about removing resources.load todo
            case 1://white
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Racer_4_Material_White");
                FindObjectOfType<InGameController>().playerCarColor = "white";
                break;
            case 2://gold
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_4_Material_Gold");
                FindObjectOfType<InGameController>().playerCarColor = "yellow";
                break;
            case 3://orange
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_4_Material_Orange");
                FindObjectOfType<InGameController>().playerCarColor = "orange";
                break;
            case 4://pink
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_4_Material_Pink");
                FindObjectOfType<InGameController>().playerCarColor = "magenta";
                break;
            case 5://red
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ShowcaseOnly/Racer_4_Material_Red");
                FindObjectOfType<InGameController>().playerCarColor = "red";
                break;
            default://white
                car3DObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Racer_4_Material_White");
                FindObjectOfType<InGameController>().playerCarColor = "white";
                break;
        }
    }
    public void MattTest()
    {
        SceneToLoad = "Circle";
        level.SetActive(false);
        level3DObject.SetActive(false);
        car.SetActive(true);
        car3DObject.SetActive(true);
        /**currCar = GM.racerType;
        switch (GM.playerCarColor)
        {
            case "white":
                currColor = 1;
                break;
            case "yellow":
                currColor = 2;
                break;
            case "orange":
                currColor = 3;
                break;
            case "magenta":
                currColor = 4;
                break;
            case "red":
                currColor = 5;
                break;
            default:
                currColor = 1;
                break;
        }
        DisplayCar(currCar);
        DisplayCarColor(currCar, currColor);**/
    }
    public void Butterfly()
    {
        SceneToLoad = "Butterfly";
        level.SetActive(false);
        level3DObject.SetActive(false);
        car.SetActive(true);
        car3DObject.SetActive(true);
    }
    public void Wave()
    {
        SceneToLoad = "Wave";
        level.SetActive(false);
        level3DObject.SetActive(false);
        car.SetActive(true);
        car3DObject.SetActive(true);
    }
    public void Dollar()
    {
        SceneToLoad = "Dollar";
        level.SetActive(false);
        level3DObject.SetActive(false);
        car.SetActive(true);
        car3DObject.SetActive(true);
    }
    public void StartRace()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
