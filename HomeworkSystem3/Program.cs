using System.Text;

namespace HomeworkSystem3
{

    class BankAccount
    {
        private decimal balance;
        private readonly object lockObject = new object();

        public BankAccount(decimal initialBalance)
        {
            balance = initialBalance;
        }

        public void Withdraw(decimal amount)
        {
            bool lockTaken = false;
            try
            {
                Monitor.Enter(lockObject, ref lockTaken);
                if (balance >= amount)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: Знімаємо {amount:C}. Баланс до: {balance:C}");
                    balance -= amount;
                    Console.WriteLine($"{Thread.CurrentThread.Name}: Успішно знято. Баланс після: {balance:C}");
                }
                else
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: Недостатньо коштів для зняття {amount:C}. Баланс: {balance:C}");
                }
            }
            finally
            {
                if (lockTaken) Monitor.Exit(lockObject);
            }
        }
    }

    internal class Program
    {
        static int totalSum = 0; 
        static int[] array;
        static int visitorCount = 0;

        static void Main(string[] args)
        {
            Console.InputEncoding = UTF8Encoding.UTF8;
            Console.OutputEncoding = UTF8Encoding.UTF8;

            //Task 1
            /*BankAccount account = new BankAccount(500);
            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    account.Withdraw(200);
                })
                {
                    Name = $"Потік {i + 1}"
                };
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }*/

            //Task 2
            /*int arraySize = 1_000_000; 
            int threadCount = 4;
            array = new int[arraySize];

            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 101); 
            }

            Thread[] threads = new Thread[threadCount];
            int chunkSize = array.Length / threadCount;

            for (int i = 0; i < threadCount; i++)
            {
                int start = i * chunkSize;
                int end = (i == threadCount - 1) ? array.Length : start + chunkSize;

                threads[i] = new Thread(() => SumArrayChunk(start, end));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"Загальна сума елементів масиву: {totalSum}");*/

            //Task 3
            /*int threadCount = 10;
            int incrementsPerThread = 1000; 
            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < incrementsPerThread; j++)
                    {
                        Interlocked.Increment(ref visitorCount);
                    }
                });
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine($"Кінцеве значення лічильника відвідувачів: {visitorCount}");*/
        }

        static void SumArrayChunk(int start, int end)
        {
            int partialSum = 0;

            for (int i = start; i < end; i++)
            {
                partialSum += array[i];
            }

            Interlocked.Add(ref totalSum, partialSum);
        }
    }
}
