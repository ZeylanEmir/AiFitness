using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class _7DaysWorkout3 : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public _7DaysWorkout3()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { WorkoutStart = "Разминка", Title = "Отжимание с прыжком", Gif = ImageSource.FromFile("Berpi.gif"), Reps = "10 Повторений" },
                new Exercise { WorkoutStart = "Начало тренировки", Title = "Алигаторские отжимания", Gif = ImageSource.FromFile("Aligator_Otzhimanya.gif"), Reps = "8 повторений на каждую ногу" },
                new Exercise { Title = "Приседания с выбросом ноги вперёд", Gif = ImageSource.FromFile("Aligator_Otzhimanya.gif"), Reps = "16 Повторений" },
                new Exercise { Title = "Отжимания кобра", Gif = ImageSource.FromFile("Otzhimaniya_Cobra.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Пресс Часы", Gif = ImageSource.FromFile("Press_Chasy.gif"), Reps = "3-5 кругов в каждую сторону" },
                new Exercise { WorkoutEnd = "Тренировка окончена" , Title = "Не забывайте отдыхать!", Reps = "Для выхода нажмите на NEXT" }
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