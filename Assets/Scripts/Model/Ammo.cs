using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ammo
{
    public AmmoTypes Type;
    public Transform Target;
    public float Damage;
    public float Speed;
    public float DamageMultiplayer;
    public float EffectTime;
    public float EffectRadius;
}
public enum AmmoTypes
{
    Standart,
    Fire,
    Ice,
    BlackHole
}
