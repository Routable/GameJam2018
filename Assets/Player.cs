using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {
    public List<Card> playerCards;
    public List<TrapCard> trapCards;
    public List<Player> otherPlayers;
    public WinCondition condition;
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
            DoTrade(g.state, amountOfCardsPerTrapCard);
    }

    //ending turn
    private void EndTurn()
    {
        GiveCard();
    }

    private void DoTrade(CardType ct, int amount)
    {
        RemoveCards(amount, ct);
        trapCards.Add(new TrapCard());
    }

    private void DrawCard()
    {
        Card card = new Card();
        //Card animations and stuff here
        playerCards.Add(card);
    }

    private void GiveCard(Player player)
    {
        //give a card to someone here
    }

    private bool HasEnoughCards(CardType ct, int targetAmount)
    {
        if (playerCards.Where(x => x.cardType == ct).Count() >= targetAmount)
        {
            return true;
        }
        return false;
    }

    private void RemoveCards(int amount, CardType cardType = CardType.None)
    {
        //remove cards here
    }
}
