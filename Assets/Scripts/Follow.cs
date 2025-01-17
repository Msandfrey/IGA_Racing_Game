using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Follow : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    [SerializeField]
    float distanceTravelled;

    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
            Debug.Log(pathCreator.path.length);
        }
    }

    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            //transform.Rotate(0, 180, 0);
        }
    }
    public void ResetCar()
    {
        distanceTravelled = 0;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
    }
    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    public void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
    public float GetDistanceTraveled()
    {
        return distanceTravelled;
    }
    public void IncreaseSpeed(float newSpeed, float min, float max)
    {
        speed += newSpeed;
        speed = Mathf.Clamp(speed, min, max);
    }

    public void DecreaseSpeed(float newSpeed, float min, float max)
    {
        speed -= newSpeed;
        speed = Mathf.Clamp(speed, min, max);
    }

    public void LeavePath()
    {
        pathCreator = null;
    }
}
