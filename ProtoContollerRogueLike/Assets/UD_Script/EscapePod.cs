using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapePod : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("can escape");
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Menu");
        }
    }

}
