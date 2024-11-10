using UnityEngine;

[CreateAssetMenu]
public class StageDatabase
{
    public Stages[] stages;

    public int Stagecount
    {
        get { return stages.Length; }
    }

    public Stages GetStage(int index)
    {
        return stages[index];
    }
}