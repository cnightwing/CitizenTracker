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
    public class TrackerButton : UIButton
    {
        public override void Start()
        {
            this.width = 36;
            this.height = 36;

            this.normalBgSprite = "InfoIconBaseNormal";
            this.hoveredBgSprite = "InfoIconBaseHovered";
            this.focusedBgSprite = "InfoIconBaseNormal";
            this.pressedBgSprite = "InfoIconBasePressed";
            this.normalFgSprite = "InfoIconHealth";
            this.hoveredFgSprite = "InfoIconHealthHovered";
            this.focusedFgSprite = "InfoIconHealth";
            this.pressedFgSprite = "InfoIconHealthPressed";
        }
    }
}
