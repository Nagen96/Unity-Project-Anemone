using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public PlayerInform playerinfo;
    public GameObject smoke;
    public float green, blue;
    Image player_img;
    Animator animator;
    IEnumerator routine;
    public int i = 0;
    public bool isFill;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        green = 1;
        blue = 1;
        player_img = GetComponent<Image>();
        routine = StateBar();
        StartCoroutine(routine);
    }

    IEnumerator StateBar()
    {
        while (!playerinfo.isDead)
        {
            if (playerinfo.hp >= 71)
            {
                animator.SetTrigger("Blue");
                smoke.SetActive(false);
            }
            else if (playerinfo.hp >= 41)
            {
                animator.SetTrigger("Orange");
                smoke.SetActive(false);
            }
            else
            {
                animator.SetTrigger("Red");
                smoke.SetActive(true);
            }

            if (!isFill)
            {
                player_img.color = new Color(1, green, blue);
                green -= 0.012f;
                blue -= 0.012f;
                i++;
                if (i > 30)
                    isFill = true;
            }
            else if (isFill)
            {
                green += 0.012f;
                blue += 0.012f;
                player_img.color = new Color(1, green, blue);
                i--;
                if (i < 0)
                    isFill = false;
            }

            yield return new WaitForSeconds(0.1f);
        }

        animator.SetTrigger("Black");
        smoke.SetActive(false);
        yield return null;
    }
}
