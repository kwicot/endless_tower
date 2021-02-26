using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask RaycastEnemyLayer;
    private Camera camera;
    

    private void Start()
    {
        camera = Camera.main;
    }

    private Touch touch;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                PlayerTouch(touch.position);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlayerTouch(Input.mousePosition);
        }
    }

    void PlayerTouch(Vector3 pos)
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.ScreenPointToRay(pos), out hit, 10000, RaycastEnemyLayer))
        {
            hit.transform.GetComponent<EnemyView>().TakeDamage(100);
        }
    }
}
