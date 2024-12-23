using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionData", menuName = "Quiz/Question Data")]
public class QuestionData : ScriptableObject
{
    [TextArea]
    public string questionText; // Soru metni

    public string[] options; // Cevap şıkları
    public int correctOptionIndex; // Doğru cevabın indeksi
}

