using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCursor : MonoBehaviour
{
    public Texture2D cursorTexture2D;
    private GameObject mainPlayer;
    public GameObject cursorGameObject;
    private float horizontalCursor;
    private float verticalCursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture2D, new Vector2(16, 16), CursorMode.Auto);
        cursorGameObject = GameObject.Find("Controller Cursor").transform.Find("Cursor").gameObject;
        mainPlayer = GameObject.Find("MainPlayer");
    }

    private void Update()
    {
        transform.position = new Vector2(mainPlayer.transform.position.x, mainPlayer.transform.position.y);
        if (GameManager.isInputEnabled)
        {
            horizontalCursor = Input.GetAxisRaw("Cursor Horizontal");

            verticalCursor = Input.GetAxisRaw("Cursor Vertical");

            if (horizontalCursor != 0f || verticalCursor != 0f)
            { 
                Cursor.visible = false;
                cursorGameObject.SetActive(true);
            }
            else
            {
                Cursor.visible = true;
                cursorGameObject.SetActive(false);
            }

            Vector2 cursorPos = new Vector2(horizontalCursor, verticalCursor * -1) * 20;
            cursorGameObject.transform.localPosition = cursorPos;
        }

    }
}
