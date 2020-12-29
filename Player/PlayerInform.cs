using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInform : MonoBehaviour
{
    Rigidbody2D rb2d;
    Collider2D collider2d;
    public float attackSpeed, attackSpeed_LMG, attackSpeed_Rocket, dmg, lmg_dmg, rocket_dmg;
    public float direct_dmg, direct_lmg_dmg, direct_rck_dmg, hp, maxhp, dmg_coefficient;

    public List<GameObject> main_bullet = new List<GameObject>();
    public List<GameObject> lmg_bullet00 = new List<GameObject>();
    public List<GameObject> lmg_bullet01 = new List<GameObject>();
    public List<GameObject> rocket00 = new List<GameObject>();
    public List<GameObject> rocket01 = new List<GameObject>();

    public List<GameObject> lmg_virtualTarget = new List<GameObject>();
    public List<GameObject> rocket_virtualTarget = new List<GameObject>();

    public List<GameObject> lmg_bulletposition = new List<GameObject>();
    public List<GameObject> rocket_position = new List<GameObject>();

    public List<GameObject> lmg_drone = new List<GameObject>();

    public GameObject main_virtualTarget, main_bulletposition;
    public GameObject barrier, overPanel;

    public int main_seq, lmg_seq, rocket_seq, scrap, killpoint;
    public List<GameObject> combo = new List<GameObject>();

    public AudioSource hit_sound, explosion_sound;
    public GameObject fire_sound;

    Animator anim;
    
    //Vector2 test;
    public Image hp_img;
    IEnumerator autofire_main, autofire_lmg, autofire_rocket;
    public bool isDead = false, randomfireOn = false;

    const string LATEST_DMG = "LatestDmg", LATEST_DMG_COEFFICIENT = "LatestDmgCoefficient";
    const string LATEST_HP = "LatestHP", LATEST_ATTACK_SPEED = "LatestAttackSpeed";
    const string BARRIER = "Barrier", RANDOM_FIRE = "RandomFire", ROCKET = "Rocket", DRONE = "Drone";
    const string LATEST_SCRAP = "LatestScrap";

    void Start()
    {
        scrap = PlayerPrefs.GetInt(LATEST_SCRAP);
        dmg *= PlayerPrefs.GetFloat(LATEST_DMG) + 1;
        lmg_dmg *= PlayerPrefs.GetFloat(LATEST_DMG) + 1;
        rocket_dmg *= PlayerPrefs.GetFloat(LATEST_DMG) + 1;
        direct_dmg = dmg;
        direct_lmg_dmg = lmg_dmg;
        direct_rck_dmg = rocket_dmg;

        dmg_coefficient = PlayerPrefs.GetFloat(LATEST_DMG_COEFFICIENT) + 1f;

        if (PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED) > 0.5f)
            PlayerPrefs.SetFloat(LATEST_ATTACK_SPEED, 0.5f);

        attackSpeed *= PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED) + 1f;
        attackSpeed -= 0.12f;
        attackSpeed = 0.12f - attackSpeed;

        attackSpeed_LMG *= PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED) + 1f;
        attackSpeed_LMG -= 0.12f;
        attackSpeed_LMG = 0.12f - attackSpeed_LMG;

        attackSpeed_Rocket *= PlayerPrefs.GetFloat(LATEST_ATTACK_SPEED) + 1f;
        attackSpeed_Rocket -= 0.5f;
        attackSpeed_Rocket = 0.5f - attackSpeed_Rocket;

        if (PlayerPrefs.GetFloat(LATEST_HP) < -60f)
            PlayerPrefs.SetFloat(LATEST_HP, -60f);

        hp = PlayerPrefs.GetFloat(LATEST_HP) + 100f;
        maxhp = hp;

        if (PlayerPrefs.GetInt(BARRIER) == 1)
            barrier.SetActive(true);

        if (PlayerPrefs.GetInt(RANDOM_FIRE) == 1)
            randomfireOn = true;

        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        autofire_main = AutoFire_Main();
        autofire_lmg = AutoFire_LMG();
        autofire_rocket = AutoFire_Rocket();

        StartCoroutine(autofire_main);
        //StartCoroutine(autofire_lmg);
        //StartCoroutine(autofire_rocket);

        if (PlayerPrefs.GetInt(DRONE) == 1)
        {
            lmg_drone[0].SetActive(true);
            lmg_drone[1].SetActive(true);
            StartCoroutine(autofire_lmg);
        }
            
        if (PlayerPrefs.GetInt(ROCKET) == 1)
            StartCoroutine(autofire_rocket);
    }

    private void Update()
    {
        ComboKill();
    }

    // Update is called once per frame
    /*void Update()
    {
        rise = rb2d.velocity.y;

        if(Input.GetButton("Fire1") && transform.position.y < maxY)
        {
            if (rb2d.velocity.y < 6)
                rb2d.AddForce(new Vector2(0, riseForce));
        }

        if(transform.position.y >= maxY)
        {
            // rb2d.AddForce(new Vector2(0, -5));
            rb2d.velocity = new Vector2(0, -3f);
        }
        else if(transform.position.y <= minY)
        {
            //rb2d.AddForce(new Vector2(0, 5));
            rb2d.velocity = new Vector2(0, 3f);
        }

        Flying();
    }

    public void Flying()
    {
        
        float targerAngle = Mathf.Atan2(rb2d.velocity.y, relativeX) * Mathf.Rad2Deg;
        angle = Mathf.Lerp(angle, targerAngle, Time.deltaTime * 10f);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //sprite.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
    */
    IEnumerator AutoFire_Main()
    {
        Vector2 vector2;
        float y, angle;

        while (true)
        {
            if (main_seq >= 40)
                main_seq = 0;
            
            yield return new WaitForSeconds(attackSpeed);

            if (randomfireOn)
            {
                y = Random.Range(-0.4f, 0.4f);
                main_virtualTarget.transform.localPosition = new Vector3(1, y, 0);
                main_bullet[main_seq].transform.position = main_bulletposition.transform.position;
                vector2 = main_virtualTarget.transform.position - main_bullet[main_seq].transform.position;
                angle = Mathf.Atan2(vector2.y, vector2.x) * Mathf.Rad2Deg;
                main_bullet[main_seq].transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                main_bullet[main_seq].transform.position = main_bulletposition.transform.position;
                vector2 = main_virtualTarget.transform.position - main_bullet[main_seq].transform.position;
            }
            main_bullet[main_seq].SetActive(true);
            main_bullet[main_seq].GetComponent<Rigidbody2D>().AddForce(vector2.normalized * 20f, ForceMode2D.Impulse);
           // main_bullet[main_seq].GetComponent<Rigidbody2D>().AddTorque(-15, ForceMode2D.Impulse);
            main_seq += 1;
        }
        
    }

    IEnumerator AutoFire_LMG()
    {
        Vector2 vector2;

        while (true)
        {
            if (lmg_seq >= 40)
                lmg_seq = 0;

            yield return new WaitForSeconds(attackSpeed_LMG);
            lmg_bullet00[lmg_seq].transform.position = lmg_bulletposition[0].transform.position;
            vector2 = lmg_virtualTarget[0].transform.position - lmg_bullet00[lmg_seq].transform.position;
            lmg_bullet00[lmg_seq].SetActive(true);
            lmg_bullet00[lmg_seq].GetComponent<Rigidbody2D>().AddForce(vector2.normalized * 20f, ForceMode2D.Impulse);

            lmg_bullet01[lmg_seq].transform.position = lmg_bulletposition[1].transform.position;
            vector2 = lmg_virtualTarget[1].transform.position - lmg_bullet01[lmg_seq].transform.position;
            lmg_bullet01[lmg_seq].SetActive(true);
            lmg_bullet01[lmg_seq].GetComponent<Rigidbody2D>().AddForce(vector2.normalized * 20f, ForceMode2D.Impulse);

            lmg_seq += 1;
        }
    }

    IEnumerator AutoFire_Rocket()
    {
        Vector2 vector2;

        while (true)
        {
            if (rocket_seq >= 20)
                rocket_seq = 0;

            yield return new WaitForSeconds(attackSpeed_Rocket);
            rocket00[rocket_seq].transform.position = rocket_position[0].transform.position;
            vector2 = rocket_virtualTarget[0].transform.position - rocket00[rocket_seq].transform.position;
            rocket00[rocket_seq].SetActive(true);
            rocket00[rocket_seq].GetComponent<Rigidbody2D>().AddForce(vector2.normalized * 10f, ForceMode2D.Impulse);

            rocket01[rocket_seq].transform.position = rocket_position[1].transform.position;
            vector2 = rocket_virtualTarget[1].transform.position - rocket01[rocket_seq].transform.position;
            rocket01[rocket_seq].SetActive(true);
            rocket01[rocket_seq].GetComponent<Rigidbody2D>().AddForce(vector2.normalized * 10f, ForceMode2D.Impulse);

            rocket_seq++;
        }
    }

    public void StopTheAttack()
    {
        fire_sound.SetActive(false);
        StopCoroutine(autofire_main);
        StopCoroutine(autofire_lmg);
        StopCoroutine(autofire_rocket);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet"))
        {
            hit_sound.Play();
            hp -= 8f * dmg_coefficient;
            collision.gameObject.SetActive(false);
            //-------------------------------------------
            //맞으면 킬포인트 한 단계씩 차감
            killpoint -= 10;
            if (killpoint < 0)
            {
                killpoint = 0;
            }
        }
        else if (collision.CompareTag("EnemyBeam"))
        {
            hit_sound.Play();
            hp -= 16f * dmg_coefficient;
            collision.enabled = false;
            //-------------------------------------------
            //맞으면 킬포인트 한 단계씩 차감
            killpoint -= 10;
            if (killpoint < 0)
            {
                killpoint = 0;
            }
        }
        else if (collision.CompareTag("EnemyBody"))
        {
            hit_sound.Play();
            hp -= 12f * dmg_coefficient;
            collision.SendMessage("Death", SendMessageOptions.DontRequireReceiver);
            //-------------------------------------------
            //맞으면 킬포인트 한 단계씩 차감
            killpoint -= 10;
            if (killpoint < 0)
            {
                killpoint = 0;
            }
        }
        else if (collision.CompareTag("CloneBullet"))
        {
            hit_sound.Play();
            hp -= 8f * dmg_coefficient;
            Destroy(collision.gameObject);
            //-------------------------------------------
            //맞으면 킬포인트 한 단계씩 차감
            killpoint -= 10;
            if (killpoint < 0)
            {
                killpoint = 0;
            }
        }
        else if (collision.CompareTag("Boss2Missile"))
        {
            hit_sound.Play();
            hp -= 16f * dmg_coefficient;
            collision.SendMessage("DamageMotion", SendMessageOptions.DontRequireReceiver);
            //-------------------------------------------
            //맞으면 킬포인트 한 단계씩 차감
            killpoint -= 10;
            if (killpoint < 0)
            {
                killpoint = 0;
            }
        }
        else if (collision.CompareTag("Scrap"))
        {
            scrap += 1;
            Destroy(collision.gameObject);
        }

        hp_img.fillAmount = hp / maxhp;

        if (hp <= 0)
        {
            Death();
        }
        //공격받았을때 반응 추가
    }

    public void Death()
    {
        explosion_sound.Play();
        StopCoroutine(autofire_main);
        StopCoroutine(autofire_lmg);
        StopCoroutine(autofire_rocket);
        anim.SetTrigger("Death");
        rb2d.bodyType = RigidbodyType2D.Static;
        collider2d.enabled = false;
        GetComponent<PlayerInform>().enabled = false;
        isDead = true;
        barrier.SetActive(false);
        lmg_drone[0].SetActive(false);
        lmg_drone[1].SetActive(false);
        fire_sound.SetActive(false);
        overPanel.SetActive(true);
    }

    public void ComboKill()
    {
        if (killpoint <= 3)
        {
            combo[5].SetActive(false);
            combo[4].SetActive(false);
            combo[3].SetActive(false);
            combo[2].SetActive(false);
            combo[1].SetActive(false);
            combo[0].SetActive(true);

            direct_dmg = dmg * 1;
            direct_lmg_dmg = lmg_dmg * 1;
            direct_rck_dmg = rocket_dmg * 1;
        }

        else if (killpoint <= 6)
        {
            combo[5].SetActive(false);
            combo[4].SetActive(false);
            combo[3].SetActive(false);
            combo[2].SetActive(false);
            combo[1].SetActive(true);
            combo[0].SetActive(false);
            direct_dmg = dmg * 1.2f;
            direct_lmg_dmg = lmg_dmg * 1.2f;
            direct_rck_dmg = rocket_dmg * 1.2f;
        }

        else if (killpoint <= 9)
        {
            combo[5].SetActive(false);
            combo[4].SetActive(false);
            combo[3].SetActive(false);
            combo[2].SetActive(true);
            combo[1].SetActive(false);
            combo[0].SetActive(false);

            direct_dmg = dmg * 1.4f;
            direct_lmg_dmg = lmg_dmg * 1.4f;
            direct_rck_dmg = rocket_dmg * 1.4f;
        }

        else if (killpoint <= 12)
        {
            combo[5].SetActive(false);
            combo[4].SetActive(false);
            combo[3].SetActive(true);
            combo[2].SetActive(false);
            combo[1].SetActive(false);
            combo[0].SetActive(false);

            direct_dmg = dmg * 1.6f;
            direct_lmg_dmg = lmg_dmg * 1.6f;
            direct_rck_dmg = rocket_dmg * 1.6f;
        }

        else if (killpoint <= 15)
        {
            combo[5].SetActive(false);
            combo[4].SetActive(true);
            combo[3].SetActive(false);
            combo[2].SetActive(false);
            combo[1].SetActive(false);
            combo[0].SetActive(false);

            direct_dmg = dmg * 1.8f;
            direct_lmg_dmg = lmg_dmg * 1.8f;
            direct_rck_dmg = rocket_dmg * 1.8f;
        }

        else
        {
            combo[5].SetActive(true);
            combo[4].SetActive(false);
            combo[3].SetActive(false);
            combo[2].SetActive(false);
            combo[1].SetActive(false);
            combo[0].SetActive(false);
            
            direct_dmg = dmg * 2.0f;
            direct_lmg_dmg = lmg_dmg * 2.0f;
            direct_rck_dmg = rocket_dmg * 2.0f;
        }
    }
}
