using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isInputEnabled = true;

    public void SetInputOff()
    {
        isInputEnabled = false;
    }

    public void SetInputOn()
    {
        isInputEnabled = true;
    }
}
