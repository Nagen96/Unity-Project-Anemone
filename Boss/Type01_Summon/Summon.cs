using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject prefab_EnemyBullet;
    public float speed1, bullet_speed, torque_power, x, y;
    public bool isForward, on;
    GameObject player;
    IEnumerator shot_routine;
    Animator animator;
    BossType01 type01;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        type01 = FindObjectOfType<BossType01>();
        y = Random.Range(-2.3f, 4.4f);
        x = Random.Range(0f, 5f);
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

        if (type01.isDead)
        {
            if (!on)
            {
                Death();
                on = true;
            }
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
        while (!player.GetComponent<PlayerInform>().isDead)
        {
            yield return new WaitForSeconds(2f);
            TargettingToPlayer();
        }
    }

    public void TargettingToPlayer()
    {
        // GameObject bullet = Instantiate(prefab_Bullet, transform.position, Quaternion.identity);

        // Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();

        GameObject clonebullet = Instantiate(prefab_EnemyBullet, transform.position, Quaternion.identity);
        Vector2 playerDir = player.transform.position - clonebullet.transform.position;
        clonebullet.SetActive(true);
        clonebullet.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bullet_speed, ForceMode2D.Impulse);
        clonebullet.GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        StopCoroutine(shot_routine);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Summon>().enabled = false;
        animator.SetTrigger("Explosion");
        Invoke(nameof(DestroyObj), 5f);
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
