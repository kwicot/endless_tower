using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class ParametrVariables
{
    public string NameElement;        // название
    public Action Improvement;        // метод улучшения
    public Action Cost;               // метод стоимости улучшения
}

public class ParameterVar
{
    public string NameElement;        // название
    public float baseParameter;
    public float koefParameter;
    public float baseCost;
    public float koefCost;
    public float NextCost;
    public float NextParameter;
    public TypeAddition Type;
}

public enum TypeAddition
{
    Multiple,
    Summary
}

public class GameController : MonoBehaviour
{
    public static GameController singleton { get; private set; }
    public static GameState GameState = new GameState();

    public TowerView tower;
    public Spawner spawner;
    public FactoryScript factory;
    public List<EnemyView> L_Enemy = new List<EnemyView>();
    public SettingWave SettingWave = new SettingWave();
    private GameDifficult gameDifficult = GameDifficult.Easy;
    
    // Dictionary<string, Func<float>> param = new Dictionary<string, Func<float>>();
    // Dictionary<string, Func<string, float, float,float>> param2 = new Dictionary<string, Func<string, float, float,float>>();
    
    public readonly Dictionary<string, int> koeffChange = new Dictionary<string, int>()
    {
        {"Red", 25 },
        {"Green", 100 },
        {"Blue", 1000 }
    };  
    
    public Dictionary<string, ParameterVar> nameToValues = new Dictionary<string, ParameterVar>()
    {
        {"Damage", new ParameterVar()
        {
            NameElement = "Damage",
            baseParameter = 1f,
            koefParameter = 1.2f,
            baseCost = 4f,
            koefCost = 1.25f
        }},
        {"HP", new ParameterVar()
        {
            NameElement = "HP",
            baseParameter = 10f,
            koefParameter = 1.2f,
            baseCost = 3f,
            koefCost = 1.25f
        }},
        {"AttackSpeed", new ParameterVar()
        {
            NameElement = "AttackSpeed",
            baseParameter = 0.1f,
            koefParameter = 0.1f,
            baseCost = 5f,
            koefCost = 1.25f
        }},
        {"AttackRange", new ParameterVar()
        {
            NameElement = "AttackRange",
            baseParameter = 10f,
            koefParameter = 0.1f,
            baseCost = 4f,
            koefCost = 1.5f
        }},
        {"Regeneration", new ParameterVar()
        {
            NameElement = "Regeneration",
            baseParameter = 0f,
            koefParameter = 0.1f,
            baseCost = 3f,
            koefCost = 1.25f
        }},
        {"Defense", new ParameterVar()
        {
            NameElement = "Defense",
            baseParameter = 1f,
            koefParameter = 1.2f,
            baseCost = 4f,
            koefCost = 1.25f
        }},
    };
    
    /// <summary>
    /// Список переменных и их делегаты для изменения
    /// </summary>
    public Dictionary<string, ParametrVariables> nameToAction = new Dictionary<string, ParametrVariables>()
    {
        {"Damage", new ParametrVariables()
        {
            NameElement = "Damage",
            Improvement = Damage,
            Cost = DamageCost
        }},
        {"HP", new ParametrVariables()
        {
            NameElement = "HP",
            Improvement = HP,
            Cost = HPCost
        }},
        {"AttackSpeed", new ParametrVariables()
        {
            NameElement = "AttackSpeed",
            Improvement = AttackSpeed,
            Cost = AttackSpeedCost
        }},
        {"AttackRange",  new ParametrVariables()
        {
            NameElement = "AttackRange",
            Improvement = AttackRange,
            Cost = AttackRangeCost
        }},
        {"Regeneration",  new ParametrVariables()
        {
            NameElement = "Regeneration",
            Improvement = Regeneration,
            Cost = RegenerationCost
        }},
        {"Defense",  new ParametrVariables()
        {
            NameElement = "Defense",
            Improvement = Defense,
            Cost = DefenseCost
        }},
        
    };
    
    public static void Defense ()
    {
        string key = "Defense";
        GameState.current.Set(key, M(key, 1, 1.2f));
    }
    public static void DefenseCost ()
    {
        string key = "Defense";
        GameState.current.Set(key, M(key, 1, 1.2f));
    }
    
    public static void Regeneration()
    {
        string key = "Regeneration";
        GameState.current.Set(key, S(key, 0, 0.1f));
    }

    public static void RegenerationCost()
    {
        string key = "Regeneration";
        GameState.current.Set(key, S(key, 0, 0.1f));
    }
    public static void AttackRange()
    {
        string key = "AttackRange";
        GameState.current.Set(key, S(key, 10, 0.1f));
    }
    public static void AttackRangeCost()
    {
        string key = "AttackRange";
        GameState.current.Set(key, S(key, 10, 0.1f));
    }
    public static void HP()
    {
        string key = "HP";
        GameState.current.Set(key, M(key, 10, 1.2f));
    }
    public static void HPCost()
    {
        string key = "HP";
        GameState.current.Set(key, M(key, 10, 1.2f));
    }
    public static void AttackSpeed()
    {
        string key = "AttackSpeed";
        GameState.current.Set(key, S(key, 0.1f, 0.1f));
    }
    public static void AttackSpeedCost()
    {
        string key = "AttackSpeed";
        GameState.current.Set(key, S(key, 0.1f, 0.1f));
    }
    public static void Damage()
    {
        string key = "Damage";
        GameState.current.Set(key, M(key, 1, 1.2f));
        
        // // передать M(key, 1, 1.2f)
        // current.Next(key, 1);
    }
    
    public static void DamageCost()
    {
        string key = "Damage";
        GameState.current.Set(key, M(key, 4, 1.25f));
        
        // // передать M(key, 1, 1.2f)
        // current.Next(key, 1);
    }

    float _(string nameElement)
    {
        float result = 0f;

        if (GameState.current.Get(nameElement) == 0)
        {
            
        }

        return result;
    }


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

    // System.Func<float> parametr = () =>
    // {
    //     string key = "Damage";
    //     float v = 1.0f;
    //     int upper = global.Get(key) + local.Get(key);
    //     for (int i = 0; i < upper; i++)
    //     {
    //         v *= 1.2f;
    //     }
    //     return v;
    // };

    
    // private System.Action<Func<string, float, float,float>> parame = (f) =>
    // {
    //     string key = "Damage";
    //     
    //     // local.SetA(key, true, f);
    //     
    //     // передать M(key, 1, 1.2f)
    // };
    
    /// <summary>
    /// key - nameElement
    /// b   - base
    /// k   - koef
    /// a   - additional
    /// </summary>
    static System.Func<string, float, float, int ,float> U = (key, b, k, a) =>
    {
        float v = b;    // base
        int upper = GameState.global.Get(key) + GameState.local.Get(key) + a;
        if (k > 1f)
        {
            for (int i = 0; i < upper; i++)
            {
                v *= k; // koef
            }
        }
        else
        {
            for (int i = 0; i < upper; i++)
            {
                v += k;    // koef
            }
        }

        return v;
    };
    
    // Multiple
    static System.Func<string, float, float,float> M = (key, b, k) =>
    {
        float v = b;    // base
        int upper = GameState.global.Get(key) + GameState.local.Get(key);
        for (int i = 0; i < upper; i++)
        {
            v *= k;    // koef
        }
        return v;
    };    
    // Multiple Next
    static System.Func<string, float, float,float> MNext = (key, b, k) =>
    {
        float v = b;    // base
        int upper = GameState.global.Get(key) + GameState.local.Get(key) + 1;
        for (int i = 0; i < upper; i++)
        {
            v *= k;    // koef
        }
        return v;
    };
    // Summary
    static System.Func<string, float, float,float> S = (key, b, k) =>
    {
        float v = b;
        int upper = GameState.global.Get(key) + GameState.local.Get(key);
        for (int i = 0; i < upper; i++)
        {
            v += k;
        }
        return v;
    };
    // Summary
    static System.Func<string, float, float,float> SNExt = (key, b, k) =>
    {
        float v = b;
        int upper = GameState.global.Get(key) + GameState.local.Get(key) + 1;
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

        factory = GetComponent<FactoryScript>();
    }

    private void Start()
    {
        Load();
        // Init(false);
    }

    public void Load()
    {
        bool isNew = true;
        try
        {
            // пробуем забьрать сейв
            GameState = PersistentCache.TryLoad<GameState>();
            if (GameState == null)
            {
                // сейв пустой - создаем новые значения
                GameState = new GameState();
            }
            else
            {
                // не нужно инициализировать 
                isNew = false;
            }

        }
        catch (Exception e)
        {
            // сейв отсутствует или поламался
            Debug.LogError($"{GetType().Name} - Load: BAD save");
            GameState = new GameState();
        }
        
        GameState.Init();
        Init(isNew);
    }

    public void Save()
    {
        // TODO: переделать чтобы брало беспосредственно из сейва
        // сохраняем текущее состояние таймеров
        factory.SaveData();
        
        // сохраняем 
        PersistentCache.Save(GameState);
    }

    void Init(bool isNew = false)
    {
        // 
        GameState.local.ActionChanged = PAramAction;
        GameState.global.ActionChanged = PAramAction;
        
        if (isNew)
        {
            // инициализируем все значения 
            // foreach (var nameA in nameToAction)
            foreach (var nameA in nameToValues)
            {
                // Init(nameA.Key, nameA.Value.Improvement, true);
                Init(nameA.Key, true);
            }
        }
        else
        {
            // идем от сейва. но возможно нужно идти как обычно вдруг новые есть
            var keyGlobal = GameState.global.param.Keys.ToList();
            foreach (var key in keyGlobal)
            {
                // if (nameToAction.TryGetValue(key, out ParametrVariables action))
                // {
                    // Init(key, action.Improvement, false);
                    Init(key, false);
                // }
            }
            
            factory.LoadData();
        }

        if (GameState.FactoryTimers == null || GameState.FactoryTimers.Count == 0)
        {
            // 
            factory.InitDefault();
        }





        // param.Add("Damage", parametr);
        // param2.Add("HP", M);
        // float d = param["Damage"].Invoke();
        // float hp = param2["HP"].Invoke("HP", 1, 1.2f);
        // float att = M("AttackSpeed", 1, 1.2f);
        
        // получить значение
        var dam = GameState.current.Get("Damage");

    }

    
    /// <summary>
    /// Мы можем купить или нет?
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="nameElement"></param>
    /// <returns></returns>
    public bool CanBuy(float amount, string nameElement = "Orange")
    {
        if (GameState.Money.TryGetValue(nameElement, out var value))
        {
            return value >= amount;
        }

        return false;
    }

    public void Buy(float amount, string nameElement = "Orange")
    {
        if (GameState.Money.TryGetValue(nameElement, out var value))
        {
            GameState.Money[nameElement] -= amount;
            Debug.Log($"{nameElement}={GameState.Money[nameElement]}");
        }
        else Debug.LogError($"{GetType().Name} - Buy: ERROR GameState.Money not FOUND {nameElement}");
    }

    // public static void DamageCost()
    // {
    //     string key = "Damage";
    //     GameState.current.Set(key, M(key, 4, 1.25f));
    //     
    //     // // передать M(key, 1, 1.2f)
    //     // current.Next(key, 1);
    // }

    public void PAramAction(string nameElement)
    {
        if (nameToValues.TryGetValue(nameElement, out ParameterVar paramOut))
        {
            GameState.current.Set(nameElement, U(nameElement, paramOut.baseParameter, paramOut.koefParameter, 0));
            // GameState.currentCost.Set(nameElement, M(nameElement, paramOut.baseCost, paramOut.koefCost));
            paramOut.NextCost = U(nameElement, paramOut.baseCost, paramOut.koefCost, 1);
            paramOut.NextParameter = U(nameElement, paramOut.baseParameter, paramOut.koefParameter, 1);
        }
        else
        {
            Debug.LogError($"{GetType().Name} - PAramAction: nameToValues.TryGetValue {nameElement} == null");
        }
    }

    public float GetCost(string nameElement)
    {
        float result = 0f;
        if (nameToValues.TryGetValue(nameElement, out ParameterVar paramOut))
        {
            
            if (paramOut.Type == TypeAddition.Multiple)
            {
                result = MNext(nameElement, paramOut.baseCost, paramOut.koefCost);
            }
            else
            {
                result = SNExt(nameElement, paramOut.baseCost, paramOut.koefCost);
            }
        }
        else
        {
            Debug.LogError($"{GetType().Name} - PAramAction: nameToValues.TryGetValue {nameElement} == null");
        }

        return result;
    }

    public float GetNext(string nameElement)
    {
        float result = 0f;
        if (nameToValues.TryGetValue(nameElement, out ParameterVar paramOut))
        {
            
            if (paramOut.Type == TypeAddition.Multiple)
            {
                result = MNext(nameElement, paramOut.baseParameter, paramOut.koefParameter);
            }
            else
            {
                result = SNExt(nameElement, paramOut.baseParameter, paramOut.koefParameter);
            }
        }
        else
        {
            Debug.LogError($"{GetType().Name} - PAramAction: nameToValues.TryGetValue {nameElement} == null");
        }

        return result;
    }

    void Init(string nameElement, Action action, bool isNew)
    {
        GameState.local.SetA(nameElement, true, action);
        GameState.global.SetA(nameElement,isNew, action);
        
        // set base or save value
        if (isNew)
            GameState.global.Set(nameElement, 0);
        
        // set current
        action?.Invoke();
    }
    void Init(string nameElement, bool isNew)
    {
        GameState.local.SetA(nameElement, true, null);
        GameState.global.SetA(nameElement,isNew, null);
        
        // set base or save value
        if (isNew)
            GameState.global.Set(nameElement, 0);
        
        // set current
        PAramAction(nameElement);
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
            
            
            if (GameState.Money["Orange"] > amount)
            {
                if (withChange)
                {
                    GameState.Money["Orange"] -= amount;
                    GameState.Money[resOut] += AmountOut;
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

    public State state = State.MainMenu;
    private float timeCalculate = 0.1f;
    private void Update()
    {
        if (state == State.Game)
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
        GameState.Money[Name] += Count;
    }

    public void SetDifficeltAndStart(int difficult)
    {
        GameDifficult = (GameDifficult) difficult;
        LoadLevel(1);
    }
}

public enum State
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
