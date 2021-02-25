using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy",fileName = "SOEnemy_")]
public class SOEnemy : ScriptableObject
{
    public float HP;
    public float Speed;
    public float Damage;
    public Dictionary<string, float> Reward; 
    public Model.EnemyType Type;
}