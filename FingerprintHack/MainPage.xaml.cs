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
                imgBox.ImageClicked = () => { ValidLabel.IsVisible = false; };
                imageBoxes[i] = imgBox;
            }
            setFingerprint(0);
        }

        public void setFingerprint(int f)
        {
            currentFingerprint = f;
            sourceFingerprint.Source = $"f{f}.PNG";
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
            List<int> required = new List<int>() { 0, 1, 2, 3 };
            for (int i = 0; i < 8; i++)
            {
                if (imageBoxes[i].isChecked())
                {
                    if (imageBoxes[i].getId() > 3)
                    {
                        break;
                    }
                    
                    int id = imageBoxes[i].getId();
                    required.Remove(id);
                }
            }
            if (required.Any())
            {
                ValidLabel.Text = "Wrong";
            }
            else
            {
                ValidLabel.Text = "Correct";
                NextButton.IsVisible = true;
            }
            ValidLabel.IsVisible = true;
        }
        private void Next_Clicked(object sender, EventArgs e)
        {
            ValidLabel.IsVisible = false;
            NextButton.IsVisible = false;
            
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
