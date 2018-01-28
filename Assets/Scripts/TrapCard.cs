using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrapCard : MonoBehaviour {
    public TrapCardType trapCardType;
    public string trapCardText;
    private Player player;
    private Enemy enemy;

    public TrapCard GetCopy()
    {
        return (TrapCard)this.MemberwiseClone();
    }

    public void SetValues(TrapCard tc)
    {
        trapCardText = tc.trapCardText;
        trapCardType = tc.trapCardType;
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
            player.trapCards.Remove(this);
        }
        else {
            user = enemy;
            target = player;
            enemy.trapCards.Remove(this);
        }

        switch (trapCardType)
        {
            case TrapCardType.Add2Cards:
                user.DrawCard(2);
                player.CheckWin();
                enemy.CheckWin();
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

            case TrapCardType.Discard5Draw5:
                int _count = user.playerCards.Count;
                if (_count < 5)
                {
                    user.playerCards.Clear();
                    user.DrawCard(_count);
                }
                else
                {
                    user.playerCards.Shuffle();
                    user.playerCards.RemoveRange(0, 5);
                    user.DrawCard(5);
                }
                player.CheckWin();
                enemy.CheckWin();                
                break;

            case TrapCardType.Swap3:
                int _min = user.playerCards.Count < target.playerCards.Count ? user.playerCards.Count : target.playerCards.Count;
                int _amount = Mathf.Clamp(_min, 0, 3);

                user.playerCards.Shuffle();
                List<Card> _cards = user.playerCards.GetRange(0, _amount);
                user.playerCards.RemoveRange(0, _amount);
                target.playerCards.Shuffle();
                List<Card> _cards1 = target.playerCards.GetRange(0, _amount);
                user.playerCards.AddRange(_cards1);
                target.playerCards.RemoveRange(0, _amount);
                target.playerCards.AddRange(_cards);

                player.CheckWin();
                enemy.CheckWin();                
                break;

            default:
                Debug.LogError("Danny you are dumb");
                break;
        }

        if (gameObject != null)
            Destroy(gameObject);
    }
}