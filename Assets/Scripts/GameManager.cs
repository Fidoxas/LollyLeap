using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string NeedinstructionKey = "Needinstruction";
    public static bool Needinstruction = true;

    private void Awake()
    {
        LoadNeedinstruction();
    }

    private void OnApplicationQuit()
    {
        SaveNeedinstruction();
    }

    private void LoadNeedinstruction()
    {
        if (PlayerPrefs.HasKey(NeedinstructionKey))
        {
            Needinstruction = PlayerPrefs.GetInt(NeedinstructionKey) == 1;
        }
        else
        {
            Needinstruction = true;
        }
    }

    private void SaveNeedinstruction()
    {
        int needinstructionValue = Needinstruction ? 1 : 0;
        PlayerPrefs.SetInt(NeedinstructionKey, needinstructionValue);
        PlayerPrefs.Save();
    }
}