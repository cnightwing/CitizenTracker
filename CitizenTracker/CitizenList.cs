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

        public byte[] SerializeList()
        {
            List<UInt32> rawList = new List<UInt32>();
            foreach (InstanceID follow in followList)
            {
                rawList.Add(follow.RawData);
            }
            List<byte> byteList = new List<byte>();
            foreach (UInt32 raw in rawList)
            {
                byte[] dataBytes = BitConverter.GetBytes(raw);
                foreach (byte dataByte in dataBytes)
                {
                    byteList.Add(dataByte);
                }
            }
            byte[] data = byteList.ToArray();
            return data;
        }

        public override void OnSaveData()
        {
            serializableDataManager.SaveData("CitizenTracker", SerializeList());
        }

        public List<InstanceID> DeserializeList(byte[] data)
        {
            int followCount = data.Length / 4;
            List<UInt32> rawList = new List<UInt32>();
            for (int i = 0; i < followCount; i++)
            {
                rawList.Add(BitConverter.ToUInt32(data, i * 4));
            }
            List<InstanceID> loadList = new List<InstanceID>();
            foreach (UInt32 raw in rawList)
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
            followList = DeserializeList(data);
        }
    }
}
