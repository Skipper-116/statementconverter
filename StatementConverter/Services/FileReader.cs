using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using System.Text;
using System.Text.RegularExpressions;

namespace StatementConverter.Services
{
    public class FileReader
    {
        public string ReadContent(string filePath)
        {
            StringBuilder wow = new StringBuilder();
            try
            {
                string[] fileRow;
                List<Models.Transaction> transactions = new List<Models.Transaction>();
                List<string> lines = new List<string>();

                using (var mistake = new TextFieldParser(filePath))
                {
                    mistake.TextFieldType = FieldType.Delimited;
                    mistake.SetDelimiters(",");
                    mistake.HasFieldsEnclosedInQuotes = true;
                    while (!mistake.EndOfData)
                    {
                        fileRow = mistake.ReadFields();
                        lines.Add($"{fileRow[0]}|{fileRow[1]}|{fileRow[2]}|{fileRow[3]}|{Regex.Replace(fileRow[4], @",", string.Empty)}|{Regex.Replace(fileRow[5], @",", string.Empty)}");
                    }
                }

                //Loop through the stuff here
                
                for(int i = 6; i < lines.Count - 2; i++)
                {
                    string[] columns = lines[i].Split('|');
                    if (string.IsNullOrEmpty(columns[0]) && columns[1] != "Balance at Period Start")
                    {
                        transactions.Last().Description += $" {columns[1]}";
                    }
                    else if(columns[1] == "Balance at Period Start")
                    {
                        //Do nothing
                    }
                    else
                    {
                        transactions.Add(new Models.Transaction
                        {
                            Amount = columns[4],
                            Balance = columns[5],
                            Description = columns[1],
                            PostDate = columns[3],
                            Reference = columns[2],
                            ValueDate = columns[0]
                        });
                    }
                }
                wow.AppendLine(lines[0].Replace('|',','));
                wow.AppendLine(lines[1].Replace('|', ','));
                wow.AppendLine(lines[2].Replace('|', ','));
                wow.AppendLine(lines[3].Replace('|', ','));
                wow.AppendLine(lines[4].Replace('|', ',')); 
                wow.AppendLine(lines[5].Replace('|', ','));
                foreach(var y in transactions)
                {
                    wow.AppendLine(y.ToString());
                }
                wow.AppendLine(lines[lines.Count - 2].Replace('|', ','));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                wow.Clear();
            }
            return wow.ToString();
        }
    }
}
