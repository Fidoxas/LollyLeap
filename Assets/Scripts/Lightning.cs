using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lightning : MonoBehaviour
{
    [SerializeField] public Type type;


    public enum Type
    {
        Teal,
        Pink,
        Red,
        Blue,
        Purple,
        Darkpurple,
    }
}