using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberBoardCell : MonoBehaviour
{
    int m_myNumber;
    [SerializeField] Text m_text = null;
    [SerializeField] Image m_image = null;
    /// <summary>割り当てられた数字を読み書きする</summary>
    public int MyNumber
    {
        get => m_myNumber;
        set
        {
            m_myNumber = value;
            m_text.text = m_myNumber.ToString();
        }
    }
    /// <summary>当てられたときに色を変える</summary>
    public void ColorChange(Color color)
    {
        m_image.color = color;
    }
}
