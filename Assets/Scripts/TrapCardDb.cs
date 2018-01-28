using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapCardType
{
    Add2Cards,
    StealEnemyResource,
    Discard5Draw5,
    Swap3
}

public class TrapCardDb : MonoBehaviour {
    public List<TrapCard> allTrapCards = new List<TrapCard>();

    public TrapCard GetTrapCardValues()
    {
        TrapCard tc = allTrapCards[Random.Range(0, allTrapCards.Count)];
        return tc.GetCopy();
    }

    private void Awake()
    {
        foreach (TrapCard tc in transform.GetComponentsInChildren<TrapCard>())
        {
            allTrapCards.Add(tc);
        }
    }
}
