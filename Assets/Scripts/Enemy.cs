using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float _speed;
    float _damage;
    float _health;
    Rigidbody _rb;
    Vector3 _direct;
    Vector3 _target;
    public enum Type
    {
        Simple,
        Fast,
        Fat,
        MiniBoss,
        Boss
    };
    public Type _type;
    void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _direct = _target - transform.position;
        _direct = (_direct.normalized * _speed) - _rb.velocity;
        _rb.AddForce(_direct);
    }
    public void Init(Vector3 target, float damage, float health)
    {
        _damage = damage;
        _health = health;
        _target = target;
    }
}
