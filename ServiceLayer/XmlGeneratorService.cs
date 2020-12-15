using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    class XmlGeneratorService : IXmlGeneratorService
    {
        public string resultOutput;
         public string CreateContent(DataEntity dataEntity)
        {
            StringBuilder content = new StringBuilder(@"<?xml version =""1.0"" encoding=""utf-8""?>" + '\n');
            string rowName = dataEntity.names[0].Substring(0, dataEntity.names[0].Length - 2);
            content.AppendLine($"<XmlTable>");
            for (int i = 0; i < dataEntity.values.Count; i++)
            {
                content.AppendLine($"\t<{rowName}>");
                for (int j = 0; j < dataEntity.names.Count; j++)
                {
                    if (j>0)
                    {
                        content.Append("\n");
                    }
                    content.Append($"\t\t<{dataEntity.names[j]}>");
                    content.Append($"{dataEntity.values[i][j]}");
                    content.Append($"</{dataEntity.names[j]}>");
                }
                content.Append("\n");
                content.AppendLine($"\t</{rowName}>");
                
                

            }
            content.AppendLine($"</XmlTable>");
            return content.ToString();
        }
    }
}
