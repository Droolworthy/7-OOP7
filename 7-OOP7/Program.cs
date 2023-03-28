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
            string startingPointRoute = "";
            string finalPointRoute = "";

            AddDirection(ref startingPointRoute, ref finalPointRoute);

            Train train = new Train();

            train.AddWagon();

            if (train.GetWagonsCount() > 0)
            {
                _trainList.Add(train);
                _listDirectionTrain.Add(new Direction(startingPointRoute, finalPointRoute));
            }
            else
            {
                Console.WriteLine("Ошибка. Такого вагона нет в списке. Попробуйте ещё раз.");
            }            
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

            for (int i = 0; i < _listDirectionTrain.Count; i++)
            {
                Console.WriteLine("\nПоздравляю! Вы приехали в город - " + _listDirectionTrain[i].PlaceArrival);

                RemoveTrain();
            }           
        }

        private void AddDirection(ref string startingPointRoute, ref string finalPointRoute)
        {
            Console.Write("\nСоздайте точку отправления - ");
            startingPointRoute = Console.ReadLine();

            Console.Write("Создайте точку прибытия - ");
            finalPointRoute = Console.ReadLine();           
        }
    }

    class Train
    {
        private List<Wagon> _wagonsList = new();

        public int GetWagonsCount()
        {
            return _wagonsList.Count; 
        }

        public void AddWagon()
        {
            List<Wagon> seatingСapacityWagon = new List<Wagon>();

            CashRegister cashRegister = new CashRegister();

            cashRegister.SellTickets();

            int сompartmentСar = 50;
            int secondClassCar = 25;
            int luxCar = 10;

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
                for (int numberWagon = 0; numberWagon < seatingСapacityWagon.Count; numberWagon++)
                {
                    СreateWagons(seatingСapacityWagon, cashRegister, trainNumber, numberWagon);
                }

                for (int numberWagon = 0; numberWagon < seatingСapacityWagon.Count; numberWagon++)
                {
                    CreateAdditionalWagon(seatingСapacityWagon, cashRegister, trainNumber, numberWagon);
                }                            
            }
            else
            {
                Console.WriteLine("Ошибка. Попробуйте ещё раз.");
            }
        }      

        private void СreateWagons(List<Wagon> seatingСapacityWagon, CashRegister cashRegister, int trainNumber, int numberWagon)
        {
            if (trainNumber == numberWagon)
            {
                numberWagon = cashRegister.NumberPassengers / seatingСapacityWagon[numberWagon].PassengerСar;

                for (int i = 0; i < numberWagon; i++)
                {
                    _wagonsList.Add(new Wagon(numberWagon));
                }
            }
        }

        private void CreateAdditionalWagon(List<Wagon> seatingСapacityWagon, CashRegister cashRegister, int trainNumber, int numberWagon)
        {
            if (trainNumber == numberWagon)
            {
                numberWagon = cashRegister.NumberPassengers % seatingСapacityWagon[numberWagon].PassengerСar;

                _wagonsList.Add(new Wagon(numberWagon));

                Console.WriteLine($"\n{cashRegister.NumberPassengers} пассажиров сели в " + _wagonsList.Count + " вагонов.");
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
            NumberPassengers = random.Next(200, 500);
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
