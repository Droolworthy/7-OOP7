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
            new CompartmentСar(52),
            new SecondClassCar(32),
            new LuxCar(16)
        }; 

        public Train() { AddWagon(); }

        public void AddWagon()
        {
            CashRegister cashRegister = new CashRegister();

            cashRegister.SellTickets();

            Console.WriteLine("Виды вагонов: " + $"Плацкарт - {_seatingСapacityWagon[0].PassengerСar} места, " 
                + $"Купе - {_seatingСapacityWagon[0].PassengerСar}, " + $"Люкс - {_seatingСapacityWagon[0].PassengerСar} мест");

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
                for (int i = 0; i < _seatingСapacityWagon.Count; i++)
                {
                    if (trainNumber == i)
                    {
                        double numberPeopleCompartmentСar = cashRegister.NumberPassengers / _seatingСapacityWagon[i].PassengerСar;
                        CreateWagon(numberPeopleCompartmentСar);
                    }
                    else if (trainNumber == i)
                    {
                        double numberPeopleSecondClassCar = cashRegister.NumberPassengers / _seatingСapacityWagon[i].PassengerСar;
                        CreateWagon(numberPeopleSecondClassCar);
                    }
                    else if (trainNumber == i)
                    {
                        double numberPeopleLuxCar = cashRegister.NumberPassengers / _seatingСapacityWagon[i].PassengerСar;
                        CreateWagon(numberPeopleLuxCar);
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

        private void CreateWagon(double numberPeopleCar)
        {
            numberPeopleCar = Math.Ceiling(numberPeopleCar);

            for (int i = 0; i < numberPeopleCar; i++)
            {
                _wagonsList.Add(new Wagon(numberPeopleCar));
            }

            Console.WriteLine("Создан поезд из - " + numberPeopleCar + " вагонов.");
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
        public double NumberPassengers { get; private set; }

        public void SellTickets()
        {
            Random random = new Random();
            NumberPassengers = random.Next(500, 1000);
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
