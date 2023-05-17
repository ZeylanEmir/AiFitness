using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AiFitness.Models
{
    public class Exercise
    {
        public string Title { get; set; }
        public ImageSource Gif { get; set; }
        public DateTime Time { get; set; }
        public string Reps { get; set; }
    }
}
