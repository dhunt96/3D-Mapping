
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3D_Urban_Environment
{
    public partial class Form1 : Form
    {
        public class Vector
        {
            double x;
            double y;
            double z;
            public Vector(double X, double Y, double Z)
            {
                x = X;
                y = Y;
                z = Z;
            }
            public Vector()
            {
                x = 0;
                y = 0;
                z = 0;
            }
            public double X
            {
                get { return x; }
                set { x = value; }
            }
            public double Y
            {
                get { return y; }
                set { y = value; }
            }
            public double Z
            {
                get { return z; }
                set { z = value; }
            }
            public override string ToString()
            {
                return "<" + x.ToString("f3") + "," + y.ToString("f3") + "," + z.ToString("f3") + ">";
            }
        }

        public class Edge
        {
            Vector v1;
            Vector v2;
            Color color;
            //PointF v1p;
            //PointF v2p;
            public Edge(Vector Vertex1, Vector Vertex2, Color Color)
            {
                v1 = Vertex1;
                v2 = Vertex2;
                color = Color;
                //v1p = null;
                //v2p = null;
            }



            public Vector Vertex1
            {
                get { return v1; }
            }
            public Vector Vertex2
            {
                get { return v2; }
            }
            public Color Color
            {
                get { return color; }
            }


        }

        public class CarEdge
        {
            Vector v1;
            Vector v2;
            Color color;
            //PointF v1p;
            //PointF v2p;
            public CarEdge(Vector Vertex1, Vector Vertex2, Color Color)
            {
                v1 = Vertex1;
                v2 = Vertex2;
                color = Color;
                //v1p = null;
                //v2p = null;
            }



            public Vector Vertex1
            {
                get { return v1; }
            }
            public Vector Vertex2
            {
                get { return v2; }
            }
            public Color Color
            {
                get { return color; }
            }


        }

        public class Building
        {

            int bheight;
            double length;
            double width;
            double startx;
            double starty;

            public Building(double StartX, double StartY, double Length, double Width, int Bheight2)
            {
                bheight = Bheight2;
                startx = StartX;
                starty = StartY;
                length = Length;
                width = Width;

            }

            public double X
            {
                get { return startx; }

            }

            public double Y
            {
                get { return starty; }
            }

            public double Blength
            {
                get { return length; }
            }

            public double Bwidth
            {
                get { return width; }
            }

            public double Bheight
            {
                get { return bheight; }
            }
        }



        public class Car
        {

            float cheight;
            float clength;
            float cwidth;
            float cstartx;
            float cstarty;
            float cspeed;
            Color ccolor;
            int direction;

            public Car(float StartX, float StartY, float CLength, float CWidth, float CHeight, float CSpeed, Color CColor, int Direction)
            {
                cheight = CHeight;
                clength = CLength;
                cwidth = CWidth;
                cstartx = StartX;
                cstarty = StartY;
                cspeed = CSpeed;
                ccolor = CColor;

            }

            public float CarX
            {
                get { return cstartx; }
                set { cstartx = value; }

            }

            public float CarY
            {
                get { return cstarty; }
                set { cstarty = value; }
            }

            public float Clength
            {
                get { return clength; }
            }

            public float Cwidth
            {
                get { return cwidth; }
            }

            public float Cheight
            {
                get { return cheight; }
            }

            public float Cspeed
            {
                get { return cspeed; }
            }

            public Color Ccolor
            {
                get { return ccolor; }
            }

            public int CDir
            {
                get { return direction; }
                set { direction = value; }
            }
        }





        Vector E;
        Vector V;
        Vector CenterCartesianPoint;
        Vector CartesianXAxis;
        Vector CartesianYAxis;
        System.Collections.ArrayList Edges;
        System.Collections.ArrayList CarEdges;
        System.Collections.ArrayList Buildings;
        System.Collections.ArrayList Cars;
        System.Collections.ArrayList TempCars;
        double da;
        double a;
        double ScaleValue = .5;



        public Form1()
        {
            InitializeComponent();

            Edges = new System.Collections.ArrayList();
            CarEdges = new System.Collections.ArrayList();
            Cars = new System.Collections.ArrayList();
            TempCars = new System.Collections.ArrayList();
            Buildings = new System.Collections.ArrayList();



            // Cartesian grid in xy plane
            for (int x = -10; x <= 10; x++)
            {
                Edges.Add(new Edge(new Vector(x, -10, 0), new Vector(x, +10, 0), Color.Green));
            }
            for (int y = -10; y <= 10; y++)
            {
                Edges.Add(new Edge(new Vector(-10, y, 0), new Vector(+10, y, 0), Color.Green));
            }

            Buildings.Add(new Building(-1, -5, -5, -5, 5));
            Buildings.Add(new Building(-4, 5, 1, 1, 10));
            Buildings.Add(new Building(2, 4, 2, 2, 4));

            

            foreach (Building building in Buildings)
            {
                //// Building top
                Edges.Add(new Edge(new Vector(building.X, building.Y, building.Bheight), new Vector(building.X + building.Blength, building.Y, building.Bheight), Color.Red));
                Edges.Add(new Edge(new Vector(building.X, building.Y, building.Bheight), new Vector(building.X, building.Y + building.Bwidth, building.Bheight), Color.Red));
                Edges.Add(new Edge(new Vector(building.X + building.Blength, building.Y, building.Bheight), new Vector(building.X + building.Blength, building.Y + building.Bwidth, building.Bheight), Color.Red));
                Edges.Add(new Edge(new Vector(building.X, building.Y + building.Bwidth, building.Bheight), new Vector(building.X + building.Blength, building.Y + building.Bwidth, building.Bheight), Color.Red));
                // Building base
                Edges.Add(new Edge(new Vector(building.X, building.Y, 0), new Vector(building.X + building.Blength, building.Y, 0), Color.Red));
                Edges.Add(new Edge(new Vector(building.X, building.Y, 0), new Vector(building.X, building.Y + building.Bwidth, 0), Color.Red));
                Edges.Add(new Edge(new Vector(building.X + building.Blength, building.Y, 0), new Vector(building.X + building.Blength, building.Y + building.Bwidth, 0), Color.Red));
                Edges.Add(new Edge(new Vector(building.X, building.Y + building.Bwidth, 0), new Vector(building.X + building.Blength, building.Y + building.Bwidth, 0), Color.Red));
                //// Building sides
                Edges.Add(new Edge(new Vector(building.X, building.Y, building.Bheight), new Vector(building.X, building.Y, 0), Color.Red));
                Edges.Add(new Edge(new Vector(building.X, building.Y + building.Bwidth, 0), new Vector(building.X, building.Y + building.Bwidth, building.Bheight), Color.Red));
                Edges.Add(new Edge(new Vector(building.X + building.Blength, building.Y, building.Bheight), new Vector(building.X + building.Blength, building.Y, 0), Color.Red));
                Edges.Add(new Edge(new Vector(building.X, building.Y + building.Bwidth, building.Bheight), new Vector(building.X, building.Y + building.Bwidth, 0), Color.Red));
                Edges.Add(new Edge(new Vector(building.X + building.Blength, building.Y + building.Bwidth, building.Bheight), new Vector(building.X + building.Blength, building.Y + building.Bwidth, 0), Color.Red));

            }

            drawCars();


            // Primary axis
            Edges.Add(new Edge(new Vector(-10, 0, 0), new Vector(+10, 0, 0), Color.Lime));
            Edges.Add(new Edge(new Vector(0, -10, 0), new Vector(0, +10, 0), Color.Lime));

            //RenderView();
            da = 0.005;
            a = -1 * Math.PI / 2;
            timer1.Enabled = true;

        }



        private Vector Add(Vector A, Vector B)
        {
            Vector Result = new Vector();
            Result.X = A.X + B.X;
            Result.Y = A.Y + B.Y;
            Result.Z = A.Z + B.Z;
            return Result;
        }

        private double Dot(Vector A, Vector B)
        {
            return A.X * B.X + A.Y * B.Y + A.Z * B.Z;
        }

        private Vector Cross(Vector A, Vector B)
        {
            Vector Result = new Vector();
            Result.X = A.Y * B.Z - A.Z * B.Y;
            Result.Y = A.Z * B.X - A.X * B.Z;
            Result.Z = A.X * B.Y - A.Y * B.X;
            return Result;
        }

        private double Magnitude(Vector A)
        {
            return Math.Sqrt(A.X * A.X + A.Y * A.Y + A.Z * A.Z);
        }

        private Vector Normalize(Vector A)
        {
            Vector Result = new Vector();
            double m = Magnitude(A);
            Result.X = A.X / m;
            Result.Y = A.Y / m;
            Result.Z = A.Z / m;
            return Result;
        }

        private Vector Scale(Vector A, double S)
        {
            Vector Result = new Vector();
            Result.X = A.X * S;
            Result.Y = A.Y * S;
            Result.Z = A.Z * S;
            return Result;
        }


        /*
        private void RenderView()
        {
            // First building:
            //   Front Lower left corner: x=2, y=3, z=0
            //   Front Top left corner: x=2, y=3, z=6
            //Vector Origin = new Vector(0, 0, 0);
            //   Front Lower right corner: x = 5, y=3, z=0
            //   Front Top right corner: x=5,y=3,z=6
            Vector P = new Vector(2, 3, 0);
            // Known constant point : Eye = eye location
            E = new Vector(2, -20, 10);
            // Known constant direction : ViewVector = direction to view pane (and magnitude)
            V = new Vector(0, 15, -5);
            // https://en.wikipedia.org/wiki/Cross_product
            // compute vector for x axis on bitmap using cross product of ViewVector
            // and <0,0,+1>.
            Vector N = new Vector(0, 0, 1);
            CartesianXAxis = Cross(V, N);
            // Assume width of bitmap in cartesian units is 10 and height is 8.
            // Scale x axis vector as appropriate.
            listBox1.Items.Add(CartesianXAxis);
            listBox1.Items.Add(Magnitude(CartesianXAxis));
            CartesianXAxis = Normalize(CartesianXAxis);
            listBox1.Items.Add(CartesianXAxis);
            CartesianXAxis = Scale(CartesianXAxis, 5);
            listBox1.Items.Add(CartesianXAxis);
            listBox1.Items.Add("");
            // compute vector for y axis on bitmap using cross product of ViewVector
            // and x axis vector. Then scale y axis vector as appropriate.
            CartesianYAxis = Cross(CartesianXAxis, V);
            listBox1.Items.Add(CartesianYAxis);
            listBox1.Items.Add(Magnitude(CartesianYAxis));
            CartesianYAxis = Normalize(CartesianYAxis);
            listBox1.Items.Add(CartesianYAxis);
            CartesianYAxis = Scale(CartesianYAxis, 4);
            listBox1.Items.Add(CartesianYAxis);
            listBox1.Items.Add("");
            // Compute cartesian point corresponding to center of bitmap
            CenterCartesianPoint = Add(E, V);
            // For each vertice in 3D building objects:
            // https://en.wikipedia.org/wiki/Euclidean_vector#Addition_and_subtraction
            //   Compute line to Eye using 3D vertice point minus the Eye (result points
            //   from Eye to vertice).
            Vector EyeToPoint = Add(P, Scale(E, -1));
            listBox1.Items.Add("Origin:");
            listBox1.Items.Add(EyeToPoint);
            // https://en.wikipedia.org/wiki/Line%E2%80%93plane_intersection
            //   Compute the point of intersection between line and View Plane.
            //   Result is a 3D cartesian point.
            listBox1.Items.Add("DEBUG");
            listBox1.Items.Add(Dot(EyeToPoint, V));
            // What about divide by 0 in next statement?
            // Dot(EyeToPoint,V)==0 implies angle between is zero.
            double d = Dot(Add(CenterCartesianPoint, Scale(E, -1)), V) / Dot(EyeToPoint, V);
            // d==0 implies that plane and line are parallel.
            listBox1.Items.Add(d);
            Vector Intersection = Add(Scale(EyeToPoint, d), E);
            listBox1.Items.Add(Intersection);
            listBox1.Items.Add("");
            //https://en.wikipedia.org/wiki/Vector_projection
            // Convert from 3D cartesian point to pixel bitmap point using vector projection.
            // We need the angle between the two vectors (computed using the dot product).
            // We do this for each axis. The projections are in cartesian units which then
            // need to be converted to bitmap units (using constant pixels per unit).
            Vector VectorToIntersection = Add(Intersection, Scale(CenterCartesianPoint, -1));
            listBox1.Items.Add("Vector from eye to point of intersection");
            listBox1.Items.Add(VectorToIntersection);
            // Compute X Projection
            double RadiansX = Math.Acos(Dot(CartesianXAxis, VectorToIntersection) / (Magnitude(CartesianXAxis) * Magnitude(VectorToIntersection)));
            listBox1.Items.Add("Angle (in degrees) with x-axis:");
            listBox1.Items.Add(RadiansX * 180 / Math.PI);
            double XProjection = Magnitude(VectorToIntersection) * Math.Cos(RadiansX);
            listBox1.Items.Add("X axis projection");
            listBox1.Items.Add(XProjection);
            // Compute Y Projection
            listBox1.Items.Add("Numerator: " + Dot(CartesianYAxis, VectorToIntersection));
            listBox1.Items.Add("Denominator: " + Magnitude(CartesianYAxis) * Magnitude(VectorToIntersection));
            //listBox1.Items.Add(Dot(CartesianYAxis, VectorToIntersection) / (Magnitude(CartesianYAxis) * Magnitude(VectorToIntersection)));
            double Fraction = Dot(CartesianYAxis, VectorToIntersection) / (Magnitude(CartesianYAxis) * Magnitude(VectorToIntersection));
            if (Fraction < -1) Fraction = -1;
            if (Fraction > 1) Fraction = 1;
            listBox1.Items.Add(Fraction);
            double RadiansY = Math.Acos(Fraction);
            listBox1.Items.Add("Angle (in degrees) with y-axis:");
            listBox1.Items.Add(RadiansY * 180 / Math.PI);
            double YProjection = Magnitude(VectorToIntersection) * Math.Cos(RadiansY);
            listBox1.Items.Add("Y axis projection");
            listBox1.Items.Add(YProjection);
            // Assume bitmap is 500 pixels wide and 10 cartesian units.
            // Bitmap is 400 pixels high.
            // Then pixels per unit = 500/10 = 50. The center of the bitmap is 250,200.
            float PixelX = 250 + 50F * (float)XProjection;
            float PixelY = 200 - 50F * (float)YProjection;
            listBox1.Items.Add("");
            listBox1.Items.Add("Pixel Location");
            listBox1.Items.Add(PixelX.ToString("f1"));
            listBox1.Items.Add(PixelY.ToString("f1"));
            // Draw edges on bitmap
            Bitmap View = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(View);
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, View.Width, View.Height);
            //g.FillEllipse(new SolidBrush(Color.White), PixelX - 2, PixelY - 2, 4, 4);
            foreach (Edge edge in Edges)
            {
                PointF V1 = Project(edge.Vertex1);
                PointF V2 = Project(edge.Vertex2);
                g.DrawLine(new Pen(Color.Red), V1.X, V1.Y, V2.X, V2.Y);
            }
            // Display View
            pictureBox1.Image = View;
        }
        */


        private PointF Project(Vector point3D)
        {
            //listBox1.Items.Add("");
            //listBox1.Items.Add("New Point");
            //listBox1.Items.Add(point3D);
            Vector EyeToPoint = Add(point3D, Scale(E, -1));
            //listBox1.Items.Add(EyeToPoint);

            // https://en.wikipedia.org/wiki/Line%E2%80%93plane_intersection
            //   Compute the point of intersection between line and View Plane.
            //   Result is a 3D cartesian point.

            double d = Dot(Add(CenterCartesianPoint, Scale(E, -1)), V) / Dot(EyeToPoint, V);
            //listBox1.Items.Add(d);
            Vector Intersection = Add(Scale(EyeToPoint, d), E);
            //listBox1.Items.Add(Intersection);
            //listBox1.Items.Add("");

            //https://en.wikipedia.org/wiki/Vector_projection
            // Convert from 3D cartesian point to pixel bitmap point using vector projection.
            // We need the angle between the two vectors (computed using the dot product).
            // We do this for each axis. The projections are in cartesian units which then
            // need to be converted to bitmap units (using constant pixels per unit).
            Vector VectorToIntersection = Add(Intersection, Scale(CenterCartesianPoint, -1));
            //listBox1.Items.Add(VectorToIntersection);

            // Compute X Projection
            double RadiansX = Math.Acos(Dot(CartesianXAxis, VectorToIntersection) / (Magnitude(CartesianXAxis) * Magnitude(VectorToIntersection)));
            //listBox1.Items.Add(RadiansX * 180 / Math.PI);
            double XProjection = Magnitude(VectorToIntersection) * Math.Cos(RadiansX);
            //listBox1.Items.Add(XProjection);

            // Compute Y Projection
            double Fraction = Dot(CartesianYAxis, VectorToIntersection) / (Magnitude(CartesianYAxis) * Magnitude(VectorToIntersection));
            if (Fraction < -1) Fraction = -1;
            if (Fraction > 1) Fraction = 1;
            //listBox1.Items.Add(Fraction);
            double RadiansY = Math.Acos(Fraction);
            //double RadiansY = Math.Acos(Dot(CartesianYAxis, VectorToIntersection) / (Magnitude(CartesianYAxis) * Magnitude(VectorToIntersection)));
            //listBox1.Items.Add(RadiansY * 180 / Math.PI);
            double YProjection = Magnitude(VectorToIntersection) * Math.Cos(RadiansY);
            //listBox1.Items.Add(YProjection);

            // Assume bitmap is 500 pixels wide and 10 cartesian units.
            // Then pixels per unit = 500/10 = 50. The center of the bitmap is 250,200.
            float PixelX = 250 + 50F * (float)XProjection;
            float PixelY = 200 - 50F * (float)YProjection;
            return new PointF(PixelX, PixelY);
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            //a += da;
            double x = 100 * Math.Cos(a);
            double y = 100 * Math.Sin(a);

      

            
            E = new Vector(x, y, 50);
            V = Add(new Vector(0, 0, 0), Scale(E, -1));
            V = Scale(V, ScaleValue);
            Vector N = new Vector(0, 0, 1);

            CartesianXAxis = Cross(V, N);
            CartesianXAxis = Normalize(CartesianXAxis);
            CartesianXAxis = Scale(CartesianXAxis, 5);

            CartesianYAxis = Cross(CartesianXAxis, V);
            CartesianYAxis = Normalize(CartesianYAxis);
            CartesianYAxis = Scale(CartesianYAxis, 4);

            CenterCartesianPoint = Add(E, V);

            label2.Text = "Eye = " + E.ToString();
            label3.Text = "View = " + V.ToString();

            int CarsOnMap = Cars.Count;
            //listBox1.Items.Add(CarsOnMap);
            Random r = new Random();

            int[] directions = new int[4];
            directions[0] = 8;
            directions[1] = 4;
            directions[2] = 6;
            directions[2] = 2;

            int direction = directions[r.Next(0, 3)];

            float randx = r.Next(1, 8);
            float randy = r.Next(1, 8);

            if (CarsOnMap < 5)
            {
                Cars.Add(new Car(randx, randy, .5F, .5F, .5F, .05F, Color.Blue, direction));
                listBox1.Items.Add("Randx: " + randx + " RandY: " + randy + " Direction: " + direction);
            }

            drawCars();
            draw();
            //timer1.Enabled = false; // DEBUG
        }


        //private void picturBox1_LeftArrow(object sender, )

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (timer1.Enabled == true)
                timer1.Enabled = false;
            else
                timer1.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void RightKey()
        {
            //DEBUG listBox1.Items.Add("RIGHT");
            a += da;
            double x = 100 * Math.Cos(a);
            double y = 100 * Math.Sin(a);

            E = new Vector(x, y, 50);
            V = Add(new Vector(0, 0, 0), Scale(E, -1));
            V = Scale(V, ScaleValue);
            Vector N = new Vector(0, 0, 1);

            CartesianXAxis = Cross(V, N);
            CartesianXAxis = Normalize(CartesianXAxis);
            CartesianXAxis = Scale(CartesianXAxis, 5);

            CartesianYAxis = Cross(CartesianXAxis, V);
            CartesianYAxis = Normalize(CartesianYAxis);
            CartesianYAxis = Scale(CartesianYAxis, 4);

            CenterCartesianPoint = Add(E, V);

            label2.Text = "Eye = " + E.ToString();
            label3.Text = "View = " + V.ToString();
            draw();
        }

        private void LeftKey()
        {
            //DEBUG listBox1.Items.Add("LEFT");
            a -= da;
            double x = 100 * Math.Cos(a);
            double y = 100 * Math.Sin(a);

            E = new Vector(x, y, 50);
            V = Add(new Vector(0, 0, 0), Scale(E, -1));
            V = Scale(V, ScaleValue);
            Vector N = new Vector(0, 0, 1);

            CartesianXAxis = Cross(V, N);
            CartesianXAxis = Normalize(CartesianXAxis);
            CartesianXAxis = Scale(CartesianXAxis, 5);

            CartesianYAxis = Cross(CartesianXAxis, V);
            CartesianYAxis = Normalize(CartesianYAxis);
            CartesianYAxis = Scale(CartesianYAxis, 4);

            CenterCartesianPoint = Add(E, V);

            label2.Text = "Eye = " + E.ToString();
            label3.Text = "View = " + V.ToString();
            draw();
        }

        private void draw()
        {
            // Draw background
            Bitmap View = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(View);
            g.FillRectangle(new SolidBrush(Color.Black), 0, 0, View.Width, View.Height);
           
            // Draw buildings
            foreach (Edge edge in Edges)
            {
                PointF V1 = Project(edge.Vertex1);
                PointF V2 = Project(edge.Vertex2);
                g.DrawLine(new Pen(edge.Color), V1.X, V1.Y, V2.X, V2.Y);
            }

            // Draw buildings
            foreach (CarEdge caredge in CarEdges)
            {
                PointF V1 = Project(caredge.Vertex1);
                PointF V2 = Project(caredge.Vertex2);
                g.DrawLine(new Pen(caredge.Color), V1.X, V1.Y, V2.X, V2.Y);
            }

            // Display View
            pictureBox1.Image = View;
            //timer1.Enabled = false; // DEBUG
        }

        private void drawCars()
        {
            CarEdges.Clear();

            foreach (Car car in Cars)
            { 
                //DEBUG listBox1.Items.Add(car.CarX);
                
                

                if (car.CarX < 9.7F && car.CarX > 0.3F && car.CarY < 9.7F && car.CarY > 0.3F)
                {
                    if (car.CDir == 2) { car.CarY -= car.Cspeed; }
                    else if (car.CDir == 4) { car.CarX -= car.Cspeed; }
                    else if (car.CDir == 6) { car.CarX += car.Cspeed; }
                    else { car.CarY += car.Cspeed; }
                    //car.CarX += car.Cspeed;
                    //// Car top
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY, car.Cheight), new Vector(car.CarX + car.Clength, car.CarY, car.Cheight), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY, car.Cheight), new Vector(car.CarX, car.CarY + car.Cwidth, car.Cheight), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX + car.Clength, car.CarY, car.Cheight), new Vector(car.CarX + car.Clength, car.CarY + car.Cwidth, car.Cheight), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY + car.Cwidth, car.Cheight), new Vector(car.CarX + car.Clength, car.CarY + car.Cwidth, car.Cheight), car.Ccolor));
                    // Car base
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY, 0), new Vector(car.CarX + car.Clength, car.CarY, 0), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY, 0), new Vector(car.CarX, car.CarY + car.Cwidth, 0), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX + car.Clength, car.CarY, 0), new Vector(car.CarX + car.Clength, car.CarY + car.Cwidth, 0), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY + car.Cwidth, 0), new Vector(car.CarX + car.Clength, car.CarY + car.Cwidth, 0), car.Ccolor));
                    //// Car sides
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY, car.Cheight), new Vector(car.CarX, car.CarY, 0), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY + car.Cwidth, 0), new Vector(car.CarX, car.CarY + car.Cwidth, car.Cheight), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX + car.Clength, car.CarY, car.Cheight), new Vector(car.CarX + car.Clength, car.CarY, 0), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX, car.CarY + car.Cwidth, car.Cheight), new Vector(car.CarX, car.CarY + car.Cwidth, 0), car.Ccolor));
                    CarEdges.Add(new CarEdge(new Vector(car.CarX + car.Clength, car.CarY + car.Cwidth, car.Cheight), new Vector(car.CarX + car.Clength, car.CarY + car.Cwidth, 0), car.Ccolor));
                }
                
            }


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //capture up arrow key
            if (keyData == Keys.Left)
            {
                LeftKey();
                return true;
            }

            if (keyData == Keys.Right)
            {
                RightKey();
                return true;
            }

            if (keyData == Keys.Up)
            {
                ScaleValue += .01;
                draw();
            }

            if (keyData == Keys.Down)
            {
                ScaleValue -= .01;
                draw();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

