using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;
using System.IO;


namespace WzorzecAdapter
{
    //KOD Z ZEWNĘTRZNEJ BIBLIOTEKI
    public class UsersApi
    {
        public async Task<string> GetUsersXmlAsync()
        {
            var apiResponse = "<?xml version= \"1.0\" encoding= \"UTF-8\"?><users><user name=\"John\" surname=\"Doe\"/><user name=\"John\" surname=\"Wayne\"/><user name=\"John\" surname=\"Rambo\"/></users>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(apiResponse);

            return await Task.FromResult(doc.InnerXml);
        }
    }

    //
    // tu trzeba dopisać klasę zwracającą zawartość pliku csv w postaci stringa
    // (jednego długiego, rozdzielanego znakami nowego wiersza)
    //
      

    public class IUserCsvReader
    {
        private string _csvFilePath = "users.csv";

        public string ReadCsvAsString()
        {
                string csvData = File.ReadAllText(_csvFilePath);
                return csvData;
          }
    }


    public interface IUserRepository
    {
        List<List<string>> GetUserNames();
    }

    public class UsersApiAdapter : IUserRepository
    {
        private UsersApi _adaptee = null;

        public UsersApiAdapter(UsersApi adaptee)
        {
            _adaptee = adaptee;
        }

        public List<List<string>> GetUserNames()
        {
            string incompatibleApiResponse = this._adaptee
              .GetUsersXmlAsync()
              .GetAwaiter()
              .GetResult();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(incompatibleApiResponse);
            
            var rootEl = doc.LastChild;

            List<List<string>> users = new List<List<string>>();

            if (rootEl.HasChildNodes)
            {
                List<string> user = new List<string> {};
                foreach (XmlNode node in rootEl.ChildNodes)
{
                    user = new List<string> {node.Attributes["name"].InnerText, node.Attributes["surname"].InnerText};
                    users.Add(user);
                }
            }
            return users;
        }

    }

    //
    // tu trzeba dopisać własny adapter implementujący odpowiedni interfejs
    //
public class CsvDataAdapter : IUserRepository
    {
        private IUserCsvReader _csvReader;

        public CsvDataAdapter(IUserCsvReader csvReader)
        {
            _csvReader = csvReader;
        }

        public List<List<string>> GetUserNames()
        {
            string csvData = _csvReader.ReadCsvAsString();
            List<List<string>> users = ProcessCsvData(csvData);
            return users;
        }

        private List<List<string>> ProcessCsvData(string csvData)
        {
            List<List<string>> users = new List<List<string>>();
            string[] lines = csvData.Split('\n');

            foreach (var line in lines)
            {
                string[] columns = line.Split(',');
                if (columns.Length >= 2)
                {
                    string name = columns[0].Trim();
                    string surname = columns[1].Trim();
                    users.Add(new List<string> { name, surname });
                }
            }

            return users;
        }
    }

    public class Program
    {

        static void Main(string[] args)
        {

            UsersApi usersRepository = new UsersApi();
            IUserRepository adapter = new UsersApiAdapter(usersRepository);

           Console.WriteLine("Użytkownicy z API:");
           List<List<string>> users = adapter.GetUserNames();
           for (int i = 0; i < users.Count; i++)
           {
               Console.WriteLine($" {i + 1}. {users[i][0]} {users[i][1]}");
           }

            Console.WriteLine();

            // TODO: wyświetl w konsoli wynik działania obu adapterów
          
           Console.WriteLine("Użytkownicy z CSV:");
           IUserCsvReader csvReader = new IUserCsvReader(); 
           IUserRepository csvAdapter = new CsvDataAdapter(csvReader);
           List<List<string>> csvUsers = csvAdapter.GetUserNames();
           for (int i = 0; i < csvUsers.Count; i++)
           {
             int numberOfDigits = (i+1).ToString().Length;
             if(numberOfDigits == 1) Console.WriteLine($" {i + 1}. {csvUsers[i][0]} {csvUsers[i][1]}");
             else Console.WriteLine($"{i + 1}. {csvUsers[i][0]} {csvUsers[i][1]}");
           }
        }
    }
}
