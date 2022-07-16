using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pickUpObjects;
    List<Transform> pickUpPositions;
    public static PickUpSpawner instance;

    int randomIndex;
    int objectIndex = 0;

    private void Awake()
    {
        pickUpPositions = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            pickUpPositions.Add(transform.GetChild(i).transform);
        }

        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        SpawnPickUp();
    }
    public void SpawnPickUp(int delay = 0)
    {
        StartCoroutine(Spawn(delay));
    }

    public IEnumerator Spawn(int delay)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log("Spawning pickup");
        randomIndex = Random.Range(0, pickUpPositions.Count);

        //Debug.Log("Random Index = " + randomIndex.ToString());
        Instantiate(pickUpObjects[objectIndex ++], pickUpPositions[randomIndex].position, Quaternion.identity);
        
        objectIndex = objectIndex % pickUpObjects.Length;

    }
}
