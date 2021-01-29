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
            foreach (var element in GameController.GameState.global.param)
            {
                var elementGo = Instantiate(GetPrefab("UpgradeElement"), content);
                UpgradeElement UElelment = elementGo.GetComponent<UpgradeElement>();
                if (UElelment != null)
                {
                    UElelment.Init(element.Key, GameController.GameState.global);
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

    
