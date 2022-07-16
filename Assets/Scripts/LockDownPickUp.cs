using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDownPickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("LockDown picked up");
        other.GetComponent<LockDownHandler>().ChangeLockDown(+1);
        Destroy(this.gameObject);
    }
}
