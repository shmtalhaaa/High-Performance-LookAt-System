using System.Collections.Generic;
using UnityEngine;

public class LookAtManager : MonoBehaviour
{
    private static readonly List<LookAtTarget> targets = new List<LookAtTarget>();

    #region Singleton
    private static LookAtManager instance;
    public static LookAtManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("LookAtManager");
                instance = go.AddComponent<LookAtManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public static void Register(LookAtTarget target)
    {
        if (!targets.Contains(target))
            targets.Add(target);
    }

    public static void Unregister(LookAtTarget target)
    {
        targets.Remove(target);
    }

    private void Update()
    {
        if (targets.Count == 0) return;

        float deltaTime = Time.deltaTime;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
                continue;
            }
            targets[i].TickRotation(deltaTime);
        }

    }

}
