using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Core.GameResources;
using UnityEngine.UI;
using System;

namespace Core
{
    public class ProductionBuilding : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Slider slider;
        public GameResource ResourceType;
        public GameResource ResourceProdType;
        public float ProductionTime = 0;
        public float SliderTime = 0;
        public bool SliderStart = false;
        public int ProdLv = 1;
        private void Awake()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => AddResource());
            slider.value = SliderTime;
            slider.maxValue = ProductionTime;
            slider.interactable = false;
            switch (ResourceType)
            {
                case GameResource.Humans: 
                    ResourceProdType = GameResource.HumanUpgrader;
                    break;
                case GameResource.Food:
                    ResourceProdType = GameResource.FoodUpgrader;
                    break;
                case GameResource.Wood:
                    ResourceProdType = GameResource.WoodUpgrader;
                    break;
                case GameResource.Stone:
                    ResourceProdType = GameResource.StoneUpgrader;
                    break;
                case GameResource.Gold:
                    ResourceProdType = GameResource.GoldUpgrader;
                    break;
            }
        }
        private void Update()
        {
            if (ResourceBank.GetResource(ResourceProdType) != ProdLv)
            {
                ProdLv = ResourceBank.GetResource(ResourceProdType);
                ProductionTime = ProductionTime * (1f - ProdLv / 100f);
                slider.maxValue = ProductionTime;
            }

        }
        public void AddResource()
        {
            button.interactable = false;
            StartTimer();
        }
        public void StartTimer()
        {
            StartCoroutine(TimerCounter());
        }
        private IEnumerator TimerCounter()
        {
            SliderStart = true;
            while (SliderStart)
            {
                SliderTime += Time.deltaTime;
                yield return new WaitForSeconds(0.001f);
                if (SliderTime >= ProductionTime)
                {
                    SliderStart = false;
                    SliderTime = 0;
                    slider.value = 0;
                    ResourceBank.ChangeResource(ResourceType, 1);
                } 
                else
                {
                    slider.value = SliderTime;
                }
            }
            button.interactable = true;
        }
    }
}
