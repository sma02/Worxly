using System;
using System.Threading.Tasks;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Tiling;
using Mapsui.Utilities;
using ReactiveUI;
using Worxly.DTOs;

namespace Worxly.Helpers
{
    public class MapHelper : ReactiveObject
    {
        private Map _map;
        private MyLocationLayer? _myLocationLayer;
        private int _pointIndex = 0;
        private (MPoint, double, double, double, bool, bool, bool)[] _points = Array.Empty<(MPoint, double, double, double, bool, bool, bool)>();

        public Map Map
        {
            get => _map;
            private set => this.RaiseAndSetIfChanged(ref _map, value);
        }

        public MapHelper(int lon, int lan)
        {
            InitializeMapAsync(lon, lan);
        }

        private async Task InitializeMapAsync(int lon, int lan)
        {
            var map = new Map();

            // Initialize MyLocationLayer
            _myLocationLayer?.Dispose();
            _myLocationLayer = new MyLocationLayer(map)
            {
                IsCentered = false
            };

            // Add layers to the map
            map.Layers.Add(OpenStreetMap.CreateTileLayer());
            map.Layers.Add(_myLocationLayer);

            // Set map center and zoom
            var centerOfLocation = new MPoint(lan, lon); // Replace with desired coordinates
            var sphericalMercatorCoordinate = SphericalMercator
                .FromLonLat(centerOfLocation.X, centerOfLocation.Y)
                .ToMPoint();

            map.Home = n => n.CenterOnAndZoomTo(sphericalMercatorCoordinate, n.Resolutions[9]);
            _myLocationLayer.UpdateMyLocation(sphericalMercatorCoordinate, true);

            // Generate sample points for simulation
            _points = CreateSamplePoints(centerOfLocation);

            // Subscribe to map interaction (e.g., click events)
            map.Info += OnMapInfoEvent;

            Map = map;

            await Task.CompletedTask; // Simulating async work
        }

        private void OnMapInfoEvent(object? sender, MapInfoEventArgs e)
        {
            if (_pointIndex >= _points.Length)
                _pointIndex = 0;

            var currentPoint = _points[_pointIndex];
            _myLocationLayer!.IsCentered = currentPoint.Item5;
            _myLocationLayer.IsMoving = currentPoint.Item6;
            _myLocationLayer.UpdateMyLocation(currentPoint.Item1, currentPoint.Item7);
            _myLocationLayer.UpdateMyDirection(currentPoint.Item2, Map.Navigator.Viewport.Rotation, currentPoint.Item7);
            _myLocationLayer.UpdateMyViewDirection(currentPoint.Item3, Map.Navigator.Viewport.Rotation, currentPoint.Item7);
            _myLocationLayer.UpdateMySpeed(currentPoint.Item4);

            _pointIndex++;
        }

        private (MPoint, double, double, double, bool, bool, bool)[] CreateSamplePoints(MPoint center)
        {
            var result = new (MPoint, double, double, double, bool, bool, bool)[20];
            var rand = new Random();

            for (var i = 0; i < result.Length; i++)
            {
                result[i].Item1 = SphericalMercator.FromLonLat(center.X + rand.NextDouble() * 0.5, center.Y + rand.NextDouble() * 0.5).ToMPoint();
                result[i].Item2 = rand.NextDouble() * 360.0;
                result[i].Item3 = rand.NextDouble() * 360.0;
                result[i].Item4 = rand.NextDouble() > 0.5 ? 1.0 : 0.0;
                result[i].Item5 = rand.NextDouble() > 0.5;
                result[i].Item6 = rand.NextDouble() > 0.5;
                result[i].Item7 = rand.NextDouble() > 0.5;
            }

            return result;
        }
    }
}
