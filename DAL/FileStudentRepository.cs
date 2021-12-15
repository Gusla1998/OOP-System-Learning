using DAL.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL
{
    public class FilePersonRepository : IRepository<Person>
    {
        string _path;
        public FilePersonRepository(string path)
        {
            _path = path;
            if (!File.Exists(path)) File.Create(path).Close();
        }

        public void Change(Person item)
        {
            // Функция удалить принимает на вход список элементов и перезаписывает его обратно в файл, меняя данные у записи с нужным Id
            var all = GetAll();
            using (var sw = new StreamWriter(_path, false))
            {
                foreach (var person in all)
                {
                    if (person.Id == item.Id)
                    {
                        person.Group = item.Group;
                        sw.WriteLine($"{person.Id} {person.Name} {person.Group} ");
                    }
                    else
                    {
                        sw.WriteLine($"{person.Id} {person.Name} {person.Group}");
                    }
                }
            }
        }

        public void Create(Person item)
        {
            var all = GetAll();
            item.Id = all.Count() + 1;
            using (var sw = new StreamWriter(_path, true))
            {
                // DB recreate
                sw.WriteLine($"{item.Id} {item.Name} {item.Group}");
            }
        }

        public void Delete(int Id)
        {
            // Функция удалить принимает на вход список элементов и перезаписывает его обратно в файл, пропуская тот у которого нужный Id
            var all = GetAll();
            int new_ID = 1;
            using (var sw = new StreamWriter(_path, false))
            {
                foreach(var person in all)
                {
                    if (person.Id == Id)
                    {
                        continue;
                    }
                    else
                    {
                        sw.WriteLine($"{new_ID} {person.Name} {person.Group}");
                    }
                    new_ID++;
                }
            }
        }

        public IEnumerable<Person> Find(Func<Person, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Person Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            var ret = new List<Person>();
            using (var sr = new StreamReader(_path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var data = s.Split();
                    ret.Add(new Person { Id = int.Parse(data[0]), Name = data[1], Group = int.Parse(data[2]) });

                }
                return ret;
            }
        }

    }
}
