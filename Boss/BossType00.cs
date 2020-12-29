using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossType00 : MonoBehaviour
{
    public bool isMove, first;
    public float torque_power, posX, posY, speed;
    public float[] x, y, bullet_speed;

    public GameObject player, pf_clonebullet;
    public GameObject[] BossBullet;
    public GameController gameController;

    public List<GameObject> smallExplosion = new List<GameObject>();
    public List<GameObject> prefab_EnemyBullet = new List<GameObject>();
    public List<GameObject> bullet_Bomb = new List<GameObject>();
    public List<GameObject> prefab_EnemyBullet01 = new List<GameObject>();
    public List<GameObject> prefab_EnemyBullet02 = new List<GameObject>();
    public List<GameObject> enemyNoticeBeam = new List<GameObject>();
    public List<GameObject> enemyBeam = new List<GameObject>();

    IEnumerator shot_routine, fp_routine, contact_dmg_routine;
    Animator animator;
    PlayerInform playerInform;

    public AudioSource b_sound, w_sound;

    // Start is called before the first frame update
    void Start()
    {
        playerInform = player.GetComponent<PlayerInform>();

        shot_routine = BossPattern();
        fp_routine = FixedPattern();
        contact_dmg_routine = ContactDamage();

        StartCoroutine(shot_routine);
        StartCoroutine(fp_routine);
        animator = GetComponentInChildren<Animator>();
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

        if (isMove)
        {
            if (transform.position.x > posX)
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            else transform.Translate(speed * Time.deltaTime, 0, 0);


            if (transform.position.y > posY)
                transform.Translate(0, -speed * Time.deltaTime, 0);
            else transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }

    IEnumerator ContactDamage()
    {
        while (true)
        {
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

    IEnumerator BossPattern()
    {
        int i;

        yield return new WaitForSeconds(2f);

        while (true)
        {
            i = Random.Range(0, 3);

            if (i == 0)
            {
                foreach (var item in prefab_EnemyBullet)
                {
                    Pattern00(item);
                    yield return new WaitForSeconds(0.05f);
                }
            }

            else if (i == 1)
            {
                foreach (var item in bullet_Bomb)
                {
                    isMove = true;

                    Pattern01(item);
                    posX = Random.Range(1f, 6f);
                    posY = Random.Range(-3.5f, 3.5f);

                    yield return new WaitForSeconds(1f);
                    isMove = false;

                    yield return new WaitForSeconds(1f);
                    Pattern01_detail(item);//터지는 부분
                }
            }

            else if (i == 2)
            {
                Pattern02(false);
                yield return new WaitForSeconds(0.5f);
                Pattern02(true);
            }

            yield return new WaitForSeconds(2f);
        }
        //  yield return null;
    }

    IEnumerator FixedPattern()
    {
        int i, j;
        float time;

        while (true)
        {
            time = Random.Range(10, 20);
            yield return new WaitForSeconds(time);

            //한 곳의 빔
            for (int k = 0; k < 4; k++)
            {
                i = Random.Range(0, 3);

                w_sound.Play();
                enemyNoticeBeam[i].SetActive(true);
                yield return new WaitForSeconds(0.2f);
                enemyNoticeBeam[i].SetActive(false);
                yield return new WaitForSeconds(0.2f);

                w_sound.Play();
                enemyNoticeBeam[i].SetActive(true);
                yield return new WaitForSeconds(0.2f);
                enemyNoticeBeam[i].SetActive(false);
                yield return new WaitForSeconds(0.2f);

                w_sound.Play();
                enemyNoticeBeam[i].SetActive(true);
                yield return new WaitForSeconds(0.2f);
                enemyNoticeBeam[i].SetActive(false);
                yield return new WaitForSeconds(0.2f);

                b_sound.Play();
                enemyBeam[i].GetComponent<Collider2D>().enabled = true;
                enemyBeam[i].transform.localScale = new Vector2(30, 2f);
                enemyBeam[i].SetActive(true);
                yield return new WaitForSeconds(0.1f);
                enemyBeam[i].transform.localScale = new Vector2(30, 1.6f);
                yield return new WaitForSeconds(0.1f);
                enemyBeam[i].transform.localScale = new Vector2(30, 1.2f);
                yield return new WaitForSeconds(0.1f);
                enemyBeam[i].transform.localScale = new Vector2(30, 0.8f);
                yield return new WaitForSeconds(0.1f);
                enemyBeam[i].transform.localScale = new Vector2(30, 0.4f);
                yield return new WaitForSeconds(0.1f);
                enemyBeam[i].SetActive(false);

                yield return new WaitForSeconds(0.5f);

            }
            i = Random.Range(0, 3);
            j = Random.Range(0, 3);

            //순서i번째와 j번째가 겹쳐지지 않도록 하는 코드
            if (i == j)
            {
                if (j == 0)
                    j++;
                else if (j == 1)
                {
                    if (Random.Range(0, 2) == 0)
                        j--;
                    else j++;
                }
                else if (j == 2)
                    j--;
            }
            //두 곳의 빔
            w_sound.Play();
            enemyNoticeBeam[i].SetActive(true);
            enemyNoticeBeam[j].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            enemyNoticeBeam[i].SetActive(false);
            enemyNoticeBeam[j].SetActive(false);
            yield return new WaitForSeconds(0.2f);

            w_sound.Play();
            enemyNoticeBeam[i].SetActive(true);
            enemyNoticeBeam[j].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            enemyNoticeBeam[i].SetActive(false);
            enemyNoticeBeam[j].SetActive(false);
            yield return new WaitForSeconds(0.2f);

            w_sound.Play();
            enemyNoticeBeam[i].SetActive(true);
            enemyNoticeBeam[j].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            enemyNoticeBeam[i].SetActive(false);
            enemyNoticeBeam[j].SetActive(false);
            yield return new WaitForSeconds(0.2f);

            b_sound.Play();
            enemyBeam[i].transform.localScale = new Vector2(30, 2f);
            enemyBeam[j].transform.localScale = new Vector2(30, 2f);
            enemyBeam[i].SetActive(true);
            enemyBeam[j].SetActive(true);
            yield return new WaitForSeconds(0.1f);
            enemyBeam[i].transform.localScale = new Vector2(30, 1.6f);
            enemyBeam[j].transform.localScale = new Vector2(30, 1.6f);
            yield return new WaitForSeconds(0.1f);
            enemyBeam[i].transform.localScale = new Vector2(30, 1.2f);
            enemyBeam[j].transform.localScale = new Vector2(30, 1.2f);
            yield return new WaitForSeconds(0.1f);
            enemyBeam[i].transform.localScale = new Vector2(30, 0.8f);
            enemyBeam[j].transform.localScale = new Vector2(30, 0.8f);
            yield return new WaitForSeconds(0.1f);
            enemyBeam[i].transform.localScale = new Vector2(30, 0.4f);
            enemyBeam[j].transform.localScale = new Vector2(30, 0.4f);
            yield return new WaitForSeconds(0.1f);
            enemyBeam[i].SetActive(false);
            enemyBeam[j].SetActive(false);
        }
    }

    public void Pattern00(GameObject prefab_EnemyBullet)
    {
        prefab_EnemyBullet.transform.position = transform.position;
        Vector2 playerDir = player.transform.position - prefab_EnemyBullet.transform.position;
        prefab_EnemyBullet.SetActive(true);
        prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bullet_speed[0], ForceMode2D.Impulse);
        prefab_EnemyBullet.GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    public void Pattern01(GameObject bullet_Bomb)
    {
        bullet_Bomb.transform.position = transform.position;
        bullet_Bomb.SetActive(true);
        bullet_Bomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * bullet_speed[1], ForceMode2D.Impulse);
    }

    public void Pattern01_detail(GameObject bullet_Bomb)
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject clonebullet = Instantiate(pf_clonebullet, bullet_Bomb.transform.position, Quaternion.identity);
            clonebullet.SetActive(true);
            clonebullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(x[i], y[i]) * bullet_speed[2], ForceMode2D.Impulse);
        }
        bullet_Bomb.SetActive(false);
    }

    public void Pattern02(bool pattern)
    {
        float y;

        if (!pattern)
        {
            y = 1;

            foreach (var item in prefab_EnemyBullet01)
            {
                item.transform.position = transform.position;
                item.SetActive(true);
                item.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, y) * bullet_speed[0], ForceMode2D.Impulse);
                y -= 0.2f;
            }
        }
        else
        {
            y = 0.9f;
            foreach (var item in prefab_EnemyBullet02)
            {
                item.transform.position = transform.position;
                item.SetActive(true);
                item.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, y) * bullet_speed[0], ForceMode2D.Impulse);
                y -= 0.2f;
            }
        }

    }

    public void Death()
    {
        playerInform.StopTheAttack();
        StopCoroutine(shot_routine);
        StopCoroutine(fp_routine);
        for (int i = 0; i < 3; i++)
        {
            enemyBeam[i].SetActive(false);
            enemyNoticeBeam[i].SetActive(false);
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BossType00>().enabled = false;
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
        animator.SetTrigger("Explosion");
        Invoke(nameof(DestroyObj), 2f);
    }

    public void DestroyObj()
    {
        gameController.ShowWindowOfNextStage();
        Destroy(BossBullet[0]);
        Destroy(BossBullet[1]);
        Destroy(gameObject);
    }

}
