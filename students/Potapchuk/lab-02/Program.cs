using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Регистрируем зависимости
services.AddSingleton<IBookRepository, InMemoryBookRepository>();
services.AddTransient<IFinishBookUseCase, BookService>();

var provider = services.BuildServiceProvider();

// Получаем сервис
var service = provider.GetService<IFinishBookUseCase>();

// Тестовый вызов
service.Execute(Guid.NewGuid(), 5);