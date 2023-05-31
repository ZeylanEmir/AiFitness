using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class _7DaysWorkout4 : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public _7DaysWorkout4()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { WorkoutStart = "Разминка", Title = "Прыжки звёздочка", Gif = ImageSource.FromFile("Jumpin_Jack.gif"), Reps = "30 Повторений" },
                new Exercise { WorkoutStart = "Начало тренировки", Title = "Выпады с выпригиванием", Gif = ImageSource.FromFile("Vipadi_S_Viprigivaniyem.gif"), Reps = "20 Повторений" },
                new Exercise { Title = "Приседания", Gif = ImageSource.FromFile("Prisedinya.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Подтягивания", Gif = ImageSource.FromFile("Podtyagivanya.gif"), Reps = "8-6 повторений" },
                new Exercise { Title = "Отжимания", Gif = ImageSource.FromFile("Otzhimaniya.gif"), Reps = "15-30 повторений" },
                new Exercise { Title = "Алигаторские отжимания", Gif = ImageSource.FromFile("Aligator_Otzhimanya.gif"), Reps = "8 повторений на каждую руку" },
                new Exercise { Title = "Ходьба на руках", Gif = ImageSource.FromFile("Hodba_Na_Rukah.gif"), Reps = "8 Повторений" },
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