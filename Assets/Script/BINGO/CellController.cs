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
        StartCoroutine(Blinking());
    }
    /// <summary>�t���[�}�X</summary>
    public void FreeCell()
    {
        m_text.text = "F";
        Hit();
    }
    /// <summary>�_�ł�����R���[�`��</summary>
    IEnumerator Blinking()
    {
        for (float i = 0; i < 1; i+= 0.01f)
        {
            m_image.color = new Color(i,i,0);
            if (Input.GetMouseButtonUp(0))
            {
                m_image.color = Color.yellow;
                break;
            }
            yield return null;
        }
        yield return null;
        while (true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                m_image.color = Color.gray;
                break;
            }
            yield return null;
        }

    }
}
