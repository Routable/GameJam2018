using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//player's turn state
public enum TurnState {
    StartOfTurn,
    MiddleTurn,
    EndTurn
}

public class Player : MonoBehaviour {
    public List<Card> playerCards;
    public List<TrapCard> trapCards;
    public List<Player> otherPlayers;
    public WinCondition condition;
    public int amountOfCardsPerTrapCard;
    public bool isTurn;
    public TurnState state;

    public void TradeCardsForTrapCard(CardType ct, int amount)
    {
        if (playerCards.Where(x => x.cardType == ct).Count() >= amount)
        {
            AddNewTrapCardToInventory();
        }
    }

    private void AddNewTrapCardToInventory()
    {
        trapCards.Add(new TrapCard());
    }

}
