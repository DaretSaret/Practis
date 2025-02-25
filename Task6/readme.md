Задачи:

Задание 1: Запуск нескольких потоков (Thread)
Создайте три потока, которые выводят свое имя и номер итерации в цикле 5 раз с паузой 500 мс. Используйте Join для ожидания завершения потоков и замерьте общее время выполнения.

Задание 2: Гонки потоков (Thread Safety)
Создайте общий счётчик, который увеличивается в двух потоках по 1000 раз. Выведите результат после завершения. Запустите несколько раз для проверки гонок потоков и исправьте с помощью lock или Interlocked.

Задание 3: Использование Task
Создайте три асинхронные задачи с помощью Task.Run, каждая из которых выполняется от 1 до 3 секунд. Используйте Task.WhenAll для ожидания завершения и замерьте общее время. Обработайте исключение в одной из задач.

Задание 4: Параллельное вычисление факториала (Parallel.For)
Используйте Parallel.For для вычисления факториалов чисел от 1 до 10. Выводите результаты в консоль и сравните скорость с обычным циклом for. Сохраните результаты в ConcurrentDictionary<int, long>.

Задание 5: Асинхронная загрузка данных (async/await)
Загрузите 3 веб-страницы с помощью HttpClient и await. Каждая загрузка должна выполняться параллельно. Выведите длину содержимого и обработайте возможные ошибки.

Используемые библиотеки:

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.Http;

Возникшие трудности:

Для 3 задания поиск конструкции Task.Run(async () => {}), использование исключений, замер времени использования методов;

В 4 задании банальная формула вычисления факториала;

Для 5 задания гуглил класс HttpClient с примером его использования и async/await;

Использованные ссылки:

https://metanit.com/sharp/tutorial/13.3.php
https://metanit.com/sharp/tutorial/11.1.php
https://metanit.com/sharp/tutorial/12.1.php
https://www.geeksforgeeks.org/how-to-create-threads-in-c-sharp/
https://stackoverflow.com/questions/6690386/using-interlocked-compareexchange-with-a-class
https://habr.com/ru/articles/793634/
https://learn.microsoft.com/ru-ru/dotnet/api/system.threading.tasks.task.delay?view=net-8.0
https://ru.stackoverflow.com/questions/438919/Применение-task-whenall
https://metanit.com/sharp/tutorial/2.29.php
https://learn.microsoft.com/ru-ru/dotnet/csharp/fundamentals/exceptions/
https://learn.microsoft.com/ru-ru/dotnet/api/system.threading.tasks.task.run?view=net-8.0
https://ru.stackoverflow.com/questions/1117617/Как-вычислить-факториал-на-c
https://learn.microsoft.com/ru-ru/dotnet/csharp/asynchronous-programming/
https://learn.microsoft.com/ru-ru/dotnet/api/system.net.http.httpclient?view=net-9.0

Автор работы: Чайко Денис. 
