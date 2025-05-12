/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 05:48:46
 * Version: 1.0.4
 */

using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class Ads : MonoBehaviour
{
    private string gameID = "4689491", type = "video";
    private bool testMode = true;

    private void Start()
    {
        Advertisement.Initialize(gameID, testMode);
        Debug.Log(Advertisement.isInitialized);
        StartCoroutine(ShowAd());
    }

    private IEnumerator ShowAd()
    {
        while (true)
        {
            if(Advertisement.IsReady(type))
            {
                Debug.Log("Ready");
            }

            yield return new WaitForSeconds(1f);
        }
    }
}
