using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static partial class Extensions
{
    // Generic
    public static void Destroy(this GameObject t)
    {
        MonoBehaviour.Destroy(t);
    }
    public static void Destroy(this Transform t)
    {
        MonoBehaviour.Destroy(t.gameObject);
    }

    public static void Destroy<T>(this T t) where T : MonoBehaviour
    {
        MonoBehaviour.Destroy(t);
    }
    public static void Disable<T>(this T t) where T : MonoBehaviour
    {
        t.enabled = false;
    }
    public static void Enable<T>(this T t) where T : MonoBehaviour
    {
        t.enabled = true;
    }

    public static GameObject Instantiate(this GameObject t)
    {
        return MonoBehaviour.Instantiate(t);
    }
    public static GameObject Instantiate(this Transform t)
    {
        return Instantiate(t.gameObject);
    }

    public static GameObject Instantiate(this GameObject t, Transform parant)
    {
        return Instantiate(t, parant);
    }

    public static void DestroyChildren(this Transform t)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
            MonoBehaviour.Destroy(t.GetChild(i).gameObject);
    }

    public static void ChildsActiveState(this Transform t, bool state)
    {
        for (int i = t.childCount - 1; i >= 0; i--)
            t.GetChild(i).gameObject.SetActive(state);
    }

    public static void Reset(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localEulerAngles = Vector3.zero;
        t.localScale = Vector3.one;
    }

 

    // Spriterenderer
    public static void SetColor(this SpriteRenderer spriteRenderer, Color color)
    {
        spriteRenderer.color = color;
    }

    public static void SetSprite(this SpriteRenderer spriteRenderer, Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public static void SetSprite(this Image i, Sprite sprite)
    {
        i.sprite = sprite;
    }

    //text
    public static void SetText(this Text t, string text)
    {
        t.text = text;
    }
}