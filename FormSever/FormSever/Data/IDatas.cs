namespace FormSever.Data
{
    interface IDatas<T> where T : IData
    {
        void Load();
        void Save();
    }

   
}
