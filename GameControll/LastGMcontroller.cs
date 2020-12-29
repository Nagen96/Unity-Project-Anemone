using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LastGMcontroller : MonoBehaviour
{
    public List<GameObject> stats_dmg = new List<GameObject>();
    public List<GameObject> stats_atk_speed = new List<GameObject>();
    public List<GameObject> stats_speed = new List<GameObject>();
    public List<GameObject> stats_dmg_cfc = new List<GameObject>();
    public GameObject black_screen00, black_screen01, white_Screen;

    const string LATEST_DMG = "LatestDmg", LATEST_DMG_COEFFICIENT = "LatestDmgCoefficient";
    const string LATEST_ATTACK_SPEED = "LatestAttackSpeed", LATEST_SPEED = "LatestSpeed";

    // Start is called before the first frame update
    void Start()
    {
        BlackScreenOpen();
        Stats_Of_Player();
    }

    public void Stats_Of_Player()
    {
        float latest_dmg = PlayerPrefs.GetFloat(LATEST_DMG);
        float latest_atk_speed = PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED);
        float latest_speed = PlayerPrefs.GetFloat(LATEST_SPEED);
        float latest_dmg_cfc = PlayerPrefs.GetFloat(LATEST_DMG_COEFFICIENT);

        //-----------------------------------
        //공격력 관련 스텟
        if (latest_dmg > 0.99f)
        {
            foreach (var item in stats_dmg)
                item.SetActive(true);
        }
        else if (latest_dmg > 0.79f)
        {
            for (int i = 0; i < 5; i++)
                stats_dmg[i].SetActive(true);
        }
        else if (latest_dmg > 0.59f)
        {
            for (int i = 0; i < 4; i++)
                stats_dmg[i].SetActive(true);
        }
        else if (latest_dmg > 0.39f)
        {
            for (int i = 0; i < 3; i++)
                stats_dmg[i].SetActive(true);
        }
        else if (latest_dmg > 0.19f)
        {
            for (int i = 0; i < 2; i++)
                stats_dmg[i].SetActive(true);
        }
        else if (latest_dmg >= 0)
        {
            for (int i = 0; i < 1; i++)
                stats_dmg[i].SetActive(true);
        }
        //---------------------------------

        //---------------------------------
        //공격속도 관련 스텟
        if (latest_atk_speed > 0.49f)
        {
            foreach (var item in stats_atk_speed)
                item.SetActive(true);
        }
        else if (latest_atk_speed > 0.39f)
        {
            for (int i = 0; i < 5; i++)
                stats_atk_speed[i].SetActive(true);
        }
        else if (latest_atk_speed > 0.29f)
        {
            for (int i = 0; i < 4; i++)
                stats_atk_speed[i].SetActive(true);
        }
        else if (latest_atk_speed > 0.19f)
        {
            for (int i = 0; i < 3; i++)
                stats_atk_speed[i].SetActive(true);
        }
        else if (latest_atk_speed > 0.09f)
        {
            for (int i = 0; i < 2; i++)
                stats_atk_speed[i].SetActive(true);
        }
        else if (latest_atk_speed >= 0)
        {
            for (int i = 0; i < 1; i++)
                stats_atk_speed[i].SetActive(true);
        }
        //-------------------------------------

        //-----------------------------------
        //이속 관련 스텟
        if (latest_speed > 0.99f)
        {
            foreach (var item in stats_speed)
                item.SetActive(true);
        }
        else if (latest_speed > 0.79f)
        {
            for (int i = 0; i < 5; i++)
                stats_speed[i].SetActive(true);
        }
        else if (latest_speed > 0.59f)
        {
            for (int i = 0; i < 4; i++)
                stats_speed[i].SetActive(true);
        }
        else if (latest_speed > 0.39f)
        {
            for (int i = 0; i < 3; i++)
                stats_speed[i].SetActive(true);
        }
        else if (latest_speed > 0.19f)
        {
            for (int i = 0; i < 2; i++)
                stats_speed[i].SetActive(true);
        }
        else if (latest_speed >= 0)
        {
            for (int i = 0; i < 1; i++)
                stats_speed[i].SetActive(true);
        }
        //---------------------------------

        //-----------------------------------
        //피해계수 관련 스텟 
        if (latest_dmg_cfc > 0.99f)
        {
            foreach (var item in stats_dmg_cfc)
                item.SetActive(true);
        }
        else if (latest_dmg_cfc > 0.79f)
        {
            for (int i = 0; i < 5; i++)
                stats_dmg_cfc[i].SetActive(true);
        }
        else if (latest_dmg_cfc > 0.59f)
        {
            for (int i = 0; i < 4; i++)
                stats_dmg_cfc[i].SetActive(true);
        }
        else if (latest_dmg_cfc > 0.39f)
        {
            for (int i = 0; i < 3; i++)
                stats_dmg_cfc[i].SetActive(true);
        }
        else if (latest_dmg_cfc > 0.19f)
        {
            for (int i = 0; i < 2; i++)
                stats_dmg_cfc[i].SetActive(true);
        }
        else if (latest_dmg_cfc >= 0)
        {
            for (int i = 0; i < 1; i++)
                stats_dmg_cfc[i].SetActive(true);
        }
        //---------------------------------
    }

    public void BlackScreenOpen()
    {
        StartCoroutine(BlackScreenOpen00());
        StartCoroutine(BlackScreenOpen01());
    }

    IEnumerator BlackScreenOpen00()
    {
        float i = 1;

        Image screen00 = black_screen00.GetComponent<Image>();
        black_screen00.SetActive(true);

        while (i > 0)
        {
            i -= 0.02f;
            screen00.fillAmount = i;

            yield return new WaitForSeconds(0.03f);
        }
        black_screen00.SetActive(false);
        //yield return null;
    }

    IEnumerator BlackScreenOpen01()
    {
        float i = 1;
        Image screen01 = black_screen01.GetComponent<Image>();
        black_screen01.SetActive(true);

        yield return new WaitForSeconds(1f);

        while (i > 0)
        {
            i -= 0.02f;
            screen01.fillAmount = i;

            yield return new WaitForSeconds(0.03f);
        }
        black_screen01.SetActive(false);
        //yield return null;
    }

    IEnumerator WhiteScreen()
    {
        float i = 0;
        Image screen01 = white_Screen.GetComponent<Image>();
        white_Screen.SetActive(true);

        while (i < 0.98f)
        {
            i += 0.02f;
            screen01.color = new Color(1, 1, 1, i);

            yield return new WaitForSeconds(0.033f);
        }

        SceneManager.LoadScene("end_credit");
        //black_screen01.SetActive(false);
        //yield return null;
    }

    public void White()
    {
        StartCoroutine(WhiteScreen());
    }
}
