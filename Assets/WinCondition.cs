using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WinCondition : MonoBehaviour {
    public List<Card> neededCards;

    public bool CheckWin(List<Card> cards)
    {
        bool win = false;
        var cardTypes = CardType.GetValues(typeof(CardType)).Cast<CardType>();

        foreach (CardType ct in cardTypes)
        {
            win = CheckEnoughCardsOfType(ct, cards);
            if (!win)
            {
                return false;
            }
        }
        if (win)
        {
            return true;
        }
        return false;
    }

    private bool CheckEnoughCardsOfType(CardType cardType, List<Card> cards)
    {
        if (cards.Where(x => x.cardType == cardType).Count() >= neededCards.Where(x => x.cardType == cardType).Count())
        {
            return true;
        }
        return false;
    }
}
