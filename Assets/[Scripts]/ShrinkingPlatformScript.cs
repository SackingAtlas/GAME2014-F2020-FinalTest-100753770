/*
 * GAME2014-F2020-FinalTest-100753770
 * ShrinkingPlatformScript
 * Harrison Black
 * 100753770
 * 
 * Date last modified: Dec.16, 2021
 * Description: floating platform that shrinks when the player touches it. Then grows back to full size when the player is not touching it.
 * 
 * 1.shrink and grow functionality 
 * 2.SFX added and code for altering pitch with scale
 * 3.internal documentation added
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatformScript : MonoBehaviour
{
    private bool isPlayerOff;
    private Vector3 sizingVector;
    public float sizingFactor, minimumSize;
    public GameObject PlatformCollider;
    private AudioSource speaker;
    private void Start()
    {
        sizingVector = new Vector3(sizingFactor, 0, 0);//creating vector to scale the sprite by
        speaker = GetComponent<AudioSource>();// getting the sound emmiting part of this object
    }
    private void Update()
    {
        speaker.pitch = transform.localScale.x; //make the audio clip's pitch rise and drop with the platforms size

        if (isPlayerOff && transform.localScale.x < 1) //when the platform isn't full size, and no one is on it, grow
        {
            transform.localScale += sizingVector * Time.deltaTime; //grows the platform
        }
        if (transform.localScale.x > 1) //when the platform gets larger than the original size, it is returned to the original size
        {
            transform.localScale = new Vector3(1, 1, 1);
            speaker.Stop(); // stops the audio from playing when the platform is finished returning to size
        }
    }

    //checking for player collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        speaker.Play(); // when the player touches the platform start the sound fx
    }

    //checking if player is still touching
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //if it's the player touching the platform start shrinking
        {
            isPlayerOff = false;
            if (transform.localScale.x > minimumSize)
            {
                transform.localScale -= sizingVector * Time.deltaTime;
            }
            if (transform.localScale.x < minimumSize) //when its shurnk to minimum size, turn off collider so player falls off
            {
                PlatformCollider.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    //checking if player is done collision
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") //when the player leaves, turn the colider back on
        {
            isPlayerOff = true;//tells the update funtion the player is off, and to start growing
            PlatformCollider.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
