using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -20f)
            Destroy(gameObject);

        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
    }
}
