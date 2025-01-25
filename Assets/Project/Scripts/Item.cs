using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image img;

    public void UpdateInformation(string text, Sprite sprite)
    {
        this.text.text = text;
        this.img.sprite = sprite;
    }

}
