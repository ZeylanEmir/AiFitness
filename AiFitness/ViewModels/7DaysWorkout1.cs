using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class _7DaysWorkout1 : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public _7DaysWorkout1()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { Title = "Прыжки Звёздочка", Gif = ImageSource.FromFile("Jumpin_Jack.gif"), Reps = "30 Повторений" },
                new Exercise { Title = "Выпады на каждую ногу", Gif = ImageSource.FromFile("Vipadi.gif"), Reps = "10 Повторений" },
                new Exercise { Title = "Зашагивание на высоту", Gif = ImageSource.FromFile("Zashagivanie_NaVisatu.gif"), Reps = "10 Повторений на каждую ногу" },
                new Exercise { Title = "Отжимания кобры", Gif = ImageSource.FromFile("Otzhimaniya_Cobra.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Отжимания", Gif = ImageSource.FromFile("Otzhimaniya.gif"), Reps = "15-30 Повторений" },
                new Exercise { Title = "Скручивание коленей к груди", Gif = ImageSource.FromFile("Scruchivaniya_Koleney_K_Grudi.gif"), Reps = "12 Повторений" },
                new Exercise { Title = "Т-Планка", Gif = ImageSource.FromFile("T_Plank.gif"), Reps = "60 секунд" },
                new Exercise { Title = "Зашагивание на высоту", Gif = ImageSource.FromFile("Zashagivanie_NaVisatu.gif"), Reps = "10 Повторений на каждую ногу" },
                new Exercise { Title = "Отжимания", Gif = ImageSource.FromFile("Otzhimaniya.gif"), Reps = "15-30 Повторений" },
                new Exercise { Title = "Скручивание коленей к груди", Gif = ImageSource.FromFile("Scruchivaniya_Koleney_K_Grudi.gif"), Reps = "12 Повторений" },
                new Exercise { Title = "Т-Планка", Gif = ImageSource.FromFile("T_Plank.gif"), Reps = "60 секунд" }
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