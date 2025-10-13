using System;
using System.Globalization;
using Enemies;
using Tower;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;


namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager İnstance;
        private TowerStats _towerStats;
        private TowerController _towerController;
        private EnemyBase _enemyBase;
        public Transform expBar;
        public Transform hpBar;
        public TMPro.TextMeshProUGUI hpText;
        public TMPro.TextMeshProUGUI expText;
        public Button hpButton;


        private void Awake()
        {
            if (İnstance == null)
            {
                İnstance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            hpButton.onClick.AddListener(TowerStats);
        }


        public void UpdateHpBar(float hpBarValue)
        {
            hpBar.GetComponent<UnityEngine.UI.Slider>().value = hpBarValue;
            hpText.text = "HP: " + (hpBarValue * 100).ToString(CultureInfo.InvariantCulture);
        }

        public void UpdateExpBar(float expBarValue)
        {
            expBar.GetComponent<UnityEngine.UI.Slider>().value = expBarValue;
            expText.text = "EXP: " + (expBarValue * 100).ToString(CultureInfo.InvariantCulture);
        }

        public void TowerStats()
        {
            _towerStats.IncreaseMaxHp(_towerController.hpIncrease);
        }
    }
}