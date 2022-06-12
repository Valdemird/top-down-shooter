using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    public Queue<GameObject> pool;
    private GameManager()
    {
        pool = new Queue<GameObject>();
    }
    public static GameManager Instance
    {
        get
        {
            if (!_Instance)
            {
                
                _Instance = new GameObject().AddComponent<GameManager>();
                _Instance.name = _Instance.GetType().ToString();
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }
}