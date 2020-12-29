using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneType01 : MonoBehaviour
{
    public List<GameObject> prefab_EnemyBullet = new List<GameObject>();
    public float speed, torque_power, xforce, yforce, destinationX;
    public GameObject enemyBullet;
    IEnumerator shot_routine;
    Animator animator;
    public bool isOver;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        yforce = 2.25f;
        shot_routine = ShotToPlayer();
        StartCoroutine(shot_routine);
    }

    private void Update()
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

    /*private void FixedUpdate()
    {
        if (transform.position.x <= -20f)
        {
            Destroy(enemyBullet);
            Destroy(gameObject);
        }
        else
            transform.Translate(-speed, 0, 0);
    }*/

    IEnumerator ShotToPlayer()
    {
        int count = 0;

        yield return new WaitForSeconds(5f);

        while (count < 5)
        {
            foreach (var item in prefab_EnemyBullet)
            {
                TargettingToPlayer(item);
                yforce -= 0.75f;
            }
            yforce = 2.25f;
            count++;
            yield return new WaitForSeconds(5f);
        }
        isOver = true;
        yield return null;
    }

    public void TargettingToPlayer(GameObject prefab_EnemyBullet)
    {       
        prefab_EnemyBullet.transform.position = transform.position;
        prefab_EnemyBullet.SetActive(true);
        prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-xforce, yforce), ForceMode2D.Impulse);
        prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        StopCoroutine(shot_routine);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlaneType01>().enabled = false;
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
