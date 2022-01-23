using DG.Tweening;
using UnityEngine.UI;
public static class Helper
{
    public static Tween DoRight(this RectMask2D rectMask2D, float to, float duration)
    {
        return DOTween.To(
            () => rectMask2D.padding.z,
            value => {
                var rect = rectMask2D.padding;
                rect.z = value;
                rectMask2D.padding = rect;
            },
            to,
            duration);
    }
    
    public static Tween DoBottom(this RectMask2D rectMask2D, float to, float duration)
    {
        return DOTween.To(
            () => rectMask2D.padding.y,
            value => {
                var rect = rectMask2D.padding;
                rect.y = value;
                rectMask2D.padding = rect;
            },
            to,
            duration);
    }
}