using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tube))]
[CanEditMultipleObjects]
public class TubeEditor : Editor
{
    Tube _tube;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _tube = target as Tube;

        if(GUILayout.Button("Create Balls"))
        {
            EditorUtility.SetDirty(_tube);
            _tube.CreateBalls();
        }
    }
}