using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    SpriteRenderer rend;
    Vector2 mousePos;
    public Sprite[] sprites = new Sprite[2];
    public bool used;

    void Start()
    {
       rend = gameObject.GetComponent<SpriteRenderer>();
       rend.sprite = sprites[0];
       used = false;
    }

    public void OnHover()
    {
        rend.sprite = sprites[1];
    }

    public void OnExit()
    {
        rend.sprite = sprites[0];
    }

    public void Use()
    {
        used = true;
    }
}
