using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Ammo",fileName ="SOAmmo_")]
public class SOAmmo : ScriptableObject
{
    public GameObject AmmoPrefab;
    [HideInInspector]
    public float Chance;
}
