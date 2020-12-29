using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneType02 : MonoBehaviour
{
    public List<GameObject> prefab_EnemyBullet00 = new List<GameObject>();
    public List<GameObject> prefab_EnemyBullet01 = new List<GameObject>();
    public float speed, torque_power, xforce, yforce, destinationX;
    public GameObject enemyBullet, exit_00, exit_01;
    public bool isOver;
    IEnumerator shot_routine;
    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        shot_routine = ShotToPlayer();
        StartCoroutine(shot_routine);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver)
        {
            if (transform.position.x > destinationX)
                transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else
        {
            if (transform.position.x >= 13f)
            {
                Destroy(enemyBullet);
                Destroy(gameObject);
            }
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    IEnumerator ShotToPlayer()
    {
        int count = 0;
        int count2 = 0;

        yield return new WaitForSeconds(5f);

        while (count2 < 5)
        {
            foreach (var item in prefab_EnemyBullet00)
            {
                SpreadBullet(item, exit_00);
                yforce -= 0.75f;
                count++;
                if(count >= 2)
                {
                    count = 0;
                    yforce = 0;
                    yield return new WaitForSeconds(0.2f);
                }
            }
            yforce = 0;
            count = 0;

            foreach (var item in prefab_EnemyBullet01)
            {
                SpreadBullet(item, exit_01);
                yforce -= 0.75f;
                count++;
                if (count >= 2)
                {
                    count = 0;
                    yforce = 0;
                    yield return new WaitForSeconds(0.2f);
                }
            }

            yforce = 0;
            count = 0;
            count2++;
            yield return new WaitForSeconds(5f);
        }

        isOver = true;
        yield return null;
    }

    public void SpreadBullet(GameObject prefab_EnemyBullet, GameObject exit_bullet)
    {
        prefab_EnemyBullet.transform.position = exit_bullet.transform.position;
        prefab_EnemyBullet.SetActive(true);
        prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xforce, yforce), ForceMode2D.Impulse);
        prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        StopCoroutine(shot_routine);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlaneType02>().enabled = false;
        //폭파모션과 폭파사운드 추가
        animator.SetTrigger("Explosion");
        Invoke("DestroyObj", 5f);
    }

    public void DestroyObj()
    {
        enemyBullet.SetActive(false);
        gameObject.SetActive(false);
    }
}
