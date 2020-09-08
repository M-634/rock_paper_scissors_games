using System;
using UnityEngine;

/// <summary>
/// シングルトンパターンを使用するオブジェクに継承させる抽象クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] bool m_dontDestroyOnLoad = false;
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + "をアタッチしているGameObjectはありません");
                }
            }
            return instance;
        }
    }

    virtual protected void Awake()
    {
        //他のGameObjectにアタッチされているか調べる
        //アタッチされている場合は破棄する
        if (this != Instance)
        {
            Destroy(this);
            Debug.LogError(typeof(T) + "は既に他のGameObjectにアタッチされているため、コンポーネントをはきしました."
                + "アタッチされているGameObjectは" + Instance.gameObject.name + "です。");
        }

        if (m_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}