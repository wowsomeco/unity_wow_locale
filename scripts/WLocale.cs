using System;
using System.Collections.Generic;
using UnityEngine;
using Wowsome.Generic;

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

    public WObservable<string> SelectedLang { get; set; }
    public string DefaultLang { get; private set; }

    public string path;
    public List<LangModel> languages = new List<LangModel>();

    protected Dictionary<string, LocaleModel> _data = new Dictionary<string, LocaleModel>();

    public void InitLocaleManager(string defaultLang = "en") {
      TextAsset[] tas = Resources.LoadAll<TextAsset>(path);
      foreach (TextAsset t in tas) {
        LocaleModel locale = JsonUtility.FromJson<LocaleModel>(t.text);
        _data[locale.lang] = locale;
      }

      DefaultLang = defaultLang;
      Assert.If(!_data.ContainsKey(DefaultLang), $"Locale: can't find {DefaultLang}");

      SelectedLang = new WObservable<string>(DefaultLang);
    }

    public string GetText(string key) {
      LocaleModel locale = _data[SelectedLang.Value];
      LocaleTextModel textModel = locale.texts.Find(x => x.key == key);
      if (null != textModel) return textModel.value;

      LocaleTextModel defaultTextModel = _data[DefaultLang].texts.Find(x => x.key == key);
      return null != defaultTextModel ? defaultTextModel.value : null;
    }
  }
}