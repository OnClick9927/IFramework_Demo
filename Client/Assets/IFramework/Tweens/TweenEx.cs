/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.2.146
 *UnityVersion:   2018.4.17f1
 *Date:           2020-04-02
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System;
using UnityEngine;
using UnityEngine.UI;
namespace IFramework.Tweens
{

    [FrameworkVersion(8)]
    public static class TweenEx
    {
        public static Tween<T> AllocateTween<T>(EnvironmentType env) /*where T : struct*/
        {
            return Tween<T>.Allocate<Tween<T>>(env);
        }
        public static Tween<T> ConfigAndRun<T>(this Tween<T> tween, T start, T end, float dur, Func<T> getter, Action<T> setter)/* where T:struct*/
        {
            tween.Config(start, end, dur, getter, setter);
            tween.Run();
            return tween;
        }


        public static Tween<T> SetRecyle<T>(this Tween<T> tween, bool rec) /*where T : struct*/
        {
            if (tween.recyled)
                throw new Exception("The Tween Has Been Recyled,Do not Do anything On it");
            tween.autoRecyle = rec;
            return tween;
        }
        public static Tween<T> OnCompelete<T>(this Tween<T> tween, Action onCompelete) /*where T : struct*/
        {
            if (tween.recyled)
                throw new Exception("The Tween Has Been Recyled,Do not Do anything On it");
            tween.onCompelete += onCompelete;
            return tween;
        }
        public static Tween<T> SetLoop<T>(this Tween<T> tween, int loop, LoopType loopType) /*where T : struct*/
        {
            if (tween.recyled)
                throw new Exception("The Tween Has Been Recyled,Do not Do anything On it");
            tween.loop = loop;
            tween.loopType = loopType;
            return tween;
        }
        public static Tween<T> SetCurve<T>(this Tween<T> tween, ValueCurve curve) /*where T : struct*/
        {
            if (tween.recyled)
                throw new Exception("The Tween Has Been Recyled,Do not Do anything On it");
            tween.curve = curve;
            return tween;
        }


        public static Tween<Vector3> DoMove(this Transform target, Vector3 end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Vector3>(env).ConfigAndRun(target.position, end, dur,
                    () => { return target.position; },
                    (value) => {
                        target.position = value;
                    }
                );
        }
        public static Tween<Vector3> DoMoveX(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.position;
            endv3.x = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.position, endv3, dur, () => { return target.position; },
 (value) => {
     target.position = value;
 });
        }
        public static Tween<Vector3> DoMoveY(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.position;
            endv3.y = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.position, endv3, dur, () => { return target.position; },
 (value) => {
     target.position = value;
 });
        }
        public static Tween<Vector3> DoMoveZ(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.position;
            endv3.z = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.position, endv3, dur, () => { return target.position; },
 (value) => {
     target.position = value;
 });
        }

        public static Tween<Vector3> DoLocalMove(this Transform target, Vector3 end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Vector3>(env).ConfigAndRun(target.localPosition, end, dur, () => { return target.localPosition; }, (value) => {
                target.localPosition = value;
            });
        }
        public static Tween<Vector3> DoLocalMoveX(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.localPosition;
            endv3.x = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.localPosition, endv3, dur, () => { return target.localPosition; }, (value) => {
                target.localPosition = value;
            });
        }
        public static Tween<Vector3> DoLocalMoveY(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.localPosition;
            endv3.y = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.localPosition, endv3, dur, () => { return target.localPosition; }, (value) => {
                target.localPosition = value;
            });
        }
        public static Tween<Vector3> DoLocalMoveZ(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.localPosition;
            endv3.z = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.localPosition, endv3, dur, () => { return target.localPosition; }, (value) => {
                target.localPosition = value;
            });
        }


        public static Tween<Vector3> DoScale(this Transform target, Vector3 end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Vector3>(env).ConfigAndRun(target.localScale, end, dur, () => { return target.localScale; }, (value) => {
                target.localScale = value;
            });
        }
        public static Tween<Vector3> DoScaleX(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.localScale;
            endv3.x = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.localScale, endv3, dur, () => { return target.localScale; }, (value) => {
                target.localScale = value;
            });
        }
        public static Tween<Vector3> DoScaleY(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.localScale;
            endv3.y = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.localScale, endv3, dur, () => { return target.localScale; }, (value) => {
                target.localScale = value;
            });
        }
        public static Tween<Vector3> DoScaleZ(this Transform target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Vector3 endv3 = target.localScale;
            endv3.z = end;
            return AllocateTween<Vector3>(env).ConfigAndRun(target.localScale, endv3, dur, () => { return target.localScale; }, (value) => {
                target.localScale = value;
            });
        }


        public static Tween<Quaternion> DoRota(this Transform target, Quaternion end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Quaternion>(env).ConfigAndRun(target.rotation, end, dur, () => { return target.rotation; }, (value) => {
                target.rotation = value;
            });
        }
        public static Tween<Quaternion> DoRota(this Transform target, Vector3 end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Quaternion>(env).ConfigAndRun(target.rotation, Quaternion.Euler(end), dur, () => { return target.rotation; }, (value) => {
                target.rotation = value;
            });
        }
        public static Tween<Quaternion> DoLocalRota(this Transform target, Quaternion end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Quaternion>(env).ConfigAndRun(target.localRotation, end, dur, () => { return target.localRotation; }, (value) => {
                target.localRotation = value;
            });
        }
        public static Tween<Quaternion> DoLocalRota(this Transform target, Vector3 end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Quaternion>(env).ConfigAndRun(target.localRotation, Quaternion.Euler(end), dur, () => { return target.localRotation; }, (value) => {
                target.localRotation = value;
            });
        }


        public static Tween<Color> DoColor(this Material target, Color end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Color>(env).ConfigAndRun(target.color, end, dur, () => { return target.color; }, (value) => {
                target.color = value;
            });
        }
        public static Tween<Color> DoColor(this Light target, Color end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Color>(env).ConfigAndRun(target.color, end, dur, () => { return target.color; }, (value) => {
                target.color = value;
            });
        }
        public static Tween<Color> DoColor(this Camera target, Color end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<Color>(env).ConfigAndRun(target.backgroundColor, end, dur, () => { return target.backgroundColor; }, (value) => {
                target.backgroundColor = value;
            });
        }
        public static Tween<Color> DoAlpha(this Material target, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            Color endColor = target.color;
            endColor.a = end;
            return AllocateTween<Color>(env).ConfigAndRun(target.color, endColor, dur, () => { return target.color; }, (value) => {
                target.color = value;
            });
        }




        public static Tween<int> DoText(this Text target, int start, int end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<int>(env).ConfigAndRun(start, end, dur, () => {
                int value;
                if (int.TryParse(target.text, out value))
                    return value;
                return 0; }, (value) => {
                target.text = value.ToString();
            });
        }
        public static Tween<float> DoText(this Text target, float start, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<float>(env).ConfigAndRun(start, end, dur, () => {
                float value;
                if (float.TryParse(target.text, out value))
                    return value;
                return 0;
            }, (value) => {
                target.text = value.ToString();
            });
        }
        public static Tween<string> DoText(this Text target, string start, string end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<string>(env).ConfigAndRun(start, end, dur, () => { return target.text; }, (value) => {
                target.text = value;
            });
        }

        public static Tween<float> DoFillAmount(this Image target, float start, float end, float dur, EnvironmentType env = EnvironmentType.Ev1)
        {
            return AllocateTween<float>(env).ConfigAndRun(start, end, dur, () => {
                return target.fillAmount;
            }, (value) => {
                target.fillAmount = value;
            });
        }

    }

}
 