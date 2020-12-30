using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    public float AttackRange { get; set; }
    public float AttackSpeed { get; set; }
    public float Damage { get; set; }
    public float CriticalDamageChance { get; set; }
    public float CriticalDamageMultiplier { get; set; }
    public float MultiFireChance { get; set; }
    public int MultiFireTargets { get; set; }
    public float RapidFireChance { get; set; }
    public float RapidFireTime { get; set; }
    public float BounceChance { get; set; }
    public float BounceRangeMax { get; set; }
    public int BounceCountMax { get; set; }
    public float BlackHoleChance { get; set; }
    public float BlackHoleTime { get; set; }


    public float HP { get; set; }
    public float Regeneration { get; set; }
    public float BlockChance { get; set; }
    public float BlockDamage { get; set; }
    public float SlowAuraRange { get; set; }
    public float SlowAuraPerfomance { get; set; }
    public float DamageAuraRange { get; set; }
    public float DamageAuraPerfomance { get; set; }
    public float DamageIncreaseAuraRange { get; set; }
    public float DamageIncreaseAuraPerfomance { get; set; }






    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
