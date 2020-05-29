/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2018.3.11f1
 *Date:           2019-09-01
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using IFramework.Serialization;
using System.Collections.Generic;

namespace IFramework.Language
{
    public class JsonGroup : ILanPairGroup
    {
        private List<LanPair> _group;

        public JsonGroup(string json)
        {
            _group = Json.ToObject<List<LanPair>>(json);
        }
        public List<LanPair> Load()
        {
            return _group;
        }
    }
}
