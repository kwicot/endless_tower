using System;
using System.Collections.Generic;
using UnityEngine;
using Model;
using Random = System.Random;

public class AmmoViewBase : MonoBehaviour
{
    public Ammo ammo = new Ammo();
    protected HashSet<EnemyView> enemyViews = new HashSet<EnemyView>();
    protected Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody>();
        foreach (var enemyView in GameController.singleton.L_Enemy)
        {
            enemyViews.Add(enemyView);
        }

    }
    void Update()
    {
        if (ammo.Target)
        {
            rb.velocity = (ammo.Target.position - transform.position) * ammo.Speed;
        }
        else Destroy(gameObject);
    }

    protected virtual void EnemyHit(EnemyView enemyView)
    {
        enemyView.TakeDamage(ammo.Damage);
        if (ammo.BounceChance > 0)
        {
            if (UnityEngine.Random.Range(0,100) <= ammo.BounceChance)
            {
                //TODO Доделать отскок, рикошет
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject.GetComponent<EnemyView>();
        if(obj != null)
        EnemyHit(obj);
    }

    private void OnDestroy()
    {
        enemyViews.Clear();
    }
}
