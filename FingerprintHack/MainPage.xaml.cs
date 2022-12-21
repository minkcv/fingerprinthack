using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FingerprintHack
{
    public partial class MainPage : ContentPage
    {
        private int currentFingerprint;
        private ImageBox[] imageBoxes = new ImageBox[8];
        private Random rand = new Random();
        public MainPage()
        {
            InitializeComponent();
            for (int i = 0; i < 8; i++)
            {
                var ibo = FindByName($"ib{i}");
                ImageBox imgBox = ibo as ImageBox;
                imageBoxes[i] = imgBox;
            }
            setFingerprint(0);
        }

        public void setFingerprint(int f)
        {
            currentFingerprint = f;
            var obj = FindByName("sourceFingerprint");
            Image img = obj as Image;
            img.Source = $"f{f}.PNG";
            List<int> indicesRemaining = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
            List<int> indices = new List<int>();
            while (indicesRemaining.Any())
            {
                int index = rand.Next(0, indicesRemaining.Count);
                indices.Add(indicesRemaining[index]);
                indicesRemaining.RemoveAt(index);
            }
            for (int i = 0; i < 8; i++)
            {
                int index = indices[i];
                var ibo = FindByName($"ib{i}");
                ImageBox imgBox = ibo as ImageBox;
                imgBox.setSource($"p{f}_{index}", index);
            }
        }

        private void Validate_Clicked(object sender, EventArgs e)
        {
            bool valid = true;
            List<int> required = new List<int>() { 0, 1, 2, 3 };
            for (int i = 0; i < 8; i++)
            {
                if (imageBoxes[i].isChecked())
                {
                    if (imageBoxes[i].getId() > 3)
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        int id = imageBoxes[i].getId();
                        required.Remove(id);
                    }
                }
            }
            if (required.Any())
            {
                valid = false;
            }
            var slo = FindByName("correct");
            StackLayout sl = slo as StackLayout;
            sl.IsVisible = valid;

        }
        private void Next_Clicked(object sender, EventArgs e)
        {
            var slo = FindByName("correct");
            StackLayout sl = slo as StackLayout;
            sl.IsVisible = false;
            for (int i = 0; i < 8; i++)
            {
                imageBoxes[i].uncheck();
            }
            int f = currentFingerprint + 1;
            if (f == 4)
            {
                f = 0;
            }
            setFingerprint(f);
        }
    }
}
