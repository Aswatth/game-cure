using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject settingUI;
    [SerializeField] TMP_Text qualitySetting;
    [SerializeField] TMP_Text soundSetting;

    bool isSettingEnabled = false;
    int qualityIndex = 0;
    bool isMute = false;

    private void Awake()
    {
        qualityIndex = PlayerPrefs.GetInt("CURE_QUALITY_SETTING", 0);
        QualitySettings.SetQualityLevel(qualityIndex);

        isMute = PlayerPrefs.GetInt("CURE_SOUND_SETTING", 0) == 1;
        Camera.main.GetComponent<AudioListener>().enabled = !isMute;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            switch (qualityIndex)
            {
                case 0: qualitySetting.text = "Low"; break;
                case 1: qualitySetting.text = "Medium"; break;
                case 2: qualitySetting.text = "High"; break;
                default:
                    break;
            }
            soundSetting.text = !isMute ? "On" : "Off";
        }
    }

    public void OnClickSetting()
    {
        isSettingEnabled = !isSettingEnabled;
        settingUI.SetActive(isSettingEnabled);
    }

    public void OnPlayPressed()
    {
        StartCoroutine(NavigateSceneWithDelay(1));
        //SceneManager.LoadScene(1);
    }

    public void OnHelpPressed()
    {
        StartCoroutine(NavigateSceneWithDelay(3));
        //SceneManager.LoadScene(3);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void OnReplayPressed()
    {
        StartCoroutine(NavigateSceneWithDelay(SceneManager.GetActiveScene().buildIndex));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnMainMenuPressed()
    {
        StartCoroutine(NavigateSceneWithDelay(0));
        //SceneManager.LoadScene(0);
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

    public void OnQualityChange()
    {

        qualityIndex++;
        qualityIndex = qualityIndex % 3;

        switch (qualityIndex)
        {
            case 0: qualitySetting.text = "Low";break;
            case 1: qualitySetting.text = "Medium"; break;
            case 2: qualitySetting.text = "High"; break;
            default:
                break;
        }

        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("CURE_QUALITY_SETTING",qualityIndex);
    }

    public void OnSoundSettingChange()
    {
        isMute = !isMute;
        Camera.main.GetComponent<AudioListener>().enabled = !isMute;

        soundSetting.text = !isMute ? "On" : "Off";

        int value = isMute ? 1 : 0;
        PlayerPrefs.SetInt("CURE_SOUND_SETTING", value);

    }
    public void OpenUrl()
    {
        Application.OpenURL("https://soundcloud.com/dloqclub-pubgm");
    }

    

}
