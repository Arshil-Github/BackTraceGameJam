using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string name;
    public bool isCompleted;
    public DialogueTrigger NPCToChange;

    public string Clue;
}
