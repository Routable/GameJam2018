using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayer : MonoBehaviour {
    public List<Card> playerCards = new List<Card>();
    public List<TrapCard> trapCards;
    public WinCondition condition;
    public int amountOfCardsPerTrapCard;
    public bool isTurn;
}
