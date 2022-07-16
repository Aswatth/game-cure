using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PickUpManager>().PickUpShield();
        
        Destroy(this.transform.parent.gameObject);

        try
        {
            PickUpSpawner.instance.SpawnPickUp(Random.Range(10, 20));
        }
        catch
        {
            //Do Nothing
        }
    }
}
