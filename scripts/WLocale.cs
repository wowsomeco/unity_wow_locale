using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wowsome {
  public class WLocale : MonoBehaviour {
    [Serializable]
    public class LangModel {
      public string id;
      public string title;
    }

    [Serializable]
    public class LocaleModel {
      public string lang;
      public List<LocaleTextModel> texts = new List<LocaleTextModel>();

      public LocaleModel(string l) {
        lang = l;
      }
    }

    [Serializable]
    public class LocaleTextModel {
      public string key;
      public string value;
    }

    public string SelectedLang { get; set; }

    public string DefaultLang => languages[0].id;

    public string path;
    public List<LangModel> languages = new List<LangModel>();
    public Dictionary<string, LocaleModel> data = new Dictionary<string, LocaleModel>();

    public void InitLocaleManager() {
      TextAsset[] tas = Resources.LoadAll<TextAsset>(path);
      foreach (TextAsset t in tas) {
        LocaleModel locale = JsonUtility.FromJson<LocaleModel>(t.text);
        data[locale.lang] = locale;
      }
    }

    public string GetText(string key) {
      if (SelectedLang.IsEmpty()) SelectedLang = DefaultLang;

      LocaleModel locale = data[SelectedLang];
      LocaleTextModel textModel = locale.texts.Find(x => x.key == key);
      if (null != textModel) return textModel.value;

      LocaleTextModel defaultTextModel = data[DefaultLang].texts.Find(x => x.key == key);
      return null != defaultTextModel ? defaultTextModel.value : null;
    }
  }
}