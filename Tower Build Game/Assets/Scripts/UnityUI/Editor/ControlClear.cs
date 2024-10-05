using UnityEngine;
using UnityEditor;

namespace ProjectEditor
{
    public class DeleteAllPlayerprefs : MonoBehaviour
    {
        [MenuItem("Project/Clear All Data")]
        private static void ClearData()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("All PlayerPrefs deleted");
        }

        [MenuItem("Project/Remove Undersea")]
        private static void RemoveName()
        {
            var names = AssetDatabase.GetAllAssetBundleNames();
            foreach (var name in names)
            {
                Debug.Log("AssetBundle: " + name);
            }
            Debug.Log("AssetBundle Clear");
        }
    }
}
