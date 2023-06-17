using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    public Transform ClueWindow;
    public Transform CluesGrid;
    public GameObject pf_cluetext;
    public List<string> clues;

    private void Start()
    {
        clues = new List<string>();
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            ClueWindow.gameObject.SetActive(true);
        }
        else {
            ClueWindow.gameObject.SetActive(false);
        }
    }
    public void AddClue(string clueTobeAdded)
    {
        if (clues.Contains(clueTobeAdded)) {
            return;
        }
        clues.Add(clueTobeAdded);

        GameObject newClue = Instantiate(pf_cluetext, CluesGrid);
        newClue.GetComponent<Text>().text = clueTobeAdded;
    }
}
