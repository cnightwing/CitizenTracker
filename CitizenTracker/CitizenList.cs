using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICities;
using ColossalFramework.IO;
using UnityEngine;

namespace CitizenTracker
{
    public class CitizenList : SerializableDataExtensionBase
    {
        public static List<InstanceID> followList = new List<InstanceID>();

        public byte[] Serialize()
        {
            List<uint> rawList = new List<uint>();
            foreach (InstanceID follow in followList)
            {
                rawList.Add(follow.RawData);
            }

            byte[] data = rawList
                .SelectMany(s => BitConverter.GetBytes(s))
                .ToArray();

            return data;
        }

        public override void OnSaveData()
        {
            serializableDataManager.SaveData("CitizenTracker", Serialize());
        }

        public List<InstanceID> Deserialize(byte[] data)
        {
            int followCount = data.Length / 4;
            List<uint> rawList = new List<uint>();
            for (int i = 1; i < followCount + 1; i++)
            {
                rawList.Add(BitConverter.ToUInt32(data, i * 4));
            }
            List<InstanceID> loadList = new List<InstanceID>();
            foreach (uint raw in rawList)
            {
                InstanceID newInstance = new InstanceID();
                newInstance.RawData = raw;
                loadList.Add(newInstance);
            }
            return (loadList);
        }

        public override void OnLoadData()
        {
            byte[] data = serializableDataManager.LoadData("CitizenTracker");
            if (data == null)
            {
                return;
            }
            followList = Deserialize(data);
        }
    }
}
