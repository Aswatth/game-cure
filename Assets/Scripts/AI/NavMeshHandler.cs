using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHandler : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 prevPosition;

    bool isLockdown = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 0.1f)
        {
            if (!isLockdown)
            {
                GetComponent<Path_Follower>().enabled = true;
                //agent.enabled = false;
            }
            else
            {
                if (agent.hasPath)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    public void LockdownEnabled(Vector3 position)
    {
        //agent.enabled = true;
        GetComponent<Path_Follower>().enabled = false;
        prevPosition = transform.position;
        isLockdown = true;
        agent.SetDestination(position);
    }
    public void LockDownDisabled()
    {
        GetComponent<MeshRenderer>().enabled = true;

        //Vector3 destinationPos = Random.Range(0, 1) == 1 ? GetComponent<Path_Follower>().GetNearestPoint() : prevPosition;
        agent.SetDestination(GetComponent<Path_Follower>().GetNearestPoint());
        isLockdown = false;
    }

}
