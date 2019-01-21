/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;
    public string gamePrefsName = "Default";//set in Editor

    public int seed;
    public float sfxVolume;
    public float musicVolume;
    public bool getPrefs = false;

    public float mouseSensetivity;
    private float xMouseSensitivity = 30.0f;
    private float yMouseSensitivity = 30.0f;

    // Use this for initialization
    void Start()
    {
        // keep this object alive
       // DontDestroyOnLoad(this.gameObject);
        gameController = this.gameObject.GetComponent<GameController>();
        GetMouseSensetivity();

    }

    // Update is called once per frame
    void Update()
    {
        if (getPrefs == true)
        {
            GetPrefs();
            getPrefs = false;
        }
    }

    public void GetPrefs()
    {
        if (PlayerPrefs.HasKey(gamePrefsName + "_SFXVol"))
            sfxVolume = PlayerPrefs.GetFloat(gamePrefsName + "_SFXVol");
        if (PlayerPrefs.HasKey(gamePrefsName + "_MusicVol"))
            musicVolume = PlayerPrefs.GetFloat(gamePrefsName + "_MusicVol");

       // MusicController.musicController.fadeState ^= 1;
       // MusicController.musicController.targetVolume = musicVolume;
    }

    //get MouseSencetivity out of PlayerPrefs
    public Vector2 GetMouseSensetivity()
    {
        Vector2 mouseXY;
        if (PlayerPrefs.HasKey(gamePrefsName + "_MouseSen"))
        {
            mouseSensetivity = PlayerPrefs.GetFloat(gamePrefsName + "_MouseSen");
            xMouseSensitivity = mouseSensetivity * 200;
            yMouseSensitivity = mouseSensetivity * 200;
        }
        else
        {
            xMouseSensitivity = 30.0f;
            yMouseSensitivity = 30.0f;
        }
        mouseXY = new Vector2(xMouseSensitivity, yMouseSensitivity);
        return (mouseXY);
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Play, Pause }
//public delegate void InventoryUsedCallback(InventoryUIButton item);
public delegate void UpdateHeroParametersHandler(HeroParameters parameters);

public class GameController : MonoBehaviour
{
    private GameState state;
   
    [SerializeField]
    private Audio audioManager;

    private MainHero _mainHero;

    [SerializeField]
    private HeroParameters hero;
   
    public event UpdateHeroParametersHandler OnUpdateHeroParameters;

    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameController = Instantiate(Resources.Load("Prefabs/GameController")) as GameObject;
                _instance = gameController.GetComponent<GameController>();
            }

            return _instance;
        }
    }
    

    public GameState State
    {
        get
        {
            return state;
        }

        set
        {
            if (value == GameState.Play)
            {
                Time.timeScale = 1.0f;
            }
            else
            {
                Time.timeScale = 0.0f;
            }
            state = value;
        }
    }

    public MainHero MainHero
    {
        get
        {
            return _mainHero;
        }

        set
        {
            _mainHero = value;
        }
    }

    public Audio AudioManager
    {
        get
        {
            return audioManager;
        }

        set
        {
            audioManager = value;
        }
    }
/*
    public List<InventoryItem> Inventory
    {
        get
        {
            return inventory;
        }

        set
        {
            inventory = value;
        }
    }
*/
    public HeroParameters Hero
    {
        get
        {
            return hero;
        }

        set
        {
            hero = value;
        }
    }
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        State = GameState.Play;
       
        InitializeAudioManager();
        
    }

   /* public void Hit(IDestructable victim)
    {

        if (victim.GetType() == typeof(Dragon))
        {
            //дракон получил урон
            Score += dragonHitScore;
        }
        if (victim.GetType() == typeof(Knight))
        {
            HUD.Instance.HealthBar.value = victim.Health;
        }

    */

        /* if (victim.GetType() == typeof(Dragon))
        {
            if (victim.Health > 0)
            {
                Score += dragonHitScore;
            }
            else
            {
                Score += dragonKillScore;
            }
            //Debug.Log("Dragon hit - add 10 score points.Current score " + Score);
        }

        if (victim.GetType() == typeof(Knight))
        {
            HUD.Instance.HealthBar.value = victim.Health;
            //HUD.Instance.UpdateCharacterValues(MaxHealth, knight.Speed, knight.AttackDamage);
            HUD.Instance.UpdateCharacterValues(knight.Health, knight.Speed, knight.AttackDamage);
        }
        
    }*/

   /* public void Killed(IDestructable victim)
    {
        if (victim.GetType() == typeof(Dragon))
        {
            Score += dragonKillScore;
            hero.Experience += dragonKillExperience;
            Destroy((victim as MonoBehaviour).gameObject);
        }
        if (victim.GetType() == typeof(Knight))
        {
            GameOver();
        }
    }
    */
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ExitFromCurrentLevelFound()
    {
        HUD.Instance.ShowLevelWonWindow();
        AudioManager.PlaySoundFX("Success 1");
    }

    public void GameOver()
    {
        HUD.Instance.ShowLevelLoseWindow();
        AudioManager.PlaySoundFX("Fail 1");
    }

    private void InitializeAudioManager()
    {
        audioManager.SourceSFX = gameObject.AddComponent<AudioSource>();
        audioManager.SourceMusic = gameObject.AddComponent<AudioSource>();
        audioManager.SourceRandomPitchSFX = gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioListener>();
    }

  



    // Use this for initialization
    public void StartNewLevel()
    {
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(hero);
        }
        State = GameState.Play;
        AudioManager.PlayMusic(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (OnUpdateHeroParameters != null)
        {
            OnUpdateHeroParameters(hero);
        }
    }
}
