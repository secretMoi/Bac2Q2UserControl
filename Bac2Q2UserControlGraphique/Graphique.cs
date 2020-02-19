using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Bac2Q2UserControlGraphique.core;
using Bac2Q2UserControlGraphique.Core.Element;
using Bac2Q2UserControlGraphique.Core.Figures;

namespace Bac2Q2UserControlGraphique
{
    public class Graphique : Element
    {
        private int compteur;
        private readonly Dictionary<string, double> maximum;
        private readonly Couple dimensionsFenetre;
        private readonly Couple ajustementZoom;

        public Graphique(Couple position, Couple dimensionsFenetre) : base(position)
        {
            this.dimensionsFenetre = dimensionsFenetre.Copie();
            compteur = 0;
            ajustementZoom = new Couple();

            maximum = new Dictionary<string, double>
            {
                {"gauche", Couple.MinValue},
                {"haut", Couple.MinValue},
                {"droite", Couple.MaxValue},
                {"bas", Couple.MaxValue}
            };
        }
        
        public void ListePoints(List<Couple> points)
        {
            if (!points.Any()) return;
            
            TrouveMaximum(points); // trouve les points extremes
            Zoom(); // effectue un "zoom" pour que les points collent au bord de la fenêtre
            PlacePoints(points); // place les points
            Abscisse(); // place les axes
            Relier(); // relie les points entre eux
        }

        private void Abscisse()
        {
            Dimensionne(dimensionsFenetre.Xi, 4);

            position = new Couple
            {
                X = 0,
                Y = PositionneY(0, dimensions.Y / 2)
            };

            AjouterRectangle("Abscisse", Color.Red);
        }

        private void PlacePoints(List<Couple> points)
        {
            Dimensionne(6, 6); // définit la taille du point

            foreach (Couple point in points)
            {
                position = Positionne(point);

                AjouterDisque("Point" + compteur, Color.Blue);
                
                compteur++;
            }
        }
        
        public void Encadre(List<Couple> points)
        {
            Dimensionne(16, 16);
            
            foreach (Couple point in points)
            {
                position = Positionne(point, -5);

                AjouterCercle("Cadre" + compteur, Color.Green, 2);
                
                compteur++;
            }
        }

        public void EncadreNettoie()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if(elements.ElementAt(i).Key.Contains("Cadre"))
                    elements.Remove(elements.ElementAt(i).Key);
            }
        }

        private Couple Positionne(Couple point, double decalage = 0)
        {
            // étire + réajuste avec le décalage étiré
            Couple pointAjuste = new Couple(
                PositionneX(point.X, decalage),
                PositionneY(point.Y, decalage)
            );

            return pointAjuste;
        }

        private double PositionneX(double x, double decalage = 0)
        {
            return x * ajustementZoom.X -
                   maximum["gauche"] * ajustementZoom.X +
                   decalage;
        }
        private double PositionneY(double y, double decalage = 0)
        {
            return y * ajustementZoom.Y +
                   (-maximum["bas"]) * ajustementZoom.Y +
                   decalage;
        }

        private void Relier()
        {
            if(ListeElements().Count < 2) return;
            
            List<Figure> liste = ListeElements();
            
            for (int i = 0; i < compteur - 1; i++)
            {
                dimensions = liste[i].Position;
                dimensions += 3;
                position = liste[i + 1].Position;
                position += 3;
                
                AjouterLigne("Ligne" + i, Color.Blue, 3);
            }
        }

        private void TrouveMaximum(List<Couple> points)
        {
            maximum["gauche"] = points[0].X;
            maximum["droite"] = points[points.Count - 1].X;
            
            foreach (Couple point in points)
            {
                if (point.Y < maximum["bas"]) maximum["bas"] = point.Y;
                if (point.Y > maximum["haut"]) maximum["haut"] = point.Y;
            }
        }

        private void Zoom()
        {
            Couple deltaMaximum = new Couple(
                maximum["droite"] - maximum["gauche"],
                maximum["haut"] - maximum["bas"]
            );

            // tailleFenetre / delta => facteur de zoom
            ajustementZoom.Y = dimensionsFenetre.Y / deltaMaximum.Y * 0.98;
            ajustementZoom.X = dimensionsFenetre.X / deltaMaximum.X * 0.98;

            // todo mettre dans la classe
            if (Math.Abs(deltaMaximum.X) < 0.001)
                ajustementZoom.X = 1;
            if (Math.Abs(deltaMaximum.Y) < 0.001)
                ajustementZoom.Y = 1;
        }
    }
}