using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<Card> playerCards;
    public List<TrapCard> trapCards;
    public List<Player> otherPlayers;
    public WinCondition condition;
    public DiscardPile discardPile;
    public int amountOfCardsPerTrapCard;
    public bool isTurn;
    private bool playPhase;

    //start of turn
    public void StartPlayerTurn()
    {
        DrawCard();
        playPhase = true;
    }

    //during turn
    public void TryTradeCardsForTrapCard(GetEnum g)
    {
        if (playPhase && HasEnoughCards(g.state, amountOfCardsPerTrapCard))
        {
            RemoveCards(amountOfCardsPerTrapCard, g.state);
            trapCards.Add(new TrapCard());
        }
    }

    //ending turn
    private void EndTurn()
    {
        //GiveCard();
    }

    public void DrawCard(int amount = 1)
    {
        while (amount > 0)
        {
            Card card = new Card();
            //Card animations and stuff here
            playerCards.Add(card);
            amount--;
        }
    }

    public void DiscardCard(GetEnum g)
    {
        if (g.state == CardType.None)
            return;

        discardPile.discardCards.Add(new Card(g.state));
    }

    private bool HasEnoughCards(CardType ct, int targetAmount)
    {
        if (playerCards.Where(x => x.cardType == ct).Count() >= targetAmount)
        {
            return true;
        }
        return false;
    }

    public void RemoveCards(int amount, CardType cardType = CardType.None)
    {
        while (amount > 0)
        {
            //random
            if (cardType == CardType.None)
            {
                playerCards.RemoveAt(Random.Range(0, playerCards.Count));
            }
            else
            {

            }
            amount--;
        }
    }
}
