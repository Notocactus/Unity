using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Core.GameResources;

namespace Core
{
    public class ResourceVisual : MonoBehaviour
    {
        public GameResource ResourceType;
        int count = 0;
        public TextMeshProUGUI text;
        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }
        private void Update()
        {
            if (ResourceBank.GetResource(ResourceType) > count)
            {
                count = ResourceBank.GetResource(ResourceType);
                text.text = count.ToString();
            }
        }
    }
}
