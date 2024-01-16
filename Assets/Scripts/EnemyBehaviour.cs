using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [SerializeField] private bool isRange;

    [SerializeField] private float startTimeBetweenAttack;
    [SerializeField] private float startStopTime;
   
    [SerializeField] private GameObject attackEffect;
    [SerializeField] private GameObject deathEffect;

    private float _normalSpeed;
    private float _timeBetweenAttack;
    private float _stopTime;

    private bool _isPlayerInRange = false;

    private PlayerController _player;
    private PlayerHealth _playerHealth;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _normalSpeed = speed;
    }

    private void Update()
    {
        if (_stopTime <= 0)
        {
            speed = _normalSpeed;
        }
        else
        {
            speed = 0;
            _stopTime -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (_player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);


        if(_timeBetweenAttack <= 0 && !isRange)
        {
            if(_isPlayerInRange)
            {
                _animator.SetTrigger("attack");
                _stopTime = startStopTime;
                _timeBetweenAttack = startTimeBetweenAttack;
            }
        }
        else
        {
            _timeBetweenAttack -= Time.deltaTime;
        }
    }

    public void OnTakeDamage(int damage)
    {
        _stopTime = startStopTime;
        health -= damage;
        _animator.Play("Hurt");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerInRange = false;
    }

    private void OnEnemyAttack()
    {
        // Debug.Log("Enemy had attacked");
        _timeBetweenAttack = startTimeBetweenAttack;
        _playerHealth.OnChangeHealth(-damage);
        Instantiate(attackEffect, _player.transform.position, Quaternion.identity);
    }
}
