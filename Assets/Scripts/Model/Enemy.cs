namespace Model
{
    [System.Serializable]
    public class Enemy
    {
        public float HP;
        public float Speed;
        public float Damage;
        public EnemyType Type;
    }

    public enum EnemyType
    {
        Default = 0,
        Fast,
        Fat,
        MiniBoss,
        Boss

    }
}