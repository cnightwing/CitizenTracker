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
        public UILabel headerLabel;
        public UIPanel bodyPanel;
        public UIScrollablePanel containerPanel;

        public override void Start()
        {
            this.height = 760;
            this.width = 370;
            this.backgroundSprite = "MenuPanel";
            this.autoLayout = true;
            this.autoLayoutDirection = LayoutDirection.Vertical;
            this.autoLayoutStart = LayoutStart.TopLeft;

            //Header label
            headerLabel = this.AddUIComponent(typeof(UILabel)) as UILabel;
            headerLabel.autoSize = false;
            headerLabel.height = 40;
            headerLabel.width = this.width;
            headerLabel.textAlignment = UIHorizontalAlignment.Center;
            headerLabel.verticalAlignment = UIVerticalAlignment.Middle;
            headerLabel.text = "Favourites";
            headerLabel.textScale = 1.3125f;

            //Body panel to contain the scrollbar and containerPanel
            bodyPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;
            bodyPanel.height = this.height - headerLabel.height;
            bodyPanel.width = this.width;
            bodyPanel.autoLayout = true;
            bodyPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            bodyPanel.autoLayoutStart = LayoutStart.TopLeft;

            //Scrollbar
            UIScrollbar scrollbar = bodyPanel.AddUIComponent<UIScrollbar>();
            scrollbar.width = 10;
            scrollbar.height = bodyPanel.height;
            scrollbar.orientation = UIOrientation.Vertical;
            scrollbar.pivot = UIPivotPoint.TopRight;
            scrollbar.AlignTo(scrollbar.parent, UIAlignAnchor.TopRight);
            scrollbar.minValue = 0;
            scrollbar.value = 0;
            scrollbar.incrementAmount = 24;

            UISlicedSprite trackSprite = scrollbar.AddUIComponent<UISlicedSprite>();
            trackSprite.relativePosition = Vector2.zero;
            trackSprite.autoSize = true;
            trackSprite.size = trackSprite.parent.size;
            trackSprite.fillDirection = UIFillDirection.Vertical;
            trackSprite.spriteName = "ScrollbarTrack";

            scrollbar.trackObject = trackSprite;

            UISlicedSprite thumbSprite = scrollbar.AddUIComponent<UISlicedSprite>();
            thumbSprite.relativePosition = Vector2.zero;
            thumbSprite.autoSize = true;
            thumbSprite.width = thumbSprite.parent.width;
            thumbSprite.fillDirection = UIFillDirection.Vertical;
            thumbSprite.spriteName = "ScrollbarThumb";

            scrollbar.thumbObject = thumbSprite;

            //Container Panel to contain the FollowedPanels
            containerPanel = bodyPanel.AddUIComponent(typeof(UIScrollablePanel)) as UIScrollablePanel;
            containerPanel.width = this.width - scrollbar.width;
            containerPanel.height = bodyPanel.height;
            containerPanel.autoLayoutDirection = LayoutDirection.Vertical;
            containerPanel.autoLayoutStart = LayoutStart.TopLeft;
            containerPanel.autoLayoutPadding = new RectOffset(0, 0, 0, 0);
            containerPanel.autoLayout = true;
            containerPanel.pivot = UIPivotPoint.TopLeft;
            containerPanel.AlignTo(containerPanel.parent, UIAlignAnchor.TopLeft);
            containerPanel.clipChildren = true;

            //Fix to make scrolling work properly
            containerPanel.verticalScrollbar = scrollbar;
            containerPanel.eventMouseWheel += (component, param) =>
            {
                var sign = Math.Sign(param.wheelDelta);
                containerPanel.scrollPosition += new Vector2(0, sign * (-1) * 32);
            };

            //Add panels for existing follows
            foreach (InstanceID follow in CitizenList.followList)
            {
                FollowedPanel newPanel;
                newPanel = containerPanel.AddUIComponent(typeof(FollowedPanel)) as FollowedPanel;
                newPanel.instanceID = follow;
            }
        }
    }
}
