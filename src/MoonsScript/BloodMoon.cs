﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LunarAnomalies.MoonsScript;

public class BloodMoon : Moon
{
    public string name { get; set; } = "Blood Moon";
    public Color color { get; set; } = new Color(224,29,20,255);
    public GameObject moonObject { get; set; }
    public float precentageChanceSpawn { get; set; } = 10f;
    public int timeBetweenEachCall { get; set; } = 60;

    public void Init(GameObject gameObject)
    {
        moonObject = gameObject;
    }

    public void ApplyImmediateEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Random.Range(0, 2)==0)
            {
                var allEnemiesList = new List<SpawnableEnemyWithRarity>();
                allEnemiesList.AddRange(RoundManager.Instance.currentLevel.Enemies);

                // Filter the enemies based on your criteria
                var filteredEnemies = allEnemiesList.Where(x => !x.enemyType.isOutsideEnemy && !x.enemyType.spawningDisabled).ToList();

                // Randomly select one enemy from the filtered list
                SpawnableEnemyWithRarity enemyToSpawn = null;
                if (filteredEnemies.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, filteredEnemies.Count);
                    enemyToSpawn = filteredEnemies[randomIndex];
                }
                else
                {
                    // Handle the case when no enemies match the criteria
                    Plugin.Logger.LogError("No enemies match the criteria");
                    return;
                }
                GameObject SpawnEnemy = RoundManager.Instance.SpawnEnemyGameObject(
                    RoundManager.Instance.GetRandomNavMeshPositionInRadius(
                        RoundManager.Instance.allEnemyVents[
                            RandomNumberGenerator.GetInt32(
                                RoundManager.Instance.allEnemyVents.Length
                            )
                        ].transform.position,
                        3f
                    ),
                    UnityEngine.Random.RandomRangeInt(0,360),
                    RoundManager.Instance.currentLevel.Enemies.IndexOf(enemyToSpawn),
                    enemyToSpawn.enemyType
                );
            }
            else
            {
                var allEnemiesList = new List<SpawnableEnemyWithRarity>();
                allEnemiesList.AddRange(RoundManager.Instance.currentLevel.Enemies);

                // Filter the enemies based on your criteria
                var filteredEnemies = allEnemiesList.Where(x => x.enemyType.isOutsideEnemy && !x.enemyType.spawningDisabled).ToList();

                // Randomly select one enemy from the filtered list
                SpawnableEnemyWithRarity enemyToSpawn = null;
                if (filteredEnemies.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, filteredEnemies.Count);
                    enemyToSpawn = filteredEnemies[randomIndex];
                }
                else
                {
                    // Handle the case when no enemies match the criteria
                    Plugin.Logger.LogError("No enemies match the criteria");
                    return;
                }

                if (enemyToSpawn == null)
                {
                    Plugin.Logger.LogError("Didn't find an Outside enemy");
                    return;
                }
                GameObject SpawnEnemy = RoundManager.Instance.SpawnEnemyGameObject(
                    RoundManager.Instance.GetRandomNavMeshPositionInRadius(
                        RoundManager.Instance.outsideAINodes[Random.Range(0,RoundManager.Instance.outsideAINodes.Length)].transform.position,
                        10f
                    ),
                    UnityEngine.Random.RandomRangeInt(0,360),
                    RoundManager.Instance.currentLevel.Enemies.IndexOf(enemyToSpawn),
                    enemyToSpawn.enemyType
                );
            }
            
        }
    }

    public void ApplyConstantEffect()
    {
        //No Constant Effect
    }
}