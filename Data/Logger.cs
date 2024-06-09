using System.Collections.Concurrent;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;


namespace Data
{
    

    internal class Logger
    {
        private class SerializableBall
        {
            public int BallID { get; }
            public float X { get; }
            public float Y { get; }
            public float vX { get; }
            public float vY { get; }
            public DateTime Time { get; }

            public SerializableBall(int id, Vector2 position, Vector2 velocity, DateTime time)
            {
                BallID = id;
                X = position.X;
                Y = position.Y;
                vX = velocity.X;
                vY = velocity.Y;
                Time = time;
            }

        }


        private BlockingCollection<SerializableBall> queue;
        private static Logger? logger = null;
        private static readonly object overflowLock =  new object();
        private static readonly object instanceLock = new object();
        private bool overflow = false;


        private Logger()
        {
            queue = new BlockingCollection<SerializableBall>(new ConcurrentQueue<SerializableBall>(), 100);
            Task.Run(() =>
                {
                    Log();
            });
        }


        public static Logger CreateInstance()
        {
            lock (instanceLock)
            {
                if (logger != null)
                {
                    return logger;
                }
                else
                {
                    logger = new Logger();
                    return logger;

                }
            }
        }

        public void addToBuffer(DataAPI ball, DateTime time)
        {
            Task.Run(() =>
            {
                bool isAdded = queue.TryAdd(new SerializableBall(ball.ID, ball.Position, ball.Velocity, time));
                if (isAdded) { return; }
                lock (overflowLock)
                {
                    overflow = true;
                }
            });
        }

        private async void Log()
        {
            
                await using StreamWriter streamWriter = new ("logs.json", false, Encoding.UTF8);
                while (!queue.IsCompleted)
                {
                    bool overflowDetected = false;

                    lock (overflowLock)
                    {
                        if (overflow)
                        {
                            overflowDetected = true;
                            overflow = false;
                        }
                    }

                    if (overflowDetected) await streamWriter.WriteLineAsync("Overflow detected");

                    SerializableBall ball = queue.Take();
                    string jsonString = JsonConvert.SerializeObject(ball);
                    await streamWriter.WriteLineAsync(jsonString);
                    await streamWriter.FlushAsync();

                }
            
        }



        
    }

   
}
