using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace AiFitness.ViewModels
{
    public class ExerciseViewModel : INotifyPropertyChanged
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private ImageSource gif;
        public ImageSource Gif
        {
            get { return gif; }
            set
            {
                if (gif != value)
                {
                    gif = value;
                    OnPropertyChanged(nameof(Gif));
                }
            }
        }

        private DateTime time;
        public DateTime Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        private string reps;
        public string Reps
        {
            get { return reps; }
            set
            {
                if (reps != value)
                {
                    reps = value;
                    OnPropertyChanged(nameof(Reps));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
