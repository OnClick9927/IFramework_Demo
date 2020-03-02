using System.Collections.Generic;
using System.IO;
using IFramework.Serialization.DataTable;


namespace FormSever.Data
{
    public abstract class AbstractDatas<T> : IDatas<T> where T : IData
    {
        protected abstract string path { get; }
        protected List<T> _datas = new List<T>();

        public void Load()
        {
            if (!File.Exists(path))
                Save();
            DataReader cr = new DataReader(new StreamReader(path), new DataRow(), new DataExplainer());
            _datas = cr.Get<T>();
            cr.Dispose();
        }

        public void Save()
        {
            DataWriter csvWriter = new DataWriter(new StreamWriter(path), new DataRow(), new DataExplainer());
            csvWriter.Write(_datas);
            csvWriter.Dispose();
        }

        protected void Add(T t)
        {
            _datas.Add(t);
        }

    }
}
