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

        public Train() { AddWagon(); }

        public void AddWagon()
        {
            List<Wagon> seatingСapacityWagon = new List<Wagon>();

            CashRegister cashRegister = new CashRegister();           

            cashRegister.SellTickets();

            double сompartmentСar = 50;
            double secondClassCar = 25;
            double luxCar = 10;

            seatingСapacityWagon.Add(new CompartmentСar(сompartmentСar));
            seatingСapacityWagon.Add(new SecondClassCar(secondClassCar));
            seatingСapacityWagon.Add(new LuxCar(luxCar));

            Console.WriteLine($"\nУ вас купили билет - {cashRegister.NumberPassengers} пассажиров.");

            for(int i = 0; i < seatingСapacityWagon.Count; i++)
            {
                Console.WriteLine(i + " - номер вагона, " + seatingСapacityWagon[i].PassengerСar + " пассажиров");
            }

            Console.Write("\nВыберите номер вагона, куда посадить пассажиров - ");
            string userInput = Console.ReadLine();

            bool isSuccess = int.TryParse(userInput, out int trainNumber);

            if (isSuccess)
            {
                CreateWagon(seatingСapacityWagon, cashRegister, trainNumber);
            }
            else
            {
                Console.WriteLine("Ошибка. Попробуйте ещё раз.");
            }
        }

        private void CreateWagon(List<Wagon> seatingСapacityWagon, CashRegister cashRegister, int trainNumber)
        {
            for (double numberWagons = 0; numberWagons < seatingСapacityWagon.Count; numberWagons++)
            {
                if (trainNumber == numberWagons)
                {
                    numberWagons = cashRegister.NumberPassengers / seatingСapacityWagon[(int)numberWagons].PassengerСar;

                    numberWagons = Math.Ceiling(numberWagons);

                    for (int i = 0; i < numberWagons; i++)
                    {
                        _wagonsList.Add(new Wagon(numberWagons));
                    }

                    Console.WriteLine("Создан поезд из - " + numberWagons + " вагонов.");
                }

            }
        }
    }

    class CompartmentСar : Wagon
    {
        public CompartmentСar(double numberSeatsСar) : base(numberSeatsСar) { }
    }

    class SecondClassCar : Wagon
    {
        public SecondClassCar(double numberSeatsСar) : base(numberSeatsСar) { }
    }

    class LuxCar : Wagon
    {
        public LuxCar(double numberSeatsСar) : base(numberSeatsСar) { }
    }

    class Wagon
    {
        public Wagon(double numberSeatsСar)
        {
            PassengerСar = numberSeatsСar;
        }

        public double PassengerСar { get; private set; }
    }

    class CashRegister
    {
        public int NumberPassengers { get; private set; }

        public void SellTickets()
        {
            Random random = new Random();
            NumberPassengers = random.Next(103, 104);
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
