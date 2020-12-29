using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerInform playerinfo;
    public GameObject nsPanel; //ns=NextStage
    public GameObject black_screen00, black_screen01;

    public List<GameObject> moduleOption00 = new List<GameObject>();
    public List<GameObject> moduleOption01 = new List<GameObject>();
    public List<GameObject> moduleOption02 = new List<GameObject>();
    public List<GameObject> moduleOption03 = new List<GameObject>();
    public List<GameObject> weaponOption = new List<GameObject>();
    public List<GameObject> PurchaseWindow = new List<GameObject>();

    public List<GameObject> stats_dmg = new List<GameObject>();
    public List<GameObject> stats_atk_speed = new List<GameObject>();
    public List<GameObject> stats_speed = new List<GameObject>();
    public List<GameObject> stats_dmg_cfc = new List<GameObject>();

    //public List<float> dmg = new List<float>();
    //public List<float> dmg_coefficient = new List<float>();
    // public List<int> hp = new List<int>();
    // public List<float> attack_speed = new List<float>();
    // public List<float> move_speed = new List<float>();

    public int moduleList_num;
    public float total_hp;
    public float total_dmg, total_dmg_coefficient, total_attack_speed, total_speed;
    public int barrier, randomfire, rocket, drone;
    public string next_stage;
    public Text scrap_text;

    const string LATEST_DMG = "LatestDmg", LATEST_DMG_COEFFICIENT = "LatestDmgCoefficient";
    const string LATEST_HP = "LatestHP", LATEST_ATTACK_SPEED = "LatestAttackSpeed", LATEST_SPEED = "LatestSpeed";
    const string BARRIER = "Barrier", RANDOM_FIRE = "RandomFire", ROCKET = "Rocket", DRONE = "Drone";
    const string LATEST_STAGE = "LatestStage", LATEST_SCRAP = "LatestScrap";

    // Start is called before the first frame update
    void Start()
    {
        BlackScreenOpen();
        Stats_Of_Player();

        if (PlayerPrefs.GetInt(DRONE) == 0)
            weaponOption[0].SetActive(true);
        if (PlayerPrefs.GetInt(ROCKET) == 0)
            weaponOption[1].SetActive(true);

        moduleOption00[Random.Range(0, 10)].SetActive(true);
        moduleOption01[Random.Range(0, 10)].SetActive(true);
        moduleOption02[Random.Range(0, 10)].SetActive(true);
        //moduleOption03[Random.Range(0, 2)].SetActive(true);
        //moduleOption04[Random.Range(0, 2)].SetActive(true);
    }

    private void Update()
    {
        if (!playerinfo.isDead)
        {
            scrap_text.text = playerinfo.scrap.ToString();
        }
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

    public void ShowWindowOfNextStage()
    {
        if (!playerinfo.isDead)
        {
            scrap_text.text = playerinfo.scrap.ToString();
            nsPanel.SetActive(true);
        }
    }

    public void CancelPurchase()
    {
        foreach (var item in PurchaseWindow)
        {
            item.SetActive(false);
        }
    }

    public void Module00()//데미지 증가, 피해계수 증가 템/ 데미지10%, 피해계수 5%//탄소코팅탄환
    {
        if (playerinfo.scrap >= 3)
        {
            playerinfo.scrap -= 3;

            //total_dmg = PlayerPrefs.GetFloat(LATEST_DMG);
            total_dmg += 0.1f;
            //PlayerPrefs.SetFloat(LATEST_DMG, total_dmg);

            //total_dmg_coefficient = PlayerPrefs.GetFloat(LATEST_DMG_COEFFICIENT);
            total_dmg_coefficient += 0.05f;
            //PlayerPrefs.SetFloat(LATEST_DMG_COEFFICIENT, total_dmg_coefficient);

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[0].SetActive(false);
        }
    }

    public void Module01()//체력증가 템/ 체력 10증가//강화장갑
    {
        if (playerinfo.scrap >= 3)
        {
            playerinfo.scrap -= 3;

            //total_hp = PlayerPrefs.GetFloat(LATEST_HP);
            total_hp += 10f;
            //PlayerPrefs.SetFloat(LATEST_HP, total_hp);

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[1].SetActive(false);
        }
    }

    public void Module02()//배리어 //중첩불가
    {
        if (playerinfo.scrap >= 3)
        {
            playerinfo.scrap -= 3;
            // PlayerPrefs.SetInt(BARRIER, 1);
            if (PlayerPrefs.GetInt(BARRIER) == 0)
                barrier = 1;

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[2].SetActive(false);
        }
    }

    public void Module03()//공속15% 증가, 주무기 명중률 하락, 중첩불가//프로토타입 A101스프링
    {
        if (playerinfo.scrap >= 3)
        {
            playerinfo.scrap -= 3;

            if (PlayerPrefs.GetInt(RANDOM_FIRE) == 0)
            {
                //PlayerPrefs.SetInt(RANDOM_FIRE, 1);
                randomfire = 1;

                //total_attack_speed = PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED);
                total_attack_speed += 0.15f;
                //PlayerPrefs.SetFloat(LATEST_ATTACK_SPEED, total_attack_speed);         
            }

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[3].SetActive(false);
        }
    }

    public void Module04() //추진력 촉진기/이속 10%
    {
        if (playerinfo.scrap >= 3)
        {
            playerinfo.scrap -= 3;

            //total_speed = PlayerPrefs.GetFloat(LATEST_SPEED);
            total_speed += 0.1f;
            //PlayerPrefs.SetFloat(LATEST_SPEED, total_speed);

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[4].SetActive(false);
        }
    }

    public void Module05()//경기관총 드론 //중첩불가//WeaponOption
    {
        if (playerinfo.scrap >= 5)
        {
            playerinfo.scrap -= 5;
            //PlayerPrefs.SetInt(DRONE, 1);
            drone = 1;

            weaponOption[0].SetActive(false);
            PurchaseWindow[5].SetActive(false);
        }
    }

    public void Module06()//푸른부전나비 로켓//WeaponOpton
    {
        if (playerinfo.scrap >= 5)
        {
            playerinfo.scrap -= 5;
            //PlayerPrefs.SetInt(ROCKET, 1);
            rocket = 1;

            weaponOption[1].SetActive(false);
            PurchaseWindow[6].SetActive(false);
        }
    }

    public void Module07()//A-형 마이크로칩/공속 5%. 체력 10, 데미지 10% 증가
    {
        if (playerinfo.scrap >= 5)
        {
            playerinfo.scrap -= 5;

            //total_dmg = PlayerPrefs.GetFloat(LATEST_DMG);
            total_dmg += 0.1f;
            //PlayerPrefs.SetFloat(LATEST_DMG, total_dmg);

            //total_attack_speed = PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED);
            total_attack_speed += 0.05f;
            //PlayerPrefs.SetFloat(LATEST_ATTACK_SPEED, total_attack_speed);

            //total_hp = PlayerPrefs.GetInt(LATEST_HP);
            total_hp += 10;
            //PlayerPrefs.SetInt(LATEST_HP, total_hp);

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[7].SetActive(false);
        }
    }

    public void Module08()//B형-마이크로칩//이속 5%, 공속5%, 데미지 20%, 피해계수 10%
    {
        if (playerinfo.scrap >= 5)
        {
            playerinfo.scrap -= 5;

            //total_speed = PlayerPrefs.GetFloat(LATEST_SPEED);
            total_speed += 0.05f;
            //PlayerPrefs.SetFloat(LATEST_SPEED, total_speed);

            //total_attack_speed = PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED);
            total_attack_speed += 0.05f;
            //PlayerPrefs.SetFloat(LATEST_ATTACK_SPEED, total_attack_speed);

            //total_dmg = PlayerPrefs.GetFloat(LATEST_DMG);
            total_dmg += 0.2f;
            //PlayerPrefs.SetFloat(LATEST_DMG, total_dmg);

            //total_dmg_coefficient = PlayerPrefs.GetFloat(LATEST_DMG_COEFFICIENT);
            total_dmg_coefficient += 0.1f;
            //PlayerPrefs.SetFloat(LATEST_DMG_COEFFICIENT, total_dmg_coefficient);

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[8].SetActive(false);
        }
    }

    public void Module09()//C형-마이크로칩//데미지 30%, 이속 -10%, 체력 -20
    {
        if (playerinfo.scrap >= 5)
        {
            playerinfo.scrap -= 5;

            //total_hp = PlayerPrefs.GetInt(LATEST_HP);
            total_hp -= 20;
            //PlayerPrefs.SetInt(LATEST_HP, total_hp);

            //total_dmg = PlayerPrefs.GetFloat(LATEST_DMG);
            total_dmg += 0.3f;
            //PlayerPrefs.SetFloat(LATEST_DMG, total_dmg);

            //total_speed = PlayerPrefs.GetFloat(LATEST_SPEED);
            total_speed -= 0.1f;
            //PlayerPrefs.SetFloat(LATEST_SPEED, total_speed);

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[9].SetActive(false);
        }
    }

    public void Module10()//자동 장전기/공속 10%, 피해 계수 5%
    {
        if (playerinfo.scrap >= 5)
        {
            playerinfo.scrap -= 5;

            //total_attack_speed = PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED);
            total_attack_speed += 0.1f;
            total_dmg_coefficient += 0.05f;
            //PlayerPrefs.SetFloat(LATEST_ATTACK_SPEED, total_attack_speed);

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[10].SetActive(false);
        }
    }

    public void Module11()//냉각팬/공속 5%
    {
        if (playerinfo.scrap >= 3)
        {
            playerinfo.scrap -= 3;

            //total_attack_speed = PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED);
            total_attack_speed += 0.05f;
            //PlayerPrefs.SetFloat(LATEST_ATTACK_SPEED, total_attack_speed);

            if (moduleList_num == 0)
            {
                foreach (var item in moduleOption00)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 1)
            {
                foreach (var item in moduleOption01)
                {
                    item.SetActive(false);
                }
            }
            else if (moduleList_num == 2)
            {
                foreach (var item in moduleOption02)
                {
                    item.SetActive(false);
                }
            }

            PurchaseWindow[11].SetActive(false);
        }
    }

    //무기전용
    public void ShowPurchase05()
    {
        PurchaseWindow[5].SetActive(true);
    }
    public void ShowPurchase06()
    {
        PurchaseWindow[6].SetActive(true);
    }
    //-------------------------------------------

    //ShowPurchase 00//모듈리스트00
    public void ShowPurchase00_00()
    {
        moduleList_num = 0;
        PurchaseWindow[0].SetActive(true);
    }

    public void ShowPurchase00_01()
    {
        moduleList_num = 0;
        PurchaseWindow[1].SetActive(true);
    }

    public void ShowPurchase00_02()
    {
        moduleList_num = 0;
        PurchaseWindow[2].SetActive(true);
    }

    public void ShowPurchase00_03()
    {
        moduleList_num = 0;
        PurchaseWindow[3].SetActive(true);
    }

    public void ShowPurchase00_04()
    {
        moduleList_num = 0;
        PurchaseWindow[4].SetActive(true);
    }

    /*public void ShowPurchase00_05()
    {
        moduleList_num = 0;
        PurchaseWindow[5].SetActive(true);
    }

    public void ShowPurchase00_06()
    {
        moduleList_num = 0;
        PurchaseWindow[6].SetActive(true);
    }*/

    public void ShowPurchase00_07()
    {
        moduleList_num = 0;
        PurchaseWindow[7].SetActive(true);
    }

    public void ShowPurchase00_08()
    {
        moduleList_num = 0;
        PurchaseWindow[8].SetActive(true);
    }

    public void ShowPurchase00_09()
    {
        moduleList_num = 0;
        PurchaseWindow[9].SetActive(true);
    }

    public void ShowPurchase00_10()
    {
        moduleList_num = 0;
        PurchaseWindow[10].SetActive(true);
    }

    public void ShowPurchase00_11()
    {
        moduleList_num = 0;
        PurchaseWindow[11].SetActive(true);
    }
    //ShowPurchase 00 끝//

    //ShowPurchase 01//모듈리스트01
    public void ShowPurchase01_00()
    {
        moduleList_num = 1;
        PurchaseWindow[0].SetActive(true);
    }

    public void ShowPurchase01_01()
    {
        moduleList_num = 1;
        PurchaseWindow[1].SetActive(true);
    }

    public void ShowPurchase01_02()
    {
        moduleList_num = 1;
        PurchaseWindow[2].SetActive(true);
    }

    public void ShowPurchase01_03()
    {
        moduleList_num = 1;
        PurchaseWindow[3].SetActive(true);
    }

    public void ShowPurchase01_04()
    {
        moduleList_num = 1;
        PurchaseWindow[4].SetActive(true);
    }

    /*public void ShowPurchase01_05()
    {
        moduleList_num = 1;
        PurchaseWindow[5].SetActive(true);
    }

    public void ShowPurchase01_06()
    {
        moduleList_num = 1;
        PurchaseWindow[6].SetActive(true);
    }*/

    public void ShowPurchase01_07()
    {
        moduleList_num = 1;
        PurchaseWindow[7].SetActive(true);
    }

    public void ShowPurchase01_08()
    {
        moduleList_num = 1;
        PurchaseWindow[8].SetActive(true);
    }

    public void ShowPurchase01_09()
    {
        moduleList_num = 1;
        PurchaseWindow[9].SetActive(true);
    }

    public void ShowPurchase01_10()
    {
        moduleList_num = 1;
        PurchaseWindow[10].SetActive(true);
    }

    public void ShowPurchase01_11()
    {
        moduleList_num = 1;
        PurchaseWindow[11].SetActive(true);
    }
    //ShowPurchase 01 끝//

    //ShowPurchase 02//모듈리스트02
    public void ShowPurchase02_00()
    {
        moduleList_num = 2;
        PurchaseWindow[0].SetActive(true);
    }

    public void ShowPurchase02_01()
    {
        moduleList_num = 2;
        PurchaseWindow[1].SetActive(true);
    }

    public void ShowPurchase02_02()
    {
        moduleList_num = 2;
        PurchaseWindow[2].SetActive(true);
    }

    public void ShowPurchase02_03()
    {
        moduleList_num = 2;
        PurchaseWindow[3].SetActive(true);
    }

    public void ShowPurchase02_04()
    {
        moduleList_num = 2;
        PurchaseWindow[4].SetActive(true);
    }

    /*public void ShowPurchase02_05()
    {
        moduleList_num = 2;
        PurchaseWindow[5].SetActive(true);
    }

    public void ShowPurchase02_06()
    {
        moduleList_num = 2;
        PurchaseWindow[6].SetActive(true);
    }*/

    public void ShowPurchase02_07()
    {
        moduleList_num = 2;
        PurchaseWindow[7].SetActive(true);
    }

    public void ShowPurchase02_08()
    {
        moduleList_num = 2;
        PurchaseWindow[8].SetActive(true);
    }

    public void ShowPurchase02_09()
    {
        moduleList_num = 2;
        PurchaseWindow[9].SetActive(true);
    }

    public void ShowPurchase02_10()
    {
        moduleList_num = 2;
        PurchaseWindow[10].SetActive(true);
    }

    public void ShowPurchase02_11()
    {
        moduleList_num = 2;
        PurchaseWindow[11].SetActive(true);
    }
    //ShowPurchase 02 끝//

    //ShowPurchase 03//모듈리스트02
    public void ShowPurchase03_00()
    {
        moduleList_num = 3;
        PurchaseWindow[0].SetActive(true);
    }

    public void ShowPurchase03_01()
    {
        moduleList_num = 3;
        PurchaseWindow[1].SetActive(true);
    }

    public void ShowPurchase03_02()
    {
        moduleList_num = 3;
        PurchaseWindow[2].SetActive(true);
    }

    public void ShowPurchase03_03()
    {
        moduleList_num = 3;
        PurchaseWindow[3].SetActive(true);
    }

    public void ShowPurchase03_04()
    {
        moduleList_num = 3;
        PurchaseWindow[4].SetActive(true);
    }

    /*public void ShowPurchase02_05()
    {
        moduleList_num = 2;
        PurchaseWindow[5].SetActive(true);
    }

    public void ShowPurchase02_06()
    {
        moduleList_num = 2;
        PurchaseWindow[6].SetActive(true);
    }*/

    public void ShowPurchase03_07()
    {
        moduleList_num = 3;
        PurchaseWindow[7].SetActive(true);
    }

    public void ShowPurchase03_08()
    {
        moduleList_num = 3;
        PurchaseWindow[8].SetActive(true);
    }

    public void ShowPurchase03_09()
    {
        moduleList_num = 3;
        PurchaseWindow[9].SetActive(true);
    }

    public void ShowPurchase03_10()
    {
        moduleList_num = 3;
        PurchaseWindow[10].SetActive(true);
    }

    public void ShowPurchase03_11()
    {
        moduleList_num = 3;
        PurchaseWindow[11].SetActive(true);
    }
    //ShowPurchase 03 끝//

    public void BlackScreenClose()
    {
        PlayerPrefs.SetFloat(LATEST_HP, PlayerPrefs.GetFloat(LATEST_HP) + total_hp);
        PlayerPrefs.SetFloat(LATEST_DMG, PlayerPrefs.GetFloat(LATEST_DMG) + total_dmg);
        PlayerPrefs.SetFloat(LATEST_SPEED, PlayerPrefs.GetFloat(LATEST_SPEED) + total_speed);
        PlayerPrefs.SetFloat(LATEST_ATTACK_SPEED, PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED) + total_attack_speed);
        PlayerPrefs.SetFloat(LATEST_DMG_COEFFICIENT, PlayerPrefs.GetFloat(LATEST_DMG_COEFFICIENT) + total_dmg_coefficient);

        PlayerPrefs.SetInt(BARRIER, PlayerPrefs.GetInt(BARRIER) + barrier);
        PlayerPrefs.SetInt(RANDOM_FIRE, PlayerPrefs.GetInt(RANDOM_FIRE) + randomfire);
        PlayerPrefs.SetInt(DRONE, PlayerPrefs.GetInt(DRONE) + drone);
        PlayerPrefs.SetInt(ROCKET, PlayerPrefs.GetInt(ROCKET) + rocket);

        PlayerPrefs.SetInt(LATEST_SCRAP, playerinfo.scrap);
        PlayerPrefs.SetString(LATEST_STAGE, next_stage);

        StartCoroutine(BlackScreenClose00());
        StartCoroutine(BlackScreenClose01());
    }

    public void BlackScreenOpen()
    {
        StartCoroutine(BlackScreenOpen00());
        StartCoroutine(BlackScreenOpen01());
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

        SceneManager.LoadScene(next_stage);
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

}
