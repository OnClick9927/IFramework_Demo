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
    public class Vector3TweenValue : TweenValue<Vector3>
    {
        protected override void MoveNext()
        {
            float f = curve.GetYWithX(percent);
            //cur = (end - start) * f + start;
            Vector3 _cur = Vector3.Lerp(start, end, f);
            cur = Vector3.Lerp( targetValue, _cur, delta);
        }

    }

}
