using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class Ammo
    {
        public Transform Target;
        public float Damage;
        public float Speed;
        public float DamageMultiplier;
        public float EffectTime;
        public float EffectRadius;
        public float EffectDamage;
    }
}
