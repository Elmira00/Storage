using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp33
{
   public abstract class Storage
    {
        protected Storage(double  media, string model)
        {
            Media = media;
            Model = model;
        }

        public double Media{ get; set; }
        public string Model{ get; set; }
        public static  double Capacity{ get; set; }

        public virtual void CopyTo()
        {
            Console.WriteLine("Copying Proccess . . . . . . . . . . .");
        }
        public virtual double  GetFreeCapacity()
        {
            return Capacity ;
        }

        public virtual void PrintInfo()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"______________I N F O______________");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"Media : {Media}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Capacity: {Capacity}");
        }
    }
    class Flash : Storage
    {
        
        public  static double Speed { get; set; } = 3.0;//Mbit
        public Flash(double media,string model)
            :base(media,model)
        {
          Capacity  = 512;//GB
        }
        public override void CopyTo()
        {
            if (Capacity >= Media) { 
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.Write("Copying to flash drive ");
            int time = Convert.ToInt32(Media / Speed);
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(time*20);
                Console.Write(" .  ");
            }
                Thread.Sleep(time * 20);
                Console.WriteLine();
                Capacity -= Media;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("C O P I E D      S U C C E S S F U L L Y  ! ");
            Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("L A C K   O F    M E M O R Y ! ! !");
            }
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Speed : {Speed}");
        }
    }
    class DVD : Storage
    {
        public static double ReadWriteSpeed { get; set; } = 0.0013;//GB
        public string Type { get; set; } 
        public DVD(double media, string model,string type)
            :base(media,model)
        {
            Type = type;
            if (Type == "double-sided")
            {
                Capacity = 9.0;//GB
            }
            else
            {
                Capacity = 4.7;//GB
            }
        }
        public override void CopyTo()
        {
            if (Capacity >= Media)
            {
                int time = Convert.ToInt32(Media / ReadWriteSpeed);
                for (int i = 0; i < 2; i++)
                {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write("Copying to DVD ");
                    for (int k = 0; k < 3; k++)
                    {
                        Thread.Sleep(time);
                        Console.Write(" .  ");
                    }
                 Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("       ");
                }
                Console.WriteLine();
                Capacity -= Media;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("C O P I E D      S U C C E S S F U L L Y  ! ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("L A C K   O F    M E M O R Y ! ! !");
            }
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Read & Write speed : {ReadWriteSpeed}");
            Console.WriteLine($"Type : {Type}");
        }
    }
    class HDD : Storage
    {
        public static double SpindleSpeed { get; set; } = 4.200;//rpm
        public static double RotationalLatancy { get; set; } = 7.14;//ms

        public HDD(double media,string model)
            :base(media,model)
        {
            Capacity = 1024;//GB
        }
        public override void CopyTo()
        {
            if (Capacity >= Media)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                int time = Convert.ToInt32(Media / SpindleSpeed);
                for (int i = 0; i < 2; i++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write("Copying to DVD ");
                    for (int k = 0; k < 3; k++)
                    {
                        Thread.Sleep(time*20);
                        Console.Write(" .  ");
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("       ");
                        
                   Thread.Sleep(time*2);
                }
                Console.WriteLine();
                Capacity -= Media;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("C O P I E D      S U C C E S S F U L L Y  ! ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("L A C K   O F    M E M O R Y ! ! !");
            }
        }
        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Spindle speed : {SpindleSpeed}");
            Console.WriteLine($"Rotational latancy : {RotationalLatancy}");
        }
    }

    public  class Program
    {
       public static void   ExecuteOperation(ref Storage obj)
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("***E X E C U T E     O P E R A T I O N***");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Copy To : [1]");
            Console.WriteLine("Show Free Memory : [2]");
            Console.WriteLine("Show Device Information : [3]");
            Console.WriteLine("Exit : [4]");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                obj.CopyTo();
            }
            else if (choice == 2)
            {
                Console.WriteLine(obj.GetFreeCapacity());
            }
            else if (choice == 3)
            {
                obj.PrintInfo();
            }
            else if (choice == 4)
            {
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new Exception("W R O N G     I N P U T ! ! !");
            }
        }
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Enter media capacity  : ");
                    double media = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Choose drive type : ");
                    Console.WriteLine("Flash Drive : [1] ");
                    Console.WriteLine("DVD : [2]");
                    Console.WriteLine("HDD : [3]");
                    Console.WriteLine("Exit : [4]");
                    int drive = Convert.ToInt32(Console.ReadLine());
                    if (drive == 1)
                    {
                        Storage flashDrive = new Flash(media, "Samsung");
                        while (true)
                        {
                            ExecuteOperation(ref flashDrive);
                        }
                    }
                    else if (drive == 2)
                    {
                        Console.WriteLine("Which DVD ?");
                        Console.WriteLine("Single-sided : [1]");
                        Console.WriteLine("Double-sided : [2]");
                        int type = Convert.ToInt32(Console.ReadLine());
                        if (type == 1)
                        {
                            Storage dvd = new DVD(media, "Sony", "single-sided");
                            while (true)
                            {
                                ExecuteOperation(ref dvd);
                            }
                        }
                        else if (type == 2)
                        {
                            Storage dvd = new DVD(media, "Sony", "double-sided");
                            while (true)
                            {
                                ExecuteOperation(ref dvd);
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            throw new Exception("W R O N G     I N P U T ! ! !");
                        }

                    }
                    else if (drive == 3)
                    {
                        Storage hdd = new HDD(media, "Samsung");
                        while (true)
                        {
                            ExecuteOperation(ref hdd);
                        }
                    }
                    else if (drive == 4)
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("W R O N G     I N P U T ! ! !");
                    }
                }
            }
            catch(Exception  ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
