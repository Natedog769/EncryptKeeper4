using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheEncryptKeeper4.Models;

namespace TheEncryptKeeper4.Services
{
    public class JSONService: IJSONService
    {

        private const string FileName = @"PWSheet.JSON";
        //private const string FileName = @"DemoSheet.JSON";
        private const string PinFile = @"PinFile.JSON";
        

        private readonly IEncryptService encryptor;

        private IList<LoginEntry> LoginList { get; set; } = new List<LoginEntry>();

        public JSONService(IEncryptService encryptor)
        {
            this.encryptor = encryptor;
        }

        /// <summary>
        /// This returns a bool, True for success, false for any error that occurred
        /// </summary>
        /// <param name="newEntry"></param>
        /// <returns></returns>
        public bool SaveNewEntry(LoginEntry newEntry)
        {
            try
            {
                LoginList.Add(newEntry);

                SaveJSONData(LoginList);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public void SaveChangedList(IList<LoginEntry> modifiedList)
        {
            SaveJSONData(modifiedList);
        }

        public void SaveJSONData(IList<LoginEntry> loginList = null )
        {
            string executablePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string directory = Path.GetDirectoryName(executablePath);

            string jsonFilePath = Path.Combine(directory, FileName);

            if (loginList == null)
            {
                loginList = new List<LoginEntry>();
            }
            else
            {
                //we need to iterate the list and encrypt the password
                foreach (LoginEntry login in loginList)
                {
                    string password = login.Password;
                    string encryptedPass = encryptor.EncryptString(password);

                    login.Password = encryptedPass;
                }
            }

            var json = JsonConvert.SerializeObject(loginList, Formatting.Indented);

            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            File.WriteAllText(FileName, json);

        }

        public IList<LoginEntry> LoadJSONData()
        {
            IList<LoginEntry> logins = new List<LoginEntry>();

            string executablePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string directory = Path.GetDirectoryName(executablePath);

            string jsonFilePath = Path.Combine(directory, FileName);

            if (!File.Exists(jsonFilePath))
            {
                MessageBox.Show("Error File Cannot Be found! Creating new empty file");

                SaveJSONData();                
            }
            else
            {

                var json = File.ReadAllText(jsonFilePath);

                logins = JsonConvert.DeserializeObject<List<LoginEntry>>(json);                
            }
            return logins;
        }

        public IList<LoginEntry> GetLoginList()
        {
            LoginList.Clear();
            LoginList = LoadJSONData();

            foreach (LoginEntry login in LoginList)
            {
                string password = login.Password;
                string dencryptedPass = encryptor.DecryptString(password);
                login.Password = dencryptedPass.Contains("=") ? encryptor.DecryptString(dencryptedPass) : dencryptedPass;
            }

            return LoginList;
        }


        //JSON PIN

        public string LoadJSONPin()
        {
            string savedPin = "";

            string executablePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string directory = Path.GetDirectoryName(executablePath);

            string jsonFilePath = Path.Combine(directory, PinFile);

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("Error File Cannot Be found! Creating new empty file");
                SaveJSONPin(string.Empty);
                return string.Empty;
            }
            else
            {

                var json = File.ReadAllText(jsonFilePath);

                savedPin = JsonConvert.DeserializeObject<string>(json);
            }
            return savedPin;
        }

        public void SaveJSONPin(string newPin)
        {
            string executablePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string directory = Path.GetDirectoryName(executablePath);

            string jsonFilePath = Path.Combine(directory, PinFile);
                        

            var json = JsonConvert.SerializeObject(newPin, Formatting.Indented);

            if (File.Exists(PinFile))
            {
                File.Delete(PinFile);
            }

            File.WriteAllText(PinFile, json);
        }
    }
}
