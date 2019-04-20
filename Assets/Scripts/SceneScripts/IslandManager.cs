using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : LevelManager, IObserver {

    private int gemCount = 0;
    public GameObject gems;


    private void Start()
    {
        initGemObservers();
    }

    private void initGemObservers()
    {
        foreach(Transform child in gems.transform)
        {
            child.GetComponent<Gem>().setObserver(this);
            gemCount++;
        }
    }

    public void Notify<T>(T t)
    {
        gemCount--;
        checkWinCondition();
    }

    private void checkWinCondition()
    {
        if(gemCount == 0)
        {
            Debug.Log("Win");
        }
    }
}
