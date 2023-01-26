using System.IO;
using UnityEditor;
using UnityEngine;

namespace Wowsome {
  using LangModel = WLocale.LangModel;
  using LocaleModel = WLocale.LocaleModel;

  [CustomEditor(typeof(WLocale))]
  public class WLocaleEditor : Editor {
    EditorMenu<LangModel> _languages = new EditorMenu<LangModel>();
    LangModel _selectedLanguage = null;
    LocaleModel _selectedLocale = null;

    public override void OnInspectorGUI() {

      DrawDefaultInspector();
      WLocale tgt = (WLocale)target;

      _languages.Build(new EditorMenu<LangModel>.BuildCallback(
          "Languages",
          tgt.languages,
          l => l.title,
          s => {
            _selectedLanguage = s.Model;
            TextAsset t = Resources.Load<TextAsset>(Path.Combine(tgt.path, $"locale_{_selectedLanguage.id}"));
            if (null == t) {
              _selectedLocale = new LocaleModel(_selectedLanguage.id);
            } else {
              _selectedLocale = JsonUtility.FromJson<LocaleModel>(t.text);
            }
          }
      ));

      if (null != _selectedLocale) {
        EditorGUILayout.LabelField(_selectedLocale.lang, EditorStyles.boldLabel);
      }
    }
  }
}