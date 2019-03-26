using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapBlock : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Vector2 corner;
    public float tapDelay = 0.100f;

    public float PixelUnit
    {
        get
        {
            return 1/spriteRenderer.sprite.pixelsPerUnit;
        }
    }
    public float PixelsPerUnit
    {
        get
        {
            return spriteRenderer.sprite.pixelsPerUnit;
        }
    }
    public Vector4 Border
    {
        get
        {
            return spriteRenderer.sprite.border;
        }
    }

    //private bool touchMoved = false;


    public virtual void Tap(GameManager gm)
    {
        Debug.Log("--------------Tapped-------------");
    }

    //returns wheather Vector2 point is inside of the the blocks dimentions
    //point 2 is in worlds space
    public bool pointIsInside(Vector2 point)
    {
        return (point.x >= transform.position.x && point.x <= transform.position.x + GetWidth() &&
                point.y >= transform.position.y && point.y <= transform.position.y + GetHeight());
    }

    public virtual void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //PixelUnit = 1/sprite.sprite.pixelsPerUnit;
        //Debug.Log(sprite.sprite.border);
    }
    public virtual void Update()
    {

    }

    public float GetWidth()
    {
        return spriteRenderer.size.x;
    }
    public float GetHeight()
    {
        return spriteRenderer.size.y;
    }
    public void SetWidth(float newWidth)
    {
        spriteRenderer.size = new Vector2(newWidth,spriteRenderer.size.y);
    }
    public void SetHeight(float newHeight)
    {
        spriteRenderer.size = new Vector2(spriteRenderer.size.x, newHeight);
    }
}
