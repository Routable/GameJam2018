using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerBase : MonoBehaviour
{
    public List<Card> playerCards;
    public List<TrapCard> trapCards;
    public WinCondition condition;
    public int amountOfCardsPerTrapCard;
    public bool playPhase;
    public bool win;
    public bool isPlayer;

    public TrapCardDb tcdb;
    public WinConditionDb wcdb;
    public GameHandler gh;

    public virtual void SetupGame()
    {
        condition = wcdb.GetNewWinCondition();
        playerCards = new List<Card>();
        trapCards = new List<TrapCard>();
        CheckWin();
    }

    private void Update()
    {
        if (playPhase)
        {
            playPhase = false;
            StartTurn();
        }
    }
    public virtual void StartTurn()
    {

    }

    //during turn
    public virtual void TryTradeCardsForTrapCard(GetEnum g)
    {
        //if (playPhase && HasEnoughCards(g.state, amountOfCardsPerTrapCard))
        //{
        //    RemoveCards(amountOfCardsPerTrapCard, g.state);
        //    GameObject tc = Instantiate(trapCardPrefab, Vector3.zero, Quaternion.identity);
        //    tc.transform.SetParent(trapCardParent);
        //    trapCards.Add(tc.GetComponent<TrapCard>().GetNewTrapCard(true));
        //}
    }

    //ending turn
    public virtual void EndTurn()
    {
        gh.PlayerTurnEnded(isPlayer);
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
        CheckWin();
    }

    //public void DiscardCard(GetEnum g)
    //{
    //    if (g.state == CardType.None)
    //        return;

    //    discardPile.discardCards.Add(new Card(g.state));
    //    CheckWinAndUpdateUI();
    //}

    protected bool HasEnoughCards(CardType ct, int targetAmount)
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
                if (playerCards.Count >= 0)
                {
                    playerCards.RemoveAt(Random.Range(0, playerCards.Count));
                }
            }
            else
            {
                if (playerCards.Where(x => x.cardType == cardType).Count() > 0)
                    playerCards.Remove(playerCards.First(x => x.cardType == cardType));
            }
            amount--;
        }
        CheckWin();
    }

    public virtual void CheckWin()
    {
        win = condition.CheckWin(playerCards);
        if (win)
        {
            gh.HandleWin(isPlayer);
        }
    }
}
