using System.Collections.Generic;

namespace Model
{
    [System.Serializable]
    public class Enemy
    {
        public float HP;
        public float Speed;
        public float Damage;
        public EnemyType Type;
        public Dictionary<int, int> Loot;
    }

    public enum EnemyType
    {
        Default = 0,

    }
}