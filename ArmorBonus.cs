using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBonus : MonoBehaviour
{
    [SerializeField]
    private float _armorBonus = 25;

    [SerializeField]
    private bool IsSuperArmorBonus = false;

    private const float MaxNormalArmor = 100;

    private const float UltraArmor = 200;

    private void OnTriggerEnter(Collider collider)
    {
        MainHero mainHero = collider.gameObject.GetComponent<MainHero>();

        if (mainHero != null)
        {
            AddArmor(_armorBonus, IsSuperArmorBonus);
        }
    }
    
    private void AddArmor(float armorBonus, bool isSuperArmorBonus)
    {
        if (isSuperArmorBonus == true)
        {
            if (GameController.Instance.Hero.Armor < UltraArmor)
            {
                GameController.Instance.Hero.Armor = UltraArmor;
                Destroy(this.gameObject);
                GameController.Instance.AudioManager.PlaySoundFX("Use1");
            }
        }

        if (isSuperArmorBonus == false)
        {
            if (GameController.Instance.Hero.Armor >= 0 && GameController.Instance.Hero.Armor < MaxNormalArmor)
            {
                GameController.Instance.Hero.Armor += armorBonus;
                if (GameController.Instance.Hero.Armor > MaxNormalArmor)
                {
                    float temp = GameController.Instance.Hero.Armor - MaxNormalArmor;
                    GameController.Instance.Hero.Armor = GameController.Instance.Hero.Armor - temp;
                    Destroy(this.gameObject);
                    GameController.Instance.AudioManager.PlaySoundFX("Use1");
                }
                Destroy(this.gameObject);
                GameController.Instance.AudioManager.PlaySoundFX("Use1");
            }

        }

    }
}
