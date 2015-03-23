using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using UnityEngine;

namespace CitizenTracker
{
    public class HeaderPanel : UIPanel
    {
        public InstanceID ID;

        UISprite placeholder;
        UILabel nameHeader;
        UILabel ageeduHeader;
        UILabel homeHeader;
        UILabel workHeader;
        UILabel statusHeader;

        public override void Start()
        {
            this.height = 36;
            this.width = 1086;
            this.backgroundSprite = "GenericTabDisabled";
            this.autoLayoutDirection = LayoutDirection.Horizontal;
            this.autoLayoutStart = LayoutStart.BottomLeft;
            this.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            this.autoLayout = true;
            this.isInteractive = false;

            placeholder = this.AddUIComponent(typeof(UISprite)) as UISprite;

            placeholder.width = 46;

            nameHeader = this.AddUIComponent(typeof(UILabel)) as UILabel;
            
            nameHeader.text = "Citizen";
            nameHeader.autoSize = false;
            nameHeader.width = 160;
            nameHeader.height = 36;
            nameHeader.textAlignment = UIHorizontalAlignment.Left;
            nameHeader.verticalAlignment = UIVerticalAlignment.Middle;

            ageeduHeader = this.AddUIComponent(typeof(UILabel)) as UILabel;
            
            ageeduHeader.text = "Education / Age";
            ageeduHeader.autoSize = false;
            ageeduHeader.width = 200;
            ageeduHeader.height = 36;
            ageeduHeader.textAlignment = UIHorizontalAlignment.Left;
            ageeduHeader.verticalAlignment = UIVerticalAlignment.Middle;
            
            homeHeader = this.AddUIComponent(typeof(UILabel)) as UILabel;
            
            homeHeader.text = "Residence";
            homeHeader.autoSize = false;
            homeHeader.width = 200;
            homeHeader.height = 36;
            homeHeader.textAlignment = UIHorizontalAlignment.Left;
            homeHeader.verticalAlignment = UIVerticalAlignment.Middle;

            workHeader = this.AddUIComponent(typeof(UILabel)) as UILabel;
            
            workHeader.text = "Occupation";
            workHeader.autoSize = false;
            workHeader.width = 240;
            workHeader.height = 36;
            workHeader.textAlignment = UIHorizontalAlignment.Left;
            workHeader.verticalAlignment = UIVerticalAlignment.Middle;

            statusHeader = this.AddUIComponent(typeof(UILabel)) as UILabel;

            statusHeader.text = "Status";
            statusHeader.autoSize = false;
            statusHeader.width = 240;
            statusHeader.height = 36;
            statusHeader.textAlignment = UIHorizontalAlignment.Left;
            statusHeader.verticalAlignment = UIVerticalAlignment.Middle;
            }
    }
}
