using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliType02 : MonoBehaviour
{
    public GameObject prefab_EnemyBullet;
    public float speed, yRotate, ySpeed, bullet_speed, torque_power, spMinus, spPlus, turningPoint;
    public bool isForward;
    public GameObject player, sprite, enemyBullet;
    Animator animator;
    IEnumerator shot_routine;

    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        isForward = true;
        shot_routine = ShotToPlayer();
        StartCoroutine(shot_routine);
    }

    private void Update()
    {
        if (transform.position.x < turningPoint)
            isForward = false;

        if (isForward)
        {
            if (transform.position.x < 3f && speed > 0.5f)
            {
                speed -= spMinus;
                //transform.Translate(0f, ySpeed, 0f);
            }

            transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            if (speed < 5f)
                speed += spPlus;
            transform.Translate(speed * Time.deltaTime, ySpeed * Time.deltaTime, 0f);

            if (sprite.transform.localRotation.y < 0.975f)
                sprite.transform.Rotate(0f, yRotate * Time.deltaTime, 0f);
            else
                sprite.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }

        if (transform.position.x >= 20f)
        {
            Destroy(enemyBullet);
            Destroy(gameObject);
        }
    }

    /*private void FixedUpdate()
    {
        if (transform.position.x < 0f)
            isForward = false;

        if (isForward)
        {
            if (transform.position.x < 2f && speed > 0.01f)
            {
                speed -= 0.0025f;
                //transform.Translate(0f, ySpeed, 0f);
            }
                
            transform.Translate(-speed, 0f, 0f);
        }
        else
        {
            if (speed < 0.1f)
                speed += 0.001f;
            transform.Translate(speed, ySpeed, 0f);

            if (sprite.transform.localRotation.y < 1f)
                sprite.transform.Rotate(0f, yRotate, 0f);
        }

        if (transform.position.x >= 20f)
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
            yield return new WaitForSeconds(3f);
            TargettingToPlayer();
            count++;
        }
        yield return null;
    }

    public void TargettingToPlayer()
    {
        // GameObject bullet = Instantiate(prefab_Bullet, transform.position, Quaternion.identity);

        // Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();

        prefab_EnemyBullet.transform.position = transform.position;
        Vector2 playerDir = player.transform.position - prefab_EnemyBullet.transform.position;
        prefab_EnemyBullet.SetActive(true);
        prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bullet_speed, ForceMode2D.Impulse);
        prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    public void Death()
    {
        StopCoroutine(shot_routine);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<HeliType02>().enabled = false;
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
