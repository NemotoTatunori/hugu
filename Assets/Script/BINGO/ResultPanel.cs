using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] Transform m_firstPrize = null;
    [SerializeField] Transform m_secondPrize = null;
    [SerializeField] Transform m_thirdPrize = null;
    [SerializeField] Transform m_remaining = null;
    public IEnumerator Result(GameObject[] prayers)
    {
        yield return new WaitForSeconds(1f);
        prayers[0].transform.SetParent(m_firstPrize);
        prayers[0].transform.position = m_firstPrize.position;
        if (prayers.Length == 1) { yield break; }
        yield return new WaitForSeconds(1f);
        prayers[1].transform.SetParent(m_secondPrize);
        prayers[1].transform.position = m_secondPrize.position;
        if (prayers.Length == 2) { yield break; }
        yield return new WaitForSeconds(1f);
        prayers[2].transform.SetParent(m_thirdPrize);
        prayers[2].transform.position = m_thirdPrize.position;
        if (prayers.Length == 3) { yield break; }
        yield return new WaitForSeconds(1f);
        for (int i = 3; i < prayers.Length; i++)
        {
            prayers[i].transform.SetParent(m_remaining);
            yield return new WaitForSeconds(0.3f);
        }
    }
    public void ObjectReset()
    {
        Destroy(m_firstPrize.transform.GetChild(0).gameObject);
        Destroy(m_secondPrize.transform.GetChild(0).gameObject);
        Destroy(m_thirdPrize.transform.GetChild(0).gameObject);
        int c = m_remaining.transform.childCount;
        for (int i = 0; i < c; i++)
        {
            Destroy(m_remaining.transform.GetChild(i).gameObject);
        }
        this.gameObject.SetActive(false);
    }
}
