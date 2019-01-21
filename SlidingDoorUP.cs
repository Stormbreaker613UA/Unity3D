using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorUP : MonoBehaviour
{
    [SerializeField]
    private GameObject Door;
    [SerializeField]
    private GameObject _trigger;
    [SerializeField]
    private Animator _doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //_doorAnimator = Door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider characterCollider)
    {
        if (characterCollider.gameObject.tag == "Player")
        {
            DoorOpenUp(true);
        }
    }
    
    private void OnTriggerExit(Collider characterCollider)
    {
        if (characterCollider.gameObject.tag == "Player")
        {
            DoorOpenUp(false);
        }
    }

    private void DoorOpenUp(bool state)
    {
        _doorAnimator.SetBool("DoorOpenerIsON", state);
    }
    
    
}
