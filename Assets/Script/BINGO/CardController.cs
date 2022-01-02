using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] CellController m_cellPrefab = null;
    [SerializeField] GameObject m_bingPanel;
    [SerializeField] Text m_nameText;
    string m_myName;
    bool m_bingo = false;
    int m_row = 5;
    int m_col = 5;
    CellController[,] m_cells;
    /// <summary>カードに数字をセットする</summary>
    public void NumberSet()
    {
        m_cells = new CellController[m_row, m_col];
        //ここから数字を付けるマスを生み出しす
        for (int r = 0; r < m_row; r++)
        {
            for (int c = 0; c < m_col; c++)
            {
                m_cells[r, c] = Instantiate(m_cellPrefab, new Vector3(-50 + 25 * r, 20 - 25 * c, 200), Quaternion.identity, this.transform);
            }
        }

        //ここから生み出したマスに数字を埋め込む
        for (int r = 0; r < m_row; r++)
        {
            //ここで数字を被らないようにしている
            int[] numbers = new int[m_col];
            List<int> nums = new List<int>();
            for (int i = 1 + 15 * r; i < 16 + 15 * r; i++)
            {
                nums.Add(i);
            }
            for (int c = 0; c < m_col; c++)
            {
                int n = Random.Range(0, nums.Count);
                numbers[c] = nums[n];
                nums.Remove(nums[n]);
            }

            //ここで抽選された数字をマスに埋め込んでいる
            for (int c = 0; c < m_col; c++)
            {
                m_cells[r, c].myNumber = numbers[c];
            }
        }

        //ここで真ん中はフリーマスにしている
        m_cells[2, 2].FreeCell();
    }
    /// <summary>名前を変える</summary>
    public void Rename(string name)
    {
        m_myName = name;
        m_nameText.text = m_myName;
    }
    /// <summary>
    /// 抽選された数字を受け取る
    /// </summary>
    /// <param name="n">抽選された数字</param>
    public void GetNumber(int n)
    {
        if (m_bingo)
        {
            return;
        }
        //ここで数字があったらそのマスが開く
        foreach (var item in m_cells)
        {
            if (item.myNumber == n)
            {
                item.Hit();
                Judgement();
                break;
            }
        }
    }
    /// <summary>ここでビンゴがになっているか判定する</summary>
    void Judgement()
    {

        for (int r = 0; r < m_row; r++)
        {
            if (m_cells[r, 0].Open == true &&
                m_cells[r, 1].Open == true &&
                m_cells[r, 2].Open == true &&
                m_cells[r, 3].Open == true &&
                m_cells[r, 4].Open == true)
            {
                Bingo();
                return;
            }
        }

        for (int c = 0; c < m_col; c++)
        {
            if (m_cells[0, c].Open == true &&
                m_cells[1, c].Open == true &&
                m_cells[2, c].Open == true &&
                m_cells[3, c].Open == true &&
                m_cells[4, c].Open == true)
            {
                Bingo();
                return;
            }
        }

        if (m_cells[0, 0].Open == true &&
            m_cells[1, 1].Open == true &&
            m_cells[2, 2].Open == true &&
            m_cells[3, 3].Open == true &&
            m_cells[4, 4].Open == true)
        {
            Bingo();
            return;
        }

        if (m_cells[0, 4].Open == true &&
            m_cells[1, 3].Open == true &&
            m_cells[2, 2].Open == true &&
            m_cells[3, 1].Open == true &&
            m_cells[4, 0].Open == true)
        {
            Bingo();
            return;
        }
    }
    /// <summary>ビンゴしたらゲームマネージャーに伝える</summary>
    void Bingo()
    {
        m_bingo = true;
        m_bingPanel.SetActive(true);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.Bingo(m_myName);
    }
}
