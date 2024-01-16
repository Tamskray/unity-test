using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GunType gunType;
    private enum GunType {Player, Enemy};

    [SerializeField] private float offset;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private float startTimeBetweenShots;

    private float _rotation;
    private float _timeBetweenShots;
    private PlayerController _player;
    private Animator _animator;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(gunType == GunType.Player)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else if(gunType == GunType.Enemy)
        {
            Vector3 difference = _player.transform.position - transform.position;
            _rotation = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        shotPoint.rotation = Quaternion.Euler(0f, 0f, _rotation + offset);

        if (_timeBetweenShots <= 0)
        {
            if (Input.GetMouseButton(0) || gunType == GunType.Enemy)
            {
                Shoot();
                _animator.SetTrigger("Attack");
            }
        }
        else
        {
            _timeBetweenShots -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        _timeBetweenShots = startTimeBetweenShots;
    }
}
