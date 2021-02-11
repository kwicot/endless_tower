using UnityEngine;
    public class AmmoViewFire : AmmoViewBase
    {
        protected override void EnemyHit(EnemyView enemyView)
        {
            enemyView.TakeDamage(ammo.Damage * ammo.DamageMultiplier);
            foreach (var enemy in enemyViews)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < ammo.EffectRadius)
                    enemy.StartFireEffect(ammo.EffectTime, ammo.EffectDamage);
            }
            Destroy(gameObject);
        }
    }
