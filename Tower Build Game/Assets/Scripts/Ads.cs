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
