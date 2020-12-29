using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public AudioSource bgm_audio;
    public List<AudioSource> normal_audios = new List<AudioSource>();
    public Slider bgm_slider, normal_slider;
    public GameObject settingPanel;
    public PlayerInform playerInfo;

    const string LATEST_DMG = "LatestDmg", LATEST_DMG_COEFFICIENT = "LatestDmgCoefficient";
    const string LATEST_HP = "LatestHP", LATEST_ATTACK_SPEED = "LatestAttackSpeed", LATEST_SPEED = "LatestSpeed";
    const string BARRIER = "Barrier", RANDOM_FIRE = "RandomFire", ROCKET = "Rocket", DRONE = "Drone";
    const string LATEST_STAGE = "LatestStage", LATEST_SCRAP = "LatestScrap";

    const string LATEST_NORMAL_AUDIOVALUE = "LatestNormal_AudioValue", LATEST_BGM_AUDIOVALUE = "LatestBGM_AudioValue";
    const string TOUCH_AUDIOSLIDER = "TouchAudioSlider";

    void Start()
    {
        if (PlayerPrefs.GetInt(TOUCH_AUDIOSLIDER) == 1)
        {
            bgm_slider.value = PlayerPrefs.GetFloat(LATEST_BGM_AUDIOVALUE);
            normal_slider.value = PlayerPrefs.GetFloat(LATEST_NORMAL_AUDIOVALUE);

            bgm_audio.volume = bgm_slider.value;
            foreach (var item in normal_audios)
            {
                item.volume = normal_slider.value;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            normal_audios[0].Play();
            settingPanel.SetActive(true);
            Time.timeScale = 0;
            playerInfo.fire_sound.SetActive(false);
        }
    }

    public void BGM_Slider()
    {
        PlayerPrefs.SetInt(TOUCH_AUDIOSLIDER, 1);
        PlayerPrefs.SetFloat(LATEST_BGM_AUDIOVALUE, bgm_slider.value);
        bgm_audio.volume = bgm_slider.value;
    }

    public void Normal_Slider()
    {
        PlayerPrefs.SetInt(TOUCH_AUDIOSLIDER, 1);
        PlayerPrefs.SetFloat(LATEST_NORMAL_AUDIOVALUE, normal_slider.value);
        foreach (var item in normal_audios)
        {
            item.volume = normal_slider.value;
        }
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void BackToGame()
    {
        normal_audios[0].Play();
        settingPanel.SetActive(false);
        if (!playerInfo.isDead)
            playerInfo.fire_sound.SetActive(true);
        Time.timeScale = 1;
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
        SceneManager.LoadScene("Stage_01");
    }

    public void FirstScreen()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
