using UnityEngine;

public class isEnabled : MonoBehaviour
{
    public int needToUnlock;
    public Material blackMaterial;

    public void Start()
    {
        if(PlayerPrefs.GetInt("score") < needToUnlock)
            GetComponent<MeshRenderer>().material = blackMaterial;
    }
}
