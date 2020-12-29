using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneType03 : MonoBehaviour
{
    public List<GameObject> prefab_EnemyBullet = new List<GameObject>();
   // public List<GameObject> prefab_EnemyBullet01 = new List<GameObject>();
    public float speed_hrz, speed_vtc, bullet_speed, torque_power;
    public bool upper, down;
    public GameObject player, enemyBullet;
    public AudioSource explosion_sound;
    IEnumerator shot_routine;
    public GameObject explosion, sprite;

    // Use this for initialization
    void Start()
    {
        shot_routine = ShotToPlayer();
        StartCoroutine(shot_routine);
    }

    private void Update()
    {
        if (upper)
        {
            transform.Translate(-speed_hrz * Time.deltaTime, speed_vtc * Time.deltaTime, 0);
        }
        else if (down)
        {
            transform.Translate(-speed_hrz * Time.deltaTime, -speed_vtc * Time.deltaTime, 0);
        }
        else
        {
            transform.Translate(-speed_hrz * Time.deltaTime, 0, 0);
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

        while (count < 1)
        {
            yield return new WaitForSeconds(0.8f);

            foreach (var item in prefab_EnemyBullet)
            {
                TargettingToPlayer(item);
                yield return new WaitForSeconds(0.1f);
            }
            count++;
        }
    }

    public void TargettingToPlayer(GameObject bullet)
    {
        Vector3 vector3, playerDir;

        bullet.transform.position = transform.position;
        vector3 = new Vector2(player.transform.position.x + Random.Range(-3f, 3f), player.transform.position.y + Random.Range(-3f, 3f));
        playerDir = vector3 - bullet.transform.position;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bullet_speed, ForceMode2D.Impulse);
        bullet.GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        StopCoroutine(shot_routine);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<PlaneType03>().enabled = false;
        //폭파모션과 폭파사운드 추가
        explosion_sound.Play();
        explosion.SetActive(true);
        sprite.SetActive(false);
        Invoke(nameof(DestroyObj), 5f);
    }

    public void DestroyObj()
    {
        enemyBullet.SetActive(false);
        gameObject.SetActive(false);
    }
}
