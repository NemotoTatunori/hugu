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
    /// <summary>������ǂݏ�������v���p�e</summary>
    public int myNumber
    {
        get => m_myNumber;
        set
        {
            m_myNumber = value;
            m_text.text = m_myNumber.ToString();
        }
    }
    /// <summary>�J���Ă��邩��ǂݏ�������v���p�e</summary>
    public bool Open
    {
        get => m_open;
    }
    /// <summary>�J������F��ς���</summary>
    public void Hit()
    {
        m_open = true;
        m_image.color = Color.gray;
    }
    /// <summary>�t���[�}�X</summary>
    public void FreeCell()
    {
        m_text.text = "F";
        Hit();
    }
}
