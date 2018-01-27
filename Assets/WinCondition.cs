using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WinCondition : MonoBehaviour {
    public List<Card> neededCards;

    public bool CheckWin(List<Card> cards)
    {
        bool win = false;
        int wood = cards.Where(x => x.cardType == CardType.Wood).Count();
        int water = cards.Where(x => x.cardType == CardType.Water).Count();
        int rock = cards.Where(x => x.cardType == CardType.Rock).Count();

        if (HasMoreOfTypeThanNeeded())
    }
}
