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
        if (ammo.Target)
        {
            rb.velocity = (ammo.Target.position - transform.position) * ammo.Speed;
        }
        else Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyView>().TakeDamage(ammo.Damage);
            if (ammo.Type != AmmoTypes.Standart)
            {
                float maxDis = ammo.EffectRadius;
                List<EnemyView> lenemy = new List<EnemyView>();
                for(int i = GameController.singleton.L_Enemy.Count; i > 0; i--)
                {
                    if (GameController.singleton.L_Enemy[i] != null)
                    {


                        EnemyView cur = GameController.singleton.L_Enemy[i];
                        if (cur != null && Vector3.Distance(cur.transform.position, transform.position) < maxDis)
                            lenemy.Add(cur);
                    }
                }
                switch (ammo.Type)
                {
                    case AmmoTypes.Fire:
                        {

                        }break;
                    case AmmoTypes.Ice:
                        {

                        }break;
                    case AmmoTypes.BlackHole:
                        {
                            for(int i = lenemy.Count; i > 0; i--)
                            {
                                if(lenemy[i] != null)
                                {
                                    lenemy[i].ienumeratorDamage(ammo.EffectTime, 0.05f, ammo.DamageMultiplayer);
                                    lenemy[i].GetComponent<MeshRenderer>().sharedMaterial.color = Color.red;
                                }
                            }
                        }break;
                }
            }
            Destroy(gameObject);
        }
    }
}
