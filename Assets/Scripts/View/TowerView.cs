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

    void Start()
    {
        GameController.singleton.tower = this;
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
                        timeCalculate = AttackSpeed;
                        //closets.TakeDamage(Damage);
                            
                            //Допилить весы для рандома типа пуль. Пока что спавниться только 1 тип
                        var obj = Instantiate(AmmoPrefabs[0] ,AmmoSpawnPoint.transform.position,Quaternion.identity);
                            
                            //Временно пока нету весов и инициализации пули при создании
                        obj.GetComponent<AmmoViewBase>().ammo = new Ammo()
                        {
                            Target = closets.transform,
                            Damage = tower.Damage,
                            Speed = 10,
                            DamageMultiplier = 1.5f,
                            EffectDamage = 2,
                            EffectRadius = 5,
                            EffectTime = 2
                        }; //Временно пока нету весов и инициализации пули при создании

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
        }
    }
    
    // void OnDrawGizmos()
    // {
    //     // Draw a yellow sphere at the transform's position
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, 2);
    // }
    
}
