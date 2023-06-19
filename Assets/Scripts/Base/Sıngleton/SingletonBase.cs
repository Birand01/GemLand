using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class SingletonBase<T> : MonoBehaviour where T:Component
{
    private static T instance;
    private static bool m_applicationQuit = false;

    public static T GetInstance()
    {
        if(m_applicationQuit)
        {
            return null;
        }
        if(instance == null)
        {
            instance=FindObjectOfType<T>();
            if(instance == null )
            {
                GameObject obj = new GameObject();
                obj.name=typeof(T).Name;
                instance= obj.AddComponent<T>();
            }
        }
        return instance;
    }
    protected virtual void Awake()
    {
        if(instance==null)
        {
            instance = this as T;
            //DontDestroyOnLoad(instance);
        }
        //else if(instance!=this as T)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //   // DontDestroyOnLoad(gameObject);
        //}
    }

    private void  OnApplicationQuit()
    {
        m_applicationQuit=true;
    }
}
