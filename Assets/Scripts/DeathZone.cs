using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;
    public StartMenuManager menuManager;

    private void Awake()
    {
        menuManager = GameObject.Find("Start Menu Manager").GetComponent<StartMenuManager>();
        Manager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);       
        Manager.GameOver();
    }
}
