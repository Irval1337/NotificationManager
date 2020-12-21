## NotificationManager
[![NuGet version (NotificationManager)](https://img.shields.io/nuget/v/NotificationManager.svg?style=flat-square)](https://www.nuget.org/packages/NotificationManager/)
[![NuGet downloads (NotificationManager)](https://img.shields.io/nuget/dt/NotificationManager)](https://www.nuget.org/packages/NotificationManager/)
[![GitHub last commit](https://img.shields.io/github/last-commit/Irval1337/NotificationManager)](https://github.com/Irval1337/NotificationManager/commits/main)

### Установка библиотеки
**Package Manager**
``` powershell
PM> Install-Package NotificationManager
```
**.NET CLI**
``` bash
> dotnet add package NotificationManager
```

### Использование в проекте
- Инициализируйте класс `Manager`
- Контролируйте максимальное количество уведомлений путем изменения значения `Manager.MaxCount` (изначально 9)
- Изменяйте шрифт уведомлений, сохраненный в `Manager.Font` (изначатьно `Century Gothic, 12px`)
- Изменийте цвета типов уведомлений, путем изменения переменной `Colors` класса `Colors` (доступны значения `Success`, `Error`, `Warning`, `Info`)
- Отправляйте уведомления на экран, путем вызова метода `Manager.Alert` (Пример реализации: `Notify.Alert("Success", NotificationType.Success);`)
<br> Возможный значения параметра `type`: `NotificationType.Success`, `NotificationType.Error`, `NotificationType.Warning`, `NotificationType.Info`</br>
- Есть возможность закрыть все текущие уведомления. Используйте метод `NotificationType.CloseAll` с отсутствием аргументов

Примеры использования библиотеки: https://github.com/Irval1337/NotificationManager/tree/main/Examples
<br>Код уведомления основан на статье с сайта https://csharpui.com/</br>

### Превью работы
![Preview](https://image.prntscr.com/image/kMrRu3WbS6aB3Uc5Qyr7Vg.png)
