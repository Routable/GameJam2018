using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionDb : MonoBehaviour {
    public List<WinCondition> winConditions = new List<WinCondition>();

    public WinCondition GetNewWinCondition()
    {
        WinCondition wc = winConditions[Random.Range(0, winConditions.Count)];
        return wc;
    }

    private void Awake()
    {
        foreach (WinCondition wc in transform.GetComponentsInChildren<WinCondition>())
        {
            winConditions.Add(wc);
        }
    }
}
