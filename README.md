## NotificationManager
[![NuGet version (NotificationManager)](https://img.shields.io/nuget/v/NotificationManager.svg?style=flat-square)](https://www.nuget.org/packages/NotificationManager/)
[![NuGet downloads (NotificationManager)](https://img.shields.io/nuget/dt/NotificationManager)](https://www.nuget.org/packages/NotificationManager/)
[![GitHub last commit](https://img.shields.io/github/last-commit/Irval1337/NotificationManager)](https://github.com/Irval1337/NotificationManager/commits/main)

### Установка библиотеки
**Package Manager**
``` powershell
PM> Install-Package NotificationManager
```

### Использование в проекте
- Инициализируйте класс `Notify`
- Изменяйте максимальное количество уведомлений путем изменения значения `Notify.MaxCount` (изначально 9)
- Отправляйте уведомления на экран, путем вызова метода `Notify.Alert` (Пример реализации: `Notify.Alert("Success", NotificationForm.enmType.Success, new Font("Century Gothic", 12));`)
<br> Возможный значения параметра `type`: `NotificationForm.enmType.Success`, `NotificationForm.enmType.Error`, `NotificationForm.enmType.Warning`, `NotificationForm.enmType.Info` </br>
- Есть возможность закрыть все текущие уведомления. Используйте метод `Notify.CloseAll` с отсутствием аргументов

Примеры использования библиотеки: https://github.com/Irval1337/NotificationManager/tree/main/Examples
Код основан на статье с сайта https://csharpui.com/

### Превью работы
![Preview](https://image.prntscr.com/image/3VGBkEIGRfuOn7fSoDcagw.png)
