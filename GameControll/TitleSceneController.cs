using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    public GameObject Continue_btn, forshowText;
    public GameObject black_screen00, black_screen01;
    public GameObject settingPanel, endcreditsPanel;
    //public List<GameObject> staff = new List<GameObject>();
    public bool isNewGame;

    IEnumerator close_routine00, close_routine01;
    const string LATEST_DMG = "LatestDmg", LATEST_DMG_COEFFICIENT = "LatestDmgCoefficient";
    const string LATEST_HP = "LatestHP", LATEST_ATTACK_SPEED = "LatestAttackSpeed", LATEST_SPEED = "LatestSpeed";
    const string BARRIER = "Barrier", RANDOM_FIRE = "RandomFire", ROCKET = "Rocket", DRONE = "Drone";
    const string LATEST_STAGE = "LatestStage", LATEST_SCRAP = "LatestScrap";
    //const string LATEST_STAGE = "LatestStage";

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        close_routine00 = BlackScreenClose00();
        close_routine01 = BlackScreenClose01();

        if (PlayerPrefs.GetString(LATEST_STAGE) != "")
        {
            Continue_btn.SetActive(true);
            forshowText.SetActive(false);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteKey(LATEST_DMG);
        PlayerPrefs.DeleteKey(LATEST_DMG_COEFFICIENT);
        PlayerPrefs.DeleteKey(LATEST_HP);
        PlayerPrefs.DeleteKey(LATEST_ATTACK_SPEED);
        PlayerPrefs.DeleteKey(LATEST_SPEED);
        PlayerPrefs.DeleteKey(BARRIER);
        PlayerPrefs.DeleteKey(RANDOM_FIRE);
        PlayerPrefs.DeleteKey(ROCKET);
        PlayerPrefs.DeleteKey(DRONE);
        PlayerPrefs.DeleteKey(LATEST_STAGE);
        PlayerPrefs.DeleteKey(LATEST_SCRAP);
        isNewGame = true;
        StartCoroutine(close_routine00);
        StartCoroutine(close_routine01);
    }
    public void ContinueGame()
    {
        isNewGame = false;
        StartCoroutine(close_routine00);
        StartCoroutine(close_routine01);
    }

    public void SettingOfGame()
    {
        settingPanel.SetActive(true);
    }

    public void EndCreditsOfGame()
    {
        endcreditsPanel.SetActive(true);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    IEnumerator BlackScreenClose00()
    {
        float i = 0;
        Image screen00 = black_screen00.GetComponent<Image>();
        black_screen00.SetActive(true);

        while (i < 1)
        {
            i += 0.02f;
            screen00.fillAmount = i;

            yield return new WaitForSeconds(0.03f);
        }
        //yield return null;
    }

    IEnumerator BlackScreenClose01()
    {
        float i = 0;
        Image screen01 = black_screen01.GetComponent<Image>();
        black_screen01.SetActive(true);

        yield return new WaitForSeconds(1f);

        while (i < 1)
        {
            i += 0.02f;
            screen01.fillAmount = i;

            yield return new WaitForSeconds(0.03f);
        }

        if (isNewGame)
            SceneManager.LoadScene("Stage_01");
        else
            SceneManager.LoadScene(PlayerPrefs.GetString(LATEST_STAGE));
    }

}
