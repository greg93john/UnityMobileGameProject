using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsClass : MonoBehaviour {
    // Run ads with no additional functions
    public void RunSimpleAdvertisement() {
        Advertisement.Show();
    }

    // Run ads that rewards players that view them to completion
    public void ShowRewardedVideo() {
        var options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        Advertisement.Show("rewardedVideo", options);
    } void HandleShowResult(ShowResult result) {
        if (result == ShowResult.Finished) {
            Debug.Log("Video completed - Offer a reward to the player");

        } else if (result == ShowResult.Skipped) {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        } else if (result == ShowResult.Failed) {
            Debug.LogError("Video failed to show");
        }
    }
}
