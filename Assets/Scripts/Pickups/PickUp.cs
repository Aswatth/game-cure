using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(transform.tag == "ShieldPickUp")
            other.GetComponent<PickUpManager>().PickUpShield();
        else if(transform.tag == "x2PickUp")
            other.GetComponent<PickUpManager>().PickUpX2();

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
