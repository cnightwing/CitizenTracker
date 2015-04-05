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
    public class RenderButton : UIButton
    {
        public bool renderEnabled;

        public override void Start()
        {
            this.width = 28;
            this.height = 28;

            this.normalFgSprite = "LocationMarkerNormal";
            this.focusedFgSprite = "LocationMarkerNormal";
            this.hoveredFgSprite = "LocationMarkerNormal";
            this.pressedFgSprite = "LocationMarkerNormal";

            renderEnabled = false;

            this.eventClick += ToggleRender;
        }

        public void ToggleRender(UIComponent component, UIMouseEventParameter eventParam)
        {
            renderEnabled = !renderEnabled;
            if (renderEnabled)
            {
                this.normalFgSprite = "LocationMarkerActiveNormal";
                this.focusedFgSprite = "LocationMarkerActiveNormal";
                this.hoveredFgSprite = "LocationMarkerActiveNormal";
                this.pressedFgSprite = "LocationMarkerActiveNormal";
            }
            else
            {
                this.normalFgSprite = "LocationMarkerNormal";
                this.focusedFgSprite = "LocationMarkerNormal";
                this.hoveredFgSprite = "LocationMarkerNormal";
                this.pressedFgSprite = "LocationMarkerNormal";
            }
        }
    }
}
