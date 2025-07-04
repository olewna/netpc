#### Oskar Lewna
# Aplikacja z kontaktami

Fullstackowa aplikacja do zarządzania kontaktami stworzona w technologii:
- ASP.NET Core (.NET 9)
- Angular 18
- SQL Server

# Wymagania

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- Opcjonalnie: [Visual Studio Code](https://code.visualstudio.com/) lub [Visual Studio](https://visualstudio.microsoft.com/)

# Instalowanie zależności

## frontend - Angular

```sh
# należy wejść do katalogu z plikami Angulara (folder 'frontend')
cd frontend

# zainstalować zależności komendą:
npm install
```

## backend - ASP.NET

```sh
# należy wejść do katalogu z plikami .NET (folder 'NetPcApi')
cd NetPcApi

# można zainstalować zależności dotnet, ale powinno się zainstalować automatycznie (jeśli Visual Studio)
dotnet restore

# opcjonalnie można wygenerować bazę danych (najpierw należy włączyć serwer SQL)
dotnet ef database update
```

## baza danych - MSQL

- posiadać SSMS (SQL Server Management Studio)
- dodać w folderze 'NetPcApi' plik 'appsettings.Development.json'
- dodać następujące dane:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(***NAZWA-URZĄDZENIA-W-SSMS***)\\SQLEXPRESS;Initial Catalog=(***NAZWA-BAZY-W-SMSS***);Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
  "JWT": {
    "Issuer": "http://localhost:2137",
    "Audience": "http://localhost:2137",
    "SigningKey": "a-valid-string-secret-that-is-at-least-512-bits-long-which-is-very-long" // PRZYKŁADOWY SECRET KEY POTRZEBNY DO PODPISYWANIA JWT 
  }
  pozostałe ustawienia...
}
```

# Włączenie projektu

## frontend

```sh
npm run start
```

Aplikacja powinna otworzyc się na porcie 4200

## backend

```sh
dotnet watch run
```

API powinno włączyć się na porcie 2137

**SWAGGER powinien być dostępny adres:2137/swagger**

## Bazę danych należy włączyć poprzez przykładowo: SQL Managera

# Specyfikacja techniczna projektu

## backend

### Controller

opisuje endpointy w REST, do których można wysyłać zapytania. Pobiera funkcje z serwisów i repozytorium.

### Service i Repository

implementuje funkcje z interfejsów, które bezpośrednio komunikują się z bazą danych.

### AppDbContext

zawiera konfigurację modeli danych do bazy danych, zachowanie podczas usuwania encji oraz zarządzanie użytkownikiem

### Mappers

dodatkowe funkcje, które mapują z jednego modelu obiektu na drugi model

### Models

Encje, jak będą zapisane w bazie danych z użyciem Entity Framework

### Dtos

Pomocnicze modele, używane m.in. podczas pobierania danych z zapytań POST i PUT, posiadają inne pola niż oryginalne modele

### Program główny

w nim inicjalizowane są kontrolery, użyte repozytoria i wszystkie middleware, a także uruchamia on sam serwer API

## frontend

### main.ts

program główny - uruchamia aplikację

### app.config.ts

inicjalizacja httpClienta wraz z interceptorami, plikiem od ścieżek

### app.routes.ts

plik zarządzający ścieżkami oraz dostępem do nich poprzez Guardy

### moduł shared

Posiada funkcje, komponenty i inne pliki używane w wielu miejscach aplikacji. M.in.:

- serwisy, które zawierają zapytania do backendu
- modale, które mogą być używane w wielu miejscach do dodatkowych powiadomień użytkownika
- modele - wzorce obiektów np: kontaktu
- form.models - modele używane do zapytań typu POST i PUT, zmodyfikowane oryginalne wzorce np: kontaktu
- dyrektywy - własne funkcje walidujące formularze 

### moduł main

Zawiera pliki, które są używane jako osobne strony. W nich jest logika pobierania z serwisów zasobów, modyfikowania i wyświetlania ich. Każdy z folderów zawiera pliki od tego, czym się zajmuje:

- contact - strona ze szczegółami kontaktu, po zalogowaniu użytkownik ma dodatkowo opcje usunięcia kontaktu lub edycji
- contact-form - strona do edycji lub edytowania wybranego kontaktu. Zawiera formularz, współpracuje z serwisami, żeby pobierać dane i wysyłać zapytania o stworzenie nowych encji
- home - strona główna - lista kontaktów
- login - strona z formularze do logowania
- not-found - strona, kiedy użytkownika znajdzie się na podstronie, która nie prowadzi na żadną zadeklarowaną ścieżkę
- register - strona z formularzem do rejestracji konta

### moduł core

Zawiera najważniejsze funkcje i komponenty

- guards - specjalne funkcje, określające kiedy użytkownik może wejść na daną podstronę, a kiedy nie może i co się z nim dzieje, jeśli na taką wejdzie
- interceptory - funkcje dodające odpowiednie headery do zapytań, w tym przypadku nagłówek "Authorization", kiedy użytkownik jest zalogowany, aby mógł korzystać z zabezpieczonych endpointów
- layout - komponenty takie jak pasek nawigacji, footer, które powinny się wyświetlać na każdej podstronie. W tym przypadku jest tylko navbar
