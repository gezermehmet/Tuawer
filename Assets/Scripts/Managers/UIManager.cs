using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Tower;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Slider expBar;
    public Slider hpBar;
    private TowerController _towerController;
    private EnemyBase _enemyBase;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateHpBar(float hpBarValue)
    {
        hpBar.value = hpBarValue;
    }

    public void UpdateExpBar()
    {
        expBar.value++;
    }
}