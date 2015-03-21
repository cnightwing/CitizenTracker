using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace CitizenTracker
{
    public class FollowButton : UIButton
    {
        public bool isToggled = false;

        public override void Start()
        {
            this.width = 46;
            this.height = 46;

            this.normalBgSprite = "InfoIconBaseNormal";
            this.focusedBgSprite = "InfoIconBaseNormal";
            this.hoveredBgSprite = "InfoIconBaseNormal";
            this.pressedBgSprite = "InfoIconBaseNormal";
            this.normalFgSprite = "";
            this.focusedFgSprite = "";
            this.hoveredFgSprite = "";
            this.pressedFgSprite = "";
        }
    }
}
