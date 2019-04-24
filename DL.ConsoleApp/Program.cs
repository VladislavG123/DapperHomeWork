using DL.DataAccess;
using DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.ConsoleApp
{
    class Program
    {
        public static int IntParser(int from, int to = 2147483647)
        {
            int result;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out result) && result >= from && result <= to)
                {
                    return result;
                }
            }
        }

        static void Main(string[] args)
        {
            using (var receiverRepository = new ReceiversRepository())
            {
                while (true)
                {
                    Console.WriteLine("1 - отправить сообщение");
                    Console.WriteLine("2 - выход");

                    int chouse = IntParser(1, 2);

                    if (chouse == 1)
                    {
                        var mail = new Mail();
                        Console.WriteLine("Введите тему письма");
                        mail.Theme = Console.ReadLine();

                        Console.WriteLine("Введите содержимое письма");
                        mail.Text = Console.ReadLine();

                        var receiver = new Receiver();
                        
                        Console.WriteLine("Введите адрес получателя");
                        receiver.Address = Console.ReadLine();

                        bool isExists = false;

                        foreach (var oldReceiver in receiverRepository.GetAll())
                        {
                            if (oldReceiver.Address == receiver.Address)
                            {
                                mail.Receiver = oldReceiver;
                                mail.ReceiverId = oldReceiver.Id;
                                
                                isExists = true;
                            }
                        }

                        if (!isExists)
                        {
                            Console.WriteLine("Введите полное имя получателя");
                            receiver.FullName = Console.ReadLine();

                            mail.Receiver = receiver;
                            mail.ReceiverId = receiver.Id;

                            receiverRepository.Add(receiver);
                        }

                        using (var mailRepository = new MailsRepository())
                        {
                            mailRepository.Add(mail);
                        }
                        Console.WriteLine("Письмо отправленно!");

                    }
                    else break;

                }
                
            }
        }
    }
}
