using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    int m_myNumber;
    bool m_open = false;
    [SerializeField] Text m_text = null;
    [SerializeField] Image m_image = null;
    /// <summary>数字を読み書きするプロパテ</summary>
    public int myNumber
    {
        get => m_myNumber;
        set
        {
            m_myNumber = value;
            m_text.text = m_myNumber.ToString();
        }
    }
    /// <summary>開いているかを読み書きするプロパテ</summary>
    public bool Open
    {
        get => m_open;
    }
    /// <summary>開いたら色を変える</summary>
    public void Hit()
    {
        m_open = true;
        m_image.color = Color.gray;
    }
    /// <summary>フリーマス</summary>
    public void FreeCell()
    {
        m_text.text = "F";
        Hit();
    }
}
