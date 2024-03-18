using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Core.GameResources;

namespace Core
{
    public class ProductionLv : MonoBehaviour
    {
        public GameResource ResourceType;
        [SerializeField] private Button button;
        public TextMeshProUGUI Text;
        public int level = 1;
        private void Awake()
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => ResourceBank.ChangeResource(ResourceType, 1));
            Text = Text.GetComponent<TextMeshProUGUI>();
            ResourceBank.ChangeResource(ResourceType, 1);
        }
        private void Update()
        {
            if (ResourceBank.GetResource(ResourceType) != level)
            {
                level = ResourceBank.GetResource(ResourceType);
                Text.text = "LV: " + level.ToString();
            }
        }
    }
}
