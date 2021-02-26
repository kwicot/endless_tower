using UnityEngine;
    public class AmmoViewFire : AmmoViewBase
    {
        void Start()
        {
            ammo.EffectRadius = GameController.GameState.current.Get("FireAmmoEffectRadius");
            ammo.EffectTime = 4f;
            ammo.EffectDamage = GameController.GameState.current.Get("FireAmmoDamageEffect") / 4;
            ammo.DamageMultiplier = 1.5f;
        }
        protected override void EnemyHit(EnemyView enemyView)
        {
            //TODO Разбросс врагов как после взрыва
            enemyView.TakeDamage(ammo.Damage * ammo.DamageMultiplier);
            foreach (var enemy in enemyViews)
            {
                if (Vector3.Distance(enemy.transform.position, transform.position) < ammo.EffectRadius)
                    enemy.StartFireEffect(ammo.EffectTime, ammo.EffectDamage);
            }
            Destroy(gameObject);
        }
    }
