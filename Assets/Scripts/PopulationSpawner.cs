using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PopulationSpawner : MonoBehaviour
{
    [SerializeField] GameObject citizen;
    [SerializeField] PathCreator[] paths;

    //[SerializeField] StatsManager statsManager;

    [SerializeField] Color[] bottomColors;

    float distanceTravelled = 0f;

    int totalCitizens;
    int infectedCitizens;

    [SerializeField] List<Difficulty> difficultyList;

    private void Start()
    {
        //totalCitizens = PlayerPrefs.GetInt("CURE_PopulationCount", 0);
        //infectedCitizens = PlayerPrefs.GetInt("CURE_InfectedPercentage", 0);
        //infectedCitizens = (int)((infectedCitizens / 100f)*totalCitizens);

        int index = PlayerPrefs.GetInt("CURE_SelectedDifficulty", 0);

        totalCitizens = difficultyList[index].populationCount;
        infectedCitizens = difficultyList[index].infectedPercentage;
        infectedCitizens = (int)((infectedCitizens / 100f) * totalCitizens);

        //Debug.Log(StatsManager.instance);

        StatsManager.instance.SetStats(totalCitizens);
        //statsManager.SetStats(totalCitizens);

        Debug.Log(totalCitizens);
        Debug.Log(infectedCitizens);
        SpawnCitizens();
    }

    void SpawnCitizens()
    {
        for (int i = 0; i < infectedCitizens; i++)
        {
            StartCoroutine(CreateCitizen(true));
        }
        for (int i = 0; i < totalCitizens - infectedCitizens; i++)
        {
            StartCoroutine(CreateCitizen(false));
        }
    }
    IEnumerator CreateCitizen(bool infected)
    {
        GameObject spawnedCitizen = Instantiate(citizen, transform.position, Quaternion.identity);

        Path_Follower pathFollow = spawnedCitizen.GetComponent<Path_Follower>();
        pathFollow.pathCreator = paths[Random.Range(0, paths.Length)];
        distanceTravelled += 4;
        pathFollow.SetDistance(distanceTravelled);

        //Cloths assinging
        int totalTops = spawnedCitizen.transform.GetChild(2).GetChild(0).childCount;
        int totalBottoms = spawnedCitizen.transform.GetChild(2).GetChild(1).childCount;

        GameObject tops = spawnedCitizen.transform.GetChild(2).GetChild(0).GetChild(Random.Range(0, totalTops)).gameObject;
        GameObject bottoms = spawnedCitizen.transform.GetChild(2).GetChild(1).GetChild(Random.Range(0, totalBottoms)).gameObject;

        if (infected)
        {
            tops.GetComponent<SkinnedMeshRenderer>().material.color = Color.red;
        }
        else
        {
            tops.GetComponent<SkinnedMeshRenderer>().material.color = Color.green;
        }

        bottoms.GetComponent<SkinnedMeshRenderer>().material.color = bottomColors[Random.Range(0, bottomColors.Length)];

        tops.SetActive(true);
        bottoms.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        InfectedAi ai = spawnedCitizen.GetComponent<InfectedAi>();
        ai.Initialize(infected, tops,bottoms);
    }

}
