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
            this.width = 28;
            this.height = 28;

            this.normalBgSprite = "InfoIconBaseNormal";
            this.focusedBgSprite = "InfoIconBaseNormal";
            this.hoveredBgSprite = "InfoIconBaseNormal";
            this.pressedBgSprite = "InfoIconBaseNormal";
            this.normalFgSprite = "InfoIconHealth";
            this.focusedFgSprite = "InfoIconHealth";
            this.hoveredFgSprite = "InfoIconHealth";
            this.pressedFgSprite = "InfoIconHealth";

            this.eventClick += TogglePanel;
        }

        public void TogglePanel(UIComponent component, UIMouseEventParameter eventParam)
        {
            UIView uiView = GameObject.FindObjectOfType<UIView>();
            UIComponent trackerPanel = uiView.FindUIComponent("TrackerPanel");
            trackerPanel.isVisible = !trackerPanel.isVisible;
            if (trackerPanel.isVisible)
            {
                this.normalBgSprite = "InfoIconBaseFocused";
                this.focusedBgSprite = "InfoIconBaseFocused";
                this.hoveredBgSprite = "InfoIconBaseFocused";
                this.pressedBgSprite = "InfoIconBasePressed";
                this.normalFgSprite = "InfoIconHealth";
                this.focusedFgSprite = "InfoIconHealth";
                this.hoveredFgSprite = "InfoIconHealth";
                this.pressedFgSprite = "InfoIconHealth";
            }
            else
            {
                this.normalBgSprite = "InfoIconBaseNormal";
                this.focusedBgSprite = "InfoIconBaseNormal";
                this.hoveredBgSprite = "InfoIconBaseNormal";
                this.pressedBgSprite = "InfoIconBaseNormal";
                this.normalFgSprite = "InfoIconHealth";
                this.focusedFgSprite = "InfoIconHealth";
                this.hoveredFgSprite = "InfoIconHealth";
                this.pressedFgSprite = "InfoIconHealth";
            }
        }
    }
}
