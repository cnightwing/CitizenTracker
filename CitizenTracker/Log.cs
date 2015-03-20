﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColossalFramework;
using ColossalFramework.Plugins;

namespace CitizenTracker
{
    public static class Log
    {
        public static void Message(string s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, s);
        }

        public static void Error(string s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, s);
        }

        public static void Warning(string s)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Warning, s);
        }
    }
}
