using Wowsome.Core;

namespace Wowsome {
  public class WLocaleSystem : WLocale, ISystem {
    public string defaultLang = "en";

    public void InitSystem() {
      InitLocaleManager(defaultLang);
    }

    public void StartSystem(WEngine gameEngine) {

    }

    public void UpdateSystem(float dt) {

    }
  }
}

