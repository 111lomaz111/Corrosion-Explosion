using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Speed = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.up * Speed;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        rb.velocity = transform.up * Speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject onColisionObject = other.gameObject;
        switch (onColisionObject.tag)
        {
            case TagsConstants.EnemyTag:
                {
                    AudioManager.Instance.PlayOneShot(AudioManager.Instance.enemySound, transform.position);
                    onColisionObject.GetComponent<Animator>().Play("EnemyDeathAnimation");
                    Destroy(gameObject);
                    break;
                }
            case TagsConstants.TeethTag:
                {
                    Destroy(gameObject);
                    break;
                }
            default: break;
        }
    }
}