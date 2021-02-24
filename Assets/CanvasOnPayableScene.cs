using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasOnPayableScene : MonoBehaviour
{
    public Transform pnWindowFail;
    public Transform[] objects;

    private void Start()
    {
        GameController.singleton.canvasOnPayableScene = this;
    }

    private void OnEnable()
    {
        pnWindowFail.gameObject.SetActive(false);
    }

    public void Activate(string nameElement, bool hide)
    {
        if (objects != null)
        {
            int len = objects.Length;
            for (int i = 0; i < len; i++)
            {
                if(objects[i].name == nameElement)
                    objects[i].gameObject.SetActive(hide);
            }
        }
    }
}
