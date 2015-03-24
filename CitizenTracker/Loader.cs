using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICities;
using ColossalFramework;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using UnityEngine;

namespace CitizenTracker
{
    public class Loader : LoadingExtensionBase
    {
        public TrackerButton trackerButton;
        public UIPanel mainPanel;
        public HeaderPanel headerPanel;
        public UIPanel bodyPanel;
        public TrackerPanel trackerPanel;
        public FollowButton followButton;

        public static List<InstanceID> citizenList = new List<InstanceID>();

        public override void OnLevelLoaded(LoadMode mode)
        {
            var uiView = GameObject.FindObjectOfType<UIView>();

            //Button to make the panel appear
            trackerButton = uiView.AddUIComponent(typeof(TrackerButton)) as TrackerButton;
            UIComponent escButton = uiView.FindUIComponent("Esc");
            trackerButton.relativePosition = new Vector2
            (
                escButton.relativePosition.x,
                escButton.relativePosition.y + 50
            );
			trackerButton.zOrder +=100;

            //Panel, which is a scrollbar and panel, containing a headerpanel and panel, containing FollowedPanels.
            mainPanel = uiView.AddUIComponent(typeof(UIPanel)) as UIPanel;
            mainPanel.isVisible = false;
            mainPanel.height = 396;
            mainPanel.width = 1086;
            mainPanel.backgroundSprite = "GenericPanel";
            mainPanel.relativePosition = new Vector2
            (
                escButton.relativePosition.x + escButton.width - 1086,
                escButton.relativePosition.y + 100
            );
            mainPanel.autoLayout = true;
            mainPanel.autoLayoutDirection = LayoutDirection.Vertical;
            mainPanel.autoLayoutStart = LayoutStart.TopLeft;

            headerPanel = mainPanel.AddUIComponent(typeof(HeaderPanel)) as HeaderPanel;

            bodyPanel = mainPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            bodyPanel.height = 360;
            bodyPanel.width = 1086;
            bodyPanel.autoLayout = true;
            bodyPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            bodyPanel.autoLayoutStart = LayoutStart.TopLeft;

            trackerPanel = bodyPanel.AddUIComponent(typeof(TrackerPanel)) as TrackerPanel;

            UIScrollbar scrollbar = bodyPanel.AddUIComponent<UIScrollbar>();
            scrollbar.width = 10;
            scrollbar.height = bodyPanel.height;
            scrollbar.orientation = UIOrientation.Vertical;
            scrollbar.pivot = UIPivotPoint.TopRight;
            scrollbar.AlignTo(scrollbar.parent, UIAlignAnchor.TopRight);
            scrollbar.minValue = 0;
            scrollbar.value = 0;
            scrollbar.incrementAmount = 36;

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

            trackerPanel.verticalScrollbar = scrollbar;
            trackerPanel.eventMouseWheel += (component, param) =>
            {
                var sign = Math.Sign(param.wheelDelta);
                trackerPanel.scrollPosition += new Vector2(0, sign * (-1) * 36);
            };

            //Button to follow citizens
            UIComponent citizenWorldInfoPanel = uiView.FindUIComponent("(Library) CitizenWorldInfoPanel");
            followButton = citizenWorldInfoPanel.AddUIComponent(typeof(FollowButton)) as FollowButton;
            UIComponent smileyFace = citizenWorldInfoPanel.Find("Happiness");
            followButton.relativePosition = new Vector2
            (
                smileyFace.relativePosition.x + (smileyFace.width / 2.0f) - 18,
                smileyFace.relativePosition.y + 40
            );
        }
    }
}
