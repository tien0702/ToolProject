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

    public void OnGUI()
    {
        DisplayHelp();

        _settings.MainFont = EditorGUILayout.ObjectField("Main font", _settings.MainFont, typeof(TMP_FontAsset), false) as TMP_FontAsset;
        DisplayTMP_Font();
        var buttonStyle = new GUIStyle(GUI.skin.button) { fontStyle = FontStyle.Bold, fixedHeight = 30 };
        if (GUILayout.Button("Setup TMP", buttonStyle))
        {
            SetupTMP();
        }

        if (GUILayout.Button("Open Localization Tutorial", buttonStyle))
        {
            OpenTutorial();
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

        /*if (property.isArray)
        {
            property.Next(true);

            var length = property.intValue;

            _settings.FallbackFonts.Clear();
        }*/
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

    void SetupTMP()
    {
        if (_settings.MainFont == null)
        {
            Debug.LogError("Main font is null!");
            return;
        }

        // Change to Dynamic to auto
        _settings.MainFont.atlasPopulationMode = AtlasPopulationMode.Dynamic;

        // To automatically create atlas at runtime
        _settings.MainFont.isMultiAtlasTexturesEnabled = true;
        Debug.Log("Finished setting MainFont.");

        // Update fallback fonts
        _settings.MainFont.fallbackFontAssetTable.Clear();
        foreach (var font in _settings.FallbackFonts)
        {
            if (!font.Equals(_settings.MainFont) && !_settings.MainFont.fallbackFontAssetTable.Contains(font))
            {
                _settings.MainFont.fallbackFontAssetTable.Add(font);
                Debug.Log($"Add {font.name} to fallback fonts!");
            }
        }
        Debug.Log("Finished adding Fontback font!");

        foreach (var fontAsset in _settings.FallbackFonts)
        {
            if (fontAsset != null && !fontAsset.Equals(_settings.MainFont))
            {
                float fontLineHeight = fontAsset.faceInfo.lineHeight;

                float scaleFactor = _settings.MainFont.faceInfo.lineHeight / fontLineHeight;

                var faceInfo = fontAsset.faceInfo;
                faceInfo.scale = scaleFactor;
                fontAsset.faceInfo = faceInfo;

                EditorUtility.SetDirty(fontAsset);
                Debug.Log($"Normalized font: {fontAsset.name}, Scale factor: {scaleFactor}");
            }
        }
        Debug.Log("Finished adding Fontback font to Main font!");
    }

    void OpenTutorial()
    {
        Application.OpenURL("https://github.com/tien0702/NixLocalization/blob/main/README.md");
    }

    private void ResetSettings()
    {
        Debug.Log("ResetSettings");
    }
}