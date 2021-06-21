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
    Color blueAlbedo = new Color(0, 0, 1);
    [SerializeField]
    Color blueEmissive = new Color(0, 0, 1);
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
        Physics.IgnoreLayerCollision(3,6);
        carRenderer = GetComponent<MeshRenderer>();
        if (enemy)
        {
            SetRacer(Random.Range(1, 2));
            carColor = GetRandomColor();
            SetColor(carColor);
        }
        else
        {
            SetRacer(FindObjectOfType<InGameController>().racerType);
            carColor = FindObjectOfType<InGameController>().playerCarColor;
            SetColor(carColor);
        }
    }
    private void Update()
    {
        LapTrackUI.GetComponentInChildren<TextMeshProUGUI>().text = (lapTracker).ToString();
    }
    string GetRandomColor()
    {
        switch (Random.Range(1,5))
        {
            case 1:
                return "red";
            case 2:
                return "yellow";
            case 3:
                return "orange";
            case 4:
                return "white";
            case 5:
                return "magenta";
            default:
                break;
        }
        return "white";
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
    void SetRacer(int racerType)
    {
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
                break;
            default:
                break;
        }
    }
}
