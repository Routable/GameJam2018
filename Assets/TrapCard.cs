using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapCardType
{
    AddThreeCards,
    StealEnemyResource
}

public class TrapCard : MonoBehaviour {
    TrapCardType trapCardType;
    public Player player;
    public Enemy enemy;
    public bool isPlayer;

    public TrapCard GetNewTrapCard(bool player = false) {
        //TODO: fancy math for weighted cards?
        //===============================================================TSIFSEFJSELIFJSELIJ THIS IS BROKEN
        Array values = Enum.GetValues(typeof(TrapCardType));
        System.Random random = new System.Random();
        trapCardType = (TrapCardType)values.GetValue(random.Next(values.Length - 1) + 1);
        isPlayer = player;
        return (TrapCard)this.MemberwiseClone();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    public void UseCard()
    {
        if (isPlayer)
        {
            switch (trapCardType)
            {
                case TrapCardType.AddThreeCards:
                    player.DrawCard(3);
                    break;

                case TrapCardType.StealEnemyResource:
                    if (enemy.playerCards.Count >= 1)
                    {
                        int r = UnityEngine.Random.Range(0, enemy.playerCards.Count);
                        player.playerCards.Add(enemy.playerCards[r]);
                        enemy.playerCards.RemoveAt(r);
                        player.CheckWinAndUpdateUI();
                        enemy.CheckWinAndUpdateUI();
                    }
                    break;

                default:
                    Debug.LogError("Danny you are dumb");
                    break;
            }
        }

        if (!isPlayer)
        {
            switch (trapCardType)
            {
                case TrapCardType.AddThreeCards:
                    enemy.DrawCard(3);
                    break;

                case TrapCardType.StealEnemyResource:
                    if (player.playerCards.Count >= 1)
                    {
                        int r = UnityEngine.Random.Range(0, player.playerCards.Count);
                        enemy.playerCards.Add(player.playerCards[r]);
                        player.playerCards.RemoveAt(r);
                        player.CheckWinAndUpdateUI();
                        enemy.CheckWinAndUpdateUI();
                    }
                    break;

                default:
                    Debug.LogError("Danny you are dumb");
                    break;
            }
        }

        Destroy(this.gameObject);
    }
}
