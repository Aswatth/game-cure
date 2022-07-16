using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform playerTransform;
    [SerializeField] bool isTransformSet = false;

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        isTransformSet = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTransformSet)
            transform.position = playerTransform.position + offset;
    }
}
