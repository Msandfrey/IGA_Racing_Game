using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarFlying : MonoBehaviour
{

    public FixedJoint fixedJoint;
    public GameObject LapTrackUI;
    public float breakForce = 800;
    public float breakTorque = 600;
    public int lapTracker = 0;
    public bool enemy;
    [HideInInspector]
    public int lapsToWin = 0;
    public AIController Controller;
    public PlayerController playerController;
    private MeshRenderer carRenderer;
    //
    [SerializeField]
    Color redAlbedo = new Color(1, .3f, .3f);
    [SerializeField]
    Color redEmissive = new Color(1, 0, 0);
    [SerializeField]
    Color orangeAlbedo = new Color(1, .25f, 0);
    [SerializeField]
    Color orangeEmissive = new Color(1, .3904f, .1921f);
    [SerializeField]
    Color yellowAlbedo = new Color(1, .745f, 0);
    [SerializeField]
    Color yellowEmissive = new Color(1, .753f, 0);
    [SerializeField]
    Color magentaAlbedo = new Color(1, .3647f, .8784f);
    [SerializeField]
    Color magentaEmissive = new Color(.83f, .4198f, .7128f);
    [SerializeField]
    Color blueAlbedo = Color.cyan;//new Color(0, 1, 1);
    [SerializeField]
    Color blueEmissive = Color.cyan;//new Color(0, 1, 1);
    [HideInInspector]
    [SerializeField]
    Color whiteAlbedo = new Color(1, 1, 1);
    [SerializeField]
    Color whiteEmissive = new Color(1, 1, 1);
    [HideInInspector]
    public string carColor;

    // Start is called before the first frame update
    void Start()
    {
        blueAlbedo = Color.cyan;
        blueEmissive = Color.cyan;
        Physics.IgnoreLayerCollision(3,6);
        carRenderer = GetComponent<MeshRenderer>();
        if (enemy)
        {
            SetRacer(Random.Range(1, 4));
            carColor = GetRandomColor();
            SetColor(carColor);
            GetComponentInChildren<TrailRenderer>().startColor = GetTrailColor(carColor);
            GetComponentInChildren<TrailRenderer>().endColor = Color.grey;
        }
        else
        {
            SetRacer(FindObjectOfType<InGameController>().racerType);
            carColor = FindObjectOfType<InGameController>().playerCarColor;
            SetColor(carColor);
            GetComponentInChildren<TrailRenderer>().startColor = GetTrailColor(carColor);
            GetComponentInChildren<TrailRenderer>().endColor = Color.grey;
        }
    }
    private void Update()
    {
        if (!enemy)
        {
            LapTrackUI.GetComponentInChildren<TextMeshProUGUI>().text = (lapTracker).ToString() + "/" + lapsToWin.ToString();
        }
    }
    string GetRandomColor()
    {
        string GMColor = FindObjectOfType<InGameController>().playerCarColor;
        switch (Random.Range(1,5))
        {
            case 1:
                return "red" == GMColor ? "yellow" : "red";
            case 2:
                return "yellow" == GMColor ? "orange" : "yellow";
            case 3:
                return "orange" == GMColor ? "white" : "orange";
            case 4:
                return "white" == GMColor ? "magenta" : "white";
            case 5:
                return "magenta" == GMColor ? "red" : "magenta";
            default:
                break;
        }
        return "white" == GMColor ? "magenta" : "white";
    }
    public void SetColor(string color)
    {
        switch (color)
        {
            case "magenta":
                carRenderer.material.SetColor("_AlbedoTint", magentaAlbedo);
                carRenderer.material.SetColor("_EmissionColor", magentaEmissive);
                break;
            case "orange":
                carRenderer.material.SetColor("_AlbedoTint", orangeAlbedo);
                carRenderer.material.SetColor("_EmissionColor", orangeEmissive);
                break;
            case "yellow":
                carRenderer.material.SetColor("_AlbedoTint", yellowAlbedo);
                carRenderer.material.SetColor("_EmissionColor", yellowEmissive);
                break;
            case "red":
                carRenderer.material.SetColor("_AlbedoTint", redAlbedo);
                carRenderer.material.SetColor("_EmissionColor", redEmissive);
                break;
            case "blue":
                carRenderer.material.SetColor("_AlbedoTint", blueAlbedo);
                carRenderer.material.SetColor("_EmissionColor", blueEmissive);
                break;
            case "white":
                break;
            default:
                break;
        }
    }
    Color GetTrailColor(string color)
    {
        switch (color)
        {
            case "magenta":
                return magentaEmissive;
            case "orange":
                return orangeEmissive;
            case "yellow":
                return yellowEmissive;
            case "red":
                return redEmissive;
            case "blue":
                return blueEmissive;
            case "white":
                return Color.white;
            default:
                return Color.white;
        }
    }
    void SetRacer(int racerType)
    {
        if(racerType == FindObjectOfType<InGameController>().racerType && enemy) { racerType++; }
        switch (racerType)
        {
            case 1:
                GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Starter_Car_2");
                carRenderer.material = Resources.Load<Material>("Materials/Racer_1_Material_White");
                break;
            case 2:
                GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Agile_Car_1");
                carRenderer.material = Resources.Load<Material>("Materials/Racer_2_Material_White");
                break;
            case 3:
                GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Heavy_Car_1");
                carRenderer.material = Resources.Load<Material>("Materials/Racer_3_Material_White");
                break;
            case 4:
                GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Speed_Car_1");
                carRenderer.material = Resources.Load<Material>("Materials/Racer_4_Material_White");
                break;
            default:
                GetComponent<MeshFilter>().sharedMesh = Resources.Load<Mesh>("Models/Starter_Car_2");
                carRenderer.material = Resources.Load<Material>("Materials/Racer_1_Material_White");
                break;
        }
    }
}
