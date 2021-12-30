using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberBoardCell : MonoBehaviour
{
    int m_myNumber;
    [SerializeField] Text m_text = null;
    [SerializeField] Image m_image = null;
    /// <summary>Š„‚è“–‚Ä‚ç‚ê‚½”š‚ğ“Ç‚İ‘‚«‚·‚é</summary>
    public int MyNumber
    {
        get => m_myNumber;
        set
        {
            m_myNumber = value;
            m_text.text = m_myNumber.ToString();
        }
    }
    /// <summary>“–‚Ä‚ç‚ê‚½‚Æ‚«‚ÉF‚ğ•Ï‚¦‚é</summary>
    public void ColorChange(Color color)
    {
        m_image.color = color;
    }
}
