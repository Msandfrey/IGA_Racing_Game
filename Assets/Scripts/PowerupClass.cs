using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupClass : MonoBehaviour
{
    public enum PowerType { Phase, None };
    public PowerType power;
    public float timer;
    public Sprite UIImage;

    public PowerupClass()
    {
        power = PowerType.None;
        timer = -1f;
        UIImage = null;
    }
    public PowerupClass(PowerType p, float time, Sprite image)
    {
        power = p;
        timer = time;
        UIImage = image;
    }

    public void UseEffect(GameObject car)
    {
        //Phase
        if(power == PowerType.Phase)
        {
            PhaseShift(car);
        }
        //Other
    }

    public void StopEffect(GameObject car)
    {
        //Phase
        ResetCar(car);
    }

    void PhaseShift(GameObject car)
    {
        car.GetComponent<MeshRenderer>().materials[0].color = Color.blue;
        car.GetComponent<CarFlying>().fixedJoint.breakForce = Mathf.Infinity;
        car.GetComponent<CarFlying>().fixedJoint.breakTorque = Mathf.Infinity;
    }

    void ResetCar(GameObject car)
    {
        car.GetComponent<MeshRenderer>().materials[0].color = car.GetComponent<CarFlying>().carColor;
        car.GetComponent<CarFlying>().fixedJoint.breakForce = car.GetComponent<CarFlying>().breakForce;
        car.GetComponent<CarFlying>().fixedJoint.breakTorque = car.GetComponent<CarFlying>().breakTorque;
    }

}
