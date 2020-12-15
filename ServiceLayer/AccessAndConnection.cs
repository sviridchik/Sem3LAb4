using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConfugurationManager;
using DataAccessLayer;
using FileManager;

namespace ServiceLayer
{
    public class AccessAndConnection
    {


        static Type resDataOptions = OptionsManager.GetOptions<DataOptions>();
        public static string ser = Logger.GetValueByNameField(resDataOptions, "Server").ToString();
        public static string db = Logger.GetValueByNameField(resDataOptions, "Database").ToString();
        public static bool con = (bool)Logger.GetValueByNameField(resDataOptions, "Trusted_Connection");        
        DataAccessLayerClass dbAccess;
        public AccessAndConnection()
        {
            Type resDataOptions = OptionsManager.GetOptions<DataOptions>();
            dbAccess = new DataAccessLayerClass(ser, db, con);
            ConfugurationManager.OptionsManager.DataAccess = dbAccess;

        }


        public string resultOutput;
        public void StartAccess()
        {
            dbAccess.StartDb();
            XmlGeneratorService xmlData = new XmlGeneratorService();
            resultOutput = xmlData.CreateContent(dbAccess.GetAdd("GETALLADDRESS"));
            FileTransferService fts = new FileTransferService(resultOutput);
            fts.LogContent();
            Thread.Sleep(10000);
        }
    }


}

