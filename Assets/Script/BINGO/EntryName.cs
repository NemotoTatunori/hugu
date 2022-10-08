using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EntryName : MonoBehaviour
{
    /// <summary>���O�̃e�L�X�g</summary>
    [SerializeField] Text m_nameText = null;
    /// <summary>�����{�^��</summary>
    [SerializeField] Button button = null;
    public void Setting(string name, Action delete)
    {
        m_nameText.text = name;
        button.onClick.AddListener(() => delete());
    }
}
