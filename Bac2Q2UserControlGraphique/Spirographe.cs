using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Bac2Q2UserControlGraphique.core;

namespace Bac2Q2UserControlGraphique
{
    public class Spirographe
    {
        private List<Couple> points;
        
        public Spirographe()
        {
            points = new List<Couple>();
        }

        public bool Add(Couple point)
        {
            if (!EstValide(point)) return false; // vérifie que le point est valide

            points.Add(point); // l'ajoute
            
            return true;
        }
        
        public bool Add(int x, int y)
        {
            // utilise l'ajoutre méthode Add
            Couple point = new Couple(x, y);

            return Add(point);
        }
        
        public bool Add(double x, double y)
        {
            // utilise l'ajoutre méthode Add
            Couple point = new Couple(x, y);

            return Add(point);
        }

        public bool EstValide(Couple point)
        {
            if (point.Y < -100 || point.Y > 100) return false; // Y dans les bornes -100 & 100
            if (points.Count > 0 && point.X <= points[points.Count - 1].X) return false; // X croissant

            return true;
        }

        public List<Couple> Liste()
        {
            return points;
        }

        public void RemoveAt(int position)
        {
            points.RemoveAt(position);
        }

        public int Count()
        {
            return points.Count;
        }

        public Couple Get(int position)
        {
            return points[position];
        }

        public List<Couple> InverseY()
        {
            // crée une copie par valeur de la liste points
            List<Couple> pointsInverse = new List<Couple>(points).ToList();

            for (int i = 0; i < points.Count; i++)
                pointsInverse[i] = new Couple(points[i].X, -points[i].Y);

            return pointsInverse;
        }
    }
}