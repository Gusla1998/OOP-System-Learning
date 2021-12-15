using DAL.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    public class FileGroupRepository : IRepository<Group>
    {
        string _path;
        public FileGroupRepository(string path)
        {
            _path = path;
            if (!File.Exists(path)) File.Create(path).Close();
        }

        public void Change(Group item)
        {
            // Функция удалить принимает на вход список элементов и перезаписывает его обратно в файл, меняя данные у файла с нужным Id
            var all = GetAll();
            using (var sw = new StreamWriter(_path, false))
            {
                foreach (var task in all)
                {
                    if (task.Id == item.Id)
                    {
                        sw.WriteLine($"{item.Id} {item.GroupName} {item.StudentsId}");
                    }
                    else
                    {
                        sw.WriteLine($"{task.Id} {task.GroupName} {task.StudentsId}");
                    }
                }
            }
        }

        public void Create(Group item)
        {
            var all = GetAll();
            item.Id = all.Count() + 1;
            using (var sw = new StreamWriter(_path, true))
            {
                sw.WriteLine($"{item.Id} {item.GroupName} {item.StudentsId}");
            }
        }

        public void Delete(int id)
        {
            // Функция удалить принимает на вход список элементов и перезаписывает его обратно в файл, пропуская тот у которого нужный Id
            var all = GetAll();
            int new_ID = 1;
            using (var sw = new StreamWriter(_path, false))
            {
                foreach (var report in all)
                {
                    if (report.Id == id)
                    {
                        continue;
                    }
                    else
                    {
                        sw.WriteLine($"{new_ID} {report.GroupName} {report.StudentsId}");
                    }
                    new_ID++;
                }
            }
        }

        public IEnumerable<Group> Find(Func<Group, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Group Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Group> GetAll()
        {
            var ret = new List<Group>();
            using (var sr = new StreamReader(_path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var data = s.Split();
                    ret.Add(new Group { Id = int.Parse(data[0]), GroupName = data[1], StudentsId = int.Parse(data[2])});
                }
                return ret;
            }
        }
    }
}
