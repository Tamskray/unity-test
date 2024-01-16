using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] public float speed = 5F;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage;
    [SerializeField] private bool enemyBullet;

    [SerializeField] private LayerMask whatIsSolid;
    [SerializeField] private GameObject bulletEffect;

    private void Start()
    {
        Invoke("DestroyBullet", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 0, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<EnemyBehaviour>().OnTakeDamage(damage);
            }

            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                hitInfo.collider.GetComponent<PlayerHealth>().OnChangeHealth(-damage);
            }

            DestroyBullet();
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void DestroyBullet()
    {
        Instantiate(bulletEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
