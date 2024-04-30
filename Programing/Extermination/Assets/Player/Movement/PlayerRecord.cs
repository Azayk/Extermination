using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRecord : MonoBehaviour
{
    public Text uiText;
    public Text uiGameOverText;
    public float value = 0f; 

    // Update is called once per frame
    void Update()
    {
        string valueText = value.ToString();
        uiText.text = valueText;
        uiGameOverText.text = valueText;
    }
}
