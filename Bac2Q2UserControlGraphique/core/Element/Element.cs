using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Bac2Q2UserControlGraphique.core;
using Bac2Q2UserControlGraphique.Core.Figures;

namespace Bac2Q2UserControlGraphique.Core.Element
{
    public abstract class Element
    {
        protected readonly Dictionary<string, Figure> elements; // contient les figures de l'élément
        protected Couple position; // position courante de l'élément
        protected Couple dimensions; // utlisé lors de la création de chaque figure
        protected double zoom;

        public Element(Couple position)
        {
            elements = new Dictionary<string, Figure>();
            
            this.position = position.Copie();

            this.zoom = 1;
        }

        // change la mise à l'échelle'
        public virtual void Zoom(double zoom)
        {
            this.zoom = zoom;
        }

        // permet de mettre à l'échelle un élément
        protected void Dimensionne(int x, int y)
        {
            dimensions = new Couple(x * zoom, y * zoom);
        }

        // affiche toutes les figures de l'élément
        public virtual void Affiche(Graphics graphics)
        {
            // redessine toutes les parties des éléments
            foreach (Figure figure in ListeElements())
            {
                figure.Afficher(graphics);

                // indique aux enfants de redéfinir leur position selon la rotation du parent
                foreach (string enfant in figure.ListeEnfants())
                {
                    // récupère le nom de la méthode dynamiquement
                    MethodInfo method = GetType().GetMethod(enfant, BindingFlags.Instance | BindingFlags.Public);
                    method?.Invoke(this, null);
                }
            }
        }

        protected void AjouterRectangle(string cle, Color? remplissage = null, Color? contour = null, int largeurContour = 0)
        {
            if (elements.ContainsKey(cle)) return;
            elements.Add(cle, new Figures.Rectangle(position, dimensions, remplissage, contour, largeurContour));
        }

        protected void AjouterDisque(string cle, Color remplissage, Color? contour = null, int largeurContour = 0)
        {
            if (elements.ContainsKey(cle)) return;
            elements.Add(cle, new Disque(position, dimensions.Xi, remplissage, contour, largeurContour));
        }
        
        protected void AjouterCercle(string cle, Color contour, int largeurContour = 1)
        {
            if (elements.ContainsKey(cle)) return;
            elements.Add(cle, new Cercle(position, dimensions.Xi, contour, largeurContour));
        }

        protected void AjouterLigne(string cle, Color contour, int largeurContour)
        {
            if (elements.ContainsKey(cle)) return;

            Couple positionSource = position;
            Couple positionDestination = dimensions;
            elements.Add(cle, new Ligne(positionSource, positionDestination, contour, largeurContour));
        }

        // déplace à une position donnée
        public void Deplace(Couple positionDestination)
        {
            positionDestination.X -= position.X;
            positionDestination.Y -= position.Y;
            
            Deplace(positionDestination.Xi, positionDestination.Yi);
        }

        // déplace en translation
        public void Deplace(int x, int y = 0)
        {
            Figure figure;

            position.X += x;
            position.Y += y;
            
            for (int id = 0; id < elements.Values.Count; id++)
            {
                figure = elements.ElementAt(id).Value;

                figure?.Deplace(figure.Position.Xi + x, figure.Position.Yi + y);
            }
        }

        public List<Figure> ListeElements()
        {
            List<Figure> figures = new List<Figure>();

            foreach (Figure figure in elements.Values)
            {
                figures.Add(figure);
            }

            return figures;
        }

        public void RotationFigure(string cle, double angle)
        {
            if(elements.ContainsKey(cle))
                elements[cle].Rotation.Position(angle);
        }

        protected void AjoutEnfant(string enfant, string parent)
        {
            // si les clés existent
            if (elements.ContainsKey(parent) && elements.ContainsKey(enfant))
                elements[parent].AjoutEnfant(enfant);
        }
        
        // rectifie la position par rapport au parent
        protected void AjustePosition(string enfant, string parent, Couple positionPreCalculee = default)
        {
            if (positionPreCalculee == default)
                elements[enfant].Position = elements[parent].PointAdjacent(Figure.Y);
            else
                elements[enfant].Position = positionPreCalculee;
            
            AjoutEnfant(enfant, parent);
        }

        public Figure GetFigure(string cle)
        {
            // on vérifie l'existence de la clé
            if (!elements.ContainsKey(cle)) return null;

            return elements[cle];
        }

        public Couple Position(string cle)
        {
            if (GetFigure(cle) != null)
                return GetFigure(cle).Position;
            
            return position;
        }

        public Couple GetPosition => position;
        public Couple GetDimension => dimensions;

        public Couple Dimension(string cle)
        {
            if (GetFigure(cle) != null)
                return GetFigure(cle).Dimension;
            
            return dimensions;
        }

        public Dictionary<string, Figure> ListeFigures()
        {
            return elements;
        }
    }
}