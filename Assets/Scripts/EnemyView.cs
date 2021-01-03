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
            HP = SOEnemy.HP, 
            Damage = SOEnemy.Damage, 
            Speed = SOEnemy.Speed
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //if (Time.timeScale <= 0.1f) return;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, 0.01f * enemy.Speed * Time.timeScale);
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
            Destroy(gameObject);
            GameController.singleton.EnemyKilled(enemy.Loot);
        }
        Debug.Log("Enemy HP= " + enemy.HP);
    }
}
