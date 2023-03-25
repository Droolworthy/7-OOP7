using System;

namespace OOP7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandSendTrain = "1";
            const string CommandExit = "2";

            Dispatcher dispatcher = new Dispatcher();

            bool isWorking = true;

            Console.WriteLine($"{CommandSendTrain} - ОТПРАВИТЬ ПОЕЗД" + $"\n{CommandExit} - ВЫХОД");

            while (isWorking)
            {
                Console.Write("\nВведите команду: ");
                string userInput = Console.ReadLine();

                if (userInput == CommandSendTrain)
                {
                    dispatcher.AddTrain();

                    dispatcher.ShowInfoSendTrain();

                    dispatcher.RemoveTrain();
                }
                else if (userInput == CommandExit)
                {
                    isWorking = false;
                }
                else
                {
                    Console.WriteLine($"Ошибка. Введите {CommandSendTrain} или {CommandExit}"); 
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
            int userInput = 0;
            _trainList.RemoveAt(userInput);
            _listDirectionTrain.RemoveAt(userInput);
        }

        public void ShowInfoSendTrain()
        {
            for (int i = 0; i < _listDirectionTrain.Count; i++)
            {
                Console.WriteLine("Внимание! Пассажиры! Двери закрываются! Поезд отправляется от станции - " 
                    + _listDirectionTrain[i].DeparturePoint + ". Следующая станция город - " + _listDirectionTrain[i].PlaceArrival);
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("\nПоезд едет.");
                Thread.Sleep(1000);
            }

            for (int i = 0; i < _listDirectionTrain.Count; i++)
            {
                Console.WriteLine("\nПоздравляю! Вы приехали в город - " + _listDirectionTrain[i].PlaceArrival);
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

        private void AddWagon()
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

            for (int i = 0; i < seatingСapacityWagon.Count; i++)
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

                    Console.WriteLine($"\n{cashRegister.NumberPassengers} пассажиров сели в " + numberWagons + " вагонов.");
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
            NumberPassengers = random.Next(200, 800);
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
