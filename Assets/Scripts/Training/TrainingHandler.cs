using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingHandler : MonoBehaviour
{
    [SerializeField] GameObject infectedTop;
    [SerializeField] GameObject infectedBottom;

    [SerializeField] GameObject healthyTop;
    [SerializeField] GameObject healthyBottom;

    [SerializeField] GameObject vaccinatedTop;
    [SerializeField] GameObject vaccinatedBottom;

    private void Start()
    {
        StatsManager.instance.SetStats(3);
        StatsManager.instance.UpdateInfectedPeople(1);
        StatsManager.instance.UpdateVirusSpread(1);

        infectedTop.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
        healthyTop.GetComponent<SkinnedMeshRenderer>().material.color = Color.green;
        vaccinatedTop.GetComponent<SkinnedMeshRenderer>().material.color = Color.white;

        infectedBottom.GetComponent<SkinnedMeshRenderer>().material.color  = healthyBottom.GetComponent<SkinnedMeshRenderer>().material.color =
            vaccinatedBottom.GetComponent<SkinnedMeshRenderer>().material.color = Color.grey;

    }
    
}
