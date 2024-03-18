using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class GameResources : MonoBehaviour
    {
        public enum GameResource
        {
            Humans, Food, Wood, Stone, Gold, HumanUpgrader, FoodUpgrader, WoodUpgrader, StoneUpgrader, GoldUpgrader
        }
        public static class ResourceBank
        {
            private static Dictionary<GameResource, ObservableInt> Objects = new Dictionary<GameResource, ObservableInt>();
            public static void ChangeResource(GameResource type, int change)
            {
                if (Objects.ContainsKey(type))
                {
                    Objects[type].Value += change;
                } else
                {
                    Objects.Add(type, new ObservableInt(change));
                }
            } 
            public static int GetResource(GameResource type)
            {
                if (Objects.ContainsKey(type))
                {
                    return Objects[type].Value;
                }
                return 0;
            }
        }
    }
}
