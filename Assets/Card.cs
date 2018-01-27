using System;
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

public class Card : MonoBehaviour {
    public CardType cardType;
    public string cardName;
    public string description;
    public Sprite cardImage;

    public Card()
    {
        Array values = Enum.GetValues(typeof(CardType));
        System.Random random = new System.Random();
        //account for default enum type
        cardType = (CardType)values.GetValue(random.Next(values.Length - 1) + 1);
    }
}
