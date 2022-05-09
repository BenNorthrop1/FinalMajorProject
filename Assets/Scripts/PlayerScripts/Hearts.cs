using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{   

    public Sprite fullHeart, threeQuarterHeart, halfHeart, quarterHeart, empty;


    Image heartImage; // easy heart system


    void Start()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch(status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = empty;
                break;
            case HeartStatus.QuarterHeart:
                heartImage.sprite = quarterHeart;
                break;
            case HeartStatus.HalfHeart:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.ThreeQuarterHeart:
                heartImage.sprite = threeQuarterHeart;
                break;
            case HeartStatus.FullHeart:
                heartImage.sprite = fullHeart;
                break;
        }
    }





}
public enum HeartStatus
    {
        Empty = 0,
        QuarterHeart = 1,
        HalfHeart = 2,
        ThreeQuarterHeart = 3,
        FullHeart = 4
    }