using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScript : MonoBehaviour
{
    public float OrangeTimeLeft;
    public float OrangeTimeMax;
    public float OrangeReward;

    float RedTimeLeft;
    float RedTimeMax;
    float RedReward;

    float BlueTimeLeft;
    float BlueTimeMax;
    float BlueReward;

    float GreenTimeLeft;
    float GreenTimeMax;
    float GreenReward;
    void Start()
    {
        GameController.singleton.factory = this;
    }

    void Update()
    {
        if(OrangeTimeMax > 0)
        {
            OrangeTimeLeft -= Time.deltaTime;
            //Debug.Log("Time left= " + (OrangeTimeLeft / 3600).ToString()+ " Hours" + (OrangeTimeLeft  % 3600) 
            if(OrangeTimeLeft <= 0)
            {
                AddReward("Orange", OrangeReward);
                OrangeTimeLeft = OrangeTimeMax;
                Debug.Log("Reward-" + GameController.singleton.GameMoney["Orange"]);
            }
        }
        if (RedTimeMax > 0)
        {
            RedTimeLeft -= Time.deltaTime;
            if (RedTimeLeft <= 0)
            {
                AddReward("Red", RedReward);
                RedTimeLeft = RedTimeMax;
            }
        }
        if (BlueTimeMax > 0)
        {
            BlueTimeLeft -= Time.deltaTime;
            if (BlueTimeLeft <= 0)
            {
                AddReward("Blue", BlueReward);
                BlueTimeLeft = BlueTimeMax;
            }
        }
        if (GreenTimeMax > 0)
        {
            GreenTimeLeft -= Time.deltaTime;
            if (GreenTimeLeft <= 0)
            {
                AddReward("Green", GreenReward);
                GreenTimeLeft = GreenTimeMax;
            }
        }
    }
    void AddReward(string Name, float count)
    {
        GameController.singleton.GameMoney[Name] += count;
    }
    public void Upgrade(string Name)
    {
        switch (Name)
        {
            case "Orange":
                {

                }break;
        }
    }
    public void SaveData()
    {

    }
    public void LoadData()
    {
        
    }


}
