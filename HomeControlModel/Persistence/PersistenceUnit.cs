using HomeControl.Models;
using HomeControlModel.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControlModel.Persistence
{
    class PersistenceUnit
    {
        JsonSerializerSettings jset = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public bool SaveInstanse(HomeModel homeModel)
        {
            try
            {
                string json = JsonConvert.SerializeObject(homeModel, typeof(HomeModel), Formatting.Indented, jset);

                FileInfo fileInf = new FileInfo(FilesLocation.homeControlJSON);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }

                using (StreamWriter sw = fileInf.CreateText())
                {
                    sw.Write(json);
                }  
            }
            catch
            {
                return false;
            }

            return true;
        }

        public HomeModel ReadInstance()
        {
            string json = string.Empty;

            FileInfo fileInf = new FileInfo(FilesLocation.homeControlJSON);
            if (fileInf.Exists)
            {
                using (StreamReader sr = fileInf.OpenText())
                {
                    json = sr.ReadToEnd();
                }
            }

            return (HomeModel)JsonConvert.DeserializeObject(json, typeof(HomeModel), jset);
        }
    }
}
