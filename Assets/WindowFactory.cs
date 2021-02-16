using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowFactory : UIElement
{
    public Transform content;
    private void OnEnable()
    {
        // content destroy children
        Utils.DestroyChild(content);

        // create prefab
        if (HasPrefab("FabricTimer"))
        {
            foreach (var name in GameController.listNameTimer)
            {
                var element = Instantiate(GetPrefab("FabricTimer"), content);
                var ft = element.GetComponent<FabricTimer>();
                
                var timer = GameController.singleton.GetTimerByName(name);
                if (timer != null)
                {
                    
                    if (timer.Resource == "Orange")
                    {
                        if (ft) ft.Init(GameController.singleton.factory.OrangeTimer);
                    }
                    else if (timer.Resource == "Red")
                    {
                        if (ft) ft.Init(GameController.singleton.factory.RedTimer);
                    }
                    else if (timer.Resource == "Blue")
                    {
                        if (ft) ft.Init(GameController.singleton.factory.BlueTimer);
                    }
                    else if (timer.Resource == "Green")
                    {
                        if (ft) ft.Init(GameController.singleton.factory.GreenTimer);
                    }
                }
                else
                {
                    if (ft)
                    {
                        ft.InitEmpty(name);
                        ft.actionEnable -= OnEnable;
                        ft.actionEnable += OnEnable;
                    }
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
