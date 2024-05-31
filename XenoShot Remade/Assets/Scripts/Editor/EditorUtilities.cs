using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations.Rigging;
using System;

public static class EditorUtils {

    public static T SetComponent<T>(GameObject go) where T : Component {
        T component = go.GetComponent<T>();
        if (component == null) {
            component = go.AddComponent<T>();
        }
        return component;
    }
    
    public static GameObject InstantiateWithName(GameObject prefab, Transform parent, string name) {
        var go = UnityEngine.Object.Instantiate(prefab, parent);
        go.name = name;
        return go;
    }

    public static T LoadAsset<T>(string assetPath) where T : UnityEngine.Object {
        return AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;
    }

    public static Editor CreateEditor(UnityEngine.Object obj, ref Editor editor) {
        Editor.CreateCachedEditor(obj, null, ref editor);
        return editor;
    }

    public static Transform FindChild(Transform root, string name) {
        var found = root.Find(name);
        if (found != null) {
            return found;
        }

        for (int i = 0; i < root.childCount; ++i) {
            var child = FindChild(root.GetChild(i), name);
            if (child != null) {
                return child;
            }
        }
        return null;
    }

    public static T FindChildType<T>(Transform root, string name) where T : Component {
        T[] components = root.GetComponentsInChildren<T>();
        foreach(var component in components) {
            if (component.gameObject.name == name) {
                return component;
            }
        }
        return null;
    }
}
