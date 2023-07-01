# Шаблон проекта микросервиса построенного на базе Asp.Net Core WebApi

## Установка шаблона

#### 1 В консоли перейти в корень проекта **Microservice Run** (там где расположен `Microservice.sln`).

![](./Assets/Images/1.png)

### 2 Выполнить команду `dotnet new --install .`

![](./Assets/Images/2.png)

### 3 Выполнить команду `dotnet new --list .` и убедиться, что шаблон присутствует в списке доступных.

![](./Assets/Images/3.png)

### 3.1 Установленный шаблон также будет доступен в списке шаблонов Visual Studio.

![](./Assets/Images/4.png)

## Создание проекта на примере Visual Studio

#### 1 Запустить Visual Studio, выбрать **Create a new project**.

![](./Assets/Images/5.png)

#### 2 В списке шаблонов выбрать **ASP.NET Core Web Api (Microservice)**.

![](./Assets/Images/4.png)

#### 3 Указать имя проекта, например: **MyMicroservice1** и установить опцию: **Place solution and project in the same directory**.

![](./Assets/Images/6.png)

#### 4 В результате будет создан проект микросервиса. В качестве **Startup Project** необходимо выбрать проект: **MyMicroservice1.WebApi**.

![](./Assets/Images/7.png)

#### 5 Установить миграции, для этого переходим: **View - Other Windows - Package Manager Console**. В качестве **Default project** выбираем **MyMicroservice1.Infrastructure** и выполняем команду: **Update-database**. По умолчанию будет использоваться сервер БД на **localhost:5432**, а в качестве имени БД использоваться имя проекта, которое мы указывали на **шаге 3**, в нашем случае: **MyMicroservice1**.

![](./Assets/Images/8.png)

#### 6 Запустить проект (F5), в результате отобразиться окно **Swagger** с реализованными операциями **PUT**, **POST**, **DELETE**, **GET**.

![](./Assets/Images/9.png)

## Известные проблемы

### Проблема 1 **Solution Folder**.

#### В текущей реализации шаблон проекта **не умеет** создавать **Solution Folder**, поэтому их придется создать руками: **правый клик на имени решения - Add - New Solution Folder**. Необходимо создать следующие папки: **Domain**, **Application**, **Infrastructure**, **Tests** и разместить в них соответствующие проекты. В результате создания папок и структурирования проектов, **Solution Explorer** должен выглядеть так:

![](./Assets/Images/10.png)

### Проблема 2 **.editorconfig**.

#### В текущей реализации шаблон проекта **не добавляет** файл **.editorconfig**, поэтому его необходимо добавить руками: **правый клик на имени решения - Add - Existing Item...** в появившемся окне выбираем файл **.editorconfig** (файл должен быть расположен рядом с файлом **.sln**).

![](./Assets/Images/11.png)

После добавления в **Solution Explorer** должна будет создаться папка **Solution Items**, в которой данный файл будет размещен.

![](./Assets/Images/12.png)
