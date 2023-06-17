using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<DialogueTrigger> peopleTalked;
    public int currentQuest;

    public bool capCollected = false;
    public GameObject cap;

    public void InitiateTalkQuest(DialogueTrigger trigger)
    {
        currentQuest = 1;
    }

    public void initiateCApQuest() {
        cap.SetActive(true);
    }
    public void TalkedtoanNPC(DialogueTrigger d)
    {
        if (currentQuest == 1)
        {
            if (!peopleTalked.Contains(d))
            {
                peopleTalked.Add(d);
            }
        }
    }
    public bool checkFOrCompletion(int numberOfPeople) {
        if (numberOfPeople <= peopleTalked.Count)
        {
            currentQuest = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool checkFOrCompletionCollectionQuest()
    {
        if (capCollected == true)
        {
            currentQuest = 0;
            return true;
        }
        else {
            return false;
        }
    }
}
