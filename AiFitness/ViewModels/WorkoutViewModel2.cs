using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class WorkoutViewModel2 : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public WorkoutViewModel2()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { Title = "Штанга стоя близким хватом сгибания", Gif = ImageSource.FromFile("Tz8X.gif"), Reps = "10 Повторений" },
                new Exercise { Title = "Приседания со штангой", Gif = ImageSource.FromFile("Tz8X.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Подтягивание подбородка обратное движение сжатие ", Gif = ImageSource.FromFile("Tz8X.gif"), Reps = "12 Повторений" },
                new Exercise { Title = "Концентрация скручивания гантель", Gif = ImageSource.FromFile("Tz8X.gif"), Reps = "12 Повторений" },
                new Exercise { Title = "Перетягивание скручивание упражнение с гантелями", Gif = ImageSource.FromFile("Tz8X.gif"), Reps = "12 Повторений" }
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