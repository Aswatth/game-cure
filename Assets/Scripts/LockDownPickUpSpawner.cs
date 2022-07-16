using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDownPickUpSpawner : MonoBehaviour
{
    [SerializeField] Transform[] pickUpPositions;
    [SerializeField] GameObject pickUpObject;

    private void Start()
    {
        SpawnPickUp();
    }

    public void SpawnPickUp()
    {
        int randomIndex = Random.Range(0, pickUpPositions.Length);
        Instantiate(pickUpObject, pickUpPositions[randomIndex].position, Quaternion.identity);
    }

}
