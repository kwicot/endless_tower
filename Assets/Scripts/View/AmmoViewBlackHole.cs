using UnityEngine;

public class AmmoViewBlackHole : AmmoViewBase
{ 

    protected override void EnemyHit(EnemyView enemyView)
    {
        enemyView.TakeDamage(ammo.Damage * ammo.DamageMultiplier);
        foreach (var enemy in enemyViews)
        {
            //if (Vector3.Distance(enemy.transform.position, transform.position) < ammo.EffectRadius)
                //enemy.StartIceEffect(ammo.EffectTime, ammo.EffectDamage);
        }
        Destroy(gameObject);
    }
}
