using System;
using System.Collections.Generic;

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

    public Dictionary<string, float> Money = new Dictionary<string, float>();
    //public List<FactoryTimer> FactoryTimers = new List<FactoryTimer>();
    public Fabric Fabric = new Fabric();

    public void Init()
    {
        if (local == null)
        {
            local = new UIParam();
            local.Init();
            local.TypeParam = TypeParam.Local;
        }

        if (current == null)
        {
            current = new UFParam();
            current.Init();
        }

        if (global == null)
        {
            global = new UIParam();
            local.TypeParam = TypeParam.Global;
        }
        global.Init();
        
        if (Money == null || Money.Count == 0)
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

[System.Serializable]
public class Fabric
{
    public Dictionary<string, int> TimerLevels = new Dictionary<string, int>();
    public List<FactoryTimer> FactoryTimers = new List<FactoryTimer>();

    public void InitDefault()
    {
        if (FactoryTimers == null)
            FactoryTimers = new List<FactoryTimer>();

        if (TimerLevels == null)
            TimerLevels = new Dictionary<string, int>();

        if (TimerLevels.Count == 0)
        {
            TimerLevels.Add("Orange", 1);
            TimerLevels.Add("Red", 0);
            TimerLevels.Add("Green", 0);
            TimerLevels.Add("Blue", 0);
        }
    }

    public void Init()
    {
        
    }
}