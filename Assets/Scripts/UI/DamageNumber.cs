using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public TextMesh textMesh;
    public float moveSpeed = 1f;
    public float fadeSpeed = 1f;

    private float alpha = 1f;

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        alpha -= fadeSpeed * Time.deltaTime;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);
        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
