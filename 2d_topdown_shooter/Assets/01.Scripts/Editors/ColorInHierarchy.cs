using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ColorInHierarchy : MonoBehaviour
{
#if UNITY_EDITOR
    private static Dictionary<Object, ColorInHierarchy> coloredObjects = new Dictionary<Object, ColorInHierarchy>();

    public string prefix;
    public Color backColor;
    public Color fontColor;

    static ColorInHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID); // 인스턴스 아이디 주면 오브젝트 반환

        if(obj != null && coloredObjects.ContainsKey(obj))
        {
            GameObject gameObj = obj as GameObject;
            ColorInHierarchy cih = gameObj.GetComponent<ColorInHierarchy>();
            if(cih != null)
            {
                PaintObject(obj, selectionRect, cih);
            }
            else
            {
                coloredObjects.Remove(obj); // 사용자가 컴포넌트 제거
            }
        }
    }

    public static void PaintObject(Object obj, Rect selectionRect, ColorInHierarchy cih)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        if(Selection.activeGameObject != obj)
        {
            EditorGUI.DrawRect(bgRect, cih.backColor);

            string name = $"{cih.prefix} {obj.name}";
            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState() { textColor = cih.fontColor },
                fontStyle = FontStyle.Bold
            });
        }
    }

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if(coloredObjects.ContainsKey(this.gameObject) == false)
        {
            coloredObjects.Add(this.gameObject, this);
        }
    }
#endif
}
