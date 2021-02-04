using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ammo
{
    public AmmoTypes Type;
    public Transform target;
    public float speed;
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
