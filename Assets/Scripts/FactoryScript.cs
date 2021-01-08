using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryScript : MonoBehaviour
{
    public float OrangeTimeMax;
    public int OrangeReward;
    FactoryTimer OrangeTimer;
    FactoryTimer RedTimer;
    FactoryTimer BlueTimer;
    FactoryTimer GreenTimer;

    //
    public Transform pnTimer;
    public GameObject[] prefabs;
    
    void Start()
    {
        GameController.singleton.factory = this;
        
        OrangeTimer = new FactoryTimer()
        {
            Seconds = OrangeTimeMax, 
            MaxSeconds = OrangeTimeMax, 
            Resource = "Orange", 
            Reward = OrangeReward
        };
        
        RedTimer = new FactoryTimer()
        {
            Seconds = OrangeTimeMax, 
            MaxSeconds = OrangeTimeMax, 
            Resource = "Red", 
            Reward = OrangeReward
        };
        
        BlueTimer = new FactoryTimer()
        {
            Seconds = OrangeTimeMax, 
            MaxSeconds = OrangeTimeMax, 
            Resource = "Blue", 
            Reward = OrangeReward
        };
        
        GreenTimer = new FactoryTimer()
        {
            Seconds = OrangeTimeMax, 
            MaxSeconds = OrangeTimeMax, 
            Resource = "Green", 
            Reward = OrangeReward
        };
        
        // create prefab
        if (HasPrefab("FabricTimer"))
        {
            // orange
            var element = Instantiate(GetPrefab("FabricTimer"), pnTimer);
            var ft = element.GetComponent<FabricTimer>();
            if (ft) ft.Init(OrangeTimer);
            
            // red
            element = Instantiate(GetPrefab("FabricTimer"), pnTimer);
            ft = element.GetComponent<FabricTimer>();
            if (ft) ft.Init(RedTimer);
            
            // blue
            element = Instantiate(GetPrefab("FabricTimer"), pnTimer);
            ft = element.GetComponent<FabricTimer>();
            if (ft) ft.Init(BlueTimer);
            
            // dark
            element = Instantiate(GetPrefab("FabricTimer"), pnTimer);
            ft = element.GetComponent<FabricTimer>();
            if (ft) ft.Init(GreenTimer);
        }

        // init facbric timer
    }

    
    public void OrangeUpdateTime()
    {
        OrangeTimeMax /= 2;
        OrangeTimer.Seconds /= 2;
    }
    public static void AddReward(string Name, float count)
    {
        GameController.singleton.GameMoney[Name] += count;
    }

    public void SaveData()
    {

    }
    public void LoadData()
    {
        
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
    public int Reward;

    int min => (int)Seconds / 60;
    int Hour => min / 60;
    int Minnutes => min - Hour * 60;
    int Sec => (int)Seconds - min * 60;

    public bool Tick(float count)
    {
        Seconds -= count;
        if (Seconds <= 0)
            return true;
        else return false;
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
