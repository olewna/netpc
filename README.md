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
