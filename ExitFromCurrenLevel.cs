using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitFromCurrentLevel : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        MainHero mainHero = collider.gameObject.GetComponent<MainHero>();

        if (mainHero != null)
        {
            GameController.Instance.ExitFromCurrentLevelFound(); //Герой нашел выход с уровня
        }
    }
}
