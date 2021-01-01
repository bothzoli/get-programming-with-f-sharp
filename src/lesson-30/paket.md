```powershell
dotnet new tool-manifest
dotnet tool install paket
dotnet tool restore

dotnet paket init
dotnet paket add FSharp.Data
dotnet paket restore
dotnet paket generate-load-scripts

dotnet paket add XPlot.GoogleCharts
dotnet paket add Google.DataTable.Net.Wrapper
dotnet paket restore
dotnet paket generate-load-scripts

```