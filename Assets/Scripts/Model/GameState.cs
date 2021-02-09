using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.LowLevel;

[Serializable]
public class GameState
{
    /// <summary>
    /// локальные значения параметров улучшений
    /// </summary>
    [NonSerialized]public UIParam local = new UIParam();
    /// <summary>
    /// глобальные значения параметров улучшений (будут браться из сейва)
    /// </summary>
    public UIParam global = new UIParam();
    /// <summary>
    /// текущие значения параметров улучшений (локальные + сумарные)
    /// </summary>
    [NonSerialized]public UFParam current = new UFParam();
    [NonSerialized]public UFParam currentCost = new UFParam();

    public Dictionary<string, float> Money = new Dictionary<string, float>();
    public List<FactoryTimer> FactoryTimers = new List<FactoryTimer>();

    public void Init()
    {
        if (local == null)
        {
            local = new UIParam();
            local.Init();
        }

        if (current == null)
        {
            current = new UFParam();
            current.Init();
        }

        if (currentCost == null)
        {
            currentCost = new UFParam();
            currentCost.Init();
        }

        if (global == null)
        {
            global = new UIParam();
        }
        global.Init();
        
        if (Money == null)
            Money = new Dictionary<string, float>()
            {
                {"White",0 },
                {"Orange",100 },
                {"Red",0 },
                {"Green",0 },
                {"Blue", 0 },
                {"Crystal", 0 }
            };
        
        
    }
}