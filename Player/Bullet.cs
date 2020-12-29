using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void DamageMotion()
    {
        //피탄 모션, 사운드 추가
        animator.SetTrigger("Damage");
        rb2d.simulated = false;
        Invoke("ObjectFalse", 0.5f);
    }

    public void ObjectFalse()
    {
        gameObject.SetActive(false);
        rb2d.simulated = true;
        animator.SetTrigger("Idle");

    }
}
