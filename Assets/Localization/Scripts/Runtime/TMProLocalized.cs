using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;

public class TMProLocalized : MonoBehaviour
{
    [SerializeField] protected string _localizationKey;
    [SerializeField] protected TextMeshProUGUI _textMeshPro;

    public string LocalizationKey => _localizationKey;

    protected virtual void Start()
    {
        if (_textMeshPro == null)
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        LocalizationManager.OnLocalizationChanged += Localize;
        Localize();
    }

    protected virtual void OnDestroy()
    {
        LocalizationManager.OnLocalizationChanged -= Localize;
    }

    protected virtual void Localize()
    {
        if (_localizationKey != null && _localizationKey != "")
            _textMeshPro.text = LocalizationManager.Localize(_localizationKey);
    }
}
