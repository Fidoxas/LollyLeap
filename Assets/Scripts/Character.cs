using UnityEngine;

[System.Serializable]
public class Character
{
    public bool locked;
    public string characterName;
    public string characterLockDescription;
    public string characterDescription;
    public Sprite lockedSprite;
    public Sprite characterSprite;
    public RuntimeAnimatorController characterAnimation;
}