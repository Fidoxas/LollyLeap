using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "level_generator_choose", menuName = "Scriptable Objects/Level Genetor Chooser")]
public class LevelGeneratorChooserScriptableObject : ScriptableObject
{
    public List<LevelGeneratorScriptableObject> generators;
    public string modeIdentifier;
    public Sprite BackGround;
    public Color backgroundGrassColor;
    
    public int GetLevelForMode(int mode)
    {
        // Implementacja
        return 0; // Zwróć odpowiednią wartość
    }

    public (LevelGeneratorScriptableObject, int) ChooseGenerator(int level)
    {
        int passedLevels = 0;

        foreach (var generator in generators)
        {
            if (passedLevels + generator.maxLevelsToGenerate > level)
            {
                return (generator, passedLevels);
            }

            passedLevels += generator.maxLevelsToGenerate;
        }

        throw new System.Exception("Could not find generator for level " + level);
    }

    public LevelSettings GenerateLevel(int level)
    {
        (LevelGeneratorScriptableObject generator, int levelsToSkip) = ChooseGenerator(level);
        //Debug.Log("Level " + level + " - skip " + levelsToSkip);
        return generator.GenerateLevel(level - levelsToSkip);
    }
}