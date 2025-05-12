/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 05:48:46
 * Version: 1.0.4
 */

using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;

namespace CustomAttributes
{
    [CustomEditor(typeof(object), true, isFallback = false)]
    [CanEditMultipleObjects]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach(var target in targets)
            {
                var mis = target.GetType().GetMethods().Where(m => m.GetCustomAttributes().Any(a => a.GetType() == typeof(EditorButtonAttribute)));
                if(mis != null)
                {
                    foreach(var mi in mis)
                    {
                        if(mi != null)
                        {
                            var attribute = (EditorButtonAttribute)mi.GetCustomAttribute(typeof(EditorButtonAttribute));
                            if (GUILayout.Button(attribute.name))
                            {
                                mi.Invoke(target, null);
                            }
                        }
                    }
                }
            }
        }
    }
}

