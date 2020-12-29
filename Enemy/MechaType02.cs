using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaType02 : MonoBehaviour
{
    public GameObject player;
    public float xSpeed, ySpeed;
    Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > player.transform.position.x)
        {
            if (player.transform.position.y > transform.position.y)
                transform.Translate(-xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0f);
            else
                transform.Translate(-xSpeed * Time.deltaTime, -ySpeed * Time.deltaTime, 0f);
        }
        else
        {
            transform.Translate(-xSpeed * Time.deltaTime, 0f, 0f);
        }

        if (transform.position.x <= -20f)
            Destroy(gameObject);
    }

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<MechaType02>().enabled = false;
        //폭파모션과 폭파사운드 추가
        animator.SetTrigger("Explosion");
        Invoke("DestroyObj", 2f);
    }

    public void DestroyObj()
    {
        //enemyBullet.SetActive(false);
        gameObject.SetActive(false);
    }
}
