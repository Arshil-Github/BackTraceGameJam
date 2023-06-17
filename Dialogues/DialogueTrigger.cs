using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string name;
    public Dialogue initialDialogue;
    public Dialogue betweenDialogue;
    public Dialogue endDialogue;

    public Quest questToAssign;
    public Dialogue dialogue;

    public bool isQuestActivate = false;
    public enum QuestType { Talk, Collect, None }

    public QuestType typeOfQuest;

    public Transform pointer;

    public bool FirstTimeDialogue;
    public bool culprit;

    // Start is called before the first frame update
    void Start()
    {
        initialDialogue.dialogue_name = name;
        betweenDialogue.dialogue_name = name;
        endDialogue.dialogue_name = name;

        dialogue = initialDialogue;

        pointer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<QuestManager>().TalkedtoanNPC(this);

        GameObject.FindObjectOfType<DialogueManager>().StartDialogue(this);

        if (FirstTimeDialogue == true)
        {
            dialogue = betweenDialogue;
        }
    }
    public void CompletedDialogue() {

        dialogue = endDialogue;
    }
    public void BetweenDialogue()
    {
        isQuestActivate = true;
        dialogue = betweenDialogue;
    }
    public void ShowDialogueCartoon() {
        pointer.gameObject.SetActive(true);
        pointer.GetComponent<SpriteRenderer>().color = new Color(pointer.GetComponent<SpriteRenderer>().color.r, pointer.GetComponent<SpriteRenderer>().color.b, pointer.GetComponent<SpriteRenderer>().color.g, 1);
        StartCoroutine(SwitchOffPointer());
    }
    IEnumerator SwitchOffPointer() {
        yield return new WaitForSeconds(1f);
        while (pointer.GetComponent<SpriteRenderer>().color.a < 0) {
            pointer.GetComponent<SpriteRenderer>().color = new Color(pointer.GetComponent<SpriteRenderer>().color.r, pointer.GetComponent<SpriteRenderer>().color.b, pointer.GetComponent<SpriteRenderer>().color.g, pointer.GetComponent<SpriteRenderer>().color.a - 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        pointer.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            ShowDialogueCartoon();
        }
    }
}
