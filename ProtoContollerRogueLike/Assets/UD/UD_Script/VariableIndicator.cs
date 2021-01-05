using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableIndicator : MonoBehaviour
{
    public bool gameIsPausedIndiactor;
    public bool gameIsLoadingIndicator;

    void Start()
    {
        gameIsPausedIndiactor = GameManagement.GameIsPaused;
        gameIsLoadingIndicator = GameManagement.GameIsLoading;
    }

    void Update()
    {
        gameIsPausedIndiactor = GameManagement.GameIsPaused;
        gameIsLoadingIndicator = GameManagement.GameIsLoading;
    }
}
