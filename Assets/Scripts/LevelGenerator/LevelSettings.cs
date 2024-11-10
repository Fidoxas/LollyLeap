using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit
    {
    }
}

public record LevelSettings(int PointsToPass,
    float PlayerSpeed,
    Vector2 MaxSpread,
    float Maxinterval,
    float MinTimeBetweenSpawns,
    float TimeBetweenSpawns,
    int ReductionThreshold,
    float NormalLollyChance,
    int ChanceToRespawnBubble,
    int GravityChanceToChange,
    bool GravityMode,
    bool Rushmode,
    bool Endless,
    bool Tunnel,
    bool HasSpecial,
    bool BubbleMadness,
    bool RedLolly,
    bool PinkLolly,
    bool PurpleLolly,
    bool DarkPurpleLolly,
    bool BlueLolly,
    bool TealLolly)

{
}