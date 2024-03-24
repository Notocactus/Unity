using UnityEngine;
using System.Collections.Generic;
using Card;

public class GameCore : MonoBehaviour
{
    [SerializeField] private List<CardLayout> cardLayouts;
    [SerializeField] private List<CardAsset> cardAssets;
    [SerializeField] private CardLayout center;
    [SerializeField] private CardLayout bucket;
    [SerializeField] private int handCapacity;


    private void Start()
    {
        int id = 0;
        foreach (var layout in cardLayouts)
        {
            layout.LayoutId = id;
            ++id;
        }
        center.LayoutId = id;
        bucket.LayoutId = id + 1;

        CardGame.Instance.Init(cardAssets, cardLayouts, center, bucket, handCapacity);
    }

    public void StartTurn()
    {
        CardGame.Instance.StartTurn();
    }
}

