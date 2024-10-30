using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;


public class SetupTMPWindow : EditorWindow
{
    private static SerializedObject _serializedObject;
    private static TMPSettings _settings => TMPSettings.Instance;

    [MenuItem("Window/SetupTMPWindow")]
    public static void ShowExample()
    {
        GetWindow<SetupTMPWindow>();
    }

    private float _lineHeight;
    private List<TMP_FontAsset> fonts = new();

    public void OnGUI()
    {
        DisplayHelp();

        EditorGUILayout.ObjectField("Main font", _settings.MainFont, typeof(TMP_FontAsset), false);
        DisplayTMP_Font();
        var buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold, fixedHeight = 30 };
        if (GUILayout.Button("Setup TMP", buttonStyle))
        {

        }

        if (GUILayout.Button("Open Tutorial Localization", buttonStyle))
        {

        }

        if (GUILayout.Button("Reset", buttonStyle))
        {

        }
    }

    private static void DisplayTMP_Font()
    {
        if (_serializedObject == null || _serializedObject.targetObject == null)
        {
            _serializedObject = new SerializedObject(_settings);
        }
        else
        {
            _serializedObject.Update();
        }

        var property = _serializedObject.FindProperty("FallbackFonts");
        EditorGUILayout.PropertyField(property, new GUIContent("Fallback fonts"), true);

        if (property.isArray)
        {

        }
        _serializedObject.ApplyModifiedProperties();
    }

    private static void DisplayHelp()
    {
        EditorGUILayout.HelpBox("" +
            "- Main font là font chính được sử dụng cho TextmeshProUGUI\n" +
            "- Fallback fonts là các font phụ hỗ trợ các ký tự còn thiếu cho MainFont\n" +
            "* Setup này sẽ tự động điều chỉnh scale của fallback fonts để cùng chiều cao với Main font, đặt Fallback fonts vào Main font và tự động thay đổi settings của Main font để phù hợp với đa ngôn ngữ",
            MessageType.None);
    }
}
