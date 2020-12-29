using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End_Credit : MonoBehaviour
{
    public RectTransform staff_text;
    public GameObject white_Screen, The_End;
    public float f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WhiteScreen());
        StartCoroutine(Staff_Roll());
    }

    IEnumerator Staff_Roll()
    {
        while (staff_text.localPosition.y<1400)
        {
            f = staff_text.localPosition.y;
            staff_text.transform.Translate(0, 5, 0);

            yield return new WaitForSeconds(0.03f);
        }
        The_End.SetActive(true);
    }

    IEnumerator WhiteScreen()
    {
        float i = 1;
        Image screen01 = white_Screen.GetComponent<Image>();
        white_Screen.SetActive(true);

        while (i > 0.1f)
        {
            i -= 0.02f;
            screen01.color = new Color(1, 1, 1, i);

            yield return new WaitForSeconds(0.033f);
        }
        screen01.color = new Color(1, 1, 1, 0);
        //black_screen01.SetActive(false);
        //yield return null;
        //Debug.Log("ggg");
    }
}
