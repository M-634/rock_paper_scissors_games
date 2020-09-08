using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum StateHands
    {
        None,
        Goo,
        Choki,
        Par
    }

    private StateHands m_myHandsState;
    private StateHands m_enemyHandsState;

    /// <summary>敵の出す手の画像</summary>
    [SerializeField] Image m_enemyGooImage;
    [SerializeField] Image m_enemyChokiImage;
    [SerializeField] Image m_enemyParImage;

    [SerializeField] Text m_text;

    [SerializeField] string m_loadSceneName;

    bool m_isOKClick;//ボタンを押していいかどうか判定
    bool m_isOnClickBtn;//ボタンを押したかどうか判定
    bool m_isAiko;//あいこかどうか判定

    private IEnumerator m_coroutine;

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();

        m_coroutine = GameStart();
        StartCoroutine(m_coroutine);
    }

    public void ResetGame()
    {
        //表示されている敵の手を非表示にする
        if (m_enemyGooImage.gameObject.activeSelf)
        {
            m_enemyGooImage.gameObject.SetActive(false);
        }
        else if (m_enemyChokiImage.gameObject.activeSelf)
        {
            m_enemyChokiImage.gameObject.SetActive(false);
        }
        else if (m_enemyParImage.gameObject.activeSelf)
        {
            m_enemyParImage.gameObject.SetActive(false);
        }
        //手の状態を初期化
        m_myHandsState = StateHands.None;
        m_enemyHandsState = StateHands.None;

        m_isOKClick = false;
        m_isOnClickBtn = false;
        m_isAiko = false;
    }

    IEnumerator GameStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            m_text.text = "最初は";
            yield return new WaitForSeconds(1f);
            m_text.text = "グー";
            m_enemyGooImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            m_enemyGooImage.gameObject.SetActive(false);
            m_text.text = "じゃんけん...";

            while (true)
            {
                yield return new WaitForSeconds(1f);
                m_isOKClick = true;
                //ボタンが押されるまでまつ
                while (!m_isOnClickBtn)
                {
                    yield return null;
                }
                m_isOKClick = false;
                yield return null;
                HandsOfEnemy();

                yield return null;
                m_text.text = Judge();

                yield return null;
                if (!m_isAiko)
                    break;

                yield return new WaitForSeconds(1f);
                ResetGame();
            }

            while (!Input.GetMouseButtonDown(0))
            {
                yield return null;
            }
            ResetGame();
        }
    }

    /// <summary>
    /// 敵の出す手をランダムで決める
    /// </summary>
    public void HandsOfEnemy()
    {
        int r = Random.Range(0, 100);//0 ~ 99の乱数
        int d = r % 3;//余りによって出す手を決める

        if (d == 0)
        {
            //Goo
            m_enemyGooImage.gameObject.SetActive(true);
            m_enemyHandsState = StateHands.Goo;
        }
        else if (d == 1)
        {
            //Choki
            m_enemyChokiImage.gameObject.SetActive(true);
            m_enemyHandsState = StateHands.Choki;
        }
        else
        {
            //Par
            m_enemyParImage.gameObject.SetActive(true);
            m_enemyHandsState = StateHands.Par;
        }
    }

    /// <summary>
    /// 勝敗を判定する
    /// </summary>
    public string Judge()
    {
        //あいこ
        if (m_myHandsState == m_enemyHandsState)
        {
            m_isAiko = true;
            return "あいこで...";
        }

        //勝った
        if (m_myHandsState == StateHands.Goo && m_enemyHandsState == StateHands.Choki
         || m_myHandsState == StateHands.Choki && m_enemyHandsState == StateHands.Par
         || m_myHandsState == StateHands.Par && m_enemyHandsState == StateHands.Goo)
        {
            RecordNumberOfWins.Instance.CountWins++;
            return "勝ったよ！！";
        }

        //負けた
        if (m_myHandsState == StateHands.Goo && m_enemyHandsState == StateHands.Par
            || m_myHandsState == StateHands.Choki && m_enemyHandsState == StateHands.Goo
            || m_myHandsState == StateHands.Par && m_enemyHandsState == StateHands.Choki)
        {
            RecordNumberOfWins.Instance.CountLosses++;
            return "負けだよ...";
        }
        return "";
    }

    /********************
     以下、ボタンを押した時に呼ばれる関数
     ********************/
    public void OnClickGooBtn()
    {
        if (m_isOKClick)
        {
            m_myHandsState = StateHands.Goo;
            m_isOnClickBtn = true;
        }
    }
    public void OnClickChokiBtn()
    {
        if (m_isOKClick)
        {
            m_myHandsState = StateHands.Choki;
            m_isOnClickBtn = true;
        }
    }
    public void OnClickParBtn()
    {
        if (m_isOKClick)
        {
            m_myHandsState = StateHands.Par;
            m_isOnClickBtn = true;
        }
    }

    public void OnClickEixtGameBtn()
    {
        StopCoroutine(m_coroutine);
        SceneManager.LoadScene(m_loadSceneName);
    }
}
