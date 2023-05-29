using AiFitness.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System;
using AiFitness.Views;

namespace AiFitness.ViewModels
{
    public class _7DaysWorkout2 : INotifyPropertyChanged
    {
        private ObservableCollection<Exercise> exercises;
        private int currentExerciseIndex;

        public _7DaysWorkout2()
        {
            exercises = new ObservableCollection<Exercise>
            {
                new Exercise { Title = "Прыжки Звёздочка", Gif = ImageSource.FromFile("Jumpin_Jack.gif"), Reps = "30 Повторений" },
                new Exercise { Title = "Отжимание с прыжком", Gif = ImageSource.FromFile("Berpi.gif"), Reps = "10 Повторений" },
                new Exercise { Title = "Выпрыгивания с приседа", Gif = ImageSource.FromFile("Viprigivanya_Spriseda.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Скручивание с поднятыми ногами", Gif = ImageSource.FromFile("Scruchivanya_S_Podnyatimi_Nogami.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Планка с выводом руки вверх", Gif = ImageSource.FromFile("Planka_S_vivodom_ruki_Vverh.gif"), Reps = "60 секунд" },
                new Exercise { Title = "Отжимания", Gif = ImageSource.FromFile("Otzhimaniya.gif"), Reps = "20 Повторений" },
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