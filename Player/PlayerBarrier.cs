using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarrier : MonoBehaviour
{
    public float torque_power;

    void Start()
    {
        GetComponent<Rigidbody2D>().AddTorque(torque_power, ForceMode2D.Impulse);
    }

    void Update()
    {
        transform.localPosition = Vector3.zero;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet"))
        {
            collision.gameObject.SetActive(false);
        }

        else if (collision.CompareTag("CloneBullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
