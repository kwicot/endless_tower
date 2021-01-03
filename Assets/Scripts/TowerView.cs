using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    public Tower tower;
    private List<EnemyView> L_Enemy => GameController.singleton.L_Enemy;

    void Start()
    {
        GameController.singleton._Tower = this;
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
                timeCalculate = tower.AttackSpeed;
                var count = L_Enemy.Count;
                if (count > 0)
                {
                    var closets = L_Enemy[0];
                    if (count > 1)
                    {
                        for (int i = 1; i < L_Enemy.Count; i++)
                        {
                            if (L_Enemy[i] != null)
                            {
                                if (closets != null)
                                {
                                    if (Vector3.Distance(transform.position, L_Enemy[i].transform.position) <
                                        Vector3.Distance(closets.transform.position, transform.position))
                                        closets = L_Enemy[i];
                                }
                                else closets = L_Enemy[i];
                            }
                            else
                            {
                                L_Enemy.RemoveAt(i);
                                i--;
                            }

                        }
                    }
                    var distance = Vector3.Distance(closets.gameObject.transform.position, transform.position);
                    if (distance < tower.AttackRange)
                    {
                        closets.TakeDamage(tower.Damage);
                    }
                }
            } //Tower logic end
        }
    }


    public void TakeDamage(float damage)
    {
        tower.HP -= damage;
        
        Debug.Log($"tower HP={tower.HP}");

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
