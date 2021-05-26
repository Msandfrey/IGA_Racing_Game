using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackLeaderController : MonoBehaviour
{
    public List<Transform> patrolPoints;

    public float pointReachedMaxDistance = .5f;

    private int currentPatrolPointIndex;
    private Vector3 currentMoveTo;
    private Rotator rotator;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (patrolPoints != null)
        {
            foreach (Transform patrolPoint in patrolPoints)
            {
                Gizmos.DrawWireSphere(patrolPoint.position, pointReachedMaxDistance);
            }
        }
    }
    public void Start()
    {
        rotator = GetComponent<Rotator>();
        currentMoveTo = patrolPoints[0].position;
    }
    public void Update()
    {

        if (GetIsWithinDistance(pointReachedMaxDistance, currentMoveTo))
        {
            if (currentPatrolPointIndex >= patrolPoints.Count-1)
            {
                currentPatrolPointIndex = 0;
            }
            else
            {
                currentPatrolPointIndex += 1;
            }
        }

        currentMoveTo = patrolPoints[currentPatrolPointIndex].position;

        rotator.RotateTowardsDirection(currentMoveTo - transform.position);

    }

    public bool GetIsWithinDistance(float maxDistance, Vector3 targetPosition)
    {
        if (targetPosition == null)
        {
            return (false);
        }
        Vector3 directionToTarget = targetPosition - transform.position;

        return (directionToTarget.magnitude <= maxDistance);
    }

}
