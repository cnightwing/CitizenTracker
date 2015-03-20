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
    public class TrackerPanel : UIPanel
    {
        public override void Start()
        {
            this.backgroundSprite = "InfoPanelBack";
            this.width = 300;
            this.height = 300;

            this.autoLayoutDirection = LayoutDirection.Vertical;
            this.autoLayoutStart = LayoutStart.TopLeft;
            this.autoLayoutPadding = new RectOffset(0,0,0,0);
            this.autoLayout = true;
        }
    }
}
