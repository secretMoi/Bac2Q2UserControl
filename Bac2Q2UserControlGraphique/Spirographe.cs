using System.Collections.Generic;
using System.Linq;
using Bac2Q2UserControlGraphique.core;

namespace Bac2Q2UserControlGraphique
{
    public class Spirographe
    {
        private List<Couple> points;
        private SortedList<double, double> listePoints;
        
        public Spirographe()
        {
            points = new List<Couple>();
            listePoints = new SortedList<double, double>();
        }

        public bool Add(Couple point)
        {
            if (!EstValide(point)) return false; // vérifie que le point est valide

            points.Add(point); // l'ajoute
            listePoints.Add(point.X, point.Y);
            
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

        public List<Couple> Liste2()
        {
            return points;
        }
        public List<Couple> Liste()
        {
            List<Couple> pointsRetour = new List<Couple>();

            for (int i = 0; i < listePoints.Count; i++)
                pointsRetour.Add(new Couple(listePoints.Keys[i], listePoints.Values[i]));
            
            return pointsRetour;
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

        public List<Couple> InverseY2()
        {
            // crée une copie par valeur de la liste points
            List<Couple> pointsInverse = new List<Couple>(points).ToList();

            for (int i = 0; i < points.Count; i++)
                pointsInverse[i] = new Couple(points[i].X, -points[i].Y);

            return pointsInverse;
        }
        public List<Couple> InverseY()
        {
            // crée une copie par valeur de la liste points
            List<Couple> pointsInverse = new List<Couple>();

            for (int i = 0; i < listePoints.Count; i++)
                pointsInverse.Add(new Couple(listePoints.Keys[i], -listePoints.Values[i]));
            
            return pointsInverse;
        }
    }
}