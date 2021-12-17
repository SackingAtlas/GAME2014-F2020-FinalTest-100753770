using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatformScript : MonoBehaviour
{
    private bool off;
    private Vector3 shrinkVector;
    public float shrinkFactor, minSize;
    public GameObject tile;
    private void Start()
    {
        shrinkVector = new Vector3(shrinkFactor, 0, 0);
    }
    private void Update()
    {
        if (off && transform.localScale.x < 1)
        {
            transform.localScale += shrinkVector * Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            off = false;
            if (transform.localScale.x > minSize)
            {
                transform.localScale -= shrinkVector * Time.deltaTime;
                Debug.Log("onand shrink");
            }
            if (transform.localScale.x < minSize)
            {
                tile.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            off = true;
            tile.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
