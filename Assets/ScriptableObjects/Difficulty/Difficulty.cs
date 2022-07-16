using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Difficulty",menuName ="DifficultySetting")]
public class Difficulty : ScriptableObject
{
    public int populationCount;
    public int infectedPercentage;
    public int difficultyBonus;
}
