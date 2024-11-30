using UnityEngine;
using UnityEngine.UI;

public class ReceiptsController : MonoBehaviour
{
    [SerializeField] private Image receiptImage;
    [SerializeField] private Sprite[] receipts;
    [SerializeField] private int activeReceipt;

    void Start()
    {
        receiptImage.sprite = receipts[0];
        activeReceipt = 0;
    }

    public void NextReceipt()
    {
        activeReceipt += 1;

        if (activeReceipt > receipts.Length - 1)
        {
            activeReceipt = 0;
        }

        receiptImage.sprite = receipts[activeReceipt];
    }

    public void PreviousReceipt()
    {
        activeReceipt -= 1;

        if (activeReceipt < 0)
        {
            activeReceipt = receipts.Length - 1;
        }

        receiptImage.sprite = receipts[activeReceipt];

    }
}
