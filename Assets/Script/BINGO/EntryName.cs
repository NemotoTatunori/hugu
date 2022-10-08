using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EntryName : MonoBehaviour
{
    /// <summary>名前のテキスト</summary>
    [SerializeField] Text m_nameText = null;
    /// <summary>消去ボタン</summary>
    [SerializeField] Button button = null;
    public void Setting(string name, Action delete)
    {
        m_nameText.text = name;
        button.onClick.AddListener(() => delete());
    }
}
