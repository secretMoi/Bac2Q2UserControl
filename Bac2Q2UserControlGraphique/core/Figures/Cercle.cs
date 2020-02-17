using System.Drawing;
using Bac2Q2UserControlGraphique.core;

namespace Bac2Q2UserControlGraphique.Core.Figures
{
    public class Cercle : Figure
    {
        public Cercle(Couple position, int rayon, Color contour, int largeurContour) :
            base(position, new Couple(rayon, rayon), null, contour, largeurContour)
        {
        }

        public override void Genere()
        {
            int rayon = dimension.Xi;

            Graphique.DrawEllipse(Contour,
                position.Xf, position.Yf,
                rayon, rayon); // dessine le cercle dans l'image
        }
    }
}