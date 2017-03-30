/*
using UnityEngine;
using UnityEditor;

/// <summary>
/// Comment this class out if you really hate all the names over objects in the scene view!!!
/// I wrote this to be able to see the names of the navigation nodes. 
/// I know it looks annoying but it really saved my life.
/// </summary> 
public class NameInEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void sdf(Transform transform, GizmoType gizmoType)
    {
        Handles.color = Color.white;
        Handles.Label(transform.position, transform.gameObject.name);
    }
}
*/