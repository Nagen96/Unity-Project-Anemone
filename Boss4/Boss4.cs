using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss4 : MonoBehaviour
{
    public GameObject player;
    public GameObject[] boss4BulletSize2;
    public GameObject chestCoordinate;
    public GameObject backCoordinate;
    public GameObject cloneBullet;

    public LastGMcontroller lastgm;
    public List<GameObject> deathEffectSmallExplosion = new List<GameObject>();
    public List<GameObject> boss4Bullet00Size15 = new List<GameObject>();
    private float bulletSpeed10;
    private float torquePower;
    private Vector2[] chestAttackDir;
    IEnumerator bossPatternIEnumerator;
    IEnumerator bossBackPatternIEnumerator;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed10 = 10f;
        torquePower = 15;

        // IEnumerator
        bossPatternIEnumerator = BossPattern();
        bossBackPatternIEnumerator = BossBackPattern();
        StartCoroutine(bossPatternIEnumerator);
        StartCoroutine(bossBackPatternIEnumerator);

        // Animator Settings
        animator = GetComponentInChildren<Animator>();

        // Chest Attack Direction Settings
        ChestAttackDirectionSettings();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(3f, 0, 0),
            3 * Time.deltaTime);
    }

    IEnumerator BossBackPattern()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            int i = 0;
            foreach (var item in boss4Bullet00Size15)
            {
                Pattern02(item);
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(0.1f);

            foreach (var item in boss4Bullet00Size15)
            {
                Pattern02Flare(i, item);
                i++;
                yield return new WaitForSeconds(0.03f);
            }

            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator BossPattern()
    {
        int patternDecisionNumber;

        yield return new WaitForSeconds(2f);

        while (true)
        {
            patternDecisionNumber = Random.Range(0, 2);
            /*patternDecisionNumber = 2;*/




            // Pattern00 Call
            // 일직선 공격
            if (patternDecisionNumber == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    Pattern00();
                    yield return new WaitForSeconds(0.2f);
                }
            }


            // Pattern01 Call
            // 가슴 회전 공격
            else if (patternDecisionNumber == 1)
            {
                for (int i = 0; i < 40; i++)
                {
                    Pattern01(i);
                    yield return new WaitForSeconds(0.1f);
                }

            }



            yield return new WaitForSeconds(2f);
        }
    }


    // Pattern00 Detail
    public void Pattern00()
    {
        GameObject temp = Instantiate(cloneBullet);
        temp.transform.position = transform.position;
        Vector2 playerDir = player.transform.position - temp.transform.position;
        temp.SetActive(true);
        temp.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bulletSpeed10 * 1.0f, ForceMode2D.Impulse);
        temp.GetComponent<Rigidbody2D>().AddTorque(torquePower, ForceMode2D.Impulse);
    }


    public void ChestAttackDirectionSettings()
    {
        chestAttackDir = new Vector2[7];
        chestAttackDir[0] = new Vector2(-1, 2).normalized;
        chestAttackDir[1] = new Vector2(-2, 2).normalized;
        chestAttackDir[2] = new Vector2(-2, 1).normalized;
        chestAttackDir[3] = new Vector2(-2, 0).normalized;
        chestAttackDir[4] = new Vector2(-2, -1).normalized;
        chestAttackDir[5] = new Vector2(-2, -2).normalized;
        chestAttackDir[6] = new Vector2(-1, -2).normalized;
    }


    public void Pattern01(int i)
    {
        int dirNum = (i % 5) +1;
        GameObject temp = Instantiate(cloneBullet);
        temp.transform.position = chestCoordinate.transform.position;
        Vector2 playerDir = player.transform.position - temp.transform.position;
        temp.SetActive(true);
        if (dirNum % 3 == 0)
            temp.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bulletSpeed10 * 0.7f, ForceMode2D.Impulse);
        else if (dirNum % 3 == 1)
            temp.GetComponent<Rigidbody2D>().AddForce((playerDir + (chestAttackDir[dirNum] * 8f)).normalized * bulletSpeed10 * 0.8f, ForceMode2D.Impulse);
        else if (dirNum % 3 == 2)
            temp.GetComponent<Rigidbody2D>().AddForce((playerDir - (chestAttackDir[dirNum] * 8f)).normalized * bulletSpeed10 * 0.8f, ForceMode2D.Impulse);
        temp.GetComponent<Rigidbody2D>().AddTorque(torquePower, ForceMode2D.Impulse);
        Destroy(temp, 4f);
    }


    public void Pattern02(GameObject boss2Bullet00)
    {
        boss2Bullet00.transform.position = backCoordinate.transform.position;
        Vector2 launchDir = new Vector2(-1, 4);
        boss2Bullet00.SetActive(true);
        boss2Bullet00.GetComponent<Rigidbody2D>().AddForce(launchDir.normalized * bulletSpeed10, ForceMode2D.Impulse);
        boss2Bullet00.GetComponent<Rigidbody2D>().AddTorque(torquePower, ForceMode2D.Impulse);
    }

    public void Pattern02Flare(int i, GameObject boss2Bullet00)
    {
        Vector2 FlareDir = new Vector2(0f, -6f);
        FlareDir += chestAttackDir[i%7] * 7;
        boss2Bullet00.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        boss2Bullet00.GetComponent<Rigidbody2D>().AddForce(FlareDir.normalized * bulletSpeed10, ForceMode2D.Impulse);
        boss2Bullet00.GetComponent<Rigidbody2D>().AddTorque(torquePower, ForceMode2D.Impulse);
    }


    // 객체 파괴 로직
    public void Death()
    {
        StopCoroutine(bossPatternIEnumerator);
        StopCoroutine(bossBackPatternIEnumerator);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Boss4>().enabled = false;
        SmallExplosion00();
    }

    public void SmallExplosion00()
    {
        deathEffectSmallExplosion[0].SetActive(true);
        deathEffectSmallExplosion[3].SetActive(true);
        Invoke(nameof(SmallExplosion01), 0.175f);
    }

    public void SmallExplosion01()
    {
        deathEffectSmallExplosion[1].SetActive(true);
        deathEffectSmallExplosion[2].SetActive(true);
        lastgm.White();
    }

    public void LastExplosion()
    {
        //폭파모션과 폭파사운드 추가
        foreach (var item in deathEffectSmallExplosion)
        {
            item.SetActive(false);
        }
        animator.SetTrigger("Explosion");
        Invoke(nameof(DestroyObj), 5f);
    }

    public void DestroyObj()
    {
        Destroy(boss4BulletSize2[0]);
        Destroy(boss4BulletSize2[1]);
        Destroy(gameObject);
    }
}

