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
    /// <summary>�J�[�h�ɐ������Z�b�g����</summary>
    public void NumberSet()
    {
        m_cells = new CellController[m_row, m_col];
        //�������琔����t����}�X�𐶂ݏo����
        for (int r = 0; r < m_row; r++)
        {
            for (int c = 0; c < m_col; c++)
            {
                m_cells[r, c] = Instantiate(m_cellPrefab, new Vector3(-50 + 25 * r, 20 - 25 * c, 200), Quaternion.identity, this.transform);
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
    /// <summary>�r���S������Q�[���}�l�[�W���[�ɓ`����</summary>
    void Bingo()
    {
        m_bingo = true;
        m_bingPanel.SetActive(true);
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.Bingo(m_myName);
    }
}
