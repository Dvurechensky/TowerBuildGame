/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 05:48:46
 * Version: 1.0.4
 */

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
