using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SetupTMPWindow : EditorWindow
{
    [MenuItem("Window/Localization/Setup TMP")]
    public static void ShowWindow()
    {
        SetupTMPWindow wnd = GetWindow<SetupTMPWindow>();
        wnd.titleContent = new GUIContent("Setup TMP");
    }

    [SerializeField] private TMP_FontAsset _mainFont;
    [SerializeField] private List<TMP_FontAsset> _fonts;

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElements following a tree hierarchy.
        Label label = new Label("These controls were created using C# code.");
        root.Add(label);
    }

    void Setup()
    {
        // Fix Line Height
        foreach (var fontAsset in _fonts)
        {
            if (fontAsset != null)
            {
                float fontLineHeight = fontAsset.faceInfo.lineHeight;

                float scaleFactor = _mainFont.faceInfo.lineHeight / fontLineHeight;

                var faceInfo = fontAsset.faceInfo;
                faceInfo.scale = scaleFactor;
                fontAsset.faceInfo = faceInfo;

                EditorUtility.SetDirty(fontAsset);
                Debug.Log($"Normalized font: {fontAsset.name}, Scale factor: {scaleFactor}");
            }
        }

        // Change to Dynamic to auto
        _mainFont.atlasPopulationMode = AtlasPopulationMode.Dynamic;

        // To automatically create atlas at runtime
        _mainFont.isMultiAtlasTexturesEnabled = true;

        // Update fallback fonts
        foreach (var font in _fonts)
        {
            if (font != _mainFont && !_mainFont.fallbackFontAssetTable.Contains(font))
            {
                _mainFont.fallbackFontAssetTable.Add(font);
                Debug.Log($"Add {font.name} to fallback fonts!");
            }
        }
    }
}
