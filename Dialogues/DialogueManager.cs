using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private List<Quest> currentQuests;
    private Quest questInDialogue;
    private DialogueTrigger dialogueTriggerinContext;

    private Text text_Dialogue;
    private Text text_name;
    private Image image_speaker;
    public GameObject DialoguePanel;
    public Button button;

    public GameObject losePanel;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        currentQuests = new List<Quest>();

        text_Dialogue = DialoguePanel.transform.Find("DialogueText").GetComponent<Text>();
        text_name = DialoguePanel.transform.Find("NameText").GetComponent<Text>();
        image_speaker = DialoguePanel.transform.Find("Speaker_image").GetComponent<Image>();

        DialoguePanel.SetActive(false);
        button.interactable = false;
    }

    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Z)) {
            DisplayNextSentence();
        }*/
        if (Input.GetKeyDown(KeyCode.C)) {
            QuestCompleted(currentQuests[0]);
        }

    }

    #region Dialogue
    public void StartDialogue(DialogueTrigger dialogueTrigger)
    {
        sentences.Clear();
        button.interactable = true;
        DialoguePanel.SetActive(true);

        dialogueTriggerinContext = dialogueTrigger;

        Dialogue dialogue = dialogueTrigger.dialogue;

        text_name.text = dialogue.dialogue_name;



        foreach (string sentence in dialogue.dialogue_sentences) {
            sentences.Enqueue(sentence);
        }

        if (dialogueTrigger.questToAssign.name != null) {
            questInDialogue = dialogueTrigger.questToAssign;
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        button.interactable = true;

        if (sentences.Count == 0)
        {
            EndConversation();
            return;
        }

        string sentence = sentences.Dequeue();

        text_Dialogue.text = sentence;
    }
    public void EndConversation() {
        Debug.Log("Conversation ended");

        if (dialogueTriggerinContext.typeOfQuest != DialogueTrigger.QuestType.None)
        {
            currentQuests.Add(questInDialogue);
            dialogueTriggerinContext.BetweenDialogue();

            if (dialogueTriggerinContext.typeOfQuest == DialogueTrigger.QuestType.Talk)
            {
                gameObject.GetComponent<QuestManager>().InitiateTalkQuest(dialogueTriggerinContext);
            }
            if (dialogueTriggerinContext.typeOfQuest == DialogueTrigger.QuestType.Collect)
            {
                gameObject.GetComponent<QuestManager>().initiateCApQuest();
            }


            if (dialogueTriggerinContext.isQuestActivate == true && dialogueTriggerinContext.typeOfQuest == DialogueTrigger.QuestType.Talk)
            {
                if (gameObject.GetComponent<QuestManager>().checkFOrCompletion(3))
                {
                    QuestCompleted(dialogueTriggerinContext.questToAssign);
                }
            }
            if (dialogueTriggerinContext.isQuestActivate == true && dialogueTriggerinContext.typeOfQuest == DialogueTrigger.QuestType.Collect)
            {
                if (gameObject.GetComponent<QuestManager>().capCollected)
                {
                    QuestCompleted(dialogueTriggerinContext.questToAssign);
                }
            }

        }

        text_name.text = "";
        text_Dialogue.text = "";
        DialoguePanel.SetActive(false);
        button.interactable = false;
    }
    public void CloseDialogueWIndow()
    {
        text_name.text = "";
        text_Dialogue.text = "";
        DialoguePanel.SetActive(false);
        button.interactable = false;
    }
    #endregion

    public void QuestCompleted(Quest completedQuest) {
        //Rewards
        currentQuests.Remove(completedQuest);

        completedQuest.NPCToChange.GetComponent<DialogueTrigger>().CompletedDialogue();

        Debug.Log("Yes");
        GameObject.FindObjectOfType<ClueManager>().AddClue(completedQuest.Clue);
        Debug.Log("Hell YEAH! Dask Completed");
    }
    public void FlareAsCulprit() {

        if (dialogueTriggerinContext.culprit == true)
        {
            //Won
            winPanel.SetActive(true);
        }
        else {
            //Lost
            winPanel.SetActive(false);
        }
    }
}
