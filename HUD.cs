using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private Text _armorValue;

    [SerializeField]
    private Text _healthValue;

    [SerializeField]
    private Text _ammoValue;

    [SerializeField]
    private GameObject LevelWonWindow;

    [SerializeField]
    private GameObject LevelLoseWindow;

    [SerializeField]
    private GameObject InGameMenuWindow;

    [SerializeField]
    private GameObject optionsWindow;

    static private HUD _instance;

    public static HUD Instance
    {
        get { return _instance; }
    }

   

    public Text ArmorValue
    {
        get
        {
            return _armorValue;
        }

        set
        {
            _armorValue = value;
        }
    }

    public Text HealthValue
    {
        get
        {
            return _healthValue;
        }

        set
        {
            _healthValue = value;
        }
    }

    public Text AmmoValue
    {
        get
        {
            return _ammoValue;
        }

        set
        {
            _ammoValue = value;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    
    public void ShowWindow(GameObject window)
    {
        window.SetActive(true);
        GameController.Instance.State = GameState.Pause;
    }

    public void HideWindow(GameObject window)
    {
        window.SetActive(false);
        GameController.Instance.State = GameState.Play;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    public void UpdateCharacterValues(float newHealthValue,  float newArmorValue)
    {
        _healthValue.text = newHealthValue.ToString();
        _armorValue.text = newArmorValue.ToString();
    }

    public void UpdateWeaponAmmoValue(float newAmmoValue)
    {
        _ammoValue.text = newAmmoValue.ToString();
    }

    public void ButtonNext()
    {
        GameController.Instance.LoadNextLevel();
    }

    public void ButtonRestart()
    {
        GameController.Instance.RestartLevel();
    }

    public void ButtonMainMenu()
    {
        GameController.Instance.LoadMainMenu();
    }

    public void ShowLevelWonWindow()
    {
        ShowWindow(LevelWonWindow);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameController.Instance.State = GameState.Pause;
    }

    public void ShowLevelLoseWindow()
    {
        ShowWindow(LevelLoseWindow);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameController.Instance.State = GameState.Pause;
    }

   /* public void LoadInventory()
    {
        InventoryUsedCallback callback = new InventoryUsedCallback(GameController.Instance.InventoryItemUsed);
        for (int i = 0; i < GameController.Instance.Inventory.Count; i++)
        {
            InventoryUIButton newItem = AddNewInventoryItem(GameController.Instance.Inventory[i]);
            newItem.Callback = callback;
        }
    }
 */   
    private void HandleOnUpdateHeroParameters(HeroParameters parameters)
    {
       UpdateCharacterValues(parameters.Health, parameters.Armor);
    }

    // Use this for initialization
    private void Start ()
    {
        GameController.Instance.OnUpdateHeroParameters += HandleOnUpdateHeroParameters;
        GameController.Instance.StartNewLevel();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnUpdateHeroParameters -= HandleOnUpdateHeroParameters;
    }

    public void SetSoundVolume(Slider slider)
    {
        GameController.Instance.AudioManager.SfxVolume = slider.value;
    }
    public void SetMusicVolume(Slider slider)
    {
        GameController.Instance.AudioManager.MusicVolume = slider.value;
    }

    // Update is called once per frame
    void Update ()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check whether it's active / inactive
            bool isActive = InGameMenuWindow.activeSelf;
            
            InGameMenuWindow.SetActive(!isActive);

            if (!isActive)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameController.Instance.State = GameState.Pause;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                GameController.Instance.State = GameState.Play;
            }


        }
        //if (Cursor.visible == true) Debug.Log("Cursor is visible"); // for debug
    }
}
