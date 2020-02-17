using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Bac2Q2UserControlGraphique;
using Bac2Q2UserControlGraphique.core;
using Bac2Q2UserControlGraphique.Core.Figures;

namespace Bac2Q2UserControlGraphique
{
    public partial class UserControl1: PictureBox
    {
        private Graphique graphique;
        private Spirographe spirographe;
        public UserControl1()
        {
            InitializeComponent();

            Figure.InitialiseConteneur(this);
            spirographe = new Spirographe();
        }

        public void Rafraichir()
        {
            graphique = new Graphique(
                new Couple(Width / 2, Height / 2),
                new Couple(Width, Height)
            );

            graphique.ListePoints(spirographe.InverseY());
            
            Invalidate();
        }

        [Category("Apparence")]
        public bool AjoutPoint(Couple point)
        {
            bool resultat = spirographe.Add(point);

            Rafraichir();

            return resultat;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            graphique.Affiche(e.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            /*spirographe.Add(0, 0);
            spirographe.Add(50, -50);
            spirographe.Add(60, -50);
            spirographe.Add(75, -75);
            spirographe.Add(100, 100);
            spirographe.Add(110, 100);*/
            
            Rafraichir();
        }

        public Spirographe Points => spirographe;
    }
}
