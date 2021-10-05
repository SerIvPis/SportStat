using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eleron.FootballStatistic.TournamentData
{
    /// <summary>
    /// Класс наблюдает за папкой Source, и в случаи добавления
    /// текстового файла, создает его копию в папке Source/Import,
    /// Исходный файл проходит обработку и принимает стандартный вид
    /// </summary>
    public class WatchSourceFileTxt
    {
        
    #region Свойства и поля
    public FileSystemWatcher Watcher;
    public  DirectoryInfo workDir;
    public DirectoryInfo ImportDir;
    const string PathSource = @".\Source\";
    const string PathImport = @".\Source\Import\";
    private List<string> ListText;
    

         
    #endregion

    #region Конструкторы
     public WatchSourceFileTxt()
        {
           
            workDir = new DirectoryInfo(PathSource);
            if (!workDir.Exists)
            {
                workDir.Create();
            }
            ImportDir = new DirectoryInfo(PathImport);
            if (!ImportDir.Exists)
            {
                ImportDir.Create();
            }
           
           Watcher = new FileSystemWatcher(PathSource);
           
                // Указать цели наблюдения
            Watcher.NotifyFilter =  NotifyFilters.FileName;
            

            //Следить только за текстовыми файлами
            Watcher.Filter = "*.txt";
            //Добавить обработчики событий
            Watcher.Created +=  OnCreated;
            // Начать наблюдения за каталогом
            Watcher.EnableRaisingEvents = true;
            
        }

     public WatchSourceFileTxt(string _path)
        {
            
            
        }
         
    #endregion

    #region Методы
    
        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            //считывает данные из файла в список строк
            ListText = new List<string>(File.ReadAllLines( e.FullPath));                    
            ListText = FormatList(ListText);   
            DeleteDuplicateMatch(ListText);  
            SaveFile( e.FullPath);
        }

        /// <summary>
        /// Метод убирает дубликаты матчей из списка
        /// по средствам использования HashSet множества
        /// </summary>
        /// <param name="listText"></param>
        private void DeleteDuplicateMatch(List<string> listText)
        {
            HashSet<string> noDuplicateSet = new HashSet<string>(listText);
            listText.Clear();
            listText.AddRange(noDuplicateSet);
        }



        /// <summary>
        /// обрабатываем строки и приводим их к стандартному виду
        /// [Дата] [Команда хозяин] - [Команда гость] [голы хозяина] : [голы гости]
        /// Пример: 
        ///        01.08.2021	Краснодар  -  Химки	0 : 1    
        /// </summary>
        /// <param name="_listTxt"></param>
        /// <returns></returns>
        public List<string> FormatList(List<string> _listTxt)
        {
            List<string> _res = new List<string>();
            List<string> countElements ;
            char[] charSeparators = new char[] { '\t' };

            foreach (string line in _listTxt)
            {
               //countElements = new List<string>(line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries));
               //Если строка содержит слово тур, добавляем без изменений
               if (line.IndexOf( "тур" ) != -1 )       
               {
                    _res.Add(line);
                    continue;
               }

               //Если строка содержит символ "-", добавляем без изменений
               if (line.IndexOf( "." ) != -1 )
               {
                   countElements = new List<string>(line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries));
                   IEnumerable<string> seqTrimString = countElements.Select(x => x.Trim()); 
                   string newStr = string.Join(" ", seqTrimString );
                   _res.Add(newStr);
                   continue;
               }

               //Если строка содержит символ ":", значит это счет - сохраняем последнюю строку в буфер, 
               //удаляем ее из списка, добавляем в список строку из буфера + текущую строку(счет)
               if (line.IndexOf( ":" ) != -1 )
               {
                   string bufString = _res.Last();
                   _res.RemoveAt(_res.Count - 1);
                   _res.Add(string.Join(' ', bufString, line));
               }
            }


            return _res;
        }

        public void PrintTerminal()
        {
            foreach (string line in FormatList(ListText))
            {
                 System.Console.WriteLine( line);
            }
           
        }

        private void SaveFile(string _path)
        {
            string renamePath = Path.Combine( ImportDir.FullName,
                                        Path.GetFileNameWithoutExtension(_path) +
                                        "_bak.txt");
                                                
            File.Copy(_path, renamePath, true);

            if (File.Exists(_path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(_path))
                {
                    foreach (var item in ListText)
                    {
                         sw.WriteLine(item);
                    }
                  
                }
                Console.WriteLine($"Редакция файла {_path} - ОК");
            }
        }
    
    #endregion
       
      
    }
}