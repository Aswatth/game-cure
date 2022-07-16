using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hosp_Pointer : MonoBehaviour
{
    //[SerializeField] Camera uiCam;

    //[SerializeField] Vector3 targetPosition;
    //private RectTransform pointerRectTransform;

    [SerializeField] float borderSize = 1000f;

    //private void Awake()
    //{
    //    //targetPosition = new Vector3(200, 45);
    //    pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    //    uiCam = Camera.main;
    //}

    //private void Update()
    //{
    //    Vector3 toPosition = targetPosition;
    //    Vector3 fromPosition = uiCam.transform.position;
    //    fromPosition.z = 0f;
    //    Vector3 dir = (toPosition - fromPosition).normalized;

    //    float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) % 360;

    //    pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

    //    Vector3 targetPositionScreenPoint = uiCam.WorldToScreenPoint(targetPosition);
    //    bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize
    //        || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

    //    Debug.Log(isOffScreen + " " + targetPositionScreenPoint);

    //    if (isOffScreen)
    //    {
    //        Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
    //        if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
    //        if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.y = Screen.width - borderSize;
    //        if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
    //        if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.x = Screen.height - borderSize;

    //        Vector3 pointerWorldPosition = uiCam.ScreenToWorldPoint(cappedTargetScreenPosition);
    //        pointerRectTransform.position = pointerWorldPosition;
    //        pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
    //    }
    //    else
    //    {
    //        Vector3 pointerWorldPosition = uiCam.ScreenToWorldPoint(targetPositionScreenPoint);
    //        pointerRectTransform.position = pointerWorldPosition;
    //        pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
    //    }
    //}
    [SerializeField] Transform hospTransform;

    private void Update()
    {
        bool isOffScreen = hospTransform.position.x <= borderSize || hospTransform.position.x >= Screen.width - borderSize
            || hospTransform.position.y <= borderSize || hospTransform.position.y >= Screen.height - borderSize;
        Debug.Log(isOffScreen);
    }




}
