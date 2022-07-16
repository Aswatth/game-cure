using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Character", menuName ="Character")]
public class Character : ScriptableObject
{
    public GameObject prefab;
    public int maxHealth;
    public int costToUnlock;
    public bool isUnlocked = false;
    public bool isEquipped = false;
}
