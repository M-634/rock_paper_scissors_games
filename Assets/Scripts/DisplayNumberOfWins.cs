using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayNumberOfWins : MonoBehaviour
{
    [SerializeField] Text m_winsText;
    [SerializeField] Text m_lossesText;

    private void Start()
    {
        m_winsText.text = "勝利数 : " + RecordNumberOfWins.Instance.CountWins.ToString();
        m_lossesText.text = "敗北数 : " + RecordNumberOfWins.Instance.CountLosses.ToString();
    }
}
