using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LockDownHandler : MonoBehaviour
{
    GameObject[] citizens;

    Vector3[] citizenPrevPositions;

    [SerializeField] Transform[] housePositions;
    
    public float lockDownDuration;

    bool canImplementLockDown = true;

    int availableLockdowns = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ImplementLockDown();
        }
    }

    public void ChangeLockDown(int count)
    {
        availableLockdowns += count;
    }

    private void ImplementLockDown()
    {
        citizens = GameObject.FindGameObjectsWithTag("AI");
        citizenPrevPositions = new Vector3[citizens.Length];

        if (availableLockdowns > 0 && canImplementLockDown)
        {
            //Debug.Log("Lockdown Implemented");

            for (int i = 0; i < citizens.Length; i++)
            {
                int randomHouse = Random.Range(0, housePositions.Length);

                citizens[i].GetComponent<NavMeshHandler>().LockdownEnabled(housePositions[randomHouse].position);
            }

            ChangeLockDown(-1);
            canImplementLockDown = false;
            StartCoroutine(LockDownDuration());
        }
        else
            Debug.Log("Lockdown not available");
    }

    IEnumerator LockDownDuration()
    {
        yield return new WaitForSeconds(lockDownDuration);
        RemoveLockDown();
    }
    private void RemoveLockDown()
    {
        Debug.Log("Removing lockDown...");
        for (int i = 0; i < citizens.Length; i++)
        {
            citizens[i].GetComponent<NavMeshHandler>().LockDownDisabled();
        }
    }

}
