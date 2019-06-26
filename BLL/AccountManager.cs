using System;
using System.Collections.Generic;
using BOL;
using DAL;
namespace BLL

{
    public static class AccountManager
    {
       public static bool Validate(string username,string password)
        {
            return AccountDAL.Validate(username,password);
        }
        public static bool Insert(int id,string name, string city, string email, string password, string mob)
        {
            return AccountDAL.Insert(id,name, city, email, password, mob);
        }
        public static bool update(int id,string password)
        {
            return AccountDAL.update(id,password);
        }
        public static bool Delete(int id)
        {
            return AccountDAL.delete(id);
        }

        public static List<customer> Display()
        {
            return AccountDAL.display();
        }
    }
}
