using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PlayerHealthManager : MonoBehaviour
{
    public bool isInfected = false;
    bool isVaccinated = false;
    bool isDead = false;
    bool isFullHealth;
    bool isHealing = false;

    TestGrid gridTest;
    public float healthHealRate;
    
    public float maxHealth;
    public ParticleSystem healEffect;

    float currentHealth;
    float nextTimeToInfect;

    Volume volume;
    private Vignette vignette;

    Animator anim;
    bool inTraining = false;
    public static PlayerHealthManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        if (SceneManager.GetActiveScene().buildIndex == 3)
            inTraining = true;
    }

    public void Initialize(float health)
    {
        maxHealth = health;
        currentHealth = maxHealth;

        //StatsManager.instance.UpdatePlayerHealth(currentHealth.ToString("0") + "/" + maxHealth.ToString("0"));
        isFullHealth = true;

        volume = GameObject.FindGameObjectWithTag("PostProcessing").GetComponent<Volume>();
        volume.profile.TryGet(out vignette);

        anim = GetComponent<Animator>();

        gridTest = GameObject.FindGameObjectWithTag("City").GetComponent<TestGrid>();

    }

    public void Affected()
    {
        if (!isVaccinated && !isInfected && !isHealing)
        {
            //GetComponent<MeshRenderer>().material.color = Color.yellow;
            isInfected = true;
            //StartCoroutine(DetoriateHealth());
        }        
    }
    
    private void Update()
    {
        if (!inTraining)
        {
            if (isInfected && !isDead)
            {
                //if (Time.time >= nextTimeToInfect)
                //{
                //    nextTimeToInfect = Time.time + 1 / infectionRate;
                //    Infect();
                //}
                gridTest.SpreadInfection(transform);
                ReduceHealth(gridTest.GetInfectionRate(transform) + 1f);
            }
            else if (gridTest.GetInfectionRate(transform) > 0 && !isHealing && !PickUpManager.instance.hasPickedShield)
            {
                Affected();
            }
        }
        
        if (isHealing)
        {
            IncreaseHealth();
        }
    }    
    
    //IEnumerator DetoriateHealth()
    //{
    //    yield return new WaitForSeconds(1);
    //    //ReduceHealth();
    //    if(isInfected && !isDead)
    //        StartCoroutine(DetoriateHealth());
    //}
    private void ReduceHealth(float reduceRate)
    {
        currentHealth -= reduceRate*Time.deltaTime;
        if(currentHealth < maxHealth)
            isFullHealth = false;
        if (currentHealth < 0)
            currentHealth = 0;
        StatsManager.instance.UpdatePlayerHealth(currentHealth.ToString("0") + "/" + maxHealth.ToString("0"));
        float healthNormalized = currentHealth / maxHealth;

        vignette.intensity.value = 1 - healthNormalized;

        if (currentHealth <= 0 && !CureMechanincs.instance.isVaccinating)
        {
            Die();
        }
    }
    private void Die()
    {
        //transform.gameObject.SetActive(false);
        isDead = true;
        anim.SetBool("isDead", isDead);
        PlayerMovement.instance.enabled = false;
        //CureMechanincs.instance.enabled = false;

        StartCoroutine(DelayedGameOverScreen());
        
    }
    IEnumerator DelayedGameOverScreen()
    {
        yield return new WaitForSeconds(3f);

        GameObject UI_CANVAS = GameObject.Find("Canvas");
        UI_CANVAS.transform.GetChild(0).gameObject.SetActive(false);
        UI_CANVAS.transform.GetChild(1).gameObject.SetActive(true);

        CureMechanincs.instance.OnDeath(UI_CANVAS.transform.GetChild(1).GetChild(3));
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "HealRegion")
        {
            //GetComponent<MeshRenderer>().material.color = Color.blue;
            isInfected = false;
            isHealing = true;
            healEffect.Play();
            //StartCoroutine(HealPlayer());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "HealRegion")
        {
            isHealing = false;
            healEffect.Stop();
        }
    }
    //IEnumerator HealPlayer()
    //{
    //    yield return new WaitForSeconds(1);
    //    if (!isFullHealth && isHealing)
    //    {
    //        IncreaseHealth();
    //        StartCoroutine(HealPlayer());
    //    }
    //    else
    //    {
    //        healEffect.Stop();
    //    }
    //}
    private void IncreaseHealth()
    {
        currentHealth += healthHealRate*Time.deltaTime;
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            isFullHealth = true;
            isHealing = false;
            healEffect.Stop();
        }
        StatsManager.instance.UpdatePlayerHealth(currentHealth.ToString("0") + "/" + maxHealth.ToString("0"));
        float healthNormalized = currentHealth / maxHealth;
        vignette.intensity.value = 1 - healthNormalized;
    }
}
