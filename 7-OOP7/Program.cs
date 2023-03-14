using System;

namespace OOP7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandCreateTrain = "1";
            const string CommandSendTrain = "2";
            const string CommandShowInformation = "3";
            const string CommandExit = "4";

            Dispatcher dispatcher = new Dispatcher();

            bool isWorking = true;

            Console.WriteLine($"{CommandCreateTrain} - СОЗДАТЬ ПОЕЗД" + $"\n{CommandSendTrain} - ОТПРАВИТЬ ПОЕЗД"
                + $"\n{CommandShowInformation} - ПОСМОТРЕТЬ ИНФОРМАЦИЮ О ПОЕЗДАХ" + $"\n{CommandExit} - ВЫХОД");

            while (isWorking)
            {
                Console.Write("\nВведите команду: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandCreateTrain:
                        dispatcher.AddTrain();
                        break;

                    case CommandSendTrain:
                        dispatcher.RemoveTrain();
                        break;

                    case CommandShowInformation:
                        dispatcher.ShowInfoSendTrain();
                        break;

                    case CommandExit:
                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine($"\nВведите {CommandCreateTrain}, {CommandSendTrain}, {CommandShowInformation} или {CommandExit}");
                        break;
                }
            }
        }
    }

    class Dispatcher
    {
        private List<Train> _trainList = new List<Train>();
        private List<Direction> _listDirectionTrain = new List<Direction>();

        public void AddTrain()
        {
            AddDirection();

            Train train = new();

            _trainList.Add(train);
        }

        public void RemoveTrain()
        {
            Console.Write("Введите номер поезда для отправки в путь - ");
            string userInput = Console.ReadLine();

            bool isSuccess = int.TryParse(userInput, out int trainNumber);

            if (isSuccess)
            {
                for (int i = 0; i < _trainList.Count; i++)
                {
                    if (trainNumber == i)
                    {
                        _trainList.RemoveAt(i);
                        Console.WriteLine("Поезд успешно отправлен.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка. Попробуйте ещё раз.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка. Неверный ввод.");
            }
        }

        public void ShowInfoSendTrain()
        {
            for (int i = 0; i < _listDirectionTrain.Count; i++)
            {
                Console.WriteLine("Точка отправления - " + _listDirectionTrain[i].DeparturePoint + "\nТочка прибытия - " + _listDirectionTrain[i].PlaceArrival);
            }
        }

        private void AddDirection()
        {
            Console.Write("\nСоздайте точку отправления - ");
            string startingPointRoute = Console.ReadLine();

            Console.Write("Создайте точку прибытия - ");
            string finalPointRoute = Console.ReadLine();

            _listDirectionTrain.Add(new Direction(startingPointRoute, finalPointRoute));
        }
    }

    class Train
    {
        private List<Wagon> _wagonsList = new();

        public Train()
        {
            AddWagon();
        }

        private void AddWagon()
        {
            const string CommandSendPeopleCompartmentСar = "Плацкарт";
            const string CommandSendPeopleSecondClassCar = "Купе";
            const string CommandSendNumberPeopleLuxCar = "Люкс";
            const string CommandExit = "Выход";

            int numberSeatsCompartmentСar = 64;
            int numberSeatsClassCar = 32;
            int numberSeatsLuxCar = 16;

            bool isWorking = true;

            CashRegister cashRegister = new CashRegister();

            cashRegister.SellTickets();

            Console.WriteLine($"\n{CommandSendPeopleCompartmentСar} - 52 места" + $"\n{CommandSendPeopleSecondClassCar} - 32 места" +
                $"\n{CommandSendNumberPeopleLuxCar} - 16 мест" + $"\nВыйти в главное меню - {CommandExit}");

            Console.WriteLine($"\nУ вас купили билет - {cashRegister.NumberPassengers} пассажиров.");

            while (isWorking)
            {
                Console.Write("\nВ какой класс вагонов вы хотите посадить пассажиров - ");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == CommandSendPeopleCompartmentСar.ToLower())
                {
                    double numberPeopleCompartmentСar = cashRegister.NumberPassengers / numberSeatsCompartmentСar;
                    numberPeopleCompartmentСar = Math.Ceiling(numberPeopleCompartmentСar);

                    Console.WriteLine("Создан поезд из - " + numberPeopleCompartmentСar + " вагонов.");

                    for (int i = 0; i < numberPeopleCompartmentСar; i++)
                    {
                        _wagonsList.Add(new Wagon(numberPeopleCompartmentСar, 0, 0));
                    }

                    return;
                }
                else if (userInput.ToLower() == CommandSendPeopleSecondClassCar.ToLower())
                {
                    double numberPeopleSecondClassCar = cashRegister.NumberPassengers / numberSeatsClassCar;
                    numberPeopleSecondClassCar = Math.Ceiling(numberPeopleSecondClassCar);

                    Console.WriteLine("Создан поезд из - " + numberPeopleSecondClassCar + " вагонов.");

                    for (int i = 0; i < numberPeopleSecondClassCar; i++)
                    {
                        _wagonsList.Add(new Wagon(0, numberPeopleSecondClassCar, 0));
                    }

                    return;
                }
                else if (userInput.ToLower() == CommandSendNumberPeopleLuxCar.ToLower())
                {
                    double numberPeopleLuxCar = cashRegister.NumberPassengers / numberSeatsLuxCar;
                    numberPeopleLuxCar = Math.Ceiling(numberPeopleLuxCar);

                    Console.WriteLine("Создан поезд из - " + numberPeopleLuxCar + " вагонов.");

                    for (int i = 0; i < numberPeopleLuxCar; i++)
                    {
                        _wagonsList.Add(new Wagon(0, 0, numberPeopleLuxCar));
                    }

                    return;
                }
                else if (userInput.ToLower() == CommandExit.ToLower())
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Ошибка. Попробуйте ещё раз.");
                }
            }
        }
    }

    class CashRegister
    {
        public int NumberPassengers { get; private set; }

        public void SellTickets()
        {
            Random random = new Random();
            NumberPassengers = random.Next(510, 511);
        }
    }

    class Wagon
    {
        public Wagon(double numberSeatsCompartmentСar, double numberSeatsClassCar, double numberSeatsLuxCar)
        {
            CompartmentСar = numberSeatsCompartmentСar;
            SecondClassCar = numberSeatsClassCar;
            LuxCar = numberSeatsLuxCar;
        }

        public double CompartmentСar { get; private set; }

        public double SecondClassCar { get; private set; }

        public double LuxCar { get; private set; }
    }

    class Direction
    {
        public Direction(string startingPointRoute, string finalPointRoute)
        {
            DeparturePoint = startingPointRoute;
            PlaceArrival = finalPointRoute;
        }

        public string DeparturePoint { get; private set; }

        public string PlaceArrival { get; private set; }
    }
}
