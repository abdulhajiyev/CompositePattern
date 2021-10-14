using System;
using System.Collections.Generic;

namespace CompositePattern
{
    public static class Program
    {
        static void Main(string[] args)
        {
            ImageEditor image = new ImageEditor();
            image.Load();
            image.GroupSelected(image.CompoundGraphic._childrens);
        }
    }

    public interface IGraphic
    {
        void Move(int x, int y);
        void Draw();
    }

    public class Dot : IGraphic
    {
        private int X { get; set; }
        private int Y { get; set; }

        public Dot(int x, int y)
        {
            X = x;
            Y = y;
        }

        public virtual void Draw() => Console.WriteLine("Dot X Y");

        public virtual void Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Circle : Dot
    {
        public double Radius { get; set; }

        public Circle(int x, int y, int radius) : base(x, y)
        {
            Radius = radius;
        }

        public override void Draw() => Console.WriteLine("Circle X Y Radius");
    }

    public class CompoundGraphic : IGraphic
    {
        public List<IGraphic> _childrens = new();

        public void Add(IGraphic child)
        {
            _childrens.Add(child);
        }

        public void Remove(IGraphic child)
        {
            _childrens.Remove(child);
        }

        public void Move(int x, int y)
        {
            foreach (var child in _childrens)
                child.Move(x, y);
        }

        public void Draw()
        {
            foreach (var child in _childrens)
                child.Draw();
        }
    }

    public class ImageEditor
    {
        public CompoundGraphic CompoundGraphic { get; set; }

        public void Load()
        {
            CompoundGraphic = new CompoundGraphic();
            CompoundGraphic.Add(new Circle(2, 5, 9));
            CompoundGraphic.Add(new Dot(4, 9));
        }

        public void GroupSelected(List<IGraphic> graphics)
        {
            var group = new CompoundGraphic();
            foreach (var g in graphics.ToArray())
            {
                group.Add(g);
                CompoundGraphic.Remove(g);
            }
            CompoundGraphic.Add(group);
            CompoundGraphic.Draw();
        }
    }
}
