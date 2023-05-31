using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class _7DaysWorkout7 : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public _7DaysWorkout7()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { WorkoutStart = "Разминка", Title = "Прыжки звёздочка", Gif = ImageSource.FromFile("Jumpin_Jack.gif"), Reps = "10 Повторений" },
                new Exercise { WorkoutStart = "Начало тренировки", Title = "Алигаторские отжимания", Gif = ImageSource.FromFile("Aligator_Otzhimanya.gif"), Reps = "8 Повторений на каждую руку" },
                new Exercise { Title = "Приседания с выбросом ноги вперёд", Gif = ImageSource.FromFile("Prisedanya_S_Vibrosom_Nogi_Vpered.gif"), Reps = "16 повторений" },
                new Exercise { Title = "Отжимания кобра", Gif = ImageSource.FromFile("Otzhimaniya_Cobra.gif"), Reps = "15 повторений" },
                new Exercise { Title = "Пресс часы", Gif = ImageSource.FromFile("Press_Chasy.gif"), Reps = "3-5 кругов в каждую сторону" },
                new Exercise { Title = "Ходьба на руках", Gif = ImageSource.FromFile("Hodba_Na_Rukah.gif"), Reps = "8 Повторений" },
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