using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparent : MonoBehaviour
{
    [SerializeField] List<I_am_in_the_way> currentlyInWayList;
    [SerializeField] List<I_am_in_the_way> alreadyTransparentList;
    [SerializeField] LayerMask transparentLayers;
    Transform playerTransform;


    private Transform cameraTransform;
    bool isPlayerSet = false;

    private void Awake()
    {
        currentlyInWayList = new List<I_am_in_the_way>();
        alreadyTransparentList = new List<I_am_in_the_way>();

        cameraTransform = transform;
    }
    public void SetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
        isPlayerSet = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlayerSet)
        {
            GetAllObjectsInTheWay();
            MakeObjectTransparent();
            MakeObjectSolid();
        }
    }

    private void GetAllObjectsInTheWay()
    {
        currentlyInWayList.Clear();

        float distance = Vector3.Magnitude(cameraTransform.position - playerTransform.position);

        Ray forwardRay = new Ray(cameraTransform.position, playerTransform.position - cameraTransform.position);
        Ray backwardRay = new Ray(playerTransform.position, cameraTransform.position - playerTransform.position);

        RaycastHit[] forwardHits =  Physics.RaycastAll(forwardRay, distance, transparentLayers);
        RaycastHit[] backwardHits =  Physics.RaycastAll(backwardRay, distance, transparentLayers);

        foreach (RaycastHit hit in forwardHits)
        {
            if (hit.collider.TryGetComponent(out I_am_in_the_way inWay))
            {
                if (!currentlyInWayList.Contains(inWay))
                {
                    currentlyInWayList.Add(inWay);
                }
            }
        }
        foreach (RaycastHit hit in backwardHits)
        {
            if (hit.collider.TryGetComponent(out I_am_in_the_way inWay))
            {
                if (!currentlyInWayList.Contains(inWay))
                {
                    currentlyInWayList.Add(inWay);
                }
            }
        }

    }

    private void MakeObjectTransparent()
    {
        for (int i = 0; i < currentlyInWayList.Count; i++)
        {
            I_am_in_the_way inWay = currentlyInWayList[i];
            if (!alreadyTransparentList.Contains(inWay))
            {
                inWay.ShowTransparent();
                alreadyTransparentList.Add(inWay);
            }
        }
    }

    private void MakeObjectSolid()
    {
        for (int i = alreadyTransparentList.Count -1 ; i >= 0; i--)
        {
            I_am_in_the_way inWay = alreadyTransparentList[i];
            if (!currentlyInWayList.Contains(inWay))
            {
                inWay.ShowSolid();
                alreadyTransparentList.Remove(inWay);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (isPlayerSet)
        {
            Ray forwardRay = new Ray(cameraTransform.position, playerTransform.position - cameraTransform.position);
            Ray backwardRay = new Ray(playerTransform.position, cameraTransform.position - playerTransform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(forwardRay);
            Gizmos.DrawRay(backwardRay);
        }
    }
}
