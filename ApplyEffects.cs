using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGallery
{
    class ApplyEffects
    {
        public void ApplyEffect(MainWindow win)
        {
            System.Windows.Media.Effects.BlurEffect objBlur =
               new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 100;
            win.Effect = objBlur;
        }
        public void ClearEffect(MainWindow win)
        {
            win.Effect = null;
        }
    }
}
