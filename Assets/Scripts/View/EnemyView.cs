using System;
using Model;
using UnityEngine;
using System.Collections;

public class EnemyView : MonoBehaviour
{
    [SerializeField]private Transform target;

    public SOEnemy SOEnemy;
    private Enemy enemy;
    private Rigidbody Rb;
    public float Damage => enemy.Damage;
    
    private void Start()
    {
        target = GameController.singleton.tower.transform;
        Rb = GetComponent<Rigidbody>();
    }
    public void Init()
    {
        enemy = new Enemy
        {
            HP = SOEnemy.HP * GameController.singleton.SettingWave.EnemyHPCoeff,
            Damage = SOEnemy.Damage * GameController.singleton.SettingWave.EnemyDamageCoeff,
            Speed = SOEnemy.Speed,
            Type = SOEnemy.Type,
        };
    }
    public void Move()
    {
        if (target)
        {
            if (Time.timeScale <= 0.1f) return;
            
            var dir = (target.position - transform.position).normalized;
            dir.y = 0;
            Rb.velocity = dir * enemy.Speed * 2;
        }
    }
    public void TakeDamage(float damage)
    {
        enemy.HP -= damage;
        if(enemy.HP <= 0)
        {
            GameController.singleton.EnemyKilled(this);
            Destroy(gameObject);
        }
        //Debug.Log("Enemy HP= " + enemy.HP);
    }

    
    public void StartIceEffect(float time,float damage)
    {
        StartCoroutine(IceEffect(time, damage));
    }

    public void StartFireEffect(float time,float damage)
    {
        StartCoroutine(FireEffect(time, damage));
    }
    
    IEnumerator IceEffect(float time, float damage)
    {
        Debug.Log("Enemy ice logic start");
        float startSpeed = enemy.Speed;
        enemy.Speed *= 0.7f;
        while (time > 0)
        {
            TakeDamage(damage);
            time -= Time.deltaTime;
            time = ControlCoroutine(time);
            Debug.Log("Enemy ice logic");
            yield return new WaitForFixedUpdate();
        }
        enemy.Speed = startSpeed;
    }

    IEnumerator FireEffect(float time, float damage)
    {
        while (time > 0)
        {
            TakeDamage(damage);
            time -= Time.deltaTime;
            time = ControlCoroutine(time);
            yield return new WaitForFixedUpdate();
        }
    }

    private float ControlCoroutine(float time)
    {
        return (enemy.HP <= 0) ? 0 : time;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        target = null;
    }
}
