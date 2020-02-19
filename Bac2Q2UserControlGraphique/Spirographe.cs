using System.Collections.Generic;
using System.Linq;
using Bac2Q2UserControlGraphique.core;

namespace Bac2Q2UserControlGraphique
{
    public class Spirographe
    {
        private SortedList<double, double> listePoints;
        
        public Spirographe()
        {
            listePoints = new SortedList<double, double>();
        }

        public bool Add(Couple point)
        {
            if (!EstValide(point)) return false; // vérifie que le point est valide

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

        public void RemoveAt(int position)
        {
            listePoints.RemoveAt(position);
        }

        public Couple GetAt(int position)
        {
            return new Couple(listePoints.Keys[position], listePoints.Values[position]);
        }

        public bool EstValide(Couple point)
        {
            if (point.Y < -100 || point.Y > 100) return false; // Y dans les bornes -100 & 100
            if (listePoints.ContainsKey(point.X)) return false;

            return true;
        }
        
        public List<Couple> Liste()
        {
            List<Couple> pointsRetour = new List<Couple>();

            for (int i = 0; i < listePoints.Count; i++)
                pointsRetour.Add(GetAt(i));
            
            return pointsRetour;
        }
        
        public List<Couple> InverseY()
        {
            // crée une copie par valeur de la liste points
            List<Couple> pointsInverse = new List<Couple>();

            for (int i = 0; i < listePoints.Count; i++)
                pointsInverse.Add(new Couple(listePoints.Keys[i], -listePoints.Values[i]));
            
            return pointsInverse;
        }

        public int Count => listePoints.Count;

        public Couple this[int position] => new Couple(listePoints.Keys[position], listePoints.Values[position]);
    }
}