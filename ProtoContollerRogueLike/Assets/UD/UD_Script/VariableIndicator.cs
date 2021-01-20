using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableIndicator : MonoBehaviour
{
    SwordAttack SA;

    public bool gameIsPausedIndiactor;
    public bool gameIsLoadingIndicator;
    public bool isSwordAttacking;
    public bool moduleCheatOn;

    [Range(0f,1f)]
    public float volumeSlider;

    void Start()
    {
        SA = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordAttack>();
        gameIsPausedIndiactor = GameManagement.GameIsPaused;
        gameIsLoadingIndicator = GameManagement.GameIsLoading;
        moduleCheatOn = UI_PointInfoCheat.moduleCheatEnable;
        if (SA != null)
        {
            isSwordAttacking = SA.isAttacking;
        }
    }

    void Update()
    {
        gameIsPausedIndiactor = GameManagement.GameIsPaused;
        gameIsLoadingIndicator = GameManagement.GameIsLoading;
        moduleCheatOn = UI_PointInfoCheat.moduleCheatEnable;
        if (SA != null)
        {
            isSwordAttacking = SA.isAttacking;
        }
        volumeSlider = AudioManager.volumeSlider;
    }
}
