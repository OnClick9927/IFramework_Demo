using IFramework;


namespace FormSever.Data
{
    public class UserData : IData
    {
       public string account;
       public string name;
       public string psd;
    }
    public class UserDatas : AbstractDatas<UserData>
    {
        protected override string path => Datas.root.CombinePath("Users.csv");
        private LockParam _lock = new LockParam();

        public bool AddUser(string account,string name,string psd,out string err)
        {
            using (new LockWait(ref _lock))
            {
                if (ExistAccount(account))
                {
                    err = "账号重复";
                    return false;
                }
                if (ExistName(name))
                {
                    err = "名字重复";

                    return false;
                }
                err = "成功";

                Add(new UserData()
                {
                    account = account,
                    name = name,
                    psd = psd
                });
                return true;
            }
            
        }

        private bool ExistAccount(string acc)
        {
            return _datas.Find((data) => { return data.account == acc; }) != null;
        }
        private bool ExistName(string name)
        {
            return _datas.Find((data) => { return data.name == name; }) != null;
        }

        public bool ChangeName(string account, string name, out string err)
        {
            using (new LockWait(ref _lock))
            {
                if (!ExistAccount(account))
                {
                    err = "account not Exist";
                    return false;
                }
                if (!ExistName(name))
                {
                    err = "name not Exist";

                    return false;
                }
                err = "sucess";
                var _data = _datas.Find((data) => { return data.name == name && data.account == account; });
                _data.name = name;
                return true;
            }

        }
        public bool Changepassword(string account, string psd, out string err)
        {
            using (new LockWait(ref _lock))
            {
                if (!ExistAccount(account))
                {
                    err = "account not Exist";
                    return false;
                }

                err = "sucess";
                var _data = _datas.Find((data) => { return data.psd == psd && data.account == account; });
                _data.psd = psd;
                return true;
            }
           
        }

        public bool TryLogin(string account, string psd,out string name, out string err)
        {
            using (new LockWait(ref _lock))
            {
                if (!ExistAccount(account))
                {
                    name = "";
                    err = "账号不存在";
                    return false;
                }

                var _data = _datas.Find((data) => { return data.psd == psd && data.account == account; });
                if (_data == null)
                {
                    name = "";
                    err = "密码错误";
                    return false;
                }
                name = _data.name;
                err = "成功";
                return true;

            }
           

        }

    }
}
