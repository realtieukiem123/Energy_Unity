using System;
using Sirenix.OdinInspector;
using UnityEngine;

// ReSharper disable CheckNamespace

namespace ObjectPooler
{
    [Serializable]
    public class PoolerConfig
    {
        [BoxGroup("Config"), HorizontalGroup("Config/1"), LabelWidth(50)] public string Tag;
        [BoxGroup("Config"), LabelWidth(50)] public GameObject Prefab;
        [HorizontalGroup("Config/1"), LabelWidth(50), PropertyRange(1, 50)] public int Size;
        
        public PoolerConfig(string tag, GameObject prefab, int size)
        {
            Tag = tag;
            Prefab = prefab;
            Size = size;
        }
    }
}