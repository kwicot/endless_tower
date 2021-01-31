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
    public FactoryTimer OrangeTimer;
    public FactoryTimer RedTimer;
    public FactoryTimer BlueTimer;
    public FactoryTimer GreenTimer;

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
        
        RedTimer = new FactoryTimer()
        {
            Seconds = MaxTimer, 
            MaxSeconds = MaxTimer, 
            Resource = "Red", 
            Reward = 1
        };
        RedTimer.Init();
        
        BlueTimer = new FactoryTimer()
        {
            Seconds = MaxTimer, 
            MaxSeconds = MaxTimer, 
            Resource = "Blue", 
            Reward = 1
        };
        BlueTimer.Init();
        
        GreenTimer = new FactoryTimer()
        {
            Seconds = MaxTimer,
            MaxSeconds = MaxTimer, 
            Resource = "Green", 
            Reward = 1
        };
        GreenTimer.Init();
        
        GameController.GameState.FactoryTimers = new List<FactoryTimer>();
        GameController.GameState.FactoryTimers.Add(OrangeTimer);
        GameController.GameState.FactoryTimers.Add(RedTimer);
        GameController.GameState.FactoryTimers.Add(BlueTimer);
        GameController.GameState.FactoryTimers.Add(GreenTimer);
    }

    public static void AddReward(string Name, float count)
    {
        GameController.GameState.Money[Name] += count;
    }

    public void SaveData()
    {
        GameController.GameState.FactoryTimers.Clear();
        GameController.GameState.FactoryTimers.Add(OrangeTimer);
        GameController.GameState.FactoryTimers.Add(RedTimer);
        GameController.GameState.FactoryTimers.Add(BlueTimer);
        GameController.GameState.FactoryTimers.Add(GreenTimer);
    }
    
    public void LoadData()
    {
        foreach (var timer in GameController.GameState.FactoryTimers)
        {
            if (timer == null) continue;
            
            if (timer.Resource == "Orange")
            {
                OrangeTimer = new FactoryTimer()
                {
                    Seconds = timer.Seconds,
                    MaxSeconds = timer.MaxSeconds,
                    Resource = timer.Resource,
                    Reward = timer.Reward
                };
                OrangeTimer.Init();
            }
            else if (timer.Resource == "Red")
            {
                RedTimer = new FactoryTimer()
                {
                    Seconds = OrangeTimeMax, 
                    MaxSeconds = OrangeTimeMax, 
                    Resource = "Red", 
                    Reward = OrangeReward
                };
                RedTimer.Init();
            }
            else if (timer.Resource == "Blue")
            {
                BlueTimer = new FactoryTimer()
                {
                    Seconds = OrangeTimeMax, 
                    MaxSeconds = OrangeTimeMax, 
                    Resource = "Blue", 
                    Reward = OrangeReward
                };
                BlueTimer.Init();
            }
            else if (timer.Resource == "Green")
            {
                GreenTimer = new FactoryTimer()
                {
                    Seconds = OrangeTimeMax, 
                    MaxSeconds = OrangeTimeMax, 
                    Resource = "Green", 
                    Reward = OrangeReward
                };
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
        string res = "H-" + Hour.ToString() + " M-" + Minnutes.ToString() + " S-" + Sec.ToString();
        return res;
    }
}
