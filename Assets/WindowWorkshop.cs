using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowWorkshop : UIElement
{
    public Transform content;
    private void OnEnable()
    {
        // content destroy children
        Utils.DestroyChild(content);

        if (HasPrefab("UpgradeElement"))
        {
            foreach (var element in GameController.global.P)
            {
                var elementGo = Instantiate(GetPrefab("UpgradeElement"), content);
                UpgradeElement UElelment = elementGo.GetComponent<UpgradeElement>();
                if (UElelment != null)
                {
                    UElelment.Init(element.Key, GameController.global);
                }
            }
        }
    }

    private void OnDisable()
    {
        // content destroy children
        Utils.DestroyChild(content);
    }
}

public class UIElement : MonoBehaviour
{
    public GameObject[] prefabs;
    
    protected bool HasPrefab(string nameElement)
    {
        if (prefabs != null)
            foreach (var p in prefabs)
            {
                if (p == null) continue;
                if (p.name == nameElement)
                    return true;
            }

        return false;
    }
    
    protected GameObject GetPrefab(string nameElement)
    {
        if (prefabs != null)
            foreach (var p in prefabs)
            {
                if (p == null) continue;
                if (p.name == nameElement)
                    return p;
            }

        return null;
    }
}
    
