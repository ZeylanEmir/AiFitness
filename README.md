# AiFitness

AiFitness — это мобильное приложение для фитнеса, которое предоставляет  множество программ тренировок и использует нейросеть для создания персонализированных тренировок в зависимости от параметров текущей тренировки, введенных пользователем. Пользователи могут следить за своим прогрессом, используя статистику, доступную в приложении.

AiFitness является проектом "дипломом" для Высшего Политехнического колледжа "Astana Polytechnic".


**Дальнейшая "инструкция" предназначена для внутреннего пользования участниками проекта (т.е разработчиками)**

# Как установить проект?

Необходимо клонировать репозиторий, ссылка на официальную инструкцию от Microsoft ниже:
**https://learn.microsoft.com/ru-ru/visualstudio/get-started/tutorial-open-project-from-repo?view=vs-2022**

# Как устроены файлы проекта?
## AiFitness
**Общая папка проекта для обеих платформ с которой будет идти работа.**
## AiFitness.Android
Папка с файлами и иконками, кодом под Android.
## AiFitness.iOS
Папка с файлами и иконками, кодом под iOS.

# Краткая инструкция по проекту

 1. Для начала нужно изучить туториал для понимания основ Xamarin, по ссылке на плейлист ниже, туториал можно освоить за полные 3-4 часа:
https://www.youtube.com/watch?v=2ycBBoqAWdQ&list=PL0lO_mIqDDFVQIun69pf7B50ICSXpu1Cw
2. Важно понимать, кто за что будет отвечать в проекте:
Я (т.е Эмир Зейлан) — Отвечаю за функционал тренировок
Даниил Тусаев — Программы тренировок, дизайн проекта.
Алиби Хасенов — Разработка сайта для приложения, дизайн проекта.
3. **Никому не передавайте ссылку на приватный репозиторий**, не давайте никаких данных, так как здесь есть секретные ключи **API** за которые я заплатил, и не хотелось бы получить штраф или кредит из-за переиспользования запросов.
4. **Все моменты по тестированию проекта согласовывать со мной**, проект использует много **API**, и к примеру у нас в проекте есть **API Переводчик** , и при достижении определенного количества запросов я не смогу переводить ответы от нейросети.
5. Доп.туториал по использованию **Github**:
https://www.youtube.com/watch?v=EcItA9wXKnQ

# Цели проекта

Оглашу краткие цели, здесь буду отмечать какие цели выполнены какие нет:

 - [x] Создание прототипа с нейросетью
 - [x] Интерфейс с готовыми перемещениями
 - [x] Создание Базы Данных с готовыми тренировками
 - [ ] Создание Демо версии и полной версии приложения
 - [ ] Выпуск приложения в Google Play
 - [x] Реализация приложения по архитектуре MVVM (Model-View-ViewModel) (В итоге получилось что то около MVVM, но больше как MVC в рамках MVVM)
 - [ ] Создание документации к защите дипломного проекта (В процессе)
 - [ ] Выпуск проекта в открытый доступ
