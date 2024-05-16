using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable CheckNamespace

namespace ObjectPooler
{
    public static class PoolerEditorItem
    {
        [MenuItem("GameObject/Externals/Pooler", priority = 1)]
        public static void AddPoolerToScene()
        {
            if (!Object.FindObjectOfType<Pooler>())
            {
                var pooler = new GameObject("[Pooler]");
                pooler.AddComponent<Pooler>();
            }
            else
            {
                Debug.Log("Scene already have Pooler.");
            }
        }
    }

    [CustomEditor(typeof(Pooler))] 
    public class PoolerEditor : OdinEditor
    {
        private Pooler pooler;
        private SerializedObject pManager;

        protected override void OnEnable()
        {
            base.OnEnable();
            pooler = (Pooler)target;
            pManager = new SerializedObject(target);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(20);
            var droppedObject = DropAreaGUI();
            if (droppedObject.Length != 0)
            {
                foreach (var droppedObj in droppedObject)
                {
                    var splitObj = droppedObj.ToString().Split('/');
                    var nameObj = splitObj[^1];
                    var splitPrefabName = nameObj.Replace(".prefab", "").Replace(" (UnityEngine.GameObject)", "");
                    var newPrefabConfig = new PoolerConfig(splitPrefabName, droppedObj as GameObject, 5);
                    if (pooler.ConfigPools == null) pooler.ConfigPools = new List<PoolerConfig>();
                    pooler.ConfigPools.Add(newPrefabConfig);
                }
            }
            pManager.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        public object[] DropAreaGUI()
        {
            var toReturn = Array.Empty<object>();

            var evt = Event.current;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            var drop_area = GUILayoutUtility.GetRect(350f, 70f, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUI.Box(drop_area, "Drag multiple prefab here.");

            switch (evt.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (drop_area.Contains(evt.mousePosition))
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        if (evt.type == EventType.DragPerform)
                        {
                            var canGo = false;
                            var quantityToGo = 0;
                            foreach (var obj in DragAndDrop.objectReferences)
                            {
                                if (PrefabUtility.IsPartOfAnyPrefab(obj))
                                {
                                    canGo = true;
                                    quantityToGo++;
                                }
                            }
                            if (canGo)
                            {
                                DragAndDrop.AcceptDrag();
                                toReturn = new object[quantityToGo];
                                var counter = 0;
                                for (var i = 0; i < DragAndDrop.objectReferences.Length; i++)
                                {
                                    if (DragAndDrop.objectReferences[i] is GameObject)
                                    {
                                        DragAndDrop.objectReferences[i].name = DragAndDrop.paths[i];
                                        toReturn[counter] = DragAndDrop.objectReferences[i];
                                        counter++;
                                    }
                                }
                            }
                        }
                    }

                    break;
            }

            return toReturn;
        }
    }
}