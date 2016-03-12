using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ICities;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace CitizenTracker
{
    public class TrackerRenderManager : IRenderableManager
    {
        DrawCallData callData;

        public DrawCallData GetDrawCallData()
        {
            return callData;
        }

        public void BeginOverlay(RenderManager.CameraInfo cameraInfo) { }

        public void BeginRendering(RenderManager.CameraInfo cameraInfo) { }

        public bool CalculateGroupData(int groupX, int groupZ, int layer, ref int vertexCount, ref int triangleCount, ref int objectCount, ref RenderGroup.VertexArrays vertexArrays)
        {
            return false;
        }

        public void EndOverlay(RenderManager.CameraInfo cameraInfo) { }

        public void EndRendering(RenderManager.CameraInfo cameraInfo)
        {
            UIView uiView = GameObject.FindObjectOfType<UIView>();
            RenderButton renderButton = (RenderButton)uiView.FindUIComponent("RenderButton");
            if (renderButton.renderEnabled)
            {
                foreach (InstanceID follow in CitizenList.followList)
                {
                    Vector3 position;
                    Quaternion rotation;
                    Vector3 size;
                    InstanceManager.GetPosition(follow, out position, out rotation, out size);

                    float zoom = ToolsModifierControl.cameraController.m_currentSize;
                    float scale = 0.71f + ((zoom - 40.0f) * 0.001f);
                    
                    position.y += (float)((double)size.y * 0.85f + (zoom / 20.0f));

                    NotificationEvent.RenderInstance(cameraInfo, NotificationEvent.Type.LocationMarker, position, scale, 1f);
                }
            }
        }

        public string GetName()
        {
            return "TrackerRenderManager";
        }

        public void InitRenderData()
        {
            callData.m_batchedCalls = 0;
            callData.m_defaultCalls = 0;
            callData.m_lodCalls = 0;
            callData.m_overlayCalls = 0;
        }

        public void PopulateGroupData(int groupX, int groupZ, int layer, ref int vertexIndex, ref int triangleIndex, Vector3 groupPosition, RenderGroup.MeshData data, ref Vector3 min, ref Vector3 max, ref float maxRenderDistance, ref float maxInstanceDistance, ref bool requireSurfaceMaps) { }

        public void UndergroundOverlay(RenderManager.CameraInfo cameraInfo) { }
    }
}
