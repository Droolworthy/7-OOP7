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

            Console.WriteLine($"\n{CommandCreateTrain} - СФОРМИРОВАТЬ ПОЕЗД" + $"\n{CommandSendTrain} - ОТПРАВИТЬ ПОЕЗД" +
                $"\n{CommandShowInformation} - ПОСМОТРЕТЬ ИНФОРМАЦИЮ О ПОЕЗДАХ" + $"\n{CommandExit} - ВЫХОД");

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
        List<Train> _trainList = new List<Train>();
        List<Direction> _listDirectionTrain = new List<Direction>();

        public void AddTrain()
        {
            AddDirection();

            Train train = new();

            _trainList.Add(train);

            Console.WriteLine("Количество поездов готовых к поездке - " + _trainList.Count);
        }

        public void RemoveTrain()
        {
            Console.Write("Введите номер поезда для отправки в путь - ");
            string userInput = Console.ReadLine();

            bool isSuccess = int.TryParse(userInput, out int serialNumber);

            if (isSuccess)
            {
                for (int i = 0; i < _trainList.Count; i++)
                {
                    if (serialNumber == i)
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
        List<Wagon> _wagonsList = new();

        public Train()
        {
            AddWagon();
        }

        public void AddWagon()
        {
            Wagon wagon = new Wagon();
            CashRegister cashRegister = new CashRegister();

            cashRegister.SellTickets();

            Console.WriteLine($"\nУ вас купили билет - {cashRegister.NumberPassengers} пассажиров.");

            int numberPeopleCompartmentСar = cashRegister.NumberPassengers / wagon.CompartmentСar;
            int remainingPeopleCompartmentСar = cashRegister.NumberPassengers % wagon.CompartmentСar;

            int numberPeopleSecondClassCar = remainingPeopleCompartmentСar / wagon.SecondClassCar;
            int remainingPeopleSecondClassCar = remainingPeopleCompartmentСar % wagon.SecondClassCar;

            int numberPeopleLuxCar = remainingPeopleSecondClassCar / wagon.LuxCar;
            int remainingPeopleLuxCar = remainingPeopleSecondClassCar % wagon.LuxCar;

            int numberAllWagons = numberPeopleCompartmentСar + numberPeopleSecondClassCar + numberPeopleLuxCar;

            for (int i = 0; i < numberAllWagons; i++)
            {
                _wagonsList.Add(new Wagon(numberPeopleCompartmentСar, numberPeopleSecondClassCar, numberPeopleLuxCar));
            }

            _wagonsList.Add(new Wagon(remainingPeopleLuxCar));

            Console.WriteLine("\nБольшой вагон - " + numberPeopleCompartmentСar);
            Console.WriteLine("Средний вагон - " + numberPeopleSecondClassCar);
            Console.WriteLine("Малый вагон - " + numberPeopleLuxCar);
            Console.WriteLine("Последний вагон сели - " + remainingPeopleLuxCar + " человек");
            Console.WriteLine("Всего вагонов - " + _wagonsList.Count);
        }
    }

    class CashRegister
    {
        public int NumberPassengers { get; private set; }

        public void SellTickets()
        {
            Random random = new Random();
            NumberPassengers = random.Next(1000, 2000);
        }
    }

    class Wagon
    {
        public Wagon(int numberSeatsCompartmentСar = 64, int numberSeatsClassCar = 32, int numberSeatsLuxCar = 16)
        {
            CompartmentСar = numberSeatsCompartmentСar;
            SecondClassCar = numberSeatsClassCar;
            LuxCar = numberSeatsLuxCar;
        }

        public int CompartmentСar { get; private set; }

        public int SecondClassCar { get; private set; }

        public int LuxCar { get; private set; }
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