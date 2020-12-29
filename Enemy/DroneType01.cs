using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneType01 : MonoBehaviour
{
    public List<GameObject> prefab_EnemyBullet = new List<GameObject>();
    public float speed1, speed2, zRotate, bullet_speed, torque_power, x, y;
    public bool isForward;
    public GameObject player, enemyBullet, bulletPosition, redLight, blueLight;
    IEnumerator shot_routine, light_routine;
    Animator animator;
    public float angle;
    public Vector2 vector2;

    // Use this for initialization
    void Start()
    {
        isForward = true;
        animator = GetComponentInChildren<Animator>();
        shot_routine = ShotToPlayer();
        light_routine = Twinkle_Light();
        StartCoroutine(shot_routine);
        StartCoroutine(light_routine);
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
        }

        if (transform.position.x <= -20f)
        {
            Destroy(enemyBullet);
            Destroy(gameObject);
        }

        vector2 = player.transform.position - bulletPosition.transform.position;
        angle = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
        bulletPosition.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

    IEnumerator Twinkle_Light()
    {
        while (true)
        {
            blueLight.SetActive(true);
            redLight.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            redLight.SetActive(true);
            blueLight.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator ShotToPlayer()
    {
        int count = 0;

        yield return new WaitForSeconds(2f);

        while (count < 4)
        {
            foreach (var item in prefab_EnemyBullet)
            {
                TargettingToPlayer(item);
                yield return new WaitForSeconds(1f);
            }
            count++;
        }

        isForward = false;
        //yield return null;
    }

    public void TargettingToPlayer(GameObject bullet)
    {
        // GameObject bullet = Instantiate(prefab_Bullet, transform.position, Quaternion.identity);

        // Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();

        bullet.transform.position = transform.position;
        Vector2 playerDir = player.transform.position - bullet.transform.position;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bullet_speed, ForceMode2D.Impulse);
        bullet.GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        StopCoroutine(shot_routine);
        StopCoroutine(light_routine);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<DroneType01>().enabled = false;
        bulletPosition.GetComponent<SpriteRenderer>().enabled = false;
        redLight.SetActive(false);
        blueLight.SetActive(false);
        //폭파모션과 폭파사운드 추가
        animator.SetTrigger("Explosion");
        Invoke(nameof(DestroyObj), 5f);
    }

    public void DestroyObj()
    {
        enemyBullet.SetActive(false);
        gameObject.SetActive(false);
    }
}
