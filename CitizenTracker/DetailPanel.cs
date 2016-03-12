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
    public class DetailPanel : UIPanel
    {
        public InstanceID instanceID;

        string Name;
        int Age;
        string AgeGroup;
        string Education;
        string WealthLevel;
        float Wealth;
        string Home;
        InstanceID homeID;
        string District;
        string Work;
        InstanceID workID;
        string Status;
        string Target;
        InstanceID targetID;
        string Health;
        string Well;
        InstanceID[] Family = new InstanceID[5];

        CitizenManager cManager = Singleton<CitizenManager>.instance;
        BuildingManager bManager = Singleton<BuildingManager>.instance;
        DistrictManager dManager = Singleton<DistrictManager>.instance;

        //Title bar
        UIPanel headerPanel;
        UILabel nameLabel;
        UIButton closeButton;
        UIDragHandle dragHandle;

        //Body panel
        UIPanel paddingPanel;
        UIPanel bodyPanel;
        
        //Age minipanels
        UIPanel agePanel;
        UILabel ageTitle;
        UILabel ageLabel;
        UIPanel agePanel2;
        UILabel ageTitle2;
        UIProgressBar ageBar;

        //Education minipanel
        UIPanel eduPanel;
        UILabel eduTitle;
        UILabel eduLabel;
        UISprite eduSprite1;
        UISprite eduSprite2;
        UISprite eduSprite3;

        //Wealth minipanel
        UIPanel wealthPanel;
        UILabel wealthTitle;
        UILabel wealthLabel;
        UIPanel wealthPanel2;
        UILabel wealthTitle2;
        UIProgressBar wealthBar;

        //Health minipanel
        UIPanel healthPanel;
        UILabel healthTitle;
        UISprite healthSprite;
        UILabel healthLabel;

        //Wellbeing minipanel
        UIPanel wellPanel;
        UILabel wellTitle;
        UISprite wellSprite;
        UILabel wellLabel;

        //Home minipanel
        UIPanel homePanel;
        UILabel homeTitle;
        UIButton homeButton;
        UIPanel homePanel2;
        UILabel homeTitle2;
        UILabel districtLabel;

        //Work minipanel
        UIPanel workPanel;
        UILabel workTitle;
        UIButton workButton;

        //Activity minipanel
        UIPanel statusPanel;
        UILabel statusTitle;
        UIPanel targetPanel;
        UILabel statusLabel;
        UIButton targetButton;
        
        //Family minipanel
        UIPanel[] familyPanels = new UIPanel[5];
        UILabel[] familyTitles = new UILabel[5];
        UIButton[] familyNames = new UIButton[5];
        FollowButton[] familyButtons = new FollowButton[5];

        int ph = 24;
        int pad = 2;
        int lw = 90;
        int rw = 240;

        public override void Start()
        {
            this.width = 360;
            this.height = ((ph + (2 * pad)) * 11) + 40 + (2 * pad) + (2 * (ph - 18));
            this.backgroundSprite = "MenuPanel";
            this.autoLayout = true;
            this.autoLayoutDirection = LayoutDirection.Vertical;
            this.autoLayoutStart = LayoutStart.TopLeft;

            //Header panel
            headerPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;
            headerPanel.width = 360;
            headerPanel.height = 40;
            
            nameLabel = headerPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            nameLabel.autoSize = false;
            nameLabel.width = 360;
            nameLabel.height = 40;
            nameLabel.relativePosition = Vector3.zero;
            nameLabel.textAlignment = UIHorizontalAlignment.Center;
            nameLabel.verticalAlignment = UIVerticalAlignment.Middle;
            nameLabel.textScale = 1.3125f;

            closeButton = headerPanel.AddUIComponent(typeof(UIButton)) as UIButton;
            closeButton.width = 32;
            closeButton.height = 32;
            closeButton.normalFgSprite = "buttonclose";
            closeButton.hoveredFgSprite = "buttonclosehover";
            closeButton.pressedFgSprite = "buttonclosepressed";
            closeButton.relativePosition = new Vector3(324, 4);
            closeButton.eventClick += CloseWindow;

            dragHandle = headerPanel.AddUIComponent(typeof(UIDragHandle)) as UIDragHandle;
            dragHandle.width = 320;
            dragHandle.height = 40;
            dragHandle.relativePosition = Vector3.zero;
            dragHandle.target = this;

            //Padding and body panels
            paddingPanel = this.AddUIComponent(typeof(UIPanel)) as UIPanel;
            paddingPanel.width = 360;
            paddingPanel.height = this.height-headerPanel.height;
            paddingPanel.autoLayoutDirection = LayoutDirection.Vertical;
            paddingPanel.autoLayoutStart = LayoutStart.TopLeft;
            paddingPanel.autoLayoutPadding = new RectOffset(5, 5, pad + (ph - 18), pad + (ph - 18));
            paddingPanel.autoLayout = true;

            bodyPanel = paddingPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            bodyPanel.width = 350;
            bodyPanel.height = paddingPanel.height - (2 * pad);
            bodyPanel.autoLayoutDirection = LayoutDirection.Vertical;
            bodyPanel.autoLayoutStart = LayoutStart.TopLeft;
            bodyPanel.autoLayoutPadding = new RectOffset(0, 0, pad, pad);
            bodyPanel.autoLayout = true;

            //Age minipanel
            agePanel = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            agePanel.width = 350;
            agePanel.height = ph;
            agePanel.autoLayoutDirection = LayoutDirection.Horizontal;
            agePanel.autoLayoutStart = LayoutStart.TopLeft;
            agePanel.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            agePanel.autoLayout = true;

            ageTitle = agePanel.AddUIComponent(typeof(UILabel)) as UILabel;
            ageTitle.text = "Age";
            ageTitle.autoSize = false;
            ageTitle.width = lw;
            ageTitle.height = ph;
            ageTitle.textAlignment = UIHorizontalAlignment.Right;
            ageTitle.verticalAlignment = UIVerticalAlignment.Middle;

            ageLabel = agePanel.AddUIComponent(typeof(UILabel)) as UILabel;
            ageLabel.autoSize = false;
            ageLabel.width = rw;
            ageLabel.height = ph;
            ageLabel.textAlignment = UIHorizontalAlignment.Left;
            ageLabel.verticalAlignment = UIVerticalAlignment.Middle;

            agePanel2 = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            agePanel2.width = 350;
            agePanel2.height = ph;
            agePanel2.autoLayoutDirection = LayoutDirection.Horizontal;
            agePanel2.autoLayoutStart = LayoutStart.TopLeft;
            agePanel2.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            agePanel2.autoLayout = true;

            ageTitle2 = agePanel2.AddUIComponent(typeof(UILabel)) as UILabel;
            ageTitle2.text = "";
            ageTitle2.autoSize = false;
            ageTitle2.width = lw;
            ageTitle2.height = ph;
            ageTitle2.textAlignment = UIHorizontalAlignment.Right;
            ageTitle2.verticalAlignment = UIVerticalAlignment.Middle;

            ageBar = agePanel2.AddUIComponent(typeof(UIProgressBar)) as UIProgressBar;
            ageBar.width = rw;
            ageBar.height = 18;
            ageBar.backgroundSprite = "GenericProgressBar";
            ageBar.progressSprite = "GenericProgressBarFill";

            //Education
            eduPanel = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            eduPanel.width = 350;
            eduPanel.height = ph;
            eduPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            eduPanel.autoLayoutStart = LayoutStart.TopLeft;
            eduPanel.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            eduPanel.autoLayout = true;

            eduTitle = eduPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            eduTitle.text = "Education";
            eduTitle.autoSize = false;
            eduTitle.width = lw;
            eduTitle.height = ph;
            eduTitle.textAlignment = UIHorizontalAlignment.Right;
            eduTitle.verticalAlignment = UIVerticalAlignment.Middle;

            eduLabel = eduPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            eduLabel.autoSize = false;
            eduLabel.width = rw - (72 + 30);
            eduLabel.height = ph;
            eduLabel.textAlignment = UIHorizontalAlignment.Left;
            eduLabel.verticalAlignment = UIVerticalAlignment.Middle;

            eduSprite1 = eduPanel.AddUIComponent(typeof(UISprite)) as UISprite;
            eduSprite1.width = ph;
            eduSprite1.height = ph;

            eduSprite2 = eduPanel.AddUIComponent(typeof(UISprite)) as UISprite;
            eduSprite2.width = ph;
            eduSprite2.height = ph;

            eduSprite3 = eduPanel.AddUIComponent(typeof(UISprite)) as UISprite;
            eduSprite3.width = ph;
            eduSprite3.height = ph;

            //Wealth minipanel
            wealthPanel = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            wealthPanel.width = 350;
            wealthPanel.height = ph;
            wealthPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            wealthPanel.autoLayoutStart = LayoutStart.TopLeft;
            wealthPanel.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            wealthPanel.autoLayout = true;

            wealthTitle = wealthPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            wealthTitle.text = "Wealth";
            wealthTitle.autoSize = false;
            wealthTitle.width = lw;
            wealthTitle.height = ph;
            wealthTitle.textAlignment = UIHorizontalAlignment.Right;
            wealthTitle.verticalAlignment = UIVerticalAlignment.Middle;

            wealthLabel = wealthPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            wealthLabel.autoSize = false;
            wealthLabel.width = rw;
            wealthLabel.height = ph;
            wealthLabel.textAlignment = UIHorizontalAlignment.Left;
            wealthLabel.verticalAlignment = UIVerticalAlignment.Middle;

            wealthPanel2 = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            wealthPanel2.width = 350;
            wealthPanel2.height = ph;
            wealthPanel2.autoLayoutDirection = LayoutDirection.Horizontal;
            wealthPanel2.autoLayoutStart = LayoutStart.TopLeft;
            wealthPanel2.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            wealthPanel2.autoLayout = true;

            wealthTitle2 = wealthPanel2.AddUIComponent(typeof(UILabel)) as UILabel;
            wealthTitle2.text = "";
            wealthTitle2.autoSize = false;
            wealthTitle2.width = lw;
            wealthTitle2.height = ph;
            wealthTitle2.textAlignment = UIHorizontalAlignment.Right;
            wealthTitle2.verticalAlignment = UIVerticalAlignment.Middle;

            wealthBar = wealthPanel2.AddUIComponent(typeof(UIProgressBar)) as UIProgressBar;
            wealthBar.width = rw;
            wealthBar.height = 18;
            wealthBar.backgroundSprite = "GenericProgressBar";
            wealthBar.progressSprite = "GenericProgressBarFill";
            wealthBar.minValue = 0;
            wealthBar.maxValue = 1;


            //Health minipanel
            healthPanel = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            healthPanel.width = 350;
            healthPanel.height = ph;
            healthPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            healthPanel.autoLayoutStart = LayoutStart.TopLeft;
            healthPanel.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            healthPanel.autoLayout = true;

            healthTitle = healthPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            healthTitle.text = "Health";
            healthTitle.autoSize = false;
            healthTitle.width = lw;
            healthTitle.height = ph;
            healthTitle.textAlignment = UIHorizontalAlignment.Right;
            healthTitle.verticalAlignment = UIVerticalAlignment.Middle;

            healthSprite = healthPanel.AddUIComponent(typeof(UISprite)) as UISprite;
            healthSprite.width = ph;
            healthSprite.height = ph;

            healthLabel = healthPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            healthLabel.autoSize = false;
            healthLabel.width = rw - (ph + (2 * pad));
            healthLabel.height = ph;
            healthLabel.textAlignment = UIHorizontalAlignment.Left;
            healthLabel.verticalAlignment = UIVerticalAlignment.Middle;

            //Wellbeing minipanel
            wellPanel = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            wellPanel.width = 350;
            wellPanel.height = ph;
            wellPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            wellPanel.autoLayoutStart = LayoutStart.TopLeft;
            wellPanel.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            wellPanel.autoLayout = true;

            wellTitle = wellPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            wellTitle.text = "Wellbeing";
            wellTitle.autoSize = false;
            wellTitle.width = lw;
            wellTitle.height = ph;
            wellTitle.textAlignment = UIHorizontalAlignment.Right;
            wellTitle.verticalAlignment = UIVerticalAlignment.Middle;

            wellSprite = wellPanel.AddUIComponent(typeof(UISprite)) as UISprite;
            wellSprite.width = ph;
            wellSprite.height = ph;

            wellLabel = wellPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            wellLabel.autoSize = false;
            wellLabel.width = rw - (ph + (2 * pad));
            wellLabel.height = ph;
            wellLabel.textAlignment = UIHorizontalAlignment.Left;
            wellLabel.verticalAlignment = UIVerticalAlignment.Middle;

            //Home minipanel
            homePanel = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            homePanel.width = 350;
            homePanel.height = ph;
            homePanel.autoLayoutDirection = LayoutDirection.Horizontal;
            homePanel.autoLayoutStart = LayoutStart.TopLeft;
            homePanel.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            homePanel.autoLayout = true;

            homeTitle = homePanel.AddUIComponent(typeof(UILabel)) as UILabel;
            homeTitle.text = "Residence";
            homeTitle.autoSize = false;
            homeTitle.width = lw;
            homeTitle.height = ph;
            homeTitle.textAlignment = UIHorizontalAlignment.Right;
            homeTitle.verticalAlignment = UIVerticalAlignment.Middle;

            homeButton = homePanel.AddUIComponent(typeof(UIButton)) as UIButton;
            homeButton.width = rw;
            homeButton.height = ph;
            homeButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            homeButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            homeButton.textColor = new Color32(55, 148, 200, 255);
            homeButton.disabledTextColor = new Color32(7, 7, 7, 255);
            homeButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            homeButton.focusedTextColor = new Color32(55, 148, 200, 255);
            homeButton.pressedTextColor = new Color32(2, 48, 61, 255);
            homeButton.eventClick += GoToHome;

            homePanel2 = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            homePanel2.width = 350;
            homePanel2.height = ph;
            homePanel2.autoLayoutDirection = LayoutDirection.Horizontal;
            homePanel2.autoLayoutStart = LayoutStart.TopLeft;
            homePanel2.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            homePanel2.autoLayout = true;

            homeTitle2 = homePanel2.AddUIComponent(typeof(UILabel)) as UILabel;
            homeTitle2.text = "";
            homeTitle2.autoSize = false;
            homeTitle2.width = lw;
            homeTitle2.height = ph;
            homeTitle2.textAlignment = UIHorizontalAlignment.Right;
            homeTitle2.verticalAlignment = UIVerticalAlignment.Middle;

            districtLabel = homePanel2.AddUIComponent(typeof(UILabel)) as UILabel;
            districtLabel.autoSize = false;
            districtLabel.width = rw;
            districtLabel.height = ph;
            districtLabel.textAlignment = UIHorizontalAlignment.Left;
            districtLabel.verticalAlignment = UIVerticalAlignment.Middle;

            //Work minipanel
            workPanel = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            workPanel.width = 350;
            workPanel.height = ph;
            workPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            workPanel.autoLayoutStart = LayoutStart.TopLeft;
            workPanel.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            workPanel.autoLayout = true;

            workTitle = workPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            workTitle.text = "Workplace";
            workTitle.autoSize = false;
            workTitle.width = lw;
            workTitle.height = ph;
            workTitle.textAlignment = UIHorizontalAlignment.Right;
            workTitle.verticalAlignment = UIVerticalAlignment.Middle;

            workButton = workPanel.AddUIComponent(typeof(UIButton)) as UIButton;
            workButton.width = rw;
            workButton.height = ph;
            workButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            workButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            workButton.textColor = new Color32(55, 148, 200, 255);
            workButton.disabledTextColor = new Color32(7, 7, 7, 255);
            workButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            workButton.focusedTextColor = new Color32(55, 148, 200, 255);
            workButton.pressedTextColor = new Color32(2, 48, 61, 255);
            workButton.eventClick += GoToWork;

            //Status Panel
            statusPanel = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            statusPanel.width = 350;
            statusPanel.height = ph;
            statusPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            statusPanel.autoLayoutStart = LayoutStart.TopLeft;
            statusPanel.autoLayoutPadding = new RectOffset(5, 5, 0, 0);
            statusPanel.autoLayout = true;

            statusTitle = statusPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            statusTitle.text = "Activity";
            statusTitle.autoSize = false;
            statusTitle.width = lw;
            statusTitle.height = ph;
            statusTitle.textAlignment = UIHorizontalAlignment.Right;
            statusTitle.verticalAlignment = UIVerticalAlignment.Middle;

            targetPanel = statusPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            targetPanel.width = rw;
            targetPanel.height = ph;
            targetPanel.autoLayoutDirection = LayoutDirection.Horizontal;
            targetPanel.autoLayoutStart = LayoutStart.TopLeft;
            targetPanel.autoLayoutPadding = new RectOffset(0, 0, 3, 3);
            targetPanel.autoLayout = true;

            statusLabel = targetPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            statusLabel.textAlignment = UIHorizontalAlignment.Left;
            statusLabel.verticalAlignment = UIVerticalAlignment.Middle;

            targetButton = targetPanel.AddUIComponent(typeof(UIButton)) as UIButton;
            targetButton.autoSize = true;
            targetButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            targetButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            targetButton.textColor = new Color32(55, 148, 200, 255);
            targetButton.disabledTextColor = new Color32(7, 7, 7, 255);
            targetButton.hoveredTextColor = new Color32(255, 255, 255, 255);
            targetButton.focusedTextColor = new Color32(55, 148, 200, 255);
            targetButton.pressedTextColor = new Color32(2, 48, 61, 255);
            targetButton.eventClick += GoToTarget;

            //Family minipanels
            for (int i = 0; i < 5; i++)
            {
                familyPanels[i] = bodyPanel.AddUIComponent(typeof(UIPanel)) as UIPanel;
                familyPanels[i].isVisible = false;
                familyPanels[i].width = 350;
                familyPanels[i].height = ph;
                familyPanels[i].autoLayoutDirection = LayoutDirection.Horizontal;
                familyPanels[i].autoLayoutStart = LayoutStart.TopLeft;
                familyPanels[i].autoLayoutPadding = new RectOffset(5, 5, 0, 0);
                familyPanels[i].autoLayout = true;

                familyTitles[i] = familyPanels[i].AddUIComponent(typeof(UILabel)) as UILabel;
                familyTitles[i].autoSize = false;
                familyTitles[i].width = lw;
                familyTitles[i].height = ph;
                familyTitles[i].textAlignment = UIHorizontalAlignment.Right;
                familyTitles[i].verticalAlignment = UIVerticalAlignment.Middle;

                familyButtons[i] = familyPanels[i].AddUIComponent(typeof(FollowButton)) as FollowButton;
                familyButtons[i].width = ph;
                familyButtons[i].height = ph;

                familyNames[i] = familyPanels[i].AddUIComponent(typeof(UIButton)) as UIButton;
                familyNames[i].width = rw - (ph + (2 * pad));
                familyNames[i].height = ph;
                familyNames[i].textHorizontalAlignment = UIHorizontalAlignment.Left;
                familyNames[i].textVerticalAlignment = UIVerticalAlignment.Middle;
                familyNames[i].textColor = new Color32(55, 148, 200, 255);
                familyNames[i].disabledTextColor = new Color32(7, 7, 7, 255);
                familyNames[i].hoveredTextColor = new Color32(255, 255, 255, 255);
                familyNames[i].focusedTextColor = new Color32(55, 148, 200, 255);
                familyNames[i].pressedTextColor = new Color32(2, 48, 61, 255);
                familyNames[i].eventClick += GoToFamily;
            }
        }

        public override void Update()
        {
            uint citizen = instanceID.Citizen;

            if (cManager.GetCitizenName(citizen) != null)
            {
                CitizenInfo citizenInfo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);

                //Name
                Name = cManager.GetCitizenName(citizen);
                nameLabel.text = this.Name;

                //Age
                Age = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Age;
                if (Age < 15)
                {
                    AgeGroup = "Child";
                    ageBar.minValue = 0;
                    ageBar.maxValue = 15;
                }
                else if (Age < 45)
                {
                    AgeGroup = "Teen";
                    ageBar.minValue = 15;
                    ageBar.maxValue = 45;
                }
                else if (Age < 90)
                {
                    AgeGroup = "Young Adult";
                    ageBar.minValue = 45;
                    ageBar.maxValue = 90;
                }
                else if (Age < 180)
                {
                    AgeGroup = "Adult";
                    ageBar.minValue = 90;
                    ageBar.maxValue = 180;
                }
                else if (Age < 256)
                {
                    AgeGroup = "Senior";
                    ageBar.minValue = 180;
                    ageBar.maxValue = 256;
                }
                ageLabel.text = this.AgeGroup;
                ageBar.value = this.Age;

                //Education
                string eduLevel = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].EducationLevel.ToString();
                switch (eduLevel)
                {
                    case "Uneducated":
                        Education = "Uneducated";
                        break;
                    case "OneSchool":
                        Education = "Educated";
                        break;
                    case "TwoSchools":
                        Education = "Well Educated";
                        break;
                    case "ThreeSchools":
                        Education = "Highly Educated";
                        break;
                }
                eduLabel.text = this.Education;
                
                bool edu1 = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Education1;
                bool edu2 = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Education2;
                bool edu3 = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Education3;
                if (edu1)
                {
                    eduSprite1.spriteName = "InfoIconEducation";
                }
                else
                {
                    eduSprite1.spriteName = "InfoIconEducationDisabled";
                }
                if (edu2)
                {
                    eduSprite2.spriteName = "InfoIconEducation";
                }
                else
                {
                    eduSprite2.spriteName = "InfoIconEducationDisabled";
                }
                if (edu3)
                {
                    eduSprite3.spriteName = "InfoIconEducation";
                }
                else
                {
                    eduSprite3.spriteName = "InfoIconEducationDisabled";
                }

                //Home and wealth
                ushort homeNo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                Vector3 homePos = bManager.m_buildings.m_buffer[(int)((UIntPtr)homeNo)].m_position;
                int districtNo = (int)dManager.GetDistrict(homePos);
                if (homeNo != 0)
                {
                    homeID.Building = homeNo;
                    Home = bManager.GetBuildingName(homeNo, instanceID);
                    BuildingInfo homeInfo = bManager.m_buildings.m_buffer[(int)homeNo].Info;
                    string levelInfo = homeInfo.m_buildingAI.GetLevelUpInfo(homeNo, ref bManager.m_buildings.m_buffer[(int)homeNo], out Wealth);
                    ItemClass.Level wealthLevel = bManager.m_buildings.m_buffer[(int)homeNo].Info.m_class.m_level;
                    switch (wealthLevel)
                    {
                        case ItemClass.Level.Level1:
                            WealthLevel = "Lowest Wealth";
                            break;
                        case ItemClass.Level.Level2:
                            WealthLevel = "Low Wealth";
                            break;
                        case ItemClass.Level.Level3:
                            WealthLevel = "Medium Wealth";
                            break;
                        case ItemClass.Level.Level4:
                            WealthLevel = "High Wealth";
                            break;
                        case ItemClass.Level.Level5:
                            WealthLevel = "Highest Wealth";
                            break;
                    }
                    wealthLabel.text = this.WealthLevel;
                    wealthBar.value = this.Wealth % 1;
                    homeButton.text = this.Home;
                    homeButton.Enable();
                }
                else
                {
                    homeButton.text = "Homeless";
                    homeButton.Disable();
                }
                if (districtNo == 0)
                {
                    District = "No district";
                }
                else
                {
                    District = dManager.GetDistrictName(districtNo);
                }
                districtLabel.text = this.District;

                //Work
                ushort workNo = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_workBuilding;
                ItemClass.Level schoolLevel = cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCurrentSchoolLevel(citizen);
                if (schoolLevel != ItemClass.Level.None)
                {
                    workTitle.text = "School";
                }
                else
                {
                    workTitle.text = "Workplace";
                }
                if (workNo != 0)
                {
                    workID.Building = workNo;
                    Work = bManager.GetBuildingName(workNo, instanceID);
                    workButton.text = this.Work;
                    workButton.Enable();
                }
                else
                {
                    workButton.text = "Unemployed";
                    workButton.Disable();
                }

                //Status
                Status = citizenInfo.m_citizenAI.GetLocalizedStatus(citizen, ref cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)], out targetID);
                statusLabel.text = this.Status;
                if (targetID.IsEmpty)
                {
                    targetButton.isVisible = false;
                }
                else
                {
                    Target = bManager.GetBuildingName(targetID.Building, instanceID);
                    targetButton.text = this.Target;
                    targetButton.isVisible = true;
                }

                //Health
                Citizen.Health healthLevel = Citizen.GetHealthLevel(cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_health);
                string healthIcon = "";
                switch (healthLevel)
                {
                    case Citizen.Health.ExcellentHealth:
                        Health = "Excellent";
                        healthIcon = "ExcellentHealth";
                        break;
                    case Citizen.Health.VeryHealthy:
                        Health = "Good";
                        healthIcon = "VeryHealthy";
                        break;
                    case Citizen.Health.Healthy:
                        Health = "Average";
                        healthIcon = "Healthy";
                        break;
                    case Citizen.Health.PoorHealth:
                        Health = "Poor";
                        healthIcon = "PoorHealth";
                        break;
                    case Citizen.Health.Sick:
                        Health = "Sick";
                        healthIcon = "VeryPoorHealth";
                        break;
                    case Citizen.Health.VerySick:
                        Health = "Very Sick";
                        healthIcon = "VerySick";
                        break;
                }
                healthLabel.text = Health;
                healthSprite.spriteName = "NotificationIcon" + healthIcon;

                //Wellbeing
                Citizen.Wellbeing wellLevel = Citizen.GetWellbeingLevel(cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].EducationLevel,cManager.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_wellbeing);
                string wellIcon = "";
                switch (wellLevel)
                {
                    case Citizen.Wellbeing.VeryUnhappy:
                        Well = "Very Unhappy";
                        wellIcon = "VeryUnhappy";
                        break;
                    case Citizen.Wellbeing.Unhappy:
                        Well = "Unhappy";
                        wellIcon = "Unhappy";
                        break;
                    case Citizen.Wellbeing.Satisfied:
                        Well = "Satisfied";
                        wellIcon = "Happy";
                        break;
                    case Citizen.Wellbeing.Happy:
                        Well = "Happy";
                        wellIcon = "VeryHappy";
                        break;
                    case Citizen.Wellbeing.VeryHappy:
                        Well = "Very Happy";
                        wellIcon = "ExtremelyHappy";
                        break;
                }
                wellLabel.text = Well;
                wellSprite.spriteName = "NotificationIcon" + wellIcon;

                //Family
                uint familyUnit = bManager.m_buildings.m_buffer[(int)homeNo].FindCitizenUnit(CitizenUnit.Flags.Home, citizen);
                Family[0].Citizen = cManager.m_units.m_buffer[(int)((UIntPtr)familyUnit)].m_citizen0;
                Family[1].Citizen = cManager.m_units.m_buffer[(int)((UIntPtr)familyUnit)].m_citizen1;
                Family[2].Citizen = cManager.m_units.m_buffer[(int)((UIntPtr)familyUnit)].m_citizen2;
                Family[3].Citizen = cManager.m_units.m_buffer[(int)((UIntPtr)familyUnit)].m_citizen3;
                Family[4].Citizen = cManager.m_units.m_buffer[(int)((UIntPtr)familyUnit)].m_citizen4;
                int familySize = 0;
                bool titleNeeded = true;
                for (int i = 0; i < 5; i++)
                {
                    if (Family[i].Citizen != 0 & Family[i].Citizen != citizen)
                    {
                        familySize++;
                        familyPanels[i].isVisible = true;
                        familyPanels[i].objectUserData = Family[i];
                        familyNames[i].text = cManager.GetCitizenName(Family[i].Citizen);
                        if (titleNeeded)
                        {
                            familyTitles[i].text = "Family";
                            titleNeeded = false;
                        }
                        else
                        {
                            familyTitles[i].text = "";
                        }
                    }
                    else
                    {
                        familyPanels[i].isVisible = false;
                    }
                }
                this.height = ((ph + (2 * pad)) * 11) + 40 + (2 * pad) + (2 * (ph - 18)) + ((ph + (2 * pad)) * familySize);
                bodyPanel.height = this.height - headerPanel.height;
                bodyPanel.height = paddingPanel.height - (2 * pad);
            }
            else
            {
                statusLabel.text = "Dead or moved away";
                targetButton.isVisible = false;
            }
        }

        private void CloseWindow(UIComponent component, UIMouseEventParameter p)
        {
            this.isVisible = false;
        }

        private void GoToHome(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(homeID, ToolsModifierControl.cameraController.transform.position, true);
        }

        private void GoToWork(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(workID, ToolsModifierControl.cameraController.transform.position, true);
        }

        private void GoToTarget(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget(targetID, ToolsModifierControl.cameraController.transform.position, true);
        }

        private void GoToFamily(UIComponent component, UIMouseEventParameter p)
        {
            ToolsModifierControl.cameraController.SetTarget((InstanceID)component.parent.objectUserData, ToolsModifierControl.cameraController.transform.position, true);
        }
    }
}
