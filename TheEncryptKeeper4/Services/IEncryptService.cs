using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheEncryptKeeper4.Services
{
    public interface IEncryptService
    {
        string EncryptString(string plainText);

        string DecryptString(string encryptedText);

    }
}