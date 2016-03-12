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
        public override void Start()
        {
            this.eventClick += ToggleFollow;
        }

        public override void Update()
        {
            InstanceID instanceID = new InstanceID();
            if (this.parent.objectUserData == null)
            {
                instanceID = WorldInfoPanel.GetCurrentInstanceID();
            }
            else
            {
                instanceID = (InstanceID)this.parent.objectUserData;
            }
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
                this.normalFgSprite = "InfoIconHealthDisabled";
                this.focusedFgSprite = "InfoIconHealthDisabled";
                this.hoveredFgSprite = "InfoIconHealthDisabled";
                this.pressedFgSprite = "InfoIconHealthDisabled";
            }
        }

        public void ToggleFollow(UIComponent component, UIMouseEventParameter eventParam)
        {
            UIView uiView = GameObject.FindObjectOfType<UIView>();
            UIComponent trackerPanel = uiView.FindUIComponent("TrackerPanel");
            UIComponent containerPanel = trackerPanel.Find("UIScrollablePanel");
            FollowedPanel[] followedPanels = containerPanel.GetComponentsInChildren<FollowedPanel>();

            InstanceID instanceID = new InstanceID();
            if (this.parent.objectUserData == null)
            {
                instanceID = WorldInfoPanel.GetCurrentInstanceID();
            }
            else
            {
                instanceID = (InstanceID)this.parent.objectUserData;
            }
            if (CitizenList.followList.Contains(instanceID))
            {
                CitizenList.followList.Remove(instanceID);
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
                newPanel = containerPanel.AddUIComponent(typeof(FollowedPanel)) as FollowedPanel;
                newPanel.instanceID = instanceID;
                CitizenList.followList.Add(instanceID);
            }
        }
    }
}
