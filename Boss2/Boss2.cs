using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : MonoBehaviour
{
    public GameObject player;
    public GameObject[] boss2BulletSize3;
    public GameController gameController;

    public List<GameObject> deathEffectSmallExplosion = new List<GameObject>();
    public List<GameObject> boss2Bullet00Size15 = new List<GameObject>();
    public List<GameObject> boss2Missile00Size5 = new List<GameObject>();
    public List<GameObject> boss2MissileDanger00Size5 = new List<GameObject>();

    private float bulletSpeed10;
    private float torquePower;
    private float[] missileX, missileY;
    IEnumerator bossPatternIEnumerator;
    IEnumerator bossMissilePatternIEnumerator;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed10 = 10f;
        torquePower = 0;

        // IEnumerator
        bossPatternIEnumerator = BossPattern();
        bossMissilePatternIEnumerator = BossMissilePattern();
        StartCoroutine(bossPatternIEnumerator);
        StartCoroutine(bossMissilePatternIEnumerator);

        // Animator Settings
        animator = GetComponentInChildren<Animator>();

        // Boss Missile Position Settings
        BossMissilePositionSettings();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(
        transform.position,
        new Vector3(4.07f, 2.68f, 0),
        3 * Time.deltaTime);
    }

    IEnumerator BossMissilePattern()
    {
        int missilePosXDeciosionNumber;

        yield return new WaitForSeconds(2f);

        while (true)
        {


            for (int i = 0; i < boss2Missile00Size5.Count; i++)
            {
                missilePosXDeciosionNumber = Random.Range(0, 6);

                if (i != (boss2Missile00Size5.Count - 1))
                {
                    BossMissileDanger(i, missilePosXDeciosionNumber);
                    yield return new WaitForSeconds(0.3f);

                    BossMissileAttack(i, missilePosXDeciosionNumber);
                    yield return new WaitForSeconds(1.0f);
                }

                else if (i == (boss2Missile00Size5.Count-1))
                {
                    BossMissileDanger(boss2Missile00Size5.Count-1, 7);
                    yield return new WaitForSeconds(0.3f);

                    BossMissileAttack(boss2Missile00Size5.Count - 1, 7);
                    yield return new WaitForSeconds(4.0f);
                }
            }



            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator BossPattern()
    {
        int patternDecisionNumber;

        yield return new WaitForSeconds(2f);

        while (true)
        {
            patternDecisionNumber = Random.Range(0, 3);
            /*patternDecisionNumber = 2;*/




            // Pattern00 Call
            // 일직선 공격
            if (patternDecisionNumber == 0)
            {
                int count = 0;
                foreach (var item in boss2Bullet00Size15)
                {
                    Pattern00(item);
                    count++;
                    if (count == 10) break;
                    yield return new WaitForSeconds(0.1f);
                }
            }



            // Pattern01 Call
            // 랜덤 탄막
            else if (patternDecisionNumber == 1)
            {
                foreach (var item in boss2Bullet00Size15)
                {
                    Pattern01(item);
                    yield return new WaitForSeconds(0.35f);
                }
            }



            // Pattern02 Call
            // Cos 탄막
            else if (patternDecisionNumber == 2)
            {
                float y = 0;
                for (int i = 0; i < 3; i++)
                {
                    foreach (var item in boss2Bullet00Size15)
                    {
                        Pattern02(item, y);
                        y += 0.2f;
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }

            yield return new WaitForSeconds(2f);
        }
    }


    // Pattern00 Detail
    public void Pattern00(GameObject boss2Bullet00)
    {
        boss2Bullet00.transform.position = transform.position;
        Vector2 playerDir = player.transform.position - boss2Bullet00.transform.position;
        boss2Bullet00.SetActive(true);
        boss2Bullet00.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bulletSpeed10 * 1.4f, ForceMode2D.Impulse);
        boss2Bullet00.GetComponent<Rigidbody2D>().AddTorque(torquePower, ForceMode2D.Impulse);
    }


    // Pattern01 Detail
    public void Pattern01(GameObject boss2Bullet00)
    {
        for (int index = 0; index < 6; index++)
        {
            boss2Bullet00.transform.position = transform.position;
            Vector2 playerDir = player.transform.position - boss2Bullet00.transform.position;
            Vector2 randVec = new Vector2(Random.Range(-3f, 3f), Random.Range(-8f, 8f));
            playerDir += randVec;
            boss2Bullet00.SetActive(true);
            boss2Bullet00.GetComponent<Rigidbody2D>().AddForce(playerDir.normalized * bulletSpeed10 * 0.2f, ForceMode2D.Impulse);
            boss2Bullet00.GetComponent<Rigidbody2D>().AddTorque(torquePower, ForceMode2D.Impulse);
        }
    }


    // Pattern02 Detail
    public void Pattern02(GameObject boss2Bullet00, float y)
    {
        boss2Bullet00.transform.position = transform.position;
        Vector2 fireDir = new Vector2(-1, Mathf.Cos(y));
        boss2Bullet00.SetActive(true);
        boss2Bullet00.GetComponent<Rigidbody2D>().AddForce(fireDir.normalized * bulletSpeed10 * 1.6f, ForceMode2D.Impulse);
        boss2Bullet00.GetComponent<Rigidbody2D>().AddTorque(torquePower, ForceMode2D.Impulse);
    }


    // BossMissilePositionSettings
    public void BossMissilePositionSettings()
    {
        missileX = new float[8];
        missileY = new float[4];
        float MissilePosX = -8.0f;
        float MissilePosY = 4.0f;
        for (int i = 0; i < missileX.Length; i++)
        {
            missileX[i] = MissilePosX;
            MissilePosX += 1.7f;
        }
        for (int i = 0; i < missileY.Length; i++)
        {
            missileY[i] = MissilePosY;
            MissilePosY -= 2.0f;
        }
        missileX[missileX.Length - 1] -= 0.4f;
    }



    // BossMissileAttack
    public void BossMissileDanger(int MissileDangerNumber, int missilePosXDeciosionNumber)
    {
        boss2MissileDanger00Size5[MissileDangerNumber].transform.position = new Vector2(missileX[missilePosXDeciosionNumber], 0);
        boss2MissileDanger00Size5[MissileDangerNumber].SetActive(true);
    }


    public void BossMissileAttack(int missileNumber, int missilePosXDeciosionNumber)
    {
        boss2MissileDanger00Size5[missileNumber].SetActive(false);
        boss2Missile00Size5[missileNumber].transform.position = new Vector2(missileX[missilePosXDeciosionNumber], 7);
        boss2Missile00Size5[missileNumber].SetActive(true);
        boss2Missile00Size5[missileNumber].GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1).normalized *
            bulletSpeed10 * 1.0f, ForceMode2D.Impulse);
    }


    // 객체 파괴 로직
    public void Death()
    {
        StopCoroutine(bossPatternIEnumerator);
        StopCoroutine(bossMissilePatternIEnumerator);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Boss2>().enabled = false;
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
        Invoke(nameof(LastExplosion), 3f);
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
        gameController.ShowWindowOfNextStage();
        Destroy(boss2BulletSize3[0]);
        Destroy(boss2BulletSize3[1]);
        Destroy(boss2BulletSize3[2]);
        Destroy(gameObject);
    }
}
