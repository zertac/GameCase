using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameHelper : MonoBehaviour
{
    // Current activated spinner object
    public Spinner Spinner;

    // Total reward score text for show to user 
    public TextMeshProUGUI TotalRewardText;

    // Retry button object
    public Button RetryButton;

    #region TOTAL REWARD VARIABLE
    private int _totalReward;

    public int TotalReward
    {
        get { return _totalReward; }
        set
        {
            _totalReward = value;
            TotalRewardText.text = "Total Reward :\n" + value.ToString();
        }
    }
    #endregion

    void Start()
    {
        // Create spinner prefab for play
        CreateSpinner();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateSpinner()
    {
        // Get dummy daily reward data from fake server
        var spinnerData = DataHelper.GetSpinner();

        // If spinner object created then destroy game object before create new one 
        if (Spinner.Instance != null)
        {
            Destroy(Spinner.Instance.gameObject);
        }

        //Hide retry button
        RetryButton.gameObject.SetActive(false);

        // initialize spinner prefab and set retrived data from server
        var spinnerObj = Instantiate(Spinner, GameObject.Find("Canvas").transform);
        Spinner.Instance.SetData(spinnerData);

        // set actions of spinner prefabs
        Spinner.Instance.Started += () =>
        {
            Debug.Log("Spinner sterted to spin");
        };

        Spinner.Instance.Ended += (reward) =>
        {
            Debug.Log("Spinner ended");

            // Set total earned reward ui
            TotalReward += reward.Value;

            // Activate Retry button for play again
            RetryButton.gameObject.SetActive(true);
        };
    }
}
