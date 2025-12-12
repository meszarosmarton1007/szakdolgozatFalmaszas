# Rock Climbing Web Application – ASP.NET Core

Ez a projekt a szakdolgozatom részeként készült egy falmászókat segítő webalkalmazás formájában.  
A rendszer célja, hogy a felhasználók falmászó helyeket, falakat, útvonalakat, hozzászólásokat és képeket tudjanak kezelni.


## Technológiák

- **C# – ASP.NET Core MVC**
- **Entity Framework Core – SQLite adatbázis**
- **HTML, CSS, JavaScript**
- **Firebase Storage – képfeltöltéshez**
- **Visual Studio 2022 ajánlott fejlesztőkörnyezet**


## A projekt futtatása

### **1. Visual Studio 2022-ből**
1. Nyisd meg a solution-t (`.sln` file)
2. Válaszd ki a fő projektet indítási projektként
3. Kattints a *Run* (▶) gombra

### **2. Futtatás parancssorból**
A projekt gyökérmappájában:

```sh
dotnet clean
dotnet run
```



A projekt képfeltöltési funkciója Google Firebase Storage használatára épül.
Mivel a privát kulcsokat biztonsági okokból nem lehet a repository-ban tárolni, minden felhasználónak saját Firebase-projektet kell beállítania.

## 1. Firebase projekt létrehozása

1. Lépj a Firebase konzolra:
 https://console.firebase.google.com

2. Hozz létre egy új projektet, vagy válassz ki egy meglévőt.

3. A bal oldali menüben válaszd a Storage menüpontot.

4. Hozz létre egy Storage Bucketet — ajánlott elnevezés:
rockclimbingapp

## 2. Service Account kulcs létrehozása

1. Navigálj ide: Project Settings → Service Accounts

2. Válaszd a Generate new private key opciót.

3. Töltsd le a generált .json fájlt.

## 3. A kulcs elhelyezése a projektben

1. A projekt gyökérkönyvtárában hozz létre egy mappát:
SecureKeys

2. Másold ide a letöltött JSON fájlt.

3. Fontos: a fájlt pontosan ezen a néven kell elmenteni, hogy az alkalmazás megtalálja:
firebase-adminsdk.json

### A könyvtárstruktúra így fog kinézni:

/Controllers

/Models

/Views

/SecureKeys (ha nem létezik létre kell hozni)

	└── firebase-adminsdk.json


Miért van erre szükség?

A Firebase privát kulcsok bizalmas adatot tartalmaznak → nem kerülhetnek fel GitHub-ra.

Az alkalmazás csak így tud hitelesítetten kapcsolódni a Storage-hoz.

A projekt lokálisan futtatható, ezért mindenkinek saját Firebase Storage példányt kell kezelnie.

Ha ez a lépés kimarad, a képfeltöltés nem fog működni.
