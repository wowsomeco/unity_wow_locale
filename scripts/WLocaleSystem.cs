using System.Collections.Generic;
using UnityEngine;
using Wowsome.Core;

namespace Wowsome {
  public class WLocaleSystem : WLocale, ISystem {
    public void InitSystem() {
      InitLocaleManager();
    }

    public void StartSystem(CavEngine gameEngine) {

    }

    public void UpdateSystem(float dt) {

    }
  }
}

