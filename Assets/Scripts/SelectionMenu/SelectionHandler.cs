using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectionHandler : MonoBehaviour
{
    [Header("Character Selection")]
    [SerializeField] List<Character> characterList;
    [SerializeField] List<GameObject> characterGameObjList;
    [SerializeField] TMP_Text maxHealth;
    int selectedCharacterIndex = 0;

    [Header("Difficulty Settings")]
    [SerializeField] List<Difficulty> difficultyList;
    int selectedDifficultyIndex = 0;
    [SerializeField] TMP_Text difficulty;
    [SerializeField] TMP_Text difficultyDescription;

    [SerializeField] GameObject buyButton;
    bool isSelectedCharacterValid = false;

    ShopData shopData;

    [SerializeField] TMP_Text coinText;
    int coins = 0;

    private void Awake()
    {
        string retrivedData = SaveHandler.Load();

        if (retrivedData == null)
        {
            shopData = new ShopData()
            {
                unlockStatus = new bool[] { true, false, false, false, false }
            };
        }
        else
        {
            shopData = JsonUtility.FromJson<ShopData>(retrivedData);

            for (int i = 0; i < shopData.unlockStatus.Length; i++)
            {
                characterList[i].isUnlocked = shopData.unlockStatus[i];
            }
        }
        coins = PlayerPrefs.GetInt("CURE_COINS", 0);
        //coins = 2000;
        coinText.text = coins.ToString();

        bool isMute = PlayerPrefs.GetInt("CURE_SOUND_SETTING", 1) == 1;
        Camera.main.GetComponent<AudioListener>().enabled = !isMute;

        string dataToStore = JsonUtility.ToJson(shopData);
        Debug.Log(dataToStore);
        SaveHandler.Save(dataToStore);
    }
    private void Start()
    {
        UpdateCharacterSelection(0);
        OnDifficutyChange();
    }

    public void OnRightArrowClick()
    {
        UpdateCharacterSelection(+1);
    }

    public void OnLeftArrowClick()
    {
        UpdateCharacterSelection(-1);
    }

    void UpdateCharacterSelection(int navigationOffset)
    {
        selectedCharacterIndex = selectedCharacterIndex + navigationOffset;

        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterList.Count + selectedCharacterIndex;
        }

        selectedCharacterIndex = selectedCharacterIndex % characterList.Count;

        for (int i = 0; i < characterGameObjList.Count; i++)
        {
            if (characterGameObjList[i] == characterGameObjList[selectedCharacterIndex])
            {
                characterGameObjList[i].SetActive(true);
                if (characterList[i].isUnlocked)
                {
                    characterList[i].isEquipped = true;
                    buyButton.SetActive(false);
                    isSelectedCharacterValid = true;
                }
                else
                {
                    buyButton.SetActive(true);
                    buyButton.transform.GetChild(1).gameObject.SetActive(false);
                    buyButton.GetComponentInChildren<TMP_Text>().text = "Buy\n" + characterList[i].costToUnlock.ToString();
                    isSelectedCharacterValid = false;
                }
                maxHealth.text =  "Max health: " + characterList[selectedCharacterIndex].maxHealth.ToString();
            }
            else
            {
                characterGameObjList[i].SetActive(false);
                characterList[i].isEquipped = false;
            }
        }
    }

    public void BuyCharacter()
    {
        if (characterList[selectedCharacterIndex].costToUnlock < coins)
        {
            characterList[selectedCharacterIndex].isUnlocked = true;
            characterList[selectedCharacterIndex].isEquipped = true;
            buyButton.SetActive(false);

            shopData.unlockStatus[selectedCharacterIndex] = true;

            string dataToStore = JsonUtility.ToJson(shopData);
            Debug.Log(dataToStore);
            SaveHandler.Save(dataToStore);

            isSelectedCharacterValid = true;

            coins -= characterList[selectedCharacterIndex].costToUnlock;
            coinText.text = coins.ToString();

            Debug.Log("Coins =" + coins);

            PlayerPrefs.SetInt("CURE_COINS", coins);
        }
        else
        {
            Debug.Log("Not enough coins to buy");
            buyButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        

    }

    public void OnDifficutyChange()
    {
        selectedDifficultyIndex = (selectedDifficultyIndex + 1) % difficultyList.Count;
        difficulty.text = "Difficulty: " + difficultyList[selectedDifficultyIndex].name;
        difficultyDescription.text = "Population: " + difficultyList[selectedDifficultyIndex].populationCount + "\nInfected Percentage: "+difficultyList[selectedDifficultyIndex].infectedPercentage;
    }

    public void OnPressGo()
    {
        if (isSelectedCharacterValid)
        {
            //PlayerPrefs.SetInt("CURE_PopulationCount", difficultyList[selectedDifficultyIndex].populationCount);
            PlayerPrefs.SetInt("CURE_SelectedDifficulty", selectedDifficultyIndex);
            //PlayerPrefs.SetInt("CURE_InfectedPercentage", difficultyList[selectedDifficultyIndex].infectedPercentage);
            PlayerPrefs.SetInt("CURE_SelectedCharacter", selectedCharacterIndex);
            StartCoroutine(NavigateSceneWithDelay(2));
            //SceneManager.LoadScene(2);
        }
    }

    public void BackToMainMenu()
    {
        StartCoroutine(NavigateSceneWithDelay(0));
        //SceneManager.LoadScene(0);
    }

    IEnumerator NavigateSceneWithDelay(int index, float delay = 0.5f)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }


    [SerializeField]
    private class ShopData
    {
        public bool[] unlockStatus;
    }

}
