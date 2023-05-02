using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HideButton : MonoBehaviour
{
    Toggle m_Toggle;
    public bool isPress;

    // Start is called before the first frame update
    void Start()
    {
        m_Toggle = GetComponent<Toggle>();
    }



    // Update is called once per frame
    void Update()
    {
        if (m_Toggle.isOn)
        {
            isPress = true;
        }
        else
        {
            isPress = false;
        }
    }
}
