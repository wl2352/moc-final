using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    TextMeshProUGUI m_TextMeshProUGUI;
    Stats ps;
    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        m_TextMeshProUGUI.text = ps.currency.ToString();
    }
}
