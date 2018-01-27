using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType {
    Wood,
    Water,
    Rock
}

public class Card : MonoBehaviour {
    public CardType cardType;
    public string cardName;
    public string description;
    public Sprite cardImage;
}
