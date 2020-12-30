using System.Collections.Generic;

[System.Serializable]
public class Enemy
{
    public float HP { get; set; }
    public float Speed { get; set; }
    public float Damage { get; set; }
    public EnemyType Type { get; set; }
    public Dictionary<int, int> Loot { get; set; }

    public Enemy() { }
}

public enum EnemyType
{
    Default = 0,
    
}
