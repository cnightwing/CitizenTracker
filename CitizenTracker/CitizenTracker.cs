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
    public class CitizenTracker : IUserMod
    {
        public string Name
        {
            get { return "Citizen Tracker"; }
        }

        public string Description
        {
            get { return "Follow your favourite citizens around town!"; }
        }
    }
}
