using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Quiz/Level Data")]
public class LevelData : ScriptableObject
{
    public QuestionData[] questions; // Sorular dizisi
}
