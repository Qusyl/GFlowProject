using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace GFlowApp.Services
{
    public class NodeConnection : Path
    {
        public static readonly StyledProperty<Point> StartPointProperty =
        AvaloniaProperty.Register<NodeConnection, Point>(nameof(StartPoint));

        public static readonly StyledProperty<Point> EndPointProperty =
        AvaloniaProperty.Register<NodeConnection, Point>(nameof(EndPoint));

        public Point StartPoint
        {
            get => GetValue(StartPointProperty);
            set => SetValue(StartPointProperty, value);
        }
        public Point EndPoint
        {
            get => GetValue(EndPointProperty);
            set => SetValue(EndPointProperty, value);
        }

        static NodeConnection()
        {
            StartPointProperty.Changed.AddClassHandler<NodeConnection>((x, _) => x.UpdateGeometry());
            EndPointProperty.Changed.AddClassHandler<NodeConnection>((x, _) => x.UpdateGeometry());
        }

        private void UpdateGeometry()
{
    var start = StartPoint;
    var end = EndPoint;

    var deltaX = end.X - start.X;

    var offset = Math.Max(Math.Abs(deltaX) * 0.5, 50);

    var direction = deltaX >= 0 ? 1 : -1;

    var control1 = new Point(
        start.X + offset * direction,
        start.Y
    );

    var control2 = new Point(
        end.X - offset * direction,
        end.Y
    );

    var geometry = new PathGeometry();

    var figure = new PathFigure
    {
        StartPoint = start,
        IsClosed = false
    };

    figure.Segments.Add(
        new BezierSegment
        {
            Point1 = control1,
            Point2 = control2,
            Point3 = end
        });

    geometry.Figures!.Add(figure);

    Data = geometry;
}
    }
}