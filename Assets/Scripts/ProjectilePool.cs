using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ProjectilePool
    {

    private static ProjectilePool _instance;
    public Queue<GameObject> pool;
    private ProjectilePool() {
        pool = new Queue<GameObject>();
    }

        public static ProjectilePool GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProjectilePool();
            }
            return _instance;
        }
    
    }