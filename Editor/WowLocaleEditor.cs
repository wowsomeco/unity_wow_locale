using UnityEditor;
using UnityEngine;

namespace Wowsome {
  using EU = EditorUtils;
  using LangModel = WowLocale.LangModel;
  using LocaleModel = WowLocale.LocaleModel;

  [CustomEditor(typeof(WowLocale))]
  public class WowLocaleEditor : Editor {
    Menu<LangModel> _languages = new Menu<LangModel>();
    LangModel _selectedLanguage = null;
    LocaleModel _selectedLocale = null;

    public override void OnInspectorGUI() {

      DrawDefaultInspector();
      WowLocale tgt = (WowLocale)target;

      _languages.Build(new Menu<LangModel>.BuildCallback(
          "Languages",
          tgt.Languages,
          l => l.title,
          s => {
            _selectedLanguage = s.Model;
            TextAsset t = Resources.Load<TextAsset>($"{tgt.Path}/locale_{_selectedLanguage.id}");
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

