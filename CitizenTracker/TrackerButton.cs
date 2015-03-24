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
        private static UIView uiView = GameObject.FindObjectOfType<UIView>();

        public override void Start()
        {
            this.width = 46;
            this.height = 46;

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
            UIComponent trackerPanel = uiView.FindUIComponent("TrackerPanel");
            UIComponent rightPanel = trackerPanel.parent;
            UIComponent mainPanel = rightPanel.parent;
            mainPanel.isVisible = !mainPanel.isVisible;
            if (mainPanel.isVisible)
            {
                this.normalBgSprite = "InfoIconBaseFocused";
                this.focusedBgSprite = "InfoIconBaseFocused";
                this.hoveredBgSprite = "InfoIconBaseFocused";
                this.pressedBgSprite = "InfoIconBasePressed";
                this.normalFgSprite = "InfoIconHealth";
                this.focusedFgSprite = "InfoIconHealth";
                this.hoveredFgSprite = "InfoIconHealth";
                this.pressedFgSprite = "InfoIconHealth";

                Log.Message("Currently following:");
                foreach (InstanceID id in CitizenList.followList)
                {
                    Log.Message(id.ToString());
                }
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
