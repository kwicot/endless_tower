using UnityEngine;

public class AmmoViewIce : AmmoViewBase
{ 

    protected override void EnemyHit(EnemyView enemyView)
    {
        Debug.Log("IceLogic");
        enemyView.TakeDamage(ammo.Damage * ammo.DamageMultiplier);
        int countNull = 0;
        foreach (var enemy in enemyViews)
        {
            if (enemy == null)
            {
                countNull++;
                continue;
            }
            
            if (Vector3.Distance(enemy.transform.position, transform.position) < ammo.EffectRadius)
                enemy.StartIceEffect(ammo.EffectTime, ammo.EffectDamage);
        }
        if (countNull > 0)
            Debug.LogError($"{GetType().Name} - EnemyHit: count={countNull}");
        
        Destroy(gameObject);
    }
}
