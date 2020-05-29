/*********************************************************************************
 *Author:         OnClick
 *Version:        0.0.1
 *UnityVersion:   2017.2.3p3
 *Date:           2019-09-02
 *Description:    IFramework
 *History:        2018.11--
*********************************************************************************/
using System.Collections.Generic;
using UnityEngine;
namespace IFramework.Language
{
    public class LanGroup:ScriptableObject
	{
        [ReadOnly] public readonly List<LanPair> lanPairs = new List<LanPair>();
        [ReadOnly] public readonly List<string> keys = new List<string>();

        public void DeletePairsByLan(SystemLanguage lan)
        {
            lanPairs.RemoveAll((pair) => { return pair.lan == lan; });
        }

        public void DeletePairsByKey(string key)
        {
            lanPairs.RemoveAll((pair) => { return pair.key == key; });
        }

        public void DeleteLanPair(LanPair pair)
        {
            lanPairs.Remove(pair);
        }
    }
}
