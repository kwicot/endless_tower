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
            // orange
            var element = Instantiate(GetPrefab("FabricTimer"), content);
            var ft = element.GetComponent<FabricTimer>();
            if (ft) ft.Init(GameController.singleton.factory.OrangeTimer);
            
            // red
            element = Instantiate(GetPrefab("FabricTimer"), content);
            ft = element.GetComponent<FabricTimer>();
            if (ft) ft.Init(GameController.singleton.factory.RedTimer);
            
            // blue
            element = Instantiate(GetPrefab("FabricTimer"), content);
            ft = element.GetComponent<FabricTimer>();
            if (ft) ft.Init(GameController.singleton.factory.BlueTimer);
            
            // green
            element = Instantiate(GetPrefab("FabricTimer"), content);
            ft = element.GetComponent<FabricTimer>();
            if (ft) ft.Init(GameController.singleton.factory.GreenTimer);
        }
    }

    private void OnDisable()
    {
        // content destroy children
        Utils.DestroyChild(content);
    }
}
