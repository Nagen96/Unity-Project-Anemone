using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossType01 : MonoBehaviour
{
    public bool pattern00, pattern01, pattern01_trg, first, isDead;
    public float posX, posY, speed_0, speed_1, speed_2, bullet_speed_0, bullet_speed_1;
    public PlayerInform playerInform;
    public GameObject player, lastboss;
    public GameObject[] BossBullet;
    public List<GameObject> boss_bullet00 = new List<GameObject>();
    public List<GameObject> boss_bullet01 = new List<GameObject>();
    public List<GameObject> enemy = new List<GameObject>();
    public List<GameObject> smallExplosion = new List<GameObject>();
    public List<AudioSource> explosion_sound = new List<AudioSource>();
    //

    Animator animator;
    private IEnumerator contact_dmg_routine, bossPattern_routine, explosion_routine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        contact_dmg_routine = ContactDamage();
        bossPattern_routine = BossPattern();
        explosion_routine = Explosion_Sound();

        StartCoroutine(bossPattern_routine);
    }

    // Update is called once per frame
    void Update()
    {
        if (!first)
        {
            if (transform.position.x > 5)
            {
                transform.Translate(-5 * Time.deltaTime, 0, 0);
            }
            else
                first = true;
        }


        if (pattern00)
        {
            transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(posX, posY, 0),
            speed_0 * Time.deltaTime);
        }

        if (pattern01)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(7.4f, 3.5f, 0), speed_1 * Time.deltaTime);
        }

        if (pattern01_trg)
        {
            transform.Translate(0, -speed_2 * Time.deltaTime, 0);
        }
    }

    IEnumerator BossPattern()
    {
        int i, k;

        yield return new WaitForSeconds(2f);

        while (true)
        {
            i = Random.Range(0, 3);

            if(i == 0)//빠른 총알 여러번 발사
            {
                pattern00 = true;

                for (int c = 0; c < 4; c++)
                {
                    posX = Random.Range(1f, 6f);
                    posY = Random.Range(-1.5f, 3.5f);       
                    yield return new WaitForSeconds(1.5f);

                    foreach (var item in boss_bullet00)
                    {
                        Pattern00(item);
                        yield return new WaitForSeconds(0.1f);
                    }
                }

                pattern00 = false;
            }
            else if(i == 1)//큰 총알을 위에서 부터 아래로 직선(가로)으로 쏘면서 내려옴
            {           
                pattern01 = true;

                yield return new WaitForSeconds(2f);

                pattern01 = false;
                pattern01_trg = true;

                foreach (var item in boss_bullet01)
                {
                    Pattern01(item);
                    yield return new WaitForSeconds(0.4f);
                }

                pattern01_trg = false;
            }
            else if (i == 2)//소환
            {
                if (!playerInform.isDead)
                {
                    k = Random.Range(0, 1);
                    for (int j = 0; j < 6; j++)
                    {
                        Pattern02(k);
                        yield return new WaitForSeconds(0.33f);
                    }

                    yield return new WaitForSeconds(2f);
                }
            }

            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator ContactDamage()
    {
        while (true)
        {
            playerInform.hit_sound.Play();

            playerInform.killpoint -= 10;
            if (playerInform.killpoint < 0)
                playerInform.killpoint = 0;

            playerInform.hp -= 5;

            playerInform.hp_img.fillAmount = playerInform.hp / playerInform.maxhp;

            if (playerInform.hp <= 0)
                playerInform.Death();

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(contact_dmg_routine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(contact_dmg_routine);
        }
    }

    public void Pattern00(GameObject bullet)//스나이퍼 총알로 구성할 것
    {
        float angle;

        bullet.transform.position = transform.position;
        Vector2 playerDir = player.transform.position - bullet.transform.position;
        angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bullet_speed_0, ForceMode2D.Impulse);
    }

    public void Pattern01(GameObject bullet)
    {
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * bullet_speed_1, ForceMode2D.Impulse);
    }

    public void Pattern02(int k)
    {
        GameObject clone_enemy = Instantiate(enemy[k], transform.position, Quaternion.identity);
        clone_enemy.SetActive(true);   
    }

    public void Death()
    {
        isDead = true;
        //playerInform.StopTheAttack();
        StopCoroutine(bossPattern_routine);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BossType01>().enabled = false;
        StartCoroutine(explosion_routine);
        SmallExplosion00();
    }

    public void SmallExplosion00()
    {
        smallExplosion[0].SetActive(true);
        smallExplosion[3].SetActive(true);
        Invoke(nameof(SmallExplosion01), 0.175f);
    }

    public void SmallExplosion01()
    {
        smallExplosion[1].SetActive(true);
        smallExplosion[2].SetActive(true);
        Invoke(nameof(LastExplosion), 3f);
    }

    public void LastExplosion()
    {
        //폭파모션과 폭파사운드 추가
        foreach (var item in smallExplosion)
        {
            item.SetActive(false);
        }
        StopCoroutine(explosion_routine);
        explosion_sound[2].Play();
        animator.SetTrigger("Explosion");
        lastboss.SetActive(true);
        lastboss.transform.parent = null;
        Invoke(nameof(DestroyObj), 2f);
    }

    public void DestroyObj()
    {
        Destroy(BossBullet[0]);
        Destroy(BossBullet[1]);
        Destroy(gameObject);
    }

    IEnumerator Explosion_Sound()
    {
        while (true)
        {
            explosion_sound[0].Play();
            yield return new WaitForSeconds(0.2f);
            explosion_sound[1].Play();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
