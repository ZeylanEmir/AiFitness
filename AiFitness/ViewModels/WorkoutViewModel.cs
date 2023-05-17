using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public WorkoutViewModel()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { Title = "Exercise 1", Gif = ImageSource.FromFile("exercise1.gif"), Reps = "10 reps" },
                new Exercise { Title = "Exercise 2", Gif = ImageSource.FromFile("exercise2.gif"), Reps = "15 reps" },
                new Exercise { Title = "Exercise 3", Gif = ImageSource.FromFile("exercise3.gif"), Reps = "12 reps" }
                // Добавь сюда другие упражнения
            };

            currentExerciseIndex = 0;

            NextExerciseCommand = new Command(NextExercise);
        }

        public Exercise CurrentExercise => exercises[currentExerciseIndex];

        public Command NextExerciseCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NextExercise()
        {
            currentExerciseIndex++;
            if (currentExerciseIndex >= exercises.Count)
            {
                // Переход на главную страницу (MainPage)
                // Например, можно использовать Navigation.PushAsync(new MainPage());
                Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                return;
            }

            OnPropertyChanged(nameof(CurrentExercise));
        }
    }
}
