using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite Pose1;
    public Sprite Pose2;

    private void Start()
    {
        StartCoroutine(ChangeSprite());
    }

    IEnumerator ChangeSprite() {

        spriteRenderer.sprite = Pose1;

        yield return new WaitForSeconds(0.15f);

        spriteRenderer.sprite = Pose2;

        yield return new WaitForSeconds(0.15f);

        StartCoroutine(ChangeSprite());
    }
}
