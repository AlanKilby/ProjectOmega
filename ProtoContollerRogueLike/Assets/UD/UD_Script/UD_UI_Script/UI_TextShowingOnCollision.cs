using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TextShowingOnCollision : MonoBehaviour
{
    [SerializeField] GameObject textToShow;

    private void Start()
    {
        textToShow.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textToShow.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textToShow.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textToShow.SetActive(false);
        }
    }
}
