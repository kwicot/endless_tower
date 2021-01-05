using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    public Tower tower;
    private List<EnemyView> L_Enemy => GameController.singleton.L_Enemy;

    void Start()
    {
        GameController.singleton.tower = this;
    }
    private float timeCalculate = 0.1f;
    private void Update()
    {
        if(tower.HP < tower.HPMax)
        {
            tower.HP += tower.Regeneration * Time.deltaTime;
            if (tower.HP > tower.HPMax) tower.HP = tower.HPMax;
        }
        AttackUpdate();
    }
    void AttackUpdate()
    {
        if (GameController.singleton.GameState == GameState.Game)
        {
            timeCalculate -= Time.deltaTime;
            if (timeCalculate <= 0) //Tower logic
            {
                EnemyView closets = null;
                // уберем пустые нулевые (правда откуда они?)
                int count = L_Enemy.Count - 1;
                for (int i = count; i >= 0; i--)
                {
                    if (L_Enemy[i] == null) 
                        L_Enemy.RemoveAt(i);
                }

                count = L_Enemy.Count;
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

                    if (closetsDistance < tower.AttackRange)
                    {
                        closets.TakeDamage(tower.Damage);
                        // атака у нас производится тут. поэтому и сброс таймера тоже тут
                        timeCalculate = tower.AttackSpeed;
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
    
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
    
}
