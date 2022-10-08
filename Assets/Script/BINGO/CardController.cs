using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    [SerializeField] CellController m_cellPrefab = null;
    [SerializeField] GameObject m_bingPanel;
    [SerializeField] Text m_nameText;
    [SerializeField] GameObject m_reachImage = null;
    [SerializeField] Transform m_cellTransfoem = null;
    string m_myName;
    bool m_bingo = false;
    bool m_reach = false;
    int m_row = 5;
    int m_col = 5;
    CellController[,] m_cells;
    /// <summary>�J�[�h�ɐ������Z�b�g����</summary>
    public void NumberSet()
    {
        m_cells = new CellController[m_row, m_col];
        //�������琔����t����}�X�𐶂ݏo����
        for (int r = 0; r < m_row; r++)
        {
            for (int c = 0; c < m_col; c++)
            {
                m_cells[r, c] = Instantiate(m_cellPrefab, new Vector3(-50 + 25 * r, 20 - 25 * c, 200), Quaternion.identity, m_cellTransfoem);
            }
        }

        //�������琶�ݏo�����}�X�ɐ����𖄂ߍ���
        for (int r = 0; r < m_row; r++)
        {
            //�����Ő�������Ȃ��悤�ɂ��Ă���
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

            //�����Œ��I���ꂽ�������}�X�ɖ��ߍ���ł���
            for (int c = 0; c < m_col; c++)
            {
                m_cells[r, c].myNumber = numbers[c];
            }
        }

        //�����Ő^�񒆂̓t���[�}�X�ɂ��Ă���
        m_cells[2, 2].FreeCell();
    }
    /// <summary>���O��ς���</summary>
    public void Rename(string name)
    {
        m_myName = name;
        m_nameText.text = m_myName;
    }
    /// <summary>
    /// ���I���ꂽ�������󂯎��
    /// </summary>
    /// <param name="n">���I���ꂽ����</param>
    public void GetNumber(int n)
    {
        if (m_bingo)
        {
            return;
        }
        //�����Ő������������炻�̃}�X���J��
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
    /// <summary>�����Ńr���S���ɂȂ��Ă��邩���肷��</summary>
    void Judgement()
    {
        int hit = 0;
        for (int r = 0; r < m_row; r++)
        {
            for (int i = 0; i < m_col; i++)
            {
                if (m_cells[r, i].Open == true)
                {
                    hit++;
                }
            }
            if (Hit(hit))
            {
                Bingo();
                return;
            }
            else
            {
                hit = 0;
            }
        }

        for (int c = 0; c < m_col; c++)
        {
            for (int i = 0; i < m_row; i++)
            {
                if (m_cells[i, c].Open == true)
                {
                    hit++;
                }
            }
            if (Hit(hit))
            {
                Bingo();
                return;
            }
            else
            {
                hit = 0;
            }
        }

        for (int i = 0; i < m_row; i++)
        {
            if (m_cells[i, i].Open == true)
            {
                hit++;
            }
        }
        if (Hit(hit))
        {
            Bingo();
            return;
        }
        else
        {
            hit = 0;
        }

        for (int i = 0; i < m_row; i++)
        {
            if (m_cells[4 - i, i].Open == true)
            {
                hit++;
            }
        }
        if (Hit(hit))
        {
            Bingo();
            return;
        }
    }
    /// <summary>�r���S������Q�[���}�l�[�W���[�ɓ`����</summary>
    void Bingo()
    {
        m_bingo = true;
        m_bingPanel.SetActive(true);
        m_reachImage.SetActive(false);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.Bingo(m_myName);
    }

    bool Hit(int hit)
    {
        if (hit == 5)
        {
            return true;
        }
        else if (hit == 4 && !m_reach)
        {
            m_reachImage.SetActive(true);
            m_reach = true;
        }
        return false;
    }
}
