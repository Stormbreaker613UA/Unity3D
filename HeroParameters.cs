using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeroParameters
{
    #region Private_Variables
    [SerializeField]
    private float _health = 100;
    
    [SerializeField]
    private float _armor = 0;
    
    #endregion

    #region Public_Properties

    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }

    public float Armor
    {
        get
        {
            return _armor;
        }

        set
        {
            _armor = value;
        }
    }
    #endregion
    
    
    
    void Start ()
    {
		
	}
	
	
	void Update ()
    {
		
	}
}
