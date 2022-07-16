using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public bool hasPickedShield;
    public bool hasPickedx2;

    public float PICKUP_DURATION;
    
    float pickUpduration = 0f;
    float durationMultiplier = 0.1f;

    GameObject pickUI;

    [SerializeField] GameObject shieldObj;

    public static PickUpManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }

    public void PickUpShield()
    {
        string typeOfPickUp = "SHIELD";
        hasPickedShield = true;
        PlayerHealthManager.instance.isInfected = false;
        StatsManager.instance.OnPickUpStart(typeOfPickUp);
        StartCoroutine(StartPickUp(Instantiate(shieldObj, transform), typeOfPickUp));
    }
    public void PickUpX2()
    {
        string typeOfPickUp = "X2";
        hasPickedx2 = true;
        StatsManager.instance.OnPickUpStart(typeOfPickUp);
        StartCoroutine(StartPickUp(null, typeOfPickUp));
    }
    IEnumerator StartPickUp(GameObject shield = null,string typeOfPickUp = "")
    {
        yield return new WaitForSeconds(durationMultiplier);
        pickUpduration += durationMultiplier;
        if (pickUpduration >= PICKUP_DURATION)
        {
            pickUpduration = 0f;
            if (hasPickedx2)
                hasPickedx2 = false;
            if (hasPickedShield)
            { 
                hasPickedShield = false;
                Destroy(shield);
            }
            
            StatsManager.instance.OnPickUpEnd(typeOfPickUp);
        }
        else
        {
            StatsManager.instance.UpdatePickUpUI((PICKUP_DURATION - pickUpduration)/ PICKUP_DURATION, typeOfPickUp);
            StartCoroutine(StartPickUp(shield,typeOfPickUp));
        }
    }


    
}
