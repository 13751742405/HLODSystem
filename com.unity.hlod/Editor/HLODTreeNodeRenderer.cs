using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TerrainUtils;

namespace Unity.HLODSystem
{
    public class HLODTreeNodeRenderer
    {
#if UNITY_EDITOR
        #region Singleton
        private static HLODTreeNodeRenderer s_instance;

        public static HLODTreeNodeRenderer Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new HLODTreeNodeRenderer();
                return s_instance;
            }
        }
        #endregion
        
        #region public
        public void Render(HLODTreeNode node, Color color, float width)
        {
            if (node == null || node.Controller == null)
                return;
            
            Vector3 min = node.Bounds.min;
            Vector3 max = node.Bounds.max;

            Vector3[] vertices = new Vector3[8];
            vertices[0] = new Vector3(min.x, min.y, min.z);
            vertices[1] = new Vector3(min.x, min.y, max.z);
            vertices[2] = new Vector3(max.x, min.y, max.z);
            vertices[3] = new Vector3(max.x, min.y, min.z);

            vertices[4] = new Vector3(min.x, max.y, min.z);
            vertices[5] = new Vector3(min.x, max.y, max.z);
            vertices[6] = new Vector3(max.x, max.y, max.z);
            vertices[7] = new Vector3(max.x, max.y, min.z);

            for (int i = 0; i < vertices.Length; ++i)
            {
                vertices[i] = node.Controller.transform.localToWorldMatrix.MultiplyPoint(vertices[i]);
            }
            
            Handles.color = color;

            Handles.DrawLine(vertices[0], vertices[1], width);
            Handles.DrawLine(vertices[1], vertices[2], width);
            Handles.DrawLine(vertices[2], vertices[3], width);
            Handles.DrawLine(vertices[3], vertices[0], width);

            Handles.DrawLine(vertices[0], vertices[4], width);
            Handles.DrawLine(vertices[1], vertices[5], width);
            Handles.DrawLine(vertices[2], vertices[6], width);
            Handles.DrawLine(vertices[3], vertices[7], width);

            Handles.DrawLine(vertices[4], vertices[5], width);
            Handles.DrawLine(vertices[5], vertices[6], width);
            Handles.DrawLine(vertices[6], vertices[7], width);
            Handles.DrawLine(vertices[7], vertices[4], width);
        }

        #endregion

        private HLODTreeNodeRenderer()
        {
        }

#endif
    }
}