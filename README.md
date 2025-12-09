Ez a program a szakdolgozatomhoz kapcsolódó projekt. Téma: falmaászókat segítő webalkalmazás.

A program C# ASP.NET valamint html,css és javascript nyelveket használja.

A programot Visual Studio 2022- ben írom.

Futtatni a Visual Studio 2022- ben ajánlom ahol a .sln file betöltése után lehet futtatni;

másik lehetőség parancsorból: a projekt mappában a dotner run paranccsal (célszerű elötte kiadni a dotnet clean parancsot az esetleg benne maradt korábbi futtatási fájlok törlése véget)



A progam futtatásához szükség van még egy google firebase stoegere, ezt a firebase-én meg lehet csinálmi. Az ottani utasitásokat követően létre kell hozni egy projektet vagy egy meglévőhöz csatolni, majd egy storage bucketet "rockclimbingapp" néven létre kell hozni. Ezek után egy service account private key-re lesz szükség. Ez egy .json fájl, ezt le kell tölteni és a "SecureKeys" mappába (létre kell hozni), ami a projektben a többi mappával egy helyen(pl.: Controller, Model). Ezt a fájlt "firebase-adminsdk.json" néven kell elmenteni, mert a program csak így fogja tudni megtalálni. Erre azért van szükség, hogy a képfeltöltés is működjön mindenkinél, mivel a progam csak lokálisan futtatható és a .json fájl publikálása nagy biztonági kockázatot jelent, emiatt van szükség arra, hogy a tárhelyet mindenki saját magának hozza létre.
