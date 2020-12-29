using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliType03 : MonoBehaviour
{
    public List<GameObject> prefab_EnemyBullet = new List<GameObject>();
    public List<GameObject> prefab_EnemyBullet01 = new List<GameObject>();
    public float speed1, speed2, zRotate, bullet_speed, x, y;
    public bool isForward;
    public GameObject player, sprite, enemyBullet;
    IEnumerator shot_routine;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        isForward = true;
        animator = GetComponentInChildren<Animator>();
        shot_routine = ShotToPlayer();
        StartCoroutine(shot_routine);
    }

    private void Update()
    {
        if (isForward)
        {
            transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(x, y, 0),
            speed1 * Time.deltaTime);
        }
        else
        {
            if (speed2 < 15f)
            {
                speed2 += 0.02f;
            }
            transform.Translate(-speed2 * Time.deltaTime, 0f, 0f);

            if (zRotate < 80f)
            {
                zRotate += 1f;
                sprite.transform.Rotate(0f, 0f, zRotate * Time.deltaTime);
            }
            else
                sprite.transform.localRotation = Quaternion.Euler(0, 0, 20f);
        }

        if (transform.position.x <= -20f)
        {
            Destroy(enemyBullet);
            Destroy(gameObject);
        }
    }

    /*private void FixedUpdate()
    {
        if (isForward)
        {
            transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(x, y, 0),
            speed1);
        }
        else if (isRetreat)
        {
            transform.Translate(-speed2, 0f, 0f);
            if (speed2 < 0.3f)
            {
                speed2 += 0.002f;
            }
            if (zRotate < 2f)
            {
                zRotate += 0.1f;
                sprite.transform.Rotate(0f, 0f, zRotate);
            }
        }

        if (transform.position.x <= -20f)
        {
            Destroy(enemyBullet);
            Destroy(gameObject);
        }
    }*/

    IEnumerator ShotToPlayer()
    {
        int count = 0;

        while (count < 5)
        {
            yield return new WaitForSeconds(2.5f);

            foreach (var item in prefab_EnemyBullet)
            {
                TargettingToPlayer(item);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.3f);

            foreach (var item in prefab_EnemyBullet01)
            {
                TargettingToPlayer(item);
                yield return new WaitForSeconds(0.1f);
            }
            count++;
        }

        isForward = false;
       // yield return null;
    }

    public void TargettingToPlayer(GameObject bullet)
    {
        float angle;
        Vector3 vector3, playerDir;

        bullet.transform.position = transform.position;
        vector3 = new Vector2(player.transform.position.x + Random.Range(-3f, 3f), player.transform.position.y + Random.Range(-3f, 3f));
        playerDir = vector3 - bullet.transform.position;
        angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bullet_speed, ForceMode2D.Impulse);
        //prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        StopCoroutine(shot_routine);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<HeliType03>().enabled = false;
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
