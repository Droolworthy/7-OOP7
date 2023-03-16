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
            Wagon wagonCompartmentСar = new(52d, "Плацкарт");
            Wagon wagonSecondClassCar = new(32f, "Купе");
            Wagon wagonLuxCar = new(16, "Люкс");

            CashRegister cashRegister = new CashRegister();

            cashRegister.SellTickets();

            Console.WriteLine($"\nУ вас купили билет - {cashRegister.NumberPassengers} пассажиров.");            
            Console.WriteLine(wagonCompartmentСar.Name + " - " + wagonCompartmentСar.CompartmentСar + " места.");
            Console.WriteLine(wagonSecondClassCar.Name + " - " + wagonSecondClassCar.SecondClassCar + " места.");
            Console.WriteLine(wagonLuxCar.Name + " - " + wagonLuxCar.LuxCar + " мест.");

            Console.Write("\nВ какой класс вагонов вы хотите посадить пассажиров - ");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == wagonCompartmentСar.Name.ToLower())
            {
                double numberPeopleCompartmentСar = cashRegister.NumberPassengers / wagonCompartmentСar.CompartmentСar;
                CreateWagon(numberPeopleCompartmentСar);
            }
            else if (userInput.ToLower() == wagonSecondClassCar.Name.ToLower())
            {
                double numberPeopleSecondClassCar = cashRegister.NumberPassengers / wagonSecondClassCar.SecondClassCar;
                CreateWagon(numberPeopleSecondClassCar);
            }
            else if (userInput.ToLower() == wagonLuxCar.Name.ToLower())
            {
                double numberPeopleLuxCar = cashRegister.NumberPassengers / wagonLuxCar.LuxCar;
                CreateWagon(numberPeopleLuxCar);
            }
            else
            {
                Console.WriteLine("Ошибка. Попробуйте ещё раз.");
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

    class CashRegister
    {
        public int NumberPassengers { get; private set; }

        public void SellTickets()
        {
            Random random = new Random();
            NumberPassengers = random.Next(500, 1000);
        }
    }

    class Wagon
    {
        public Wagon(double numberSeatsCompartmentСar, string nameCompartmentСar)
        {
            CompartmentСar = numberSeatsCompartmentСar;
            Name = nameCompartmentСar;
        }

        public Wagon(float numberSeatsClassCar, string nameSeatsClassCar)
        {
            SecondClassCar = numberSeatsClassCar;
            Name = nameSeatsClassCar;
        }

        public Wagon(int numberSeatsLuxCar, string nameSeatsLuxCar)
        {
            LuxCar = numberSeatsLuxCar;
            Name = nameSeatsLuxCar;
        }

        public Wagon(double numberSeatsСar)
        {
            PassengerСar = numberSeatsСar;
        }

        public double PassengerСar { get; private set; }

        public double CompartmentСar { get; private set; }

        public float SecondClassCar { get; private set; }

        public int LuxCar { get; private set; }

        public string Name { get; private set; }
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
