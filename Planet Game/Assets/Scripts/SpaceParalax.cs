using UnityEngine;

public class SpaceParalax : MonoBehaviour
{
    private float length, startposX, height, startposY;
    public GameObject rocketCam;
    public float parallaxEffect;
    private Transform t;
    private static bool onVictory;

    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        height = GetComponentInChildren<SpriteRenderer>().bounds.size.y;
        t = transform;
    }

    void Update()
    {
        if (onVictory)
        {
            transform.localPosition = new Vector3(0,0,100);
        }
        else
        {
            t.eulerAngles = new Vector3(t.eulerAngles.x, t.eulerAngles.y, 0);

            float tempX = (rocketCam.transform.position.x * (1 - parallaxEffect));
            float tempY = (rocketCam.transform.position.y * (1 - parallaxEffect));

            float distX = (rocketCam.transform.position.x * parallaxEffect);
            float distY = (rocketCam.transform.position.y * parallaxEffect);

            transform.position = new Vector3(startposX + distX, startposY + distY, transform.position.z);

            if (tempX > startposX + length) startposX += length;
            else if (tempX < startposX - length) startposX -= length;

            if (tempY > startposY + height) startposY += height;
            else if (tempY < startposY - height) startposY -= height;
        }
    }

    public bool OnVictory
    {
        get => onVictory;
        set => onVictory = value;
    }
}
