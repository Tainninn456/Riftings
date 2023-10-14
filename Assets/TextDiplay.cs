using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDiplay : MonoBehaviour
{
    public void TextDisplaing(TextMeshProUGUI InputTex, int value)
    {
        InputTex.text = value.ToString();
    }
}
