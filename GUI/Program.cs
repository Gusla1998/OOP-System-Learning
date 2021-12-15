using BLL;
using DAL;
using DAL.Infrostructures;
using System;
using System.IO;

namespace GUI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string task_file = "task.db";
            string person_file = "person.db";
            string report_file = "report.db";
            string sprint_file = "sprint.db";
            string sprint_status = "sprint_status.txt";

            var repo_person = new FileStudentRepository(person_file);
            var repo_report = new FileGroupRepository(report_file);
            var repo_sprint = new FileGroupRepository(sprint_file);

            var service_person = new PersonService(repo_person);
            var service_report = new ReportService(repo_report);
            var service_sprint = new ReportService(repo_sprint);

            var controller_person = new PersonController(service_person);
            var controller_report = new ReportController(service_report);
            var controller_sprint = new ReportController(service_sprint);

            int admin_password = 1111;

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine($"Выберите раздел: \n1)сотрудники\n2)задачи\n3)отчет за день\n4)отчет за спринт\n5)выход");
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "1":
                        Console.WriteLine($"Введите команду: \n1)добавить\n2)поменять начальника\n3)показать иерархию\n4)показать задачи сотрудника\n5)удалить");
                        cmd = Console.ReadLine();
                        PersonViewModel person = new PersonViewModel();
                        switch (cmd)
                        {                                
                                case "1":
                                person = new PersonViewModel();
                                Console.Write("Введите Имя: ");
                                person.Name = Console.ReadLine();
                                Console.Write("Введите Id Начальника: ");
                                person.Boss = int.Parse(Console.ReadLine());
                                controller_person.Add(person);
                                break;
                            case "2":
                                person = new PersonViewModel();
                                person.Name = "";
                                Console.Write("Введите Id Сотрудника: ");
                                person.Id = int.Parse(Console.ReadLine());
                                Console.Write("Введите Id нового Начальника: ");
                                person.Boss = int.Parse(Console.ReadLine());
                                controller_person.Change(person);
                                break;
                            case "3":
                                Console.WriteLine("=== Иерархия персонала ===");
                                controller_person.Show();                                
                                break;
                            case "4":
                                Console.Write("Введите Id Сотрудника: ");
                                int Id = int.Parse(Console.ReadLine());
                                Console.WriteLine("=== Список задач ===");
                                Console.WriteLine("ID  Текст  ID_Работника  Статус  Комментарий");
                                foreach (var t in controller_task.Show())
                                {
                                    if (Id == t.PersonId)
                                    {
                                        Console.WriteLine($"{t.Id}   {t.Text}      {t.PersonId}     {t.Status}     {t.Comment}");
                                    }
                                }
                                break;
                            case "5":
                                Console.WriteLine("Введите id сотрудника, которого необходимо удалить: ");
                                controller_person.Delete(int.Parse(Console.ReadLine()));
                                break;
                            default:
                                Console.Write("Неверная команда.");
                                break;
                        }
                        break;
                    case "2":
                        Console.WriteLine($"Введите команду: \n1)добавить\n2)внести изменения\n3)показать\n4)переместить в отчет\n5)удалить");
                        cmd = Console.ReadLine();
                        switch (cmd)
                        {
                            case "1":
                                Console.Write("Введите задачу: ");
                                controller_task.Add(new TaskViewModel { Text = Console.ReadLine(), PersonId = 0, Status = "open", Comment = "No comment"});
                                break;
                            case "2":
                                TaskViewModel change_task = new TaskViewModel();

                                Console.Write("Введите Id задачи: ");                                
                                change_task.Id = int.Parse(Console.ReadLine());
                                change_task.Text = "";

                                Console.Write("Введите Id сотрудника, который будет выполнять задачу: ");
                                change_task.PersonId = int.Parse(Console.ReadLine());

                                Console.Write("Введите статус задачи ( open, active, resolved ): ");
                                change_task.Status = Console.ReadLine();

                                Console.Write("Введите комментарий к задаче: ");
                                change_task.Comment = Console.ReadLine();

                                controller_task.Change(change_task);                                
                                break;
                            case "3":
                                Console.WriteLine("=== Список задач ===");
                                Console.WriteLine("ID  Текст  ID_Работника  Статус  Комментарий");
                                foreach (var t in controller_task.Show())
                                {
                                    Console.WriteLine($"{t.Id}   {t.Text}      {t.PersonId}     {t.Status}     {t.Comment}");
                                }
                                break;
                            case "4":
                                Console.WriteLine("Введите id задачи, которую необходимо перенести в отчет: ");
                                int id = int.Parse(Console.ReadLine());
                                foreach (var t in controller_task.Show())
                                {
                                    if (t.Id == id)
                                    {
                                        controller_report.Add(new ReportViewModel { Text = t.Text, PersonId = t.PersonId });
                                        controller_sprint.Add(new ReportViewModel { Text = t.Text, PersonId = t.PersonId });
                                    }
                                }                                
                                controller_task.Delete(id);
                                break;
                            case "5":
                                Console.WriteLine("Введите id задачи, которую необходимо удалить: ");
                                controller_task.Delete(int.Parse(Console.ReadLine()));
                                break;
                            default:
                                Console.Write("Неверная команда.");
                                break;
                        }
                        break;
                    case "3":
                        Console.WriteLine($"Введите команду: \n1)редактировать\n2)удалить задачу\n3)посмотреть выполненные задачи\n4)удалить отчет");
                        cmd = Console.ReadLine();
                        switch (cmd)
                        {
                            case "1":
                                ReportViewModel change_report = new ReportViewModel();

                                Console.Write("Введите Id задачи: ");
                                change_report.Id = int.Parse(Console.ReadLine());

                                Console.Write("Введите текст выполненной задачи: ");
                                change_report.Text = Console.ReadLine();

                                Console.Write("Введите Id сотрудника, который выполнил задачу: ");
                                change_report.PersonId = int.Parse(Console.ReadLine());

                                controller_report.Change(change_report);
                                break;
                            case "2":
                                Console.WriteLine("Введите id задачи, которую необходимо удалить: ");
                                controller_report.Delete(int.Parse(Console.ReadLine()));
                                break;
                            case "3":
                                Console.Write("Введите Id сотрудника, задачи которого хотите просмотреть: ");
                                int Id = int.Parse(Console.ReadLine());

                                Console.WriteLine("=== Список задач ===");
                                Console.WriteLine("ID  Текст");
                                foreach (var t in controller_report.Show())
                                {
                                    if (Id == t.PersonId)
                                    {
                                        Console.WriteLine($"{t.Id}   {t.Text}");
                                    }
                                }
                                break;
                            case "4":
                                Console.Write("Введите пароль для подтверждения удаления: ");
                                int pass = int.Parse(Console.ReadLine());
                                if (pass == admin_password)
                                {
                                    using (var sw = new StreamWriter(report_file, false)) ;
                                }
                                break;                                
                            default:
                                Console.Write("Неверная команда.");
                                break;
                        }
                        break;
                    case "4":
                        Console.WriteLine($"Введите команду: \n1)редактировать\n2)удалить задачу\n3)посмотреть выполненные задачи\n4)посмотреть выполненные задачи(для начальника и сотрудников)\n5)посмотреть статус отчета\n6)изменить статус отчета");
                        cmd = Console.ReadLine();

                        string status = "";
                        using (var sr = new StreamReader(sprint_status))
                        {
                            status = sr.ReadLine();
                        }
                        switch (cmd)
                        {
                            case "1":
                                if (status == "open")
                                {
                                    ReportViewModel change_report = new ReportViewModel();

                                    Console.Write("Введите Id задачи: ");
                                    change_report.Id = int.Parse(Console.ReadLine());

                                    Console.Write("Введите текст выполненной задачи: ");
                                    change_report.Text = Console.ReadLine();

                                    Console.Write("Введите Id сотрудника, который выполнил задачу: ");
                                    change_report.PersonId = int.Parse(Console.ReadLine());

                                    controller_sprint.Change(change_report);
                                }
                                else
                                {
                                    Console.WriteLine("Отчет закрыт для редактирования.");
                                }
                                break;
                            case "2":
                                if (status == "open")
                                {
                                    Console.WriteLine("Введите id задачи, которую необходимо удалить: ");
                                    controller_sprint.Delete(int.Parse(Console.ReadLine()));
                                }
                                else
                                {
                                    Console.WriteLine("Отчет закрыт для редактирования.");
                                }
                                break;
                            case "3":
                                Console.Write("Введите Id сотрудника, задачи которого хотите просмотреть: ");
                                int Id = int.Parse(Console.ReadLine());

                                Console.WriteLine("=== Список задач ===");
                                Console.WriteLine("ID  Текст");
                                foreach (var t in controller_sprint.Show())
                                {
                                    if (Id == t.PersonId)
                                    {
                                        Console.WriteLine($"{t.Id}   {t.Text}");
                                    }
                                }
                                break;
                            case "4":
                                Console.Write("Введите Id начальника, задачи сотрудников которого хотите просмотреть: ");
                                int star_Id = int.Parse(Console.ReadLine());

                                Console.WriteLine("=== Список задач ===");
                                Console.WriteLine("ID  Текст");
                                controller_person.Show_Task(star_Id, service_sprint);
                                break;
                            case "5":
                                Console.WriteLine(status);
                                break;
                            case "6":
                                Console.Write("Введите пароль для смены статуса: ");
                                int pass = int.Parse(Console.ReadLine());
                                if (pass == admin_password)
                                {
                                    Console.Write("Введите статус отчета за спринт(open, close): ");
                                    status = Console.ReadLine();
                                    using (var sw = new StreamWriter(sprint_status, false))
                                    {
                                        sw.Write(status);
                                    }
                                }
                                break;
                            default:
                                Console.Write("Неверная команда.");
                                break;
                        }
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверная команда.");
                        break;
                }                
            }
        }
    }
}
