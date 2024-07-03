using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheEncryptKeeper4.Models;

namespace TheEncryptKeeper4.Services
{
    public interface IJSONService
    {
        bool SaveNewEntry(LoginEntry login);

        void SaveChangedList(IList<LoginEntry> modifiedList);

        IList<LoginEntry> GetLoginList();

        string LoadJSONPin();

        void SaveJSONPin(string newPin);
    }
}
