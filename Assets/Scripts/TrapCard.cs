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

    public TrapCard GetNewTrapCard(bool player = false) {
        //TODO: fancy math for weighted cards?

        int randType = Random.Range(0, TrapCardType.GetValues(typeof(TrapCardType)).Length);
        trapCardType = (TrapCardType)randType;
        return (TrapCard)this.MemberwiseClone();
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
    }

    public void UseCard(bool isPlayer)
    {
        PlayerBase user;
        PlayerBase target;

        if (isPlayer)
        {
            user = player;
            target = enemy;
        }
        else {
            user = enemy;
            target = player;
        }

        switch (trapCardType)
        {
            case TrapCardType.AddThreeCards:
                user.DrawCard(3);
                break;

            case TrapCardType.StealEnemyResource:
                if (target.playerCards.Count >= 1)
                {
                    int r = Random.Range(0, target.playerCards.Count);
                    user.playerCards.Add(target.playerCards[r]);
                    target.playerCards.RemoveAt(r);
                    player.CheckWin();
                    enemy.CheckWin();
                }
                break;

            default:
                Debug.LogError("Danny you are dumb");
                break;
        }

        Destroy(this.gameObject);
    }
}
