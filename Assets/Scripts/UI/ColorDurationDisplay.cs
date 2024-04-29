using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorDurationDisplay : MonoBehaviour
{
    private Image colorCircle;
    private P_ColorSwitch ps;
    void Start()
    {
        ps = FindObjectOfType<P_ColorSwitch>();
        colorCircle = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.currentColor != ps.baseColor)
        {
            //colorCircle.gameObject.SetActive(true);
            colorCircle.color = ps.currentColor;
            colorCircle.fillAmount = (ps.colorDuration - ps.colorEffectTimer) / 3f;
        }
        else
        {
            colorCircle.fillAmount = 0f;
            //colorCircle.gameObject.SetActive(false);
        }
    }
}
