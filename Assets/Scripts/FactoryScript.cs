using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryScript : MonoBehaviour
{
    public float OrangeTimeMax;
    public int OrangeReward;
    FactoryTimer OrangeTimer;
    public Image OrangeProgressBar;
    public Text OrangeText;

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
        OrangeTimer = new FactoryTimer() { Seconds = OrangeTimeMax, Resource = "Orange", Reward = OrangeReward };
    }

    void Update()
    {
        if (OrangeProgressBar.IsActive())
        {
            OrangeProgressBar.fillAmount = OrangeTimer.Seconds / OrangeTimeMax;
            OrangeText.text = OrangeTimer.GetTimeLeft();
        }
        if (OrangeTimer.Tick(Time.deltaTime))
        {
            AddReward("Orange", OrangeReward);
            OrangeTimer.Seconds = OrangeTimeMax;
            Debug.Log("Reward- " + GameController.singleton.GameMoney["Orange"]);
        }
        Debug.Log(OrangeTimer.GetTimeLeft());
        
    }
    public void OrangeUpdateTime()
    {
        OrangeTimeMax /= 2;
        OrangeTimer.Seconds /= 2;
    }
    void AddReward(string Name, float count)
    {
        GameController.singleton.GameMoney[Name] += count;
    }

    public void SaveData()
    {

    }
    public void LoadData()
    {
        
    }


}
[System.Serializable]
public class FactoryTimer
{
    public string Resource;
    public float Seconds;
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
    public string GetTimeLeft()
    {
        string res = "H-" + Hour.ToString() + " M-" + Minnutes.ToString() + " S-" + Sec.ToString();
        return res;
    }
}
