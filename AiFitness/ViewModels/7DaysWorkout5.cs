using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class _7DaysWorkout5 : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public _7DaysWorkout5()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { Title = "Прыжки звёздочка", Gif = ImageSource.FromFile("Jumpin_Jack.gif"), Reps = "10-15 Повторений" },
                new Exercise { Title = "Выпрыгивания с приседа", Gif = ImageSource.FromFile("Viprigivanya_Spriseda.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Скручивания с поднятыми ногами", Gif = ImageSource.FromFile("Scruchivanya_S_Podnyatimi_Nogami.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Планка с выводом руки вверх", Gif = ImageSource.FromFile("Planka_S_vivodom_ruki_Vverh.gif"), Reps = "60 секунд" },
                new Exercise { Title = "Т-Планка", Gif = ImageSource.FromFile("T_Plank.gif"), Reps = "60 секунд" },
                new Exercise { Title = "Выпады", Gif = ImageSource.FromFile("Vipadi.gif"), Reps = "На каждую ногу 10 повторений" },
                new Exercise { Title = "Отжимания кобра", Gif = ImageSource.FromFile("Otzhimaniya_Cobra.gif"), Reps = "15 повторений" },
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