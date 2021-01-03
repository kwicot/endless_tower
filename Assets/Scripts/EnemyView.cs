using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField]private Transform target;

    public SOEnemy SOEnemy;
    private Enemy enemy;
    public float Damage => enemy.Damage;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameController.singleton._Tower.transform;
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
            if (Time.timeScale <= 0.1f) return;

            transform.position = Vector3.MoveTowards(transform.position, target.position, 0.1f * enemy.Speed);
        }
    }
}
