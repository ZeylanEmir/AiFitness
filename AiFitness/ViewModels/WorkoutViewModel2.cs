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
                new Exercise { WorkoutStart = "Разминка", Title = "Бёрпи", Gif = ImageSource.FromFile("Berpi.gif"), Reps = "10-15 Повторений" },
                new Exercise { WorkoutStart = "Начало тренировки", Title = "Прыжки звёздочка", Gif = ImageSource.FromFile("Jumpin_Jack.gif"), Reps = "30 Повторений" },
                new Exercise { Title = "Отжимания", Gif = ImageSource.FromFile("Otzhimaniya.gif"), Reps = "10-15 Повторений 3 подхода" },
                new Exercise { Title = "Подтягивания", Gif = ImageSource.FromFile("Podtyagivanya.gif"), Reps = "10-18 Повторений 3 подхода" },
                new Exercise { Title = "Отжимания на брусьях", Gif = ImageSource.FromFile("Brusiya.gif"), Reps = "12-20 Повторений 3 подхода" },
                new Exercise { Title = "Планка с выводом руки вверх", Gif = ImageSource.FromFile("Planka_S_vivodom_ruki_Vverh.gif"), Reps = "60-180 секунд" },
                new Exercise { Title = "Выпрыгивания с приседа", Gif = ImageSource.FromFile("Viprigivanya_Spriseda.gif"), Reps = "15-30 Повторений 3 подхода" },
                new Exercise { Title = "Выпады с выпрыгиванием", Gif = ImageSource.FromFile("T_Plank.gif"),  Reps = "10-15 Повторений 3 подхода" },
                new Exercise { Title = "Приседания с выбросом ноги вперёд", Gif = ImageSource.FromFile("Prisedanya_S_Vibrosom_Nogi_Vpered.gif"),  Reps = "10-15 Повторений 3 подхода" },
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