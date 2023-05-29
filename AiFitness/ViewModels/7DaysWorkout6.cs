using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class _7DaysWorkout6 : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public _7DaysWorkout6()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { Title = "Прыжки звёздочка", Gif = ImageSource.FromFile("Jumpin_Jack.gif"), Reps = "30 Повторений" },
                new Exercise { Title = "Выпады", Gif = ImageSource.FromFile("Vipadi.gif"), Reps = "На каждую ногу 10 повторений" },
                new Exercise { Title = "Зашагивание на высоту", Gif = ImageSource.FromFile("Zashagivanie_NaVisatu.gif"), Reps = "На каждую ногу 10 повторений" },
                new Exercise { Title = "Отжимание кобры", Gif = ImageSource.FromFile("Otzhimaniya_Cobra.gif"), Reps = "15 повторений" },
                new Exercise { Title = "Отжимания", Gif = ImageSource.FromFile("Otzhimaniya.gif"), Reps = "15-30 повторений" },
                new Exercise { Title = "Скручивание коленей к груди", Gif = ImageSource.FromFile("Scruchivaniya_Koleney_K_Grudi.gif"), Reps = "12 повторений" },
                new Exercise { Title = "Т-планка", Gif = ImageSource.FromFile("T_Plank.gif"), Reps = "60 секунд" },
                /*new Exercise { Title = "Тренировка завершена", Reps = "Нажмите Next для выхода" },*/
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