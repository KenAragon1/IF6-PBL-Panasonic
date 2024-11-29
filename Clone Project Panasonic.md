
Cara clone project

tools yang diperlukan:
1. git
2. vscode atau visual studio
3. dotnet cli (jika menggunakan vscode)

langkah - langkah
1. Clone Project ```
```
git clone https://github.com/KenAragon1/PBL-IF6-Panasonic.git
```
2. Untuk vs code, buka project lalu buka terminal. masukkan command berikut
```
dotnet restore
dotnet build
```
3. Ubah DefaultConnection di file appsettings.json sesuai dengan database kalian. Contoh :
```json
"DefaultConnection": "Server=localhost;Database=panasonic;User Id=PanasonicUser;Password=Panasonic123;Trusted_Connection=False;TrustServerCertificate=True;"
```
4. Install entity framework secara global Jalankan migrasi menggunakan command : 
```
dotnet tool install --global dotnet-ef
dotnet ef database update
```
5. Jalankan proyek
```
dotnet watch run
```
