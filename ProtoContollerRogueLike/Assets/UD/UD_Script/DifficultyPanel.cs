using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty { Easy, Normal, Hard }
public class DifficultyPanel : MonoBehaviour
{
    public static Difficulty currentDifficulty;
    public static int currentStage;

    [Header("Difficulty HP multiplier for All Mob")]
    [HideInInspector] public float currentModHPMultiplier;
    [SerializeField] private float easyModHPMultiplier;
    [SerializeField] private float normalModHPMultiplier;
    [SerializeField] private float hardModHPMultiplier;

    [Header("Difficulty Damage multiplier for All Mob")]
    [HideInInspector] public float currentModDamageMultiplier;
    [SerializeField] private float easyModDamageMultiplier;
    [SerializeField] private float normalModDamageMultiplier;
    [SerializeField] private float hardModDamageMultiplier;
    
    [Header("HP bonus for Mob per Stage")]
    [HideInInspector] public float currentStageHPBonusForZombie;
    public float stageHPBonusForZombie;
    [HideInInspector] public float currentStageHPBonusForSpider;
    public float stageHPBonusForSpider;
    [HideInInspector] public float currentStageHPBonusForSummoner;
    public float stageHPBonusForSummoner;

    [Header("Damage bonus for Mob per Stage")]
    [HideInInspector] public float currentStageDamageBonusForZombie;
    public float stageDamageBonusForZombie;
    [HideInInspector] public float currentStageDamageBonusForSpider;
    public float stageDamageBonusForSpider;

    [Header("Spawn rate bonus for Summoner per Stage")]
    [HideInInspector] public float currentStageSpawnRateBonusForSummoner;
    public float stageSpawnRateBonusForSummoner;

    private void Start()
    {
        currentStageHPBonusForZombie = 0;
        currentStageHPBonusForSpider = 0;
        currentStageHPBonusForSummoner = 0;

        currentStageDamageBonusForZombie = 0;
        currentStageDamageBonusForSpider = 0;

        currentStageSpawnRateBonusForSummoner = 0;
    }

    private void Update()
    {
        if(currentDifficulty == Difficulty.Easy)
        {
            currentModHPMultiplier = easyModHPMultiplier;
            currentModDamageMultiplier = easyModDamageMultiplier;
        }
        if(currentDifficulty == Difficulty.Normal)
        {
            currentModHPMultiplier = normalModHPMultiplier;
            currentModDamageMultiplier = normalModDamageMultiplier;
        }
        if(currentDifficulty == Difficulty.Hard)
        {
            currentModHPMultiplier = hardModHPMultiplier;
            currentModDamageMultiplier = hardModDamageMultiplier;
        }
    }

    public void StageUpDifficultyIncreased(int levelID)
    {
        //HP Upgrade
        currentStageHPBonusForZombie = stageHPBonusForZombie*levelID;
        currentStageHPBonusForSpider = stageHPBonusForSpider*levelID;
        currentStageHPBonusForSummoner = stageHPBonusForSummoner*levelID;
        
        //Damage Upgrade
        currentStageDamageBonusForZombie = stageDamageBonusForZombie * levelID;
        currentStageDamageBonusForSpider = stageDamageBonusForSpider * levelID;

        //Spawn Rate Upgrade
        currentStageSpawnRateBonusForSummoner = stageSpawnRateBonusForSummoner * levelID;
    }
}