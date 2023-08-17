using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveConfig;
    List<Transform> wayPoints;
    int wayPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = waveConfig.GetWayPoints();
        transform.position = wayPoints[wayPointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (wayPointIndex < wayPoints.Count)
        {
            Vector3 targetPosition = wayPoints[wayPointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                ++wayPointIndex;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
