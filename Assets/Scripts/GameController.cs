using System;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController singleton { get; private set; }

    public TowerView tower;
    public Spawner spawner;
    public List<EnemyView> L_Enemy = new List<EnemyView>();
    public SettingWave SettingWave = new SettingWave();
    private GameDifficult gameDifficult = GameDifficult.Easy;

    public Dictionary<string, float> GameMoney = new Dictionary<string, float>()
    {
        {"White",0 },
        {"Orange",100 },
        {"Red",0 },
        {"Green",0 },
        {"Blue", 0 },
        {"Crystal", 0 }
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



    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);

        //TODO: это установить с настроек меню
        GameDifficult = GameDifficult.Normal;
    }


    public void StartGame(int level)
    {
        SceneManager.LoadScene(level);
    }

    public GameState GameState = GameState.Game;
    private float timeCalculate = 0.1f;
    private void Update()
    {
        EnemyDistanceUpdate();
    }

    void EnemyDistanceUpdate()
    {
        if (GameState == GameState.Game)
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
    }
    
    public void EnemyKilled(EnemyType type)
    {
        for(int i = 0; i< L_Enemy.Count; i++) //Очистка листа от пустых ссылок
        {
            if(L_Enemy[i] == null)
            {
                L_Enemy.RemoveAt(i);
                i--;
            }
        }
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
