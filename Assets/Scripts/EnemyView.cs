using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField]private Transform target;

    public SOEnemy SOEnemy;
    private Enemy enemy;
    private Rigidbody Rb;
    public float Damage => enemy.Damage;
    
    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    public void Move()
    {
        if (target != null)
        {
            if (Time.timeScale <= 0.1f) return;
            
            Vector3 dir = (target.position - transform.position).normalized;
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
        Debug.Log("Enemy HP= " + enemy.HP);
    }
    public IEnumerator enumeratorDamage(float time, float interval, float damage)
    {
        while (time > 0)
        {
            time -= interval;
            TakeDamage(damage);
            yield return new WaitForSeconds(interval);
        }
    }
}
