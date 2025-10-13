using System;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Tower
{
    public class TowerStats : MonoBehaviour
    {
        private TowerController _towerController;
        [SerializeField] private float towerHp;


        private void Start()
        {
            towerHp = _towerController.GetHp();
        }


        public void IncreaseMaxHp(float amount)
        {
            towerHp += amount;
            _towerController.SetHp(towerHp);
        }
    }
}