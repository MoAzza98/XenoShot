using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptableObjectEditorTab {
    public string tabName;
    public ScriptableObject scriptableObject;
}

[CreateAssetMenu()]
public class ScriptableObjectEditorWorkspace : ScriptableObject {
    public string displayName;
    public List<ScriptableObjectEditorTab> tabs = new List<ScriptableObjectEditorTab>();
}
