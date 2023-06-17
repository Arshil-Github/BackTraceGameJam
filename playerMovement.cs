using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    #region Movement variables
    [Header("Movement")]
    public float initialSpeed;
    public float finalSpeed;
    public float acceleration;
    public bool actionButton;

    private float horizontal;
    private float vertical;
    private float speed;
    #endregion
    Rigidbody2D rb;
    DialogueManager dialogeManager;

    Animator anim;
    SpriteRenderer spriteRen;

    Transform target;
    public float distanceForInteraction;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        dialogeManager = FindObjectOfType<DialogueManager>();
        anim = gameObject.GetComponent<Animator>();

        spriteRen = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        speed = initialSpeed;
    }

    bool canAccelerate = true; 
    // Update is called once per frame
    void Update()
    {
        #region Movement
        horizontal = Input.GetAxisRaw("Horizontal");

        vertical = Input.GetAxisRaw("Vertical");
        #endregion

        if (horizontal > 0)
        {
            spriteRen.flipX = false;
            anim.SetBool("SideMovement", true);
        }
        else if (horizontal < 0)
        {
            spriteRen.flipX = true;
            anim.SetBool("SideMovement", true);
        }
        else if (horizontal == 0) {
            spriteRen.flipX = false;
            anim.SetBool("SideMovement", false);           
        }

        if (vertical > 0)
        {
            anim.SetBool("UpMovement", true);
            anim.SetBool("DownMovement", false);
        }
        else if (vertical < 0)
        {
            anim.SetBool("DownMovement", true);
            anim.SetBool("UpMovement", false);
        }else if (vertical == 0)
        {
            anim.SetBool("DownMovement", false);
            anim.SetBool("UpMovement", false);


        }

        if (Input.GetKeyDown(KeyCode.Z) && actionButton == false)
        {
            actionButton = true;
            ActionButtonDelay();
        }
        if (!FindObjectOfType<QuestManager>().capCollected)
        {
            if (Mathf.Abs((transform.position - FindObjectOfType<Cap>().transform.position).magnitude) < distanceForInteraction / 2)
            {

                FindObjectOfType<QuestManager>().capCollected = true;
                FindObjectOfType<Cap>().gameObject.SetActive(false);

            }
        }

    }
    void ActionButtonDelay() {
        if (target != null) {
            if (Mathf.Abs((transform.position - target.position).magnitude) < distanceForInteraction) {
                if (dialogeManager.DialoguePanel.activeSelf == true)
                {
                    dialogeManager.DisplayNextSentence();
                }
                else
                {
                    target.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                }
            }
        }
        actionButton = false;
    }
    private void FixedUpdate()
    {
        #region Movement
        if (canAccelerate)
        {
            StartCoroutine(acceratePlayer());
            canAccelerate = false;
        }
        if (horizontal == 0 && vertical == 0)
        {
            speed = initialSpeed;
            canAccelerate = true;
        }

        rb.velocity = new Vector3(horizontal * speed * Time.deltaTime, vertical * speed * Time.deltaTime, 0);
        #endregion
    }
    #region Movement Functions
    IEnumerator acceratePlayer() {
        while (speed < finalSpeed)
        {
            yield return new WaitForSeconds(1f);
            speed += acceleration;

        }
        StopAllCoroutines();
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            target = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            target = null;
            dialogeManager.CloseDialogueWIndow();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceForInteraction);

    }

}
