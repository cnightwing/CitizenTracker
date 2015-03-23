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
    public class TrackerPanel : UIScrollablePanel
    {
        public override void Start()
        {
            this.width = 1076;
            this.height = 360;
            this.autoLayoutDirection = LayoutDirection.Vertical;
            this.autoLayoutStart = LayoutStart.TopLeft;
            this.autoLayoutPadding = new RectOffset(0,0,0,0);
            this.autoLayout = true;
            this.pivot = UIPivotPoint.TopLeft;
            this.AlignTo(this.parent, UIAlignAnchor.TopLeft);
            this.clipChildren = true;
        }
    }
}
