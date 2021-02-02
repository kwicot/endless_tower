using UnityEngine;

[CreateAssetMenu(fileName = "SOEnemy_", menuName = "SOEnemy", order = 0)]
public class SOEnemy : ScriptableObject
{
    public float HP;
    public float Speed;
    public float Damage;
    public Model.EnemyType Type;
}