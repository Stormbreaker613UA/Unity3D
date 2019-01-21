using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonus : MonoBehaviour
{
    [SerializeField]
    private float _healthBonus = 25;

    [SerializeField]
    private bool IsSuperHealthBonus = false;

    private const float MaxNormalHealth = 100;

    private const float UltraHeath = 200;

    private void OnTriggerEnter(Collider collider)
    {
        MainHero mainHero = collider.gameObject.GetComponent<MainHero>();

        if (mainHero != null)
        {
            AddHeath(_healthBonus, IsSuperHealthBonus);
        }
    }

    private void AddHeath(float healthBonus, bool isSuperHealthBonus)
    {
        if (isSuperHealthBonus == true)
        {
            if (GameController.Instance.Hero.Health < UltraHeath)
            {
                GameController.Instance.Hero.Health = UltraHeath;
                Destroy(this.gameObject);
                GameController.Instance.AudioManager.PlaySoundFX("Use1");
            }
        }
        else
        if (isSuperHealthBonus == false)
        {
            if (GameController.Instance.Hero.Health >= 0 && GameController.Instance.Hero.Health < MaxNormalHealth)
            {
                GameController.Instance.Hero.Health += healthBonus;

                if (GameController.Instance.Hero.Health > MaxNormalHealth)
                {
                    float temp = GameController.Instance.Hero.Health - MaxNormalHealth;
                    GameController.Instance.Hero.Health = GameController.Instance.Hero.Health - temp;
                    Destroy(this.gameObject);
                    GameController.Instance.AudioManager.PlaySoundFX("Use1");
                }

                Destroy(this.gameObject);
                GameController.Instance.AudioManager.PlaySoundFX("Use1");
            }

        }

    }
}