using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Net.Security;
using System.Xml.Linq;

// дозапись в файл, а не перезапись

namespace ConsoleApp36
{
    class Program
    {
        static void Main(string[] args)
        {
            string userChoice = "";
            while (userChoice != "q")
            {
                Console.WriteLine("\n1. Вывести информацию о файловой системы \n" +
            "2. Работа с файлами \n" +
            "3. Работа с JSON-файлами \n" +
            "4. Работа с XML-файлами \n" +
            "5. Работа с ZIP-архивами \n" +
            "Нажмите \"q\" для выхода\n");
                Console.Write("> ");
                userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "q":
                        Console.WriteLine("Завершение программы");
                        break;
                    case "1":
                        DriveInfo[] drives = DriveInfo.GetDrives();

                        foreach (DriveInfo drive in drives)
                        {
                            Console.WriteLine($"Название: {drive.Name}");
                            Console.WriteLine($"Тип: {drive.DriveType}");
                            if (drive.IsReady)
                            {
                                Console.WriteLine($"Объем диска: {drive.TotalSize}");
                                Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
                                Console.WriteLine($"Метка: {drive.VolumeLabel}");
                            }
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        WorkWithTextFile txtFile = new WorkWithTextFile();
                        //Filename fn = new Filename();
                        Console.WriteLine("\n1. Создать файл \n" +
                        "2. Записать в файл строку, введённую пользователем \n" +
                        "3. Прочитать файл в консоль \n" +
                        "4. Удалить файл\n");
                        Console.Write("> ");
                        userChoice = Console.ReadLine();
                        switch (userChoice)
                        {
                            case "1":
                                txtFile.CreateTextFile();
                                break;
                            case "2":
                                txtFile.WriteStringToTextFile();
                                break;
                            case "3":
                                txtFile.ReadTextFileToConsole();
                                break;
                            case "4":
                                txtFile.DeleteTextFile();
                                break;
                        }
                        break;
                    case "3":
                        WorkWithJSON jsonfile = new WorkWithJSON();
                        Console.WriteLine("\n1. Создать JSON-файл\n" +
                        "2. Создать JSON-объект, записывать в файл \n" +
                        "3. Прочитать файл в консоль \n" +
                        "4. Удалить файл\n");
                        Console.Write("> ");
                        userChoice = Console.ReadLine();
                        switch (userChoice)
                        {
                            case "1":
                                jsonfile.CreateJSONFile();
                                break;
                            case "2":
                                jsonfile.WriteJSONObject();
                                break;
                            case "3":
                                jsonfile.ReadJSONFile();
                                break;
                            case "4":
                                jsonfile.DeleteJsonFile();
                                break;
                        }
                        break;
                    case "4":
                        WorkWithXMLFile xml = new WorkWithXMLFile();
                        Console.WriteLine("\n1. Создать XML-файл\n" +
                        "2. Записать в файл новые данные из консоли \n" +
                        "3. Прочитать файл в консоль \n" +
                        "4. Удалить файл\n");
                        Console.Write("> ");
                        userChoice = Console.ReadLine();
                        switch (userChoice)
                        {
                            case "1":
                                xml.CreateXMLFile();
                                break;
                            case "2":
                                xml.WriteToXMLFile();
                                break;
                            case "3":
                                xml.ReadXmlFile();
                                break;
                            case "4":
                                xml.DeleteXMLFile();
                                break;
                        }
                        break;
                    case "5":
                        WorkWithZIPFile zipFile = new WorkWithZIPFile();
                        Console.WriteLine("\n1. Создать архив в формате zip \n" +
                        "2. Добавить файл, выбранный пользователем, в архив \n" +
                        "3. Разархивировать файл и вывести данныне о нём \n" +
                        "4. Удалить файл и архив\n");
                        Console.Write("> ");
                        userChoice = Console.ReadLine();
                        switch (userChoice)
                        {
                            case "1":
                                zipFile.CreateZipFile();
                                break;
                            case "2":
                                zipFile.AddFile();
                                zipFile.InfoZip();
                                break;
                            case "3":
                                zipFile.ExtraxtFile();
                                zipFile.InfoFile();
                                break;
                            case "4":
                                zipFile.DeleteZipFile();
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Введите номер доступной команды");
                        break;
                }

            }
        }
    }

    static class Utility
    {
        public static void DeleteAnyTypeFile (string path)
        {
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                Console.WriteLine($"Файл {path} удален\n");
            }
            else
            {
                Console.WriteLine("Файла с таким именем не существует!");
            }
        }
    }

    class WorkWithTextFile
    {
        string textFileName;

        public string TextFileName
        {
            get { return textFileName; }
            set { textFileName = value; }
        }
        public string GetTextFilePath()
        {
            Console.Write("Введите название текстового файла > ");
            textFileName = Console.ReadLine() + ".txt";
            return textFileName;
        }
        public void CreateTextFile()
        {
            GetTextFilePath();
            FileInfo fileInf = new FileInfo(textFileName);
            if (fileInf.Exists)
            {
                Console.WriteLine("Файл с таким именем уже существует!");
            }
            else
            {
                StreamWriter file = File.CreateText(textFileName);
                file.Close();
                Console.WriteLine($"Файл {textFileName} создан\n");
            }
        }
        public void DeleteTextFile()
        {
            GetTextFilePath();
            Utility.DeleteAnyTypeFile(textFileName);
        }
        public void WriteStringToTextFile()
        {
            GetTextFilePath();
            FileInfo fileInf = new FileInfo(textFileName);
            if (fileInf.Exists)
            {
                Console.Write("Введите строку для записи в файл > ");
                string text = "\n" + Console.ReadLine();
                using (FileStream fstream = new FileStream($"{textFileName}", FileMode.Append))
                {
                    byte[] input = Encoding.Default.GetBytes(text);
                    fstream.Write(input, 0, input.Length);
                    Console.WriteLine($"Текст записан в файл {textFileName}\n");
                }
            }
            else
            {
                Console.WriteLine("Файла с таким именем не существует!");
            }
        }
        public void ReadTextFileToConsole()
        {
            GetTextFilePath();
            FileInfo fileInf = new FileInfo(textFileName);
            if (fileInf.Exists)
            {
                using (FileStream fstream = File.OpenRead($"{textFileName}"))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                    Console.WriteLine($"Текст в файле {textFileName}:");
                    Console.WriteLine(textFromFile);
                }
            }
            else
            {
                Console.WriteLine("Файла с таким именем не существует!");
            }

        }


    }

    class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }

    class WorkWithJSON
    {
        string jsonFileName;

        public string JsonFileName
        {
            get { return jsonFileName; }
            set { jsonFileName = value; }
        }

        public string GetJSONFilePath()
        {
            Console.Write("Введите название JSON-файла > ");
            jsonFileName = Console.ReadLine() + ".json";
            return jsonFileName;
        }
        public void CreateJSONFile()
        {
            GetJSONFilePath();
            FileInfo file = new FileInfo(jsonFileName);
            if (file.Exists)
            {
                Console.WriteLine("Файл с таким именем уже существует!");
            }
            else
            {
                file.Create();
                Console.WriteLine($"Файл {jsonFileName} создан\n");
            }
        }
        public void DeleteJsonFile()
        {
            GetJSONFilePath();
            Utility.DeleteAnyTypeFile(jsonFileName);
        }

        public void WriteJSONObject()
        {
            GetJSONFilePath();

            Console.Write("Введите название книги > ");
            string title = Console.ReadLine();
            Console.Write("Введите имя автора > ");
            string author = Console.ReadLine();
            Console.Write("Введите название издательства > ");
            string publisher = Console.ReadLine();

            Book book = new Book { Title = title, Author = author, Publisher = publisher };
            string json = System.Text.Json.JsonSerializer.Serialize<Book>(book);
            Console.WriteLine(json);

            File.WriteAllText(jsonFileName, json);
            Console.WriteLine($"Объект записан в файл {jsonFileName}\n");
        }
        public void ReadJSONFile()
        {
            GetJSONFilePath();
            if (File.Exists(jsonFileName))
            {
                Console.WriteLine($"Объект в файле {jsonFileName}:\n");
                string jsonContent = File.ReadAllText(jsonFileName);
                var jsonData = JsonConvert.DeserializeObject(jsonContent);
                Console.WriteLine(jsonData);
            }
            else
            {
                Console.WriteLine("Файла с таким именем не существует!");
            }
        }

    }
    class WorkWithZIPFile
    {
        string zipFileName;

        public string ZipFileName
        {
            get { return zipFileName; }
            set { zipFileName = value; }
        }

        string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }


        public string GetZIPFileName()
        {
            Console.Write("Введите название ZIP-архива > ");
            zipFileName = Console.ReadLine() + ".zip";
            return zipFileName;
        }
        public string GetFileName()
        {
            Console.Write("Введите название файла > ");
            fileName = Console.ReadLine();
            return fileName;
        }

        public void CreateZipFile()
        {
            //StreamWriter file = File.CreateText(zipFileName);
            //file.Close();

            GetZIPFileName();
            FileInfo zipFileInf = new FileInfo(zipFileName);
            if (zipFileInf.Exists)
            {
                Console.WriteLine("Архив с таким именем уже существует!");
            }
            else
            {
                StreamWriter file = File.CreateText(zipFileName);
                file.Close();
                Console.WriteLine($"Файл {zipFileName} создан\n");
            }
        }
        public void DeleteZipFile()
        {
            GetZIPFileName();
            Utility.DeleteAnyTypeFile(zipFileName);
        }
        public void AddFile()
        {
            GetZIPFileName();
            FileInfo zipFileInf = new FileInfo(zipFileName);
            
            if (!zipFileInf.Exists)
            {
                Console.WriteLine("Архива с таким именем не существует!\n");
            }
            else
            {
                ZipArchive zipArchive = ZipFile.Open(zipFileName, ZipArchiveMode.Update);
                GetFileName();
                FileInfo file = new FileInfo(fileName);
                if (!file.Exists)
                {
                    Console.WriteLine("Файла с таким именем не существует!\n");
                }
                else
                {
                    zipArchive.CreateEntryFromFile(fileName, fileName);
                }
            }

        }
        public void ExtraxtFile()
        {
            GetZIPFileName();
            FileInfo zipFileInf = new FileInfo(zipFileName);
            if (!zipFileInf.Exists)
            {
                Console.WriteLine("Архива с таким именем не существует!\n");
            }
            else
            {
                ZipArchive zipArchive = ZipFile.OpenRead(zipFileName);
                GetFileName();
                FileInfo file = new FileInfo(fileName);
                if (file.Exists)
                {
                    Console.WriteLine("Файла с таким именем уже существует!\n");
                }
                else
                {
                    zipArchive.Entries.FirstOrDefault(x => x.Name == fileName)?.
                    ExtractToFile(fileName);
                }
            }
        }
        public void InfoFile()
        {
            FileInfo fileInf = new FileInfo(fileName);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
            else
            {
                Console.WriteLine("Файла с таким именем не существует!\n");
            }
        }
        public void InfoZip()
        {
            FileInfo fileInf = new FileInfo(zipFileName);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }
            else
            {
                Console.WriteLine("Файла с таким именем не существует!\n");
            }
        }
        public void Problem()
        {
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
            {
                Console.WriteLine("Файла с таким именем не существует!\n");
            }
            else
            {
                using (ZipArchive zipArchive = ZipFile.Open(zipFileName, ZipArchiveMode.Update))
                {
                    zipArchive.CreateEntryFromFile(fileName, fileName);
                }
            }
        }
    }

    public class WorkWithXMLFile
    {
        string xmlFileName;

        public string ZipFileName
        {
            get { return xmlFileName; }
            set { xmlFileName = value; }
        }

        public string GetXMLFileName()
        {
            Console.Write("Введите название XML-файла > ");
            xmlFileName = Console.ReadLine() + ".xml";
            return xmlFileName;
        }
        public void CreateXMLFile()
        {
            GetXMLFileName();
            AddClientToXMLFile("Alex", "Base", "10");
        }
        public void WriteToXMLFile()
        {
            GetXMLFileName();
            var FileInfo = new FileInfo(xmlFileName);

            if (FileInfo.Exists)
            {
                Console.Write("Введите имя клиента > ");
                string _name = Console.ReadLine();
                Console.Write("Введите уровень абонемента > ");
                string _level = Console.ReadLine();
                Console.Write("Введите скидку клиента > ");
                string _sale = Console.ReadLine();

                XDocument xdoc = XDocument.Load(xmlFileName);
                XElement root = xdoc.Element("Clients");
                root.Add(new XElement("Client",
                new XAttribute("Name", _name), new XElement("Level", _level), new XElement("Sale", _sale)));
                xdoc.Save(xmlFileName);
            }
            else
            {
                Console.WriteLine("Файла с таким именем не существует!\n");
            }
        }

        public void AddClientToXMLFile(string name, string level, string sale)
        {
            XDocument xdoc = new XDocument();
            XElement client = new XElement("Client");
            XAttribute clientName = new XAttribute("Name", name);
            XElement clientLevel = new XElement("Level", level);
            XElement clientSale = new XElement("Sale", sale);
            client.Add(clientName);
            client.Add(clientLevel);
            client.Add(clientSale);

            XElement clients = new XElement("Clients");
            clients.Add(client);
            xdoc.Add(clients);

            xdoc.Save(xmlFileName);
            Console.WriteLine($"Объект добавлен в файл {xmlFileName}");
        }

        public void ReadXmlFile()
        {
            GetXMLFileName();

            var FileInfo = new FileInfo(xmlFileName);
            if (FileInfo.Exists)
            {
                Console.WriteLine($"Данные в файле {xmlFileName}:\n");
                XDocument xdoc = XDocument.Load(xmlFileName);
                foreach (XElement phoneElement in xdoc.Element("Clients").Elements("Client"))
                {
                    XAttribute nameAttribute = phoneElement.Attribute("Name");
                    XElement levelElement = phoneElement.Element("Level");
                    XElement saleElement = phoneElement.Element("Sale");

                    if (nameAttribute != null && levelElement != null && saleElement != null)
                    {
                        Console.WriteLine($"Имя: {nameAttribute.Value}");
                        Console.WriteLine($"Уровень: {levelElement.Value}");
                        Console.WriteLine($"Скидка: {saleElement.Value}");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Файла с таким именем не существует!\n");
            }
        }

        public void DeleteXMLFile()
        {
            GetXMLFileName();
            Utility.DeleteAnyTypeFile(xmlFileName);
        }
    }
}

