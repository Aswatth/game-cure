using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] List<Character> characterList;
    [SerializeField] List<Difficulty> difficultyList;

    [SerializeField] StatsManager statsManager;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        Camera cam = Camera.main;
        int difficultyIndex = PlayerPrefs.GetInt("CURE_SelectedDifficulty", 0);
        
        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].isEquipped)
            {
                GameObject obj = Instantiate(characterList[i].prefab);
                obj.GetComponent<PlayerHealthManager>().Initialize(characterList[i].maxHealth);
                //Debug.Log(obj.transform);
                cam.GetComponent<PlayerFollow>().SetPlayerTransform(obj.transform);
                cam.GetComponent<MakeTransparent>().SetPlayerTransform(obj.transform);

                float maxHealth = characterList[i].maxHealth;
                
                StatsManager.instance.UpdatePlayerHealth(maxHealth + "/" + maxHealth);
                CureMechanincs.instance.difficultyBonus = difficultyList[difficultyIndex].difficultyBonus;
                //break;
            }
                
        }
    }
}
