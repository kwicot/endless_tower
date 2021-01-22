using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController singleton { get; private set; }

    public TowerView tower;
    public Spawner spawner;
    public FactoryScript factory;
    public List<EnemyView> L_Enemy = new List<EnemyView>();
    public SettingWave SettingWave = new SettingWave();
    private GameDifficult gameDifficult = GameDifficult.Easy;
    public static UIParam local = new UIParam();
    public static  UIParam global = new UIParam();
    public static  UFParam current = new UFParam();
    Dictionary<string, Func<float>> param = new Dictionary<string, Func<float>>();
    Dictionary<string, Func<string, float, float,float>> param2 = new Dictionary<string, Func<string, float, float,float>>();
    

    public Dictionary<string, float> GameMoney = new Dictionary<string, float>()
    {
        {"White",0 },
        {"Orange",100 },
        {"Red",0 },
        {"Green",0 },
        {"Blue", 0 },
        {"Crystal", 0 }
    };
    
    public readonly Dictionary<string, int> koeffChange = new Dictionary<string, int>()
    {
        {"Red", 25 },
        {"Green", 100 },
        {"Blue", 1000 }
    };  
        

    public GameDifficult GameDifficult
    {
        get => gameDifficult;
        set
        {
            gameDifficult = value;
            switch (gameDifficult)
            {
                case GameDifficult.Easy:
                    SettingWave.EnemyHPCoeff = 0.5f;
                    SettingWave.EnemyDamageCoeff = 0.5f;
                    SettingWave.RewardCoeff = 0.25f;
                    SettingWave.BossInWaves = 25;
                    break;
                case GameDifficult.Normal:
                    SettingWave.EnemyHPCoeff = 1f;
                    SettingWave.EnemyDamageCoeff = 1f;
                    SettingWave.RewardCoeff = 1f;
                    SettingWave.BossInWaves = 10;
                    break;
                case GameDifficult.Medium:
                    SettingWave.EnemyHPCoeff = 2f;
                    SettingWave.EnemyDamageCoeff = 2f;
                    SettingWave.RewardCoeff = 4f;
                    SettingWave.BossInWaves = 5;
                    break;
                case GameDifficult.Hard:
                    SettingWave.EnemyHPCoeff = 6f;
                    SettingWave.EnemyDamageCoeff = 6f;
                    SettingWave.RewardCoeff = 8f;
                    SettingWave.BossInWaves = 2;
                    break;
                case GameDifficult.VeryHard:
                    SettingWave.EnemyHPCoeff = 10f;
                    SettingWave.EnemyDamageCoeff = 10f;
                    SettingWave.RewardCoeff = 12f;
                    SettingWave.BossInWaves = 1;
                    break;
            }
        }
    }

    //Временное
    public float SpawnInterval;
    public float WaveInterval;
    public int EnemyPerWave;
    //Временное

    System.Func<float> parametr = () =>
    {
        string key = "Damage";
        float v = 1.0f;
        int upper = global.Get(key) + local.Get(key);
        for (int i = 0; i < upper; i++)
        {
            v *= 1.2f;
        }
        return v;
    };

    public System.Action Damage = () =>
    {
        string key = "Damage";
        // current.Set(key, param[key].Invoke());
        current.Set(key, M(key, 1, 1.2f));
        
        // передать M(key, 1, 1.2f)
        current.Next(key, 1);
    };
    private System.Action<Func<string, float, float,float>> parame = (f) =>
    {
        string key = "Damage";
        
        // local.SetA(key, true, f);
        
        // передать M(key, 1, 1.2f)
    };
    static System.Func<string, float, float,float> M = (key, b, k) =>
    {
        float v = b;
        int upper = global.Get(key) + local.Get(key);
        for (int i = 0; i < upper; i++)
        {
            v *= k;
        }
        return v;
    };
    System.Func<string, float, float,float> S = (key, b, k) =>
    {
        float v = b;
        int upper = global.Get(key) + local.Get(key);
        for (int i = 0; i < upper; i++)
        {
            v += k;
        }
        return v;
    };

    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);


        //TODO: это установить с настроек меню
        GameDifficult = GameDifficult.Medium;
        Init();
    }

    void Init()
    {
        
        // тут подписка на изменения в самой игре и ГУИ 
        local.Set("HP", 0);
        local.Set("AttackSpeed", 0);
        local.Set("AttackRange", 0);
        local.Set("Regeneration", 0);
        local.Set("Defense", 0);
        
        
        local.SetA("Damage", true, Damage);
        global.SetA("Damage",true, Damage);
        
        // тут подписка на изменения в меню
        global.Set("HP", 0);
        global.Set("AttackSpeed", 0);
        global.Set("AttackRange", 0);
        global.Set("Regeneration", 0);
        global.Set("Defense", 0);
        
        // ловит изменения и считает
        current.Set("HP", 0);
        current.Set("Damage", 0);
        
        current.Set("AttackSpeed", 0);
        current.Set("AttackRange", 0);
        current.Set("Regeneration", 0);
        current.Set("Defense", 0);
        
        

        param.Add("Damage", parametr);
        param2.Add("HP", M);
        float d = param["Damage"].Invoke();
        float hp = param2["HP"].Invoke("HP", 1, 1.2f);
        float att = M("AttackSpeed", 1, 1.2f);
        
        // получить значение
        var dam = current.Get("Damage");

    }

    /// <summary>
    /// Обмен ресурсов ()
    /// </summary>
    /// <param name="resIn">что тратим</param>
    /// <param name="AmountOut">прибавка меняемого элемента</param>
    /// <param name="resOut">на что меняем</param>
    /// <param name="withChange">false - просто проверить на возможность обмена</param>
    public bool ChangeResources(string resIn, int AmountOut, string resOut, bool withChange = true)
    {
        if (resIn == "Orange")
        {
            int amount = 0;
            switch (resOut)
            {
                case "Red":
                    amount = AmountOut * 50; // TODO: коэфициенты возможно будут меняться при улучшениях?
                    break;
                case "Green":
                    amount = AmountOut * 200;
                    break;
                case "Blue":
                    amount = AmountOut * 1000;
                    break;
            }
            
            
            if (GameMoney["Orange"] > amount)
            {
                if (withChange)
                {
                    GameMoney["Orange"] -= amount;
                    GameMoney[resOut] += AmountOut;
                }

                return true;
            }
        }
        
        
        return false;
    }

    public void LoadLevel(int index)
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (currentLevel == 0)
        {
            factory.SaveData();
            SceneManager.LoadScene(index);
        }
        else if (index == 0)
        {
            SceneManager.LoadScene(index);
            factory.LoadData();
        }
        else
        {
            SceneManager.LoadScene(index);
        }
    }

    public GameState GameState = GameState.Game;
    private float timeCalculate = 0.1f;
    private void Update()
    {
        if (GameState == GameState.Game)
        {
            // двигаем мобов
            EnemyMove();
            
            // стрельба башни
            tower?.AttackUpdate();
            
            // перерасчет дистанции для нанесения урона башне
            EnemyDistanceUpdate();
        }
    }

    void EnemyMove()
    {
        int count = L_Enemy.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            L_Enemy[i]?.Move();
        }
    }

    void EnemyDistanceUpdate()
    {
            timeCalculate -= Time.deltaTime;
            if (timeCalculate <= 0f) //Enemy logic
            {
                timeCalculate = 0.1f;

                var count = L_Enemy.Count;
                if (count > 0)
                {
                    for (var i = count - 1; i >= 0; i--)
                    {
                        if (L_Enemy[i] != null)
                        {
                            var distance = Vector3.Distance(L_Enemy[i].gameObject.transform.position, tower.transform.position);
                            if (distance < 2f)
                            {
                                tower.TakeDamage(L_Enemy[i].Damage);
                                Destroy(L_Enemy[i].gameObject);
                                L_Enemy.RemoveAt(i);
                            }
                        }
                    }
                }
            } //Enemy logic end
    }
    
    public void EnemyKilled(EnemyView enemy)
    {
        EnemyType type = enemy.SOEnemy.Type;
        L_Enemy.Remove(enemy);
        if (L_Enemy.Count == 0) spawner.CanTime = true;

        //switch (type)
        //{
        //    case EnemyType.Default: AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff); break;
        //    case EnemyType.Fast:
        //        {
        //            AddReward("Orange", EnemyRewardOrange * OrangeBonusPerKill * SettingWave.RewardCoeff);
        //            AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff);
        //        } break;
        //    case EnemyType.Fat:
        //        {
        //            AddReward("Orange", EnemyRewardOrange * OrangeBonusPerKill * SettingWave.RewardCoeff);
        //            AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff);
        //        }
        //        break;
        //    case EnemyType.MiniBoss:
        //        {
        //            AddReward("Orange", EnemyRewardOrange * OrangeBonusPerKill * SettingWave.RewardCoeff);
        //            AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff);
        //        }
        //        break;
        //    case EnemyType.Boss:
        //        {
        //            AddReward("Crystal", EnemyRewardCrystal * CrystalBonusPerKill);
        //            AddReward("Orange", EnemyRewardOrange * OrangeBonusPerKill * SettingWave.RewardCoeff);
        //            AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff);
        //        }
        //        break;

        //}
        //Вознаграждение
    }

    void AddReward(string Name, float Count)
    {
        GameMoney[Name] += Count;
    }
    
    
}

public enum GameState
{
    MainMenu,
    Game,
    Pause
}

public enum GameDifficult
{
    Easy = 0,
    Normal,
    Medium,
    Hard,
    VeryHard
}
public class SettingWave
{
    public float EnemyHPCoeff;
    public float EnemyDamageCoeff;
    public float RewardCoeff;
    public int BossInWaves;
}
