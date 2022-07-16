using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfectedAi : MonoBehaviour
{
    [SerializeField] bool isInfected = false;
    [SerializeField] bool hasBeenVaccinated = false;

    bool isInitialized = false;

    public TestGrid gridTest;

    public float movementSpeed;
    //StatsManager statsManager;
    Path_Follower follower;

    Animator anim;
    [SerializeField] GameObject tops, bottoms;

    SpriteRenderer sprite;

    [SerializeField] GameObject vaccinateUI;
    Image vaccinationBar;

    bool inTraining = false;


    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            inTraining = true;
        }

        if(!inTraining)
            gridTest = GameObject.Find("City").GetComponent<TestGrid>();

        anim = GetComponent<Animator>();

        follower = GetComponent<Path_Follower>();

        vaccinationBar = vaccinateUI.transform.GetChild(1).GetComponent<Image>();

        sprite = GetComponentInChildren<SpriteRenderer>();

    }

    public void Initialize(bool isInfected, GameObject tops, GameObject bottoms)
    {
        //this.statsManager = statsManager;
        this.isInfected = isInfected;

        this.tops = tops;
        this.bottoms = bottoms;

        if (isInfected)
            Affected();
        
        isInitialized = true;
    }

    private void Update()
    {
        if (isInitialized)
        {
            if (isInfected)
            {
                gridTest.SpreadInfection(transform);
            }
            else if (gridTest.GetInfectionRate(transform) > 5)
            {
                Affected();
            }
        }
    }

    public void Affected()
    {
        if (!hasBeenVaccinated)
        {
            //GetComponent<MeshRenderer>().material.color = Color.red;
            //Debug.Log("Infected");
            isInfected = true;
            StatsManager.instance.UpdateInfectedPeople(+1);
            tops.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
            sprite.color = Color.red;
        }
    }
    public void OnVaccinateStart(Vector3 lookPos)
    {
        anim.SetBool("isVaccinating", true);
        follower.StopMovement(lookPos,true);
        vaccinateUI.SetActive(true);
        vaccinationBar.fillAmount = 0;
    }
    public void UpdateVaccinateUI(float value, float maxValue)
    {
        vaccinationBar.fillAmount = value/maxValue;
    }
    public void OnVaccinateEnd()
    {
        hasBeenVaccinated = true;
        //GetComponent<MeshRenderer>().material.color = Color.black;
        if (isInfected)
            StatsManager.instance.UpdateInfectedPeople(-1);
        isInfected = false;
        tops.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;
        sprite.color = Color.white;
        anim.SetBool("isVaccinating", false);
        follower.ResumeMovement();
        vaccinationBar.fillAmount = 1;
        vaccinateUI.SetActive(false);
    }
    public bool isVaccinated()
    {
        return hasBeenVaccinated;
    }
}
