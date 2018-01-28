using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public enum CardType {
    None = 0,
    Wood,
    Water,
    Rock
}

[System.Serializable]
public class Card {
    public CardType cardType;
    public string cardName;
    public string cardDescription;
    public Sprite cardImage;

    public Card(CardType type = CardType.None, string name = "Default", string description = "default", Sprite image = null)
    {
        if (type == CardType.None)
        {
            cardType = (CardType)Random.Range(1, 4);
        }
        else
        {
            cardType = type;
        }

        cardName = name;
        cardDescription = description;
        cardImage = image;
    }
}
