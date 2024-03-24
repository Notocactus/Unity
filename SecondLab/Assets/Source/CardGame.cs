using System;
using System.Collections.Generic;
using System.Linq;
using Card;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class CardGame
{
    private static CardGame game;
    private static int cardNameCounter = 1;
    public int HandCapacity;
    private List<CardAsset> StartCardList;
    public List<CardLayout> ListLayout = new();
    private readonly Dictionary<CardInstance, CardView> _cardDictionary = new();
    private CardLayout beat;
    public CardLayout center;

    public static CardGame Instance
    {
        get
        {
            if (game == null)
            {
                game = new CardGame();
            }
            return game;
        }
    }

    public void Init(List<CardAsset> cardAssets, List<CardLayout> cardLayouts, CardLayout center, CardLayout beated, int handCapacity)
    {
        ListLayout = cardLayouts;

        StartCardList = cardAssets;

        this.HandCapacity = handCapacity;

        this.center = center;

        this.beat = beated;

        StartGame();
    }

    private void StartGame()
    {
        foreach (var layout in ListLayout)
        {
            foreach (var startCard in StartCardList)
            {
                CreateCard(startCard, layout.LayoutId);
            }
        }
    }

    private void CreateCard(CardAsset asset, int layout)
    {
        var instance = new CardInstance(asset)
        {
            LayoutId = layout,
            CardPosition = ListLayout[layout].cardsCount++
        };
        CreateCardView(instance);
        MoveToLayout(instance, layout);
    }

    private void CreateCardView(CardInstance instance)
    {
        GameObject newCardInstance = new GameObject($"{instance.GetCardAsset.name} {cardNameCounter}");

        ++cardNameCounter;

        CardView cardView = newCardInstance.AddComponent<CardView>();
        Image image = newCardInstance.AddComponent<Image>();

        cardView.Init(instance, image);

        Button button = newCardInstance.AddComponent<Button>();

        button.onClick.AddListener(cardView.PlayCard);

        newCardInstance.transform.SetParent(ListLayout[instance.LayoutId].transform);

        _cardDictionary[instance] = cardView;
    }

    private void RecalculateLayout(int layoutId)
    {
        var cards = GetCardsInLayout(layoutId);

        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].getCardPosition = i;
        }
    }

    private void MoveToLayout(CardInstance card, int layout)
    {
        int currentCardLayout = card.LayoutId;
        card.LayoutId = layout;

        _cardDictionary[card].transform.SetParent(ListLayout[layout].transform);

        RecalculateLayout(currentCardLayout);
        RecalculateLayout(layout);
    }

    public void MoveToCenter(CardInstance card)
    {
        int currentCardLayout = card.LayoutId;

        card.LayoutId = center.LayoutId;

        _cardDictionary[card].transform.SetParent(center.transform);

        RecalculateLayout(currentCardLayout);
        RecalculateLayout(center.LayoutId);
    }

    public void MoveToBeated(CardInstance card)
    {
        int currentCardLayout = card.LayoutId;
        card.LayoutId = beat.LayoutId;
        _cardDictionary[card].transform.SetParent(beat.transform);

        try
        {
            Button button = _cardDictionary[card].GetComponent<Button>();
            button.enabled = false;
            button.onClick.RemoveAllListeners();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        RecalculateLayout(currentCardLayout);
        RecalculateLayout(beat.LayoutId);
    }

    public List<CardView> GetCardsInLayout(int id)
    {
        return _cardDictionary.Where(x => x.Key.LayoutId == id).Select(x => x.Value).ToList();
    }

    private List<CardInstance> GetLayoutInstances(int id)
    {
        return _cardDictionary.Where(x => x.Key.LayoutId == id).Select(x => x.Key).ToList();
    }

    private void ShuffleLayout(int id)
    {
        var cards = GetLayoutInstances(id);

        List<(int, int)> pairs = new();
        for (int i = 0; i < cards.Count; ++i)
        {
            for (int j = i + 1; j < cards.Count; ++j)
            {
                pairs.Add((i, j));
            }
        }

        Random rnd = new();
        pairs = pairs.OrderBy(_ => rnd.Next()).ToList();

        for (var i = 1; i < cards.Count; ++i)
        {
            var pair_item = pairs[i].Item1;
            var item = cards[pair_item];
            _cardDictionary[item].transform.SetSiblingIndex(pairs[i].Item2);
        }
    }

    public void StartTurn()
    {
        foreach (var layout in ListLayout)
        {
            ShuffleLayout(layout.LayoutId);

            layout.FaceUp = true;

            var cards = GetCardsInLayout(layout.LayoutId);

            for (int i = 0; i < HandCapacity; ++i)
            {
                cards[i].TypeOfLayout = 2;
            }
        }
    }
}