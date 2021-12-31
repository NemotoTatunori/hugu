using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    List<int> m_numbers = new List<int>();
    [SerializeField] NumberBoardCell m_numberBoardCellPrefab = null;
    [SerializeField] GridLayoutGroup m_numberBoard = null;
    NumberBoardCell[,] m_numberBoardCells;
    [SerializeField] GameObject m_cardPrefab = null;
    GameObject[] m_players;
    CardController[] m_cards;
    Coroutine m_coroutine;
    [SerializeField] GameObject m_entryPanel = null;
    [SerializeField] RectTransform m_winnerList = null;
    [SerializeField] GameObject m_winnerNamePrefab = null;
    [SerializeField] InputField m_entryName = null;
    [SerializeField] GameObject m_entryNamePrefab = null;
    [SerializeField] RectTransform m_nameList = null;
    [SerializeField] GameObject m_caveat = null;
    [SerializeField] GameObject m_camera = null;
    float m_cameraX = 0;
    float m_cameraY = 0;
    float m_cameraZ = 0;
    float m_moveSpeed = 2;
    float m_moveY = 0;
    [SerializeField] GameObject m_progressPanel = null;
    GameObject m_progressLottery;
    RectTransform m_progressCovar;
    Text m_progressAlphabet;
    Text m_progressNumber;
    Text m_turnText;
    float m_people = 0;
    float m_winners = 0;
    int m_turn = 0;

    void Start()
    {
        //ここで抽選機に１から７５までの数字を入れている
        for (int i = 1; i < 76; i++)
        {
            m_numbers.Add(i);
        }

        //ここで番号板を作っている
        m_numberBoardCells = new NumberBoardCell[15, 5];
        for (int c = 0; c < 5; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                var cell = Instantiate(m_numberBoardCellPrefab);
                cell.transform.SetParent(m_numberBoard.transform);
                cell.MyNumber = 1 + r + 15 * c;
                m_numberBoardCells[r, c] = cell;
            }
        }

        //ここでカメラの座標をセットしている
        m_cameraX = m_camera.transform.position.x;
        m_cameraY = m_camera.transform.position.y;
        m_cameraZ = m_camera.transform.position.z;

        m_progressLottery = m_progressPanel.transform.GetChild(0).gameObject;
        m_progressCovar = m_progressPanel.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        m_turnText = m_progressPanel.transform.GetChild(2).gameObject.GetComponent<Text>();
        m_progressAlphabet = m_progressLottery.transform.GetChild(0).gameObject.GetComponent<Text>();
        m_progressNumber = m_progressLottery.transform.GetChild(1).gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (m_cameraY > 0)
            {
                return;
            }
            else
            {
                m_cameraY += m_moveSpeed;
                CameraMove();
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (m_cameraY < -1 * m_moveY * 300)
            {
                return;
            }
            else
            {
                m_cameraY -= m_moveSpeed;
                CameraMove();
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (m_cameraX > 800)
            {
                return;
            }
            else
            {
                m_cameraX += m_moveSpeed;
                CameraMove();
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (m_cameraX < 0)
            {
                return;
            }
            else
            {
                m_cameraX -= m_moveSpeed;
                CameraMove();
            }
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (m_cameraZ > -100)
            {
                return;
            }
            else
            {
                m_cameraZ += m_moveSpeed;
                CameraMove();
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (m_cameraZ < -500)
            {
                return;
            }
            else
            {
                m_cameraZ -= m_moveSpeed;
                CameraMove();
            }
        }
    }
    /// <summary>カメラを動かす</summary>
    void CameraMove()
    {
        m_camera.transform.position = new Vector3(m_cameraX, m_cameraY, m_cameraZ);
    }
    /// <summary>ボタンが押されたときに数字を抽選する</summary>
    public void Lottery()
    {
        if (m_numbers.Count != 0)
        {
            m_coroutine = StartCoroutine(LotteryProgress());
        }
        else
        {
            Debug.Log("もうないよ");
        }
    }
    /// <summary>人数を決めた時にカードを生成する</summary>
    public void GameStart()
    {
        int p = m_nameList.transform.childCount;
        m_players = new GameObject[p];
        m_cards = new CardController[p];
        for (int i = 0; i < p; i++)
        {
            var card = Instantiate(m_cardPrefab);
            m_cards[i] = card.transform.Find("Canvas/Card").gameObject.GetComponent<CardController>();
            m_cards[i].NumberSet();
            GameObject name = m_nameList.transform.GetChild(i).gameObject;
            Text n = name.transform.GetChild(0).GetComponent<Text>();
            m_cards[i].Rename(n.text);
            m_players[i] = card;
        }
        Leveling();
        m_entryPanel.SetActive(false);
    }
    /// <summary>生成したカードを並べる</summary>
    void Leveling()
    {
        int r = 0;
        int c = 0;
        for (int i = 0; i < m_players.Length; i++)
        {
            m_players[i].transform.position = new Vector3(r * 200, c * -300, 0);
            r++;
            if (r == 5)
            {
                r = 0;
                c++;
            }
        }
        m_moveY = c;
        if (m_players.Length % 5 == 0)
        {
            m_moveY--;
        }
    }
    /// <summary>名前リストに追加</summary>
    public void AddName()
    {
        if (m_people < 30)
        {
            m_people++;
            var name = Instantiate(m_entryNamePrefab);
            Text nameText = name.transform.GetChild(0).gameObject.GetComponent<Text>();
            nameText.text = m_entryName.text;
            name.transform.SetParent(m_nameList);
            m_nameList.sizeDelta = new Vector2(0, m_people * 50);
        }
        else
        {
            if (m_coroutine != null)
            {
                StopCoroutine(m_coroutine);
            }
            m_coroutine = StartCoroutine(Caveat());
        }
    }
    /// <summary>名前リストから消去</summary>
    public void RemoveName(GameObject nameObject)
    {
        m_people--;
        Destroy(nameObject);
        m_nameList.sizeDelta = new Vector2(0, m_people * 50);
    }
    /// <summary>ビンゴした人をリストに入れる</summary>
    public void Bingo(string name)
    {
        m_winners++;
        m_winnerList.sizeDelta = new Vector2(0, m_winners * 30);
        var winnerName = Instantiate(m_winnerNamePrefab);
        winnerName.transform.GetChild(0).gameObject.GetComponent<Text>().text = name;
        winnerName.transform.GetChild(1).gameObject.GetComponent<Text>().text = m_turn.ToString();
        winnerName.transform.SetParent(m_winnerList.transform);
    }
    /// <summary>エントリー画面に戻る</summary>
    public void EntryReturn()
    {
        foreach (var item in m_players)
        {
            Destroy(item);
        }
        foreach (var item in m_numberBoardCells)
        {
            item.ColorChange(Color.white);
        }
        for (int i = 0; i < m_winnerList.childCount; i++)
        {
            Destroy(m_winnerList.transform.GetChild(i).gameObject);
        }
        while (m_numbers.Count != 0)
        {
            m_numbers.Remove(m_numbers[0]);
        }
        for (int i = 1; i < 76; i++)
        {
            m_numbers.Add(i);
        }
        m_turn = 0;
        m_players = null;
        m_cards = null;
        m_entryPanel.SetActive(true);
    }

    IEnumerator Caveat()
    {
        m_caveat.SetActive(true);
        Image image = m_caveat.GetComponent<Image>();
        for (int i = 1; i < 11; i++)
        {
            if (i % 2 == 0)
            {
                image.color = new Color(1, 0.5f, 0.5f);
            }
            else
            {
                image.color = Color.white;
            }
            yield return new WaitForSeconds(0.1f);
        }
        m_caveat.SetActive(false);
    }
    /// <summary>抽選の進行</summary>
    IEnumerator LotteryProgress()
    {
        m_turn++;
        m_turnText.text = m_turn + "回目";
        int n = Random.Range(0, m_numbers.Count);
        int num = m_numbers[n];
        m_progressPanel.SetActive(true);
        m_progressNumber.text = num.ToString();
        m_progressCovar.anchoredPosition = new Vector2(0, 0);
        if (num <= 15)
        {
            m_progressAlphabet.text = "B";
        }
        else if (num <= 30)
        {
            m_progressAlphabet.text = "I";
        }
        else if (num <= 45)
        {
            m_progressAlphabet.text = "N";
        }
        else if (num <= 60)
        {
            m_progressAlphabet.text = "G";
        }
        else
        {
            m_progressAlphabet.text = "O";
        }
        yield return null;
        while (true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                break;
            }
            yield return null;
        }
        for (int i = 0; i > -100; i--)
        {
            m_progressCovar.anchoredPosition = new Vector2(0, i);
            yield return null;
        }
        while (true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                break;
            }
            yield return null;
        }
        m_progressCovar.anchoredPosition = new Vector2(0, -200);
        yield return null;
        while (true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                break;
            }
            yield return null;
        }
        m_progressPanel.SetActive(false);
        foreach (var item in m_numberBoardCells)
        {
            if (item.MyNumber == num)
            {
                item.ColorChange(Color.red);
            }
        }
        foreach (var item in m_cards)
        {
            item.GetNumber(num);
        }
        m_numbers.Remove(num);
    }
}
