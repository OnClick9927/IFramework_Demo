using System.IO;
using System.Windows.Forms;
using IFramework;


namespace FormSever.Data
{
    public class Datas
    {
        public static string root = Application.ExecutablePath.GetDirPath().CombinePath("Datas");
        public static string _usersPath = root.CombinePath("Users.csv");

        public UserDatas users = new UserDatas();

        public void Load()
        {
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);
            users.Load();
        }
        public void Save()
        {
            if (!Directory.Exists(root)) Directory.CreateDirectory(root);
            users.Save();
        }

    }

   
}
