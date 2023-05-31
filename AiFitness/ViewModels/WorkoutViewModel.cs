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
                new Exercise { WorkoutStart = "Разминка", Title = "Бёрпи", Gif = ImageSource.FromFile("Berpi.gif"), Reps = "10-15 Повторений" },
                new Exercise { WorkoutStart = "Разминка", Title = "Прыжки Звёздочка", Gif = ImageSource.FromFile("Jumpin_Jack.gif"), Reps = "30 Повторений" },
                new Exercise { WorkoutStart = "Начало тренировки", Title = "Отжимания", Gif = ImageSource.FromFile("Otzhimaniya.gif"), Reps = "20-30 Повторений" },
                new Exercise { Title = "Аллигаторские отжимания", Gif = ImageSource.FromFile("Aligator_Otzhimanya.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Подтягивания", Gif = ImageSource.FromFile("Podtyagivanya.gif"), Reps = "10-18 Повторений" },
                new Exercise { Title = "Выпады с выпрыгиванием", Gif = ImageSource.FromFile("Vipadi_S_Viprigivaniyem.gif"), Reps = "10 Повторений на каждую сторону" },
                new Exercise { Title = "Выпрыгивания с приседа", Gif = ImageSource.FromFile("Viprigivanya_Spriseda.gif"), Reps = "12-20 Повторений" },
                new Exercise { Title = "Зашагивание на высоту", Gif = ImageSource.FromFile("Zashagivanie_NaVisatu.gif"), Reps = "10 Повторений на каждую сторону" },
                new Exercise { Title = "Скручивание с поднятыми ногами", Gif = ImageSource.FromFile("Scruchivanya_S_Podnyatimi_Nogami.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Пресс часы", Gif = ImageSource.FromFile("Press_Chasy.gif"), Reps = "5-8 кругов на каждую сторону" },
                new Exercise { Title = "Скручивание с поднятыми ногами", Gif = ImageSource.FromFile("Scruchivanya_S_Podnyatimi_Nogami.gif"), Reps = "15 Повторений" },
                new Exercise { Title = "Т-Планка", Gif = ImageSource.FromFile("T_Plank.gif"), Reps = "60-120 секунд" },
                new Exercise { Title = "Ходьба на руках", Gif = ImageSource.FromFile("Hodba_Na_Rukah.gif"), Reps = "7-10 Повторений" },
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
                Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                return;
            }

            OnPropertyChanged(nameof(CurrentExercise));
        }
    }
}