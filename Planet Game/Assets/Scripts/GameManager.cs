using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isInputEnabled = true;
    public static bool isBoostEnabled = true;
    public static int score;

    public void SetInputOff()
    {
        isInputEnabled = false;
    }

    public void SetInputOn()
    {
        isInputEnabled = true;
    }

    public void SetBoostOff()
    {
        isBoostEnabled = false;
    }

    public void SetBoostOn()
    {
        isBoostEnabled = true;
    }

    public static int Score
    {
        get => score;
        set => score = value;
    }
}
