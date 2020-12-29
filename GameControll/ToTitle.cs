using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
    public AudioSource bgm_audio;
    const string TOUCH_AUDIOSLIDER = "TouchAudioSlider", LATEST_BGM_AUDIOVALUE = "LatestBGM_AudioValue";
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(TOUCH_AUDIOSLIDER) == 1)
        {
            bgm_audio.volume = PlayerPrefs.GetFloat(LATEST_BGM_AUDIOVALUE);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
