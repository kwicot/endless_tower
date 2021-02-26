using System;
using System.Collections.Generic;
using UnityEngine;
using Model;

public class TowerView : MonoBehaviour
{
    public List<GameObject> AmmoPrefabs = new List<GameObject>();
    public GameObject AmmoSpawnPoint;
    public Tower tower;
    private List<EnemyView> L_Enemy => GameController.singleton.L_Enemy;

    private static float HPMax => GameController.GameState.current.Get("HP");
    private float Regeneration => GameController.GameState.current.Get("Regeneration");
    private float Damage => GameController.GameState.current.Get("Damage");
    private float AttackRange => GameController.GameState.current.Get("AttackRange");
    private float AttackSpeed => GameController.GameState.current.Get("AttackSpeed");
    private float ExplosiveChance => GameController.GameState.current.Get("ExplosiveChance");
    private float DamageAura => GameController.GameState.current.Get("DamageAura");
    private float DamageAuraEffect => GameController.GameState.current.Get("DamageAuraEffect");
    private float DefenseAuraEffect => GameController.GameState.current.Get("DefenseAuraEffect");
    private float DefenseAura => GameController.GameState.current.Get("DefenseAura");
    private float DefenseAuraRadius => GameController.GameState.current.Get("DefenseAuraRadius");
    private float BlockChance => GameController.GameState.current.Get("BlockChance");
    private float RapidFireTime => GameController.GameState.current.Get("RapidFireTime");
    private float RapidFireChance => GameController.GameState.current.Get("RapidFireChance");
    
    private float CriticalChance => GameController.GameState.current.Get("CriticalChance");
    //private float  => GameController.GameState.current.Get("");
    //private float  => GameController.GameState.current.Get("");
    //private float  => GameController.GameState.current.Get("");
    //private float  => GameController.GameState.current.Get("");


    private Dictionary<int, int> ammoWeight = new Dictionary<int, int>
    {
        {0, (int) GameController.GameState.current.Get("IceAmmoChance")},
        {1, (int) GameController.GameState.current.Get("FireAmmoChance")},
        {2, (100
             - (int) GameController.GameState.current.Get("IceAmmoChance")
             - (int) GameController.GameState.current.Get("FireAmmoChance"))
        }
    };
    void Start()
    {
        GameController.singleton.tower = this;
        RecalcWeight();
    }
    private float timeCalculate = 0.1f;
    private void Update()
    {
        if (tower.HP < HPMax)
        {
            tower.HP += Regeneration * Time.deltaTime;
            if (tower.HP > HPMax) 
                tower.HP = HPMax;
        }
    }
    public void AttackUpdate()
    {
        if (GameController.singleton.state == State.Game)
        {
            timeCalculate -= Time.deltaTime;
            if (timeCalculate <= 0) //Tower logic
            {
                EnemyView closets = null;
                int count = L_Enemy.Count;
                if (count > 0)
                {
                    float closetsDistance = float.MaxValue;
                    float currentDistance = closetsDistance;
                    for (int i = 0; i < count; i++)
                    {
                        // дистанция до текущего врага
                        currentDistance = Vector3.Distance(transform.position, L_Enemy[i].transform.position);
                        if (currentDistance < closetsDistance)
                        {
                            closets = L_Enemy[i];
                            // наименьшая дистанция
                            closetsDistance = currentDistance;
                        }
                    }

                    if (closetsDistance < AttackRange && closets)
                    {
                        // атака у нас производится тут. поэтому и сброс таймера тоже тут
                        timeCalculate = 1 / AttackSpeed;
                        //closets.TakeDamage(Damage);
                        
                        var randomAmmo = Utils.GetRandomOnWeight(ammoWeight);
                        var obj = Instantiate(AmmoPrefabs[randomAmmo], AmmoSpawnPoint.transform.position, Quaternion.identity);
                        Debug.Log(obj);
                        //Инициализация основных характеристик
                        //Персональные характеристики инициализируються при в скрипте каждого типа
                        var amm = obj.GetComponent<AmmoViewBase>().ammo;
                        amm.Target = closets.transform;
                        amm.Damage = Damage;
                        amm.Speed = 10;
                        amm.BounceChance = GameController.GameState.current.Get("BounceChance");
                        amm.BounceCount = (int) GameController.GameState.current.Get("BounceCount");
                        
                        //TODO сделать обновление весов после покупки улучшения снарядов
                        RecalcWeight();

                    }
                }
            } //Tower logic end
        }
    }


    public void TakeDamage(float damage)
    {
        tower.HP -= damage;
        
        //Debug.Log($"tower HP={tower.HP}");

        if (tower.HP <= 0f)
        {
            Time.timeScale = 0f;
            GameController.singleton.EndRound();
        }
    }

    void RecalcWeight()
    {
        ammoWeight.Clear();
        ammoWeight = new Dictionary<int, int>
        {
            {0, (int) GameController.GameState.current.Get("IceAmmoChance")},
            {1, (int) GameController.GameState.current.Get("FireAmmoChance")},
            {2, (100
                 - (int) GameController.GameState.current.Get("IceAmmoChance")
                 - (int) GameController.GameState.current.Get("FireAmmoChance"))
            }
        };
    }
    
    // void OnDrawGizmos()
    // {
    //     // Draw a yellow sphere at the transform's position
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, 2);
    // }
    
}
