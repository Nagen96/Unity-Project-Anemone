using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borderline : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet") || collision.CompareTag("enemyBullet") || collision.CompareTag("lmg_bullet") || collision.CompareTag("rocket") || collision.CompareTag("Boss2Missile"))
        {
            collision.gameObject.SetActive(false);
        }

        else if (collision.CompareTag("CloneBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
