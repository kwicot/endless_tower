using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    public Tower tower;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        GameController.singleton._Tower = this;
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
