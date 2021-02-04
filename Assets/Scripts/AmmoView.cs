using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoView : MonoBehaviour
{
    public Ammo ammo;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb) rb = gameObject.AddComponent<Rigidbody>();
    }
    void Update()
    {
        if (ammo.target)
        {
            rb.velocity = ammo.target.position - transform.position + ammo.speed;
        }
    }
}
