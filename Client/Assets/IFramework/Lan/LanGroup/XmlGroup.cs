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
    public class XmlGroup : ILanPairGroup
    {
        private List<LanPair> _group;

        public XmlGroup(string xml)
        {
            _group= Xml.ToObject<List<LanPair>>(xml);
        }
        public List<LanPair> Load()
        {
            return _group;
        }
    }
}
