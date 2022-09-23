Console.WriteLine("Data generation application is running...");

const int NUM_CARS = 2;

Task[] cars = new Task[NUM_CARS];
for(int i = 0; i < NUM_CARS; i++)
{
    string carId = i.ToString();
    cars[i] = Task.Run(async () => await DataGen.StartDataGen(carId));
}
Task.WaitAll(cars);

Console.WriteLine("Data generation is completed. Please restart...");