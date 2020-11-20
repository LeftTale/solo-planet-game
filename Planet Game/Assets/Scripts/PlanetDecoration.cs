
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetDecoration : MonoBehaviour
{
    public List<Sprite> planetSprites;
    private SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        int tempSprite = Random.Range(0, 30);
        spriteRend.sprite = planetSprites[tempSprite];
    }
}
