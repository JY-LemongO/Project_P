using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // 씬 넘어갈 때 필요하면 DontDestroy에 올리기, 필요 없으면 하지 않음.
    protected static bool _isDontDestroy = true;

    protected static T s_Instance;
    public static T Instance => s_Instance;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (s_Instance == null)
        {
            s_Instance = (T)this;

            if (_isDontDestroy)
                DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (!_isDontDestroy)
            s_Instance = null;
    }

    public virtual void Clear() { }
}
