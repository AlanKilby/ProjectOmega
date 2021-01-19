using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Sprite cursorMenu;
    public Sprite cursorFight;

    SpriteRenderer ownSprite;

    public static bool isFighting;

    public Vector2 mousePosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        ownSprite = GetComponent<SpriteRenderer>();
        ownSprite.sprite = cursorFight;
        SetFightCursor();
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = mousePosition;

        if (Input.GetKeyDown(KeyCode.B))
        {
            SetMenuCursor();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SetFightCursor();
        }

        if (!isFighting)
        {
            ownSprite.sprite = cursorMenu;
        }
        if (isFighting)
        {
            ownSprite.sprite = cursorFight;
        }
    }

    public static void SetMenuCursor()
    {
        isFighting = false;
    }

    public static void SetFightCursor()
    {
        isFighting = true;
    }
}
