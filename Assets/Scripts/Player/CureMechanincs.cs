using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CureMechanincs : MonoBehaviour
{
    public float rippleSpeed;
    public Material rippleMaterial;
    public LayerMask virusLayer;
    public float maxArea;

    public float killRate;
    private float nextTimeToKill;

    Vector3 ripplePos;
    float killradius;

    public LayerMask infectedLayer;
    public LayerMask vaccinateLayer;
    public float cureRadius;
    public float VACCINATION_TIME;
    private float vaccinationMultipier = 0.1f;
    private float vaccinationDuration = 0f;

    public bool isVaccinating = false;

    public static CureMechanincs instance;

    bool inTraining = false;

    public int coins = 0;
    private int coinPerVaccine = 10;
    public int difficultyBonus;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        if (SceneManager.GetActiveScene().buildIndex == 3)
            inTraining = true;

        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextTimeToKill)
        {
            nextTimeToKill = Time.time + 1f / killRate;
            GameObject ripple = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ripple.GetComponent<MeshRenderer>().material = rippleMaterial;
            ripple.GetComponent<SphereCollider>().enabled = false;
            ripple.transform.position = transform.position - Vector3.up;
            ripplePos = ripple.transform.position;
            StartCoroutine(RippleEffect(ripple));
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isVaccinating)
        {
            Vaccinate();
        }
    }

    public void OnSanitizePressed()
    {
        if (Time.time >= nextTimeToKill)
        {
            nextTimeToKill = Time.time + 1f / killRate;
            GameObject ripple = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ripple.GetComponent<MeshRenderer>().material = rippleMaterial;
            ripple.GetComponent<SphereCollider>().enabled = false;
            ripple.transform.position = transform.position - Vector3.up;
            ripplePos = ripple.transform.position;
            StartCoroutine(RippleEffect(ripple));
        }
        
    }
    public void OnVaccinatePressed()
    {
        if(!isVaccinating)
            Vaccinate();
    }

    IEnumerator RippleEffect(GameObject ripple)
    {
        yield return new WaitForSeconds(Time.deltaTime);
        ripple.transform.localScale += Vector3.one * rippleSpeed * Time.deltaTime;
        killradius = ripple.transform.localScale.x;
        if (ripple.transform.localScale.x <= maxArea)
            StartCoroutine(RippleEffect(ripple));
        else
        {
            KillVirusWithinRadius(ripple.transform, killradius/2f);
            yield return new WaitForSeconds(1f);
            Destroy(ripple);
        }
    }
    private void Vaccinate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, cureRadius, vaccinateLayer);

        //Debug.Log(colliders.Length);

        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider.name);
            if (collider.TryGetComponent<InfectedAi>(out InfectedAi infectedAi))
            {
                if (!infectedAi.isVaccinated())
                {
                    isVaccinating = true;
                    transform.LookAt(infectedAi.transform);
                    infectedAi.OnVaccinateStart(transform.position);
                    StartCoroutine(Vaccinating(infectedAi));
                    break;
                }
            }

        }
    }

    IEnumerator Vaccinating(InfectedAi infectedAi)
    {
        yield return new WaitForSeconds(vaccinationMultipier);
        vaccinationDuration += vaccinationMultipier;
        if (vaccinationDuration >= VACCINATION_TIME)
        {
            infectedAi.UpdateVaccinateUI(vaccinationDuration, VACCINATION_TIME);
            vaccinationDuration = 0f;

            yield return new WaitForSeconds(0.1f);

            infectedAi.OnVaccinateEnd();
            isVaccinating = false;
            coins += coinPerVaccine;
            if (PickUpManager.instance.hasPickedx2)
                coins += coinPerVaccine;
            StatsManager.instance.UpdateCoinUI(coins);
            Debug.Log(coins);
        }
        else
        {
            infectedAi.UpdateVaccinateUI(vaccinationDuration,VACCINATION_TIME);
            StartCoroutine(Vaccinating(infectedAi));
        }
    }

    private void KillVirusWithinRadius(Transform ripple, float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(ripple.position, radius, virusLayer);
        
        StatsManager.instance.UpdateVirusSpread(-colliders.Length);

        foreach (Collider collider in colliders)
        {
            //Debug.Log(collider.transform.name);
            Destroy(collider.gameObject);
        }
    }
    public void OnDeath(Transform coinUI)
    {
        coinUI.GetChild(0).GetComponent<TMP_Text>().text = "Earned coins:\t" + coins.ToString();

        if (coins < coinPerVaccine * 2)
        {
            difficultyBonus = 0;
        }

        coinUI.GetChild(1).GetComponent<TMP_Text>().text = "Difficulty bonus:\t" + difficultyBonus.ToString();

        int totalCoins = coins + difficultyBonus;

        coinUI.GetChild(2).GetComponent<TMP_Text>().text = "Total:\t" + totalCoins.ToString();

        int prevCoins = PlayerPrefs.GetInt("CURE_COINS", 0);
        PlayerPrefs.SetInt("CURE_COINS",prevCoins + totalCoins);

        Destroy(GetComponentInChildren<Canvas>().gameObject);

        instance.enabled = false;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, cureRadius);
    //}
}
