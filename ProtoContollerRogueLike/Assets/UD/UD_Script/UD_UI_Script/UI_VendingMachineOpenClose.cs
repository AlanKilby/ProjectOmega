﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_VendingMachineOpenClose : MonoBehaviour
{
    [SerializeField] GameObject vendingMachineUI;

    bool playerCanActivateUI;

    void Start()
    {
        vendingMachineUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if(playerCanActivateUI && Input.GetKeyDown(KeyCode.X))
        {
            OpenVendingMachine();
        }
    }

    public void OpenVendingMachine()
    {
        CursorManager.SetMenuCursor();
        FindObjectOfType<AudioManager>().Play("Opening Shop");
        vendingMachineUI.SetActive(true);
        GameManagement.GameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void CloseVendingMachine()
    {
        CursorManager.SetFightCursor();
        FindObjectOfType<AudioManager>().Play("Closing Shop"); 
        vendingMachineUI.SetActive(false);
        GameManagement.GameIsPaused = false;
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCanActivateUI = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCanActivateUI = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCanActivateUI = false;
        }
    }
}
