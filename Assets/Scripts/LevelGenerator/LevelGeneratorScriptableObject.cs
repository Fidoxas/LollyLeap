using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "level_generator", menuName = "Scriptable Objects/Level Generator")]
public class LevelGeneratorScriptableObject : ScriptableObject
{
    public int maxLevelsToGenerate;
    public int pointsToPassLv;
    public float startPlayerSpeed; // 5.0f
    public float modPlayerSpeed; // 0.1f
    public Vector2 startMaxSpread;
    public Vector2 modMaxSpread;
    [FormerlySerializedAs("Maxinterval")] public float maxinterval;

    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public float startTimeBetweenSpawns;
    public float modStartTimeBetweenSpawns;

    [FormerlySerializedAs("ReductionThreshold")]
    public int reductionThreshold;

    [FormerlySerializedAs("ChanceToRespawnBubble")]
    public int chanceToRespawnBubble;
    public int modBubbleSpawn;
    public int gravityChanceToChange;
    
    public bool gravityMode;
    public bool rushmode;
    public bool endless;
    public bool tunnel;
    public bool hasSpecial;
    public bool bubbleMadness;

    public int normalLollyChance;

    //public int SpecialLollyChance;
   public float modNormalLollyChance;
   public bool redLolly;
   public bool pinkLolly;
   public bool purpleLolly;
   public bool darkPurpleLolly;
   public bool blueLolly;
   public bool tealLolly;


    public LevelSettings GenerateLevel(int level)
    {
        if (level < 1)
        {
            throw new Exception("Level needs to be at least 1");
        }

        if (level > maxLevelsToGenerate)
        {
            throw new Exception("Level needs to be max " + maxLevelsToGenerate);
        }

        int modifier = level - 1;

        //float playerSpeed = startPlayerSpeed * (1 + modPlayerSpeed);
        float spawnTime = startTimeBetweenSpawns - modStartTimeBetweenSpawns * modifier;
        spawnTime = Mathf.Clamp(spawnTime, minTimeBetweenSpawns, maxTimeBetweenSpawns);

        float playerSpeed = startPlayerSpeed + modPlayerSpeed * modifier;
        Vector2 maxSpread = startMaxSpread + (modMaxSpread * modifier);
        float maxinterval = this.maxinterval;
        float normalLollyChance = this.normalLollyChance - modNormalLollyChance * modifier;
        int reductionThreshold = this.reductionThreshold - modifier / 2;
        int chanceToRespawnBubble = this.chanceToRespawnBubble + modBubbleSpawn * modifier;

        return new LevelSettings(
            PointsToPass: pointsToPassLv,
            PlayerSpeed: playerSpeed,
            MaxSpread: maxSpread,
            Maxinterval: maxinterval,
            MinTimeBetweenSpawns: minTimeBetweenSpawns,
            TimeBetweenSpawns: spawnTime,
            ReductionThreshold: reductionThreshold,
            NormalLollyChance: normalLollyChance,
            //SpecialLollyChance: specialLollyChance,
            ChanceToRespawnBubble: chanceToRespawnBubble,
            GravityChanceToChange :gravityChanceToChange,
            GravityMode: gravityMode,
            Rushmode: rushmode,
            Endless: endless,
            Tunnel: tunnel,
            HasSpecial: hasSpecial,
            BubbleMadness: bubbleMadness,
            RedLolly: redLolly,
            PinkLolly: pinkLolly,
            PurpleLolly: purpleLolly,
            DarkPurpleLolly: darkPurpleLolly,
            BlueLolly: blueLolly,
            TealLolly: tealLolly
        );
    }
}