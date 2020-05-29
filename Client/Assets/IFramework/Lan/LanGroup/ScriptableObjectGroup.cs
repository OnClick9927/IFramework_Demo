/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2019-09-01
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System.Collections.Generic;

namespace IFramework.Language
{
    public class ScriptableObjectGroup : ILanPairGroup
    {
        private LanGroup _group;

        public ScriptableObjectGroup(LanGroup group)
        {
            this._group = group;
        }
        public List<LanPair> Load()
        {
            if (_group == null) return null;
            return _group.lanPairs;
        }
    }
}
