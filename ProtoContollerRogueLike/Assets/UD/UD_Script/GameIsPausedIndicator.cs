using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIsPausedIndicator : MonoBehaviour
{
    public bool gameIsPausedIndiactor;

    void Start()
    {
        gameIsPausedIndiactor = GameManagement.GameIsPaused;
    }

    void Update()
    {
        gameIsPausedIndiactor = GameManagement.GameIsPaused;
    }
}
