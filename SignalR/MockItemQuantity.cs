using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNet.SignalR;

namespace SignalR
{
    public class MockItemQuantity
    {
        private readonly IHubContext _hubContext;
        private Timer _timer;
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(5);
        private MockData _data = new MockData();
        private Random _random = new Random();
        private object _lock = new object();

        public MockItemQuantity(IHubContext hubContext)
        {
#if DEBUG
            Debug.WriteLine("mock ctor");
#endif
            _hubContext = hubContext;
            _timer = new Timer(UpdateQuantity, null, _updateInterval, _updateInterval);

        }

        public MockData Data
        {
            get { return _data; }
        }

        private void UpdateQuantity(object state)
        {
            var updated = false;
            lock (_lock)
            {
                var now = string.Format("{0}:00", DateTime.Now.Hour.ToString("D2"));
                if (_data.Labels[6] != now)
                {
                    for (var i = 0; i < 6; i++)
                    {
                        _data.Labels[i] = _data.Labels[i + 1];
                        for (var j = 0; j < 6; j++)
                        {
                            _data.Data[i][j] = _data.Data[i][j + 1];
                        }
                    }
                    _data.Labels[6] = now;
                    for (var j = 0; j < 7; j++)
                    {
                        _data.Data[j][6] = 0;
                    }
                    updated = true;
                }
                _data.Total = 0;
                for (var i = 0; i < 7; i++)
                {
                    _data.Total += _data.Data[i][6];
                    if (!(_random.NextDouble() < 0.1)) continue;
                    _data.Data[i][6]++;
                    _data.Total++;
                    updated = true;
                }
                if (updated)
                {
                    _hubContext.Clients.All.updateBrandQuantity(_data);
                }
            }
        }
    }

    public class MockData
    {
        public string[] Labels { get; set; }
        public string[] Series { get; set; }
        public int[][] Data { get; set; }
        public int Total { get; set; }
        private Random _random = new Random();

        public MockData()
        {
            Series = new[] { "Brand A", "Brand B", "Brand C", "Brand D", "Brand E", "Brand F", "Brand G" };
            Labels = new string[7];
            Data = new int[7][];
            for (var i = 0; i < 7; i++)
            {
                Labels[i] = string.Format("{0}:00", DateTime.Now.Subtract(TimeSpan.FromHours(6 - i)).Hour.ToString("D2"));
                Data[i] = new int[7];
                for (var j = 0; j < 7; j++)
                {
                    Data[i][j] = (int)(_random.NextDouble() * 100);
                }
                Data[i][6] *= (int)((double)DateTime.Now.Minute / 60);
            }
        }
    }
}