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
        private List<Wagon> _seatingСapacityWagon = new()
        {
            new CompartmentСar(50),
            new SecondClassCar(25),
            new LuxCar(10)
        }; 

        public Train() { AddWagon(); }

        public void AddWagon()
        {
            CashRegister cashRegister = new CashRegister();

            cashRegister.SellTickets();           

            for (int i = 0; i < _seatingСapacityWagon.Count; i++)
            {
                Console.WriteLine(i + " - номер вагона, " + _seatingСapacityWagon[i].PassengerСar + " места");
            }

            Console.WriteLine($"\nУ вас купили билет - {cashRegister.NumberPassengers} пассажиров.");

            Console.Write("\nВыберите номер вагона куда вы хотите посадить пассажиров - ");
            string userInput = Console.ReadLine();

            bool isSuccess = int.TryParse(userInput, out int trainNumber);

            if (isSuccess)
            {
                for (int numberWagons = 0; numberWagons < _seatingСapacityWagon.Count; numberWagons++)
                {
                    if (trainNumber == numberWagons)
                    {
                        numberWagons = cashRegister.NumberPassengers / _seatingСapacityWagon[numberWagons].PassengerСar;

                        for (int i = 0; i < numberWagons; i++)
                        {
                            _wagonsList.Add(new Wagon(numberWagons));
                        }

                        Console.WriteLine("Создан поезд из - " + numberWagons + " вагонов.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка. Даный вагон не найден в списке.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Ошибка. Неверный ввод.");
            }
        }
    }

    class CompartmentСar : Wagon
    {
        public CompartmentСar(int numberSeatsСar) : base(numberSeatsСar) { }
    }

    class SecondClassCar : Wagon
    {
        public SecondClassCar(int numberSeatsСar) : base(numberSeatsСar) { }
    }

    class LuxCar : Wagon
    {
        public LuxCar(int numberSeatsСar) : base(numberSeatsСar) { }
    }

    class Wagon
    {
        public Wagon(int numberSeatsСar)
        {
            PassengerСar = numberSeatsСar;
        }

        public int PassengerСar { get; private set; }
    }

    class CashRegister
    {
        public int NumberPassengers { get; private set; }

        public void SellTickets()
        {
            Random random = new Random();
            NumberPassengers = random.Next(100, 101);
        }
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
