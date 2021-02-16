using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryScript : MonoBehaviour
{
    public float OrangeTimeMax;
    public float MaxTimer = 24 * 60 * 60;
    public int OrangeReward;
    public FactoryTimer OrangeTimer = null;
    public FactoryTimer RedTimer = null;
    public FactoryTimer BlueTimer = null;
    public FactoryTimer GreenTimer = null;

    //
    public Transform pnTimer;
    public GameObject[] prefabs;
    
    // void Awake()
    // {
    //     GameController.singleton.factory = this;
    // }

    public void StartFactory(bool isNew = false)
    {
        if (isNew)
            InitDefault();
        else
        {
            
        }
    }

    public void InitByName(string nameElement)
    {
        if (nameElement == "Red")
        {
            RedTimer = new FactoryTimer()
            {
                Seconds = MaxTimer, 
                MaxSeconds = MaxTimer, 
                Resource = "Red", 
                Reward = 1
            };
            RedTimer.Init();
        }
        else if (nameElement == "Blue")
        {
            BlueTimer = new FactoryTimer()
            {
                Seconds = MaxTimer, 
                MaxSeconds = MaxTimer, 
                Resource = "Blue", 
                Reward = 1
            };
            BlueTimer.Init();
        }
        else if (nameElement == "Green")
        {
            GreenTimer = new FactoryTimer()
            {
                Seconds = MaxTimer,
                MaxSeconds = MaxTimer, 
                Resource = "Green", 
                Reward = 1
            };
            GreenTimer.Init();
        }
    }

    public void InitDefault()
    {
        OrangeTimer = new FactoryTimer()
        {
            Seconds = MaxTimer, 
            MaxSeconds = MaxTimer,
            Resource = "Orange", 
            Reward = 1
        };
        OrangeTimer.Init();
        
        // RedTimer = new FactoryTimer()
        // {
        //     Seconds = MaxTimer, 
        //     MaxSeconds = MaxTimer, 
        //     Resource = "Red", 
        //     Reward = 1
        // };
        // RedTimer.Init();
        //
        // BlueTimer = new FactoryTimer()
        // {
        //     Seconds = MaxTimer, 
        //     MaxSeconds = MaxTimer, 
        //     Resource = "Blue", 
        //     Reward = 1
        // };
        // BlueTimer.Init();
        //
        // GreenTimer = new FactoryTimer()
        // {
        //     Seconds = MaxTimer,
        //     MaxSeconds = MaxTimer, 
        //     Resource = "Green", 
        //     Reward = 1
        // };
        // GreenTimer.Init();
        
        // GameController.GameState.Fabric.FactoryTimers = new List<FactoryTimer>();
        GameController.GameState.Fabric.FactoryTimers.Add(OrangeTimer);
        // GameController.GameState.Fabric.FactoryTimers.Add(RedTimer);
        // GameController.GameState.Fabric.FactoryTimers.Add(BlueTimer);
        // GameController.GameState.Fabric.FactoryTimers.Add(GreenTimer);
    }

    public static void AddReward(string Name, float count)
    {
        GameController.GameState.Money[Name] += count;
    }

    public void SaveData()
    {
        GameController.GameState.Fabric.FactoryTimers.Clear();
        
        if (OrangeTimer != null)
            GameController.GameState.Fabric.FactoryTimers.Add(OrangeTimer);
        
        if (RedTimer != null)
            GameController.GameState.Fabric.FactoryTimers.Add(RedTimer);
        
        if (BlueTimer != null)
            GameController.GameState.Fabric.FactoryTimers.Add(BlueTimer);
        
        if (GreenTimer != null)
            GameController.GameState.Fabric.FactoryTimers.Add(GreenTimer);
    }
    
    public void LoadData(bool isNew)
    {
        if (GameController.GameState.Fabric.FactoryTimers.Count == 0)
        {
            InitDefault();
        }

        foreach (var timer in GameController.GameState.Fabric.FactoryTimers)
        {
            if (timer == null) continue;
            
            if (timer.Resource == "Orange")
            {
                // OrangeTimer = new FactoryTimer()
                // {
                //     Seconds = timer.Seconds,
                //     MaxSeconds = timer.MaxSeconds,
                //     Resource = timer.Resource,
                //     Reward = timer.Reward
                // };
                // OrangeTimer.Init();
                OrangeTimer = timer;
                OrangeTimer.Init();
            }
            else if (timer.Resource == "Red")
            {
                // RedTimer = new FactoryTimer()
                // {
                //     Seconds = OrangeTimeMax, 
                //     MaxSeconds = OrangeTimeMax, 
                //     Resource = "Red", 
                //     Reward = OrangeReward
                // };
                // RedTimer.Init();
                RedTimer = timer;
                RedTimer.Init();
            }
            else if (timer.Resource == "Blue")
            {
                // BlueTimer = new FactoryTimer()
                // {
                //     Seconds = OrangeTimeMax, 
                //     MaxSeconds = OrangeTimeMax, 
                //     Resource = "Blue", 
                //     Reward = OrangeReward
                // };
                // BlueTimer.Init();
                BlueTimer = timer;
                BlueTimer.Init();
            }
            else if (timer.Resource == "Green")
            {
                // GreenTimer = new FactoryTimer()
                // {
                //     Seconds = OrangeTimeMax, 
                //     MaxSeconds = OrangeTimeMax, 
                //     Resource = "Green", 
                //     Reward = OrangeReward
                // };
                // GreenTimer.Init();
                GreenTimer = timer;
                GreenTimer.Init();
            }
        }
    }


    bool HasPrefab(string nameElement)
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
    GameObject GetPrefab(string nameElement)
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


[System.Serializable]
public class FactoryTimer
{
    public string Resource;
    public float Seconds;
    public float MaxSeconds;
    public float Reward;
    [NonSerialized] public System.Action ActionTick = null;

    int min => (int)Seconds / 60;
    int Hour => min / 60;
    int Minnutes => min - Hour * 60;
    int Sec => (int)Seconds - min * 60;

    public void Init()
    {
        Utilities.Timer.OnTick -= OnTick;
        Utilities.Timer.OnTick += OnTick;
    }

    void OnTick()
    {
        Seconds--;

        if (Seconds > 0)
        {
            ActionTick?.Invoke();
        }
        else
        {
            Restart();
        }
    }

    void Restart()
    {
        FactoryScript.AddReward(Resource, Reward);
        Seconds = MaxSeconds;
        
        Debug.Log("Reward- " + GameController.GameState.Money[Resource]);
    }
    
    public void UpdateMaxTime()
    {
        MaxSeconds /= 2;
        Seconds /= 2;
    }
    
    
    public string GetTimeLeft()
    {
        string res = $"{Hour}:{Minnutes}:{Sec}";
        // string res = $"H-{Hour} M-{Minnutes} S-{Sec}";
        return res;
    }
}
