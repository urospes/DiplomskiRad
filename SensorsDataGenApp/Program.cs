Console.WriteLine("Data generation application is running...");

if (args.Length == 0)
{
    Console.WriteLine("No arguments for car id supplied...");
    return;
}

int NUM_CARS = args.Length;
const int INTERVAL_MS = 500;

Task[] cars = new Task[NUM_CARS];
for(int i = 0; i < NUM_CARS; i++)
{
    string carId = args[i];
    int DELAY = INTERVAL_MS / 2 * i;
    cars[i] = Task.Run(async () => await DataGen.StartDataGen(carId, INTERVAL_MS + DELAY));
}
Task.WaitAll(cars);

Console.WriteLine("Data generation is completed. Please restart...");