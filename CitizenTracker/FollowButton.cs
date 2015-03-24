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
        private static UIView uiView = GameObject.FindObjectOfType<UIView>();

        public override void Start()
        {
            this.width = 36;
            this.height = 36;

            this.normalBgSprite = "InfoIconBaseNormal";
            this.focusedBgSprite = "InfoIconBaseNormal";
            this.hoveredBgSprite = "InfoIconBaseNormal";
            this.pressedBgSprite = "InfoIconBaseNormal";
            this.normalFgSprite = "";
            this.focusedFgSprite = "";
            this.hoveredFgSprite = "";
            this.pressedFgSprite = "";

            this.eventClick += ToggleFollow;
        }

        public override void Update()
        {
            InstanceID instanceID = WorldInfoPanel.GetCurrentInstanceID();
            if (CitizenList.followList.Contains(instanceID))
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
            else
            {
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

        public void ToggleFollow(UIComponent component, UIMouseEventParameter eventParam)
        {
            UIComponent trackerPanel = uiView.FindUIComponent("TrackerPanel");
            FollowedPanel[] followedPanels = trackerPanel.GetComponentsInChildren<FollowedPanel>();

            InstanceID instanceID = WorldInfoPanel.GetCurrentInstanceID();
            if (CitizenList.followList.Contains(instanceID))
            {
                CitizenList.followList.Remove(instanceID);
                Log.Message("Unfollowing " + instanceID.ToString());
                foreach (FollowedPanel followedPanel in followedPanels)
                {
                    if (followedPanel.instanceID == instanceID)
                    {
                        Destroy(followedPanel);
                    }
                }
            }
            else
            {
                FollowedPanel newPanel;
                newPanel = trackerPanel.AddUIComponent(typeof(FollowedPanel)) as FollowedPanel;
                newPanel.instanceID = instanceID;
                CitizenList.followList.Add(instanceID);
                Log.Message("Following " + instanceID.ToString());
            }
        }
    }
}
