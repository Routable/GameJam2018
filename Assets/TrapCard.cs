using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapCardType
{
    AddThreeCards
}

public class TrapCard : MonoBehaviour {
    TrapCardType trapCardType;
    public Player player;
    public Enemy enemy;
    public bool isPlayer;

    public TrapCard GetNewTrapCard(bool player = false) {
        //TODO: fancy math for weighted cards?
        Array values = Enum.GetValues(typeof(TrapCardType));
        System.Random random = new System.Random();
        trapCardType = (TrapCardType)values.GetValue(random.Next(values.Length));
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

                default:
                    Debug.LogError("Danny you are dumb");
                    break;
            }
        }

        Destroy(this.gameObject);
    }
}
