using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardItem : MonoBehaviour
{
    public RewardItem rewardItem;

    public RewardSelectedData rewardSelectedData;
    public Image rewardIcon;
    public Image rewardFrame;
    public TMP_Text rewardAmount;
    public TMP_Text dayText;
    public Transform check;
    public Transform focus;

    [ContextMenu("GetData")]
    public void SetTransform()
    {
        rewardIcon = transform.Find("ItemIcon").GetComponent<Image>();
        rewardFrame = this.GetComponent<Image>();
        rewardAmount = transform.Find("Text_Value").GetComponent<TMP_Text>();
        dayText = transform.Find("Text_Day").GetComponent<TMP_Text>();
        check = transform.Find("Check").GetComponent<Transform>();
        focus = transform.Find("Focus").GetComponent<Transform>();
    }

    public void SetReward()
    {
        check.gameObject.SetActive(false);
        focus.gameObject.SetActive(false);

        rewardIcon.sprite = rewardItem.image;
        rewardAmount.text = rewardItem.amount.ToString();
        dayText.color = rewardSelectedData.colorTextNormal;
        rewardFrame.sprite = rewardSelectedData.normalFrame;
    }

    public void CompleteReward()
    {
        focus.gameObject.SetActive(false);
        check.gameObject.SetActive(true);
        rewardFrame.sprite = rewardSelectedData.completeFrame;
        dayText.color = rewardSelectedData.colorTextNormal;

    }

    public void FocusReward()
    {
        focus.gameObject.SetActive(true);
        rewardFrame.sprite = rewardSelectedData.focusFrame;
        dayText.color = rewardSelectedData.colorTextSelected;
    }
}
