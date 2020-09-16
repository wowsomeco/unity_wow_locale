using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wowsome {
  public class WowLocale : MonoBehaviour {
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

    public string Path;
    public List<LangModel> Languages = new List<LangModel>();
    public Dictionary<string, LocaleModel> Data = new Dictionary<string, LocaleModel>();

    public string SelectedLang { get; set; }

    public string DefaultLang {
      get { return Languages[0].id; }
    }

    public void InitLocaleManager() {
      TextAsset[] tas = Resources.LoadAll<TextAsset>(Path);
      foreach (TextAsset t in tas) {
        LocaleModel locale = JsonUtility.FromJson<LocaleModel>(t.text);
        Data[locale.lang] = locale;
      }
    }

    public string GetText(string key) {
      if (SelectedLang.IsEmpty()) SelectedLang = Languages[0].id;

      LocaleModel locale = Data[SelectedLang];
      LocaleTextModel textModel = locale.texts.Find(x => x.key == key);
      if (null != textModel) return textModel.value;

      LocaleTextModel defaultTextModel = Data[DefaultLang].texts.Find(x => x.key == key);
      return null != defaultTextModel ? defaultTextModel.value : null;
    }
  }
}

