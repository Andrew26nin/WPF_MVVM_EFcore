using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Entity db = new Entity())
            {
                // создаем два объекта User


                Client c1 = new Client { Name = "Tom", Address = "sfsdfsd", IsVIP = false };

                // добавляем их в бд
                db.Clients.Add(c1);

                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.Clients.ToList();
                Console.WriteLine("Список объектов:");
                foreach (Client u in users)
                {
                    Console.WriteLine($"{u.Id}.{u.Name} - {u.Address}");
                }
            }
            Console.Read();
        }
    }
}
