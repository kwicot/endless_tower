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
    public float NextLocalCost;
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
    public CanvasOnPayableScene canvasOnPayableScene;
    public float Points;
    public List<EnemyView> L_Enemy = new List<EnemyView>();
    public SettingWave SettingWave = new SettingWave();
    private GameDifficult gameDifficult = GameDifficult.Easy;
    
    public static string[] listNameTimer = {"Orange", "Red", "Blue", "Green"};
    
    public readonly Dictionary<string, int> koeffChange = new Dictionary<string, int>()
    {
        {"Red", 25 },
        {"Green", 100 },
        {"Blue", 1000 }
    };
    
    public readonly Dictionary<string, int> baseCostFabric = new Dictionary<string, int>()
    {
        {"Orange", 1 },
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
    
    
    /// <summary>
    /// Universal - global+local
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
    
    /// <summary>
    /// Local
    /// </summary>
    static System.Func<string, float, float, int ,float> L = (key, b, k, a) =>
    {
        float v = b;    // base
        int upper = GameState.local.Get(key) + a;
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
        // TODO: переделать чтобы брало непосредственно из сейва
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
            foreach (var nameA in nameToValues)
            {
                Init(nameA.Key, true);
            }
        }
        else
        {
            // идем от сейва. но возможно нужно идти как обычно вдруг новые есть
            var keyGlobal = GameState.global.param.Keys.ToList();
            foreach (var key in keyGlobal)
            {
                Init(key, false);
            }
        }      
        
        InitFabric();
        factory.LoadData(isNew);
    }

    private void InitFabric()
    {
        if (GameState.Fabric == null)
            GameState.Fabric = new Fabric();
        
        GameState.Fabric.InitDefault();
    }

    public int GetCostTimerLevel(string resources)
    {
        int result = 0;
        int baseValue = 2;

        // current level
        if (GameState.Fabric.TimerLevels.TryGetValue(resources, out var level))
        {
            result = baseValue * level;
        }

        return result;
    }

    public int GetCostNew(string resources)
    {
        int baseValue = 2;

        baseCostFabric.TryGetValue(resources, out baseValue);

        return baseValue;
    }

    public void UpTimerLevel(string resources)
    {
        // current level
        if (GameState.Fabric.TimerLevels.ContainsKey(resources))
        {
            if (GameState.Fabric.TimerLevels[resources] == 0)
            {
                factory.InitByName(resources);
                factory.SaveData();
            }

            GameState.Fabric.TimerLevels[resources]++;
        }
        else
        {
            GameState.Fabric.TimerLevels[resources] = 1;
        }
    }

    public FactoryTimer GetTimerByName(string nameElement)
    {
        foreach (var timer in GameState.Fabric?.FactoryTimers)
        {
            if (timer.Resource == nameElement)
                return timer;
        }

        return null;
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

    /// <summary>
    /// Покупаем
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="nameElement"></param>
    public void Buy(float amount, string nameElement = "Orange")
    {
        if (GameState.Money.TryGetValue(nameElement, out var value))
        {
            GameState.Money[nameElement] -= amount;
            Debug.Log($"{nameElement}={GameState.Money[nameElement]}");
        }
        else Debug.LogError($"{GetType().Name} - Buy: ERROR GameState.Money not FOUND {nameElement}");
    }

    public void PAramAction(string nameElement)
    {
        if (nameToValues.TryGetValue(nameElement, out ParameterVar paramOut))
        {
            GameState.current.Set(nameElement, U(nameElement, paramOut.baseParameter, paramOut.koefParameter, 0));
            // GameState.currentCost.Set(nameElement, M(nameElement, paramOut.baseCost, paramOut.koefCost));
            paramOut.NextCost = U(nameElement, paramOut.baseCost, paramOut.koefCost, 1);
            paramOut.NextLocalCost = L(nameElement, paramOut.baseCost, paramOut.koefCost, 1);
            paramOut.NextParameter = U(nameElement, paramOut.baseParameter, paramOut.koefParameter, 1);
        }
        else
        {
            Debug.LogError($"{GetType().Name} - PAramAction: nameToValues.TryGetValue {nameElement} == null");
        }
    }

    
    void Init(string nameElement, bool isNew)
    {
        GameState.local.SetA(nameElement, true, null);
        GameState.global.SetA(nameElement,isNew, null);
        
        // set base or save value
        if (isNew)
            GameState.global.Set(nameElement, 0);
        
        GameState.local.Set(nameElement, 0);
        
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
            factory.LoadData(false);
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

    public void EndRound()
    {
        canvasOnPayableScene.Activate("pnWindowFail", true);
    }

    public void EnemyKilled(EnemyView enemy)
    {
        EnemyType type = enemy.SOEnemy.Type;
        L_Enemy.Remove(enemy);
        if (L_Enemy.Count == 0) 
            spawner.CanTime = true;
            
        //TODO Вознаграждение для каждого типо врагов

        Points += enemy.SOEnemy.HP;

        /*
        switch (type)
        {
            case EnemyType.Default: AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff); break;
            case EnemyType.Fast:
                {
                    AddReward("Orange", EnemyRewardOrange * OrangeBonusPerKill * SettingWave.RewardCoeff);
                    AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff);
                } break;
            case EnemyType.Fat:
                {
                    AddReward("Orange", EnemyRewardOrange * OrangeBonusPerKill * SettingWave.RewardCoeff);
                    AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff);
                }
                break;
            case EnemyType.MiniBoss:
                {
                    AddReward("Orange", EnemyRewardOrange * OrangeBonusPerKill * SettingWave.RewardCoeff);
                    AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff);
                }
                break;
            case EnemyType.Boss:
                {
                    AddReward("Crystal", EnemyRewardCrystal * CrystalBonusPerKill);
                    AddReward("Orange", EnemyRewardOrange * OrangeBonusPerKill * SettingWave.RewardCoeff);
                    AddReward("White", EnemyRewardWhite * WhiteBonusPerKill * SettingWave.RewardCoeff);
                }
                break;

        }
             Вознаграждение   */

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
