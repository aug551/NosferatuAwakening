using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : Item
{
    [SerializeField] MaterialType type;
    public enum MaterialType
    {
        Stone, Metal, Leather, ore
    }
}
