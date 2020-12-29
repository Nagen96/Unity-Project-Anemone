using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public PlayerInform playerinfo;
    public GameObject scrap;
    public AudioSource hit_sound;
    public float hp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hit_sound.Play();

            hp -= playerinfo.direct_dmg;
            collision.SendMessage("DamageMotion", SendMessageOptions.DontRequireReceiver);

            if (hp <= 0)
            {
                if (playerinfo.killpoint < 16)
                    playerinfo.killpoint++;

                if (Random.Range(0, 2) == 1)
                {
                    scrap.transform.parent = null;
                    scrap.SetActive(true);
                }

                SendMessage("Death", SendMessageOptions.DontRequireReceiver);
                Destroy(GetComponent<EnemyHP>());
            }
        }
        else if (collision.CompareTag("rocket"))
        {
            hit_sound.Play();

            hp -= playerinfo.direct_rck_dmg;
            collision.SendMessage("DamageMotion", SendMessageOptions.DontRequireReceiver);

            if (hp <= 0)
            {
                if (playerinfo.killpoint < 16)
                    playerinfo.killpoint++;

                if (Random.Range(0, 2) == 1)
                {
                    scrap.transform.parent = null;
                    scrap.SetActive(true);
                }

                SendMessage("Death", SendMessageOptions.DontRequireReceiver);
                Destroy(GetComponent<EnemyHP>());
            }
        }
        else if (collision.CompareTag("lmg_bullet"))
        {
            hit_sound.Play();

            hp -= playerinfo.direct_lmg_dmg;
            collision.SendMessage("DamageMotion", SendMessageOptions.DontRequireReceiver);

            if (hp <= 0)
            {
                if (playerinfo.killpoint < 16)
                    playerinfo.killpoint++;

                if (Random.Range(0, 2) == 1)
                {
                    scrap.transform.parent = null;
                    scrap.SetActive(true);
                }

                SendMessage("Death", SendMessageOptions.DontRequireReceiver);
                Destroy(GetComponent<EnemyHP>());
            }
        }
    }
}
