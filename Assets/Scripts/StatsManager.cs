using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{
    [SerializeField] TMP_Text healthyPeopleTMP;
    [SerializeField] TMP_Text infectedPeopleTMP;
    [SerializeField] TMP_Text virusSpreadTMP;
    [SerializeField] TMP_Text healthTMP;

    //Pick-ups
    [SerializeField] GameObject shieldPickUp;
    [SerializeField] GameObject x2PickUp;

    //Coin UI
    [SerializeField] TMP_Text coinText;
    [SerializeField] Transform conisUI;

    Image shieldDurationUI, x2DurationUI;

    int infectedCount;
    int healthyCount;
    int virusSpreadCount;

    public static StatsManager instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
        shieldDurationUI = shieldPickUp.transform.GetChild(0).GetComponent<Image>();
        x2DurationUI = x2PickUp.transform.GetChild(0).GetComponent<Image>();
    }

    public void SetStats(int healthyPeopleCount)
    {
        healthyCount = healthyPeopleCount;
        healthyPeopleTMP.text = healthyCount.ToString();

        infectedCount = 0;
        infectedPeopleTMP.text = infectedCount.ToString();
    }

    public void UpdateInfectedPeople(int value)
    {
        infectedCount += value;
        infectedPeopleTMP.text = infectedCount.ToString();

        healthyCount -= value;
        healthyPeopleTMP.text = healthyCount.ToString();

    }

    public void UpdateVirusSpread(int value)
    {
        virusSpreadCount += value;
        virusSpreadTMP.text = virusSpreadCount.ToString();

        if (infectedCount == 0 && virusSpreadCount == 0)
        {
            StartCoroutine(DisplayScreen());
        }
    }

    public void UpdatePlayerHealth(string amount)
    {
        healthTMP.text = amount;
    }

    public void OnPickUpStart(string typeOfPickUp)
    {
        //Debug.Log(typeOfPickUp);
        if (typeOfPickUp == "SHIELD")
            shieldPickUp.SetActive(true);
        else if (typeOfPickUp == "X2")
            x2PickUp.SetActive(true);

    }
    public void OnPickUpEnd(string typeOfPickUp)
    {
        if (typeOfPickUp == "SHIELD")
            shieldPickUp.SetActive(false);
        else if (typeOfPickUp == "X2")
            x2PickUp.SetActive(false);
    }

    public void UpdatePickUpUI(float fillAmount, string typeOfPickUp)
    {
        if(typeOfPickUp == "SHIELD")
            shieldDurationUI.fillAmount = fillAmount;
        else if(typeOfPickUp == "X2")
            x2DurationUI.fillAmount = fillAmount;

    }
    public void UpdateCoinUI(int coin)
    {
        coinText.text = coin.ToString();
    }
    IEnumerator DisplayScreen()
    {
        yield return new WaitForSeconds(1);

        GameObject UI_CANVAS = GameObject.Find("Canvas");
        UI_CANVAS.transform.GetChild(0).gameObject.SetActive(false);
        UI_CANVAS.transform.GetChild(2).gameObject.SetActive(true);

        CureMechanincs.instance.OnDeath(conisUI);

    }
}
