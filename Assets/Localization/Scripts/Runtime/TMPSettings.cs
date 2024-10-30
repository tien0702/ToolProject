using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Localization", menuName = "TMP Settings")]
public class TMPSettings : ScriptableObject
{
    private static TMPSettings _instance;
    public static TMPSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = LoadSettings();
            }

            return _instance;
        }
    }

    static TMPSettings LoadSettings()
    {
        const string path = @"Assets/Localization/Resources/Localization.asset";

        var settings = Resources.Load<TMPSettings>(Path.GetFileNameWithoutExtension(path));

        if (settings != null) return settings;

#if UNITY_EDITOR
        settings = CreateInstance<TMPSettings>();

        AssetDatabase.CreateAsset(settings, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#else
        throw new Exception($"Localization settings not found: {path}");
#endif

        return settings;
    }

    public TMP_FontAsset MainFont;
    public List<TMP_FontAsset> FallbackFonts;

#if UNITY_EDITOR

    public void Awake()
    {
        if (MainFont == null)
        {
            //MainFont = ;
        }
    }

#endif
}
