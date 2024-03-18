using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Core.GameResources;
using Unity.PlasticSCM.Editor.WebApi;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            ResourceBank.ChangeResource(GameResource.Humans, 10);
            ResourceBank.ChangeResource(GameResource.Food, 5);
            ResourceBank.ChangeResource(GameResource.Wood, 5);
        }
    }
}