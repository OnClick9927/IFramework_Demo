/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.146
 *UnityVersion:   2018.4.17f1
 *Date:           2020-04-02
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using UnityEngine;

namespace IFramework.Tweens
{
    public class RectTweenValue : TweenValue<Rect>
    {
        protected override void MoveNext()
        {
            float f = curve.GetYWithX(percent);
            //cur = (end - start) * f + start;
            //cur = start.Lerp(start, end, f);

            Rect _cur = start.Lerp(start, end, f);
            cur = start.Lerp(  targetValue, _cur, delta);
        }

    }

}
